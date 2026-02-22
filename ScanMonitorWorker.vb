Imports System.Collections.Concurrent
Imports System.IO
Imports System.Threading
Imports System.Threading.Tasks

Public Class ScanMonitorWorker

    Public Event ItemAdded(item As ScanItem)
    Public Event LastScanChanged(item As ScanItem)
    Public Event LogLine(text As String)

    Public Property InDir As String
    Public Property WorkDir As String
    Public Property SubDir As String = ""

    Private _watcher As FileSystemWatcher
    Private _cts As CancellationTokenSource
    Private _workerTask As Task

    Private ReadOnly _queue As New ConcurrentQueue(Of String)()
    Private ReadOnly _signal As New SemaphoreSlim(0)
    Private ReadOnly _seen As New ConcurrentDictionary(Of String, DateTime)()

    Public Sub Start()
        If _cts IsNot Nothing Then Return
        If String.IsNullOrWhiteSpace(InDir) Then Throw New ArgumentException("InDir vuota")
        If String.IsNullOrWhiteSpace(WorkDir) Then Throw New ArgumentException("WorkDir vuota")

        Directory.CreateDirectory(InDir)
        Directory.CreateDirectory(WorkDir)

        _cts = New CancellationTokenSource()

        _watcher = New FileSystemWatcher(InDir) With {
            .IncludeSubdirectories = False,
            .EnableRaisingEvents = True,
            .NotifyFilter = NotifyFilters.FileName Or NotifyFilters.Size Or NotifyFilters.LastWrite,
            .Filter = "*.tif*"
        }

        AddHandler _watcher.Created, AddressOf OnFsEvent
        AddHandler _watcher.Changed, AddressOf OnFsEvent
        AddHandler _watcher.Renamed, AddressOf OnRenamed

        RaiseEvent LogLine("START watching: " & InDir)
        EnqueueExisting()

        _workerTask = Task.Run(Function() WorkerLoop(_cts.Token))
    End Sub

    Public Sub [Stop]()
        If _cts Is Nothing Then Return
        RaiseEvent LogLine("STOP requested...")

        Try
            _watcher.EnableRaisingEvents = False
            RemoveHandler _watcher.Created, AddressOf OnFsEvent
            RemoveHandler _watcher.Changed, AddressOf OnFsEvent
            RemoveHandler _watcher.Renamed, AddressOf OnRenamed
            _watcher.Dispose()
        Catch
        End Try
        _watcher = Nothing

        Try : _cts.Cancel() : Catch : End Try
        Try : _signal.Release() : Catch : End Try
        Try
            If _workerTask IsNot Nothing Then _workerTask.Wait(1500)
        Catch
        End Try

        Try : _cts.Dispose() : Catch : End Try
        _cts = Nothing
        _workerTask = Nothing

        RaiseEvent LogLine("STOPPED.")
    End Sub

    Private Sub OnFsEvent(sender As Object, e As FileSystemEventArgs)
        If Not IsTiff(e.FullPath) Then Return
        EnqueueOnce(e.FullPath)
    End Sub

    Private Sub OnRenamed(sender As Object, e As RenamedEventArgs)
        If Not IsTiff(e.FullPath) Then Return
        EnqueueOnce(e.FullPath)
    End Sub

    Private Sub EnqueueOnce(path As String)
        Dim now = DateTime.UtcNow
        Dim prev As DateTime

        If _seen.TryGetValue(path, prev) Then
            If (now - prev).TotalSeconds < 5 Then
                _seen(path) = now
                Return
            End If
            _seen(path) = now
        Else
            _seen(path) = now
        End If

        _queue.Enqueue(path)
        _signal.Release()
    End Sub

    Private Async Function WorkerLoop(ct As CancellationToken) As Task
        While Not ct.IsCancellationRequested
            Try
                Await _signal.WaitAsync(ct)
            Catch
                Exit While
            End Try

            Dim inPath As String = Nothing
            While _queue.TryDequeue(inPath)
                If ct.IsCancellationRequested Then Exit While
                Try
                    Await ProcessOneAsync(inPath, ct)
                Catch ex As Exception
                    RaiseEvent LogLine("Process error: " & ex.Message)
                Finally
                    Try : _seen.TryRemove(inPath, Nothing) : Catch : End Try
                End Try
            End While
        End While
    End Function

    Private Async Function ProcessOneAsync(inPath As String, ct As CancellationToken) As Task
        If String.IsNullOrWhiteSpace(inPath) Then Return
        If Not File.Exists(inPath) Then Return

        If Not Await WaitForStableFileAsync(inPath, ct) Then
            RaiseEvent LogLine("SKIP not stable: " & inPath)
            Return
        End If

        Dim targetFolder = WorkDir
        Dim subd = (If(SubDir, "")).Trim()
        If subd <> "" Then targetFolder = Path.Combine(WorkDir, subd)
        Directory.CreateDirectory(targetFolder)

        Dim nextNum = GetNextGlobalNumber(WorkDir) ' globale su tutto WORK (anche sottocartelle)
        Dim newName = nextNum.ToString("D5") & ".tif"
        Dim destPath = Path.Combine(targetFolder, newName)

        File.Move(inPath, destPath)

        Dim rel As String = If(subd = "", newName, subd & "\" & newName)

        Dim fi As New FileInfo(destPath)
        Dim item As New ScanItem With {
            .RelPath = rel,
            .FullPath = destPath,
            .FileSizeBytes = fi.Length,
            .PageInfo = "",
            .Rotate = 0,
            .CreatedAt = DateTime.Now
        }

        RaiseEvent ItemAdded(item)
        RaiseEvent LastScanChanged(item)
        RaiseEvent LogLine("MOVED -> " & rel)
    End Function

    Private Function IsTiff(p As String) As Boolean
        Dim ext = Path.GetExtension(p).ToLowerInvariant()
        Return ext = ".tif" OrElse ext = ".tiff"
    End Function

    Private Async Function WaitForStableFileAsync(path As String, ct As CancellationToken) As Task(Of Boolean)
        Dim lastLen As Long = -1
        Dim stableCount As Integer = 0

        For i = 1 To 600 ' ~60s
            ct.ThrowIfCancellationRequested()

            Try
                Dim fi As New FileInfo(path)
                If Not fi.Exists Then Return False

                Dim len = fi.Length
                Dim canOpen = CanOpenExclusive(path)

                If len = lastLen AndAlso canOpen Then
                    stableCount += 1
                    If stableCount >= 3 Then Return True
                Else
                    stableCount = 0
                    lastLen = len
                End If
            Catch
                stableCount = 0
            End Try

            Await Task.Delay(500, ct)
        Next

        Return False
    End Function

    Private Function CanOpenExclusive(path As String) As Boolean
        Try
            Using fs = New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.None)
                Return True
            End Using
        Catch
            Return False
        End Try
    End Function

    Private Function GetNextGlobalNumber(workRoot As String) As Integer
        Dim maxNum As Integer = 0
        For Each f In Directory.EnumerateFiles(workRoot, "*.tif*", SearchOption.AllDirectories)
            Dim name = Path.GetFileNameWithoutExtension(f)
            Dim n As Integer
            If Integer.TryParse(name, n) Then
                If n > maxNum Then maxNum = n
            End If
        Next
        Return maxNum + 1
    End Function
    Private Sub EnqueueExisting()
        Try
            ' prende i tif già presenti in IN quando si avvia
            For Each f In Directory.EnumerateFiles(InDir, "*.tif", SearchOption.TopDirectoryOnly)
                EnqueueOnce(f)
            Next
            For Each f In Directory.EnumerateFiles(InDir, "*.tiff", SearchOption.TopDirectoryOnly)
                EnqueueOnce(f)
            Next
        Catch ex As Exception
            RaiseEvent LogLine("EnqueueExisting error: " & ex.Message)
        End Try
    End Sub

End Class

