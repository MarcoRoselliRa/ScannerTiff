Imports System.Diagnostics
Imports System.IO
Imports System.Threading.Tasks
Imports System.Drawing

Public Class PdfExportService
    Public Event LogLine(text As String)
    Public Property MaxPdfHeightMm As Integer = 5000 ' soglia (5 metri)
    Public Property CopyTiffOnFailure As Boolean = True
    Public Property OutRoot As String                ' My.Settings.OutDir
    Public Property ExportFolderName As String       ' nome scelto dall'utente
    Public Property JpegQ As Integer = 80
    Public Property MagickExe As String = "magick"
    Public Property GhostscriptExe As String = "gswin64c"
    Public Event Progress(current As Integer, total As Integer, relPath As String)

    Public Async Function ExportAllAsync(items As IEnumerable(Of ScanItem)) As Task
        If String.IsNullOrWhiteSpace(OutRoot) Then Throw New ArgumentException("OutRoot vuota")
        If String.IsNullOrWhiteSpace(ExportFolderName) Then Throw New ArgumentException("Nome cartella export vuoto")

        Dim baseOut = Path.Combine(OutRoot, ExportFolderName.Trim())
        Directory.CreateDirectory(baseOut)
        Dim list = items.ToList()
        Dim total = list.Count
        Dim i As Integer = 0

        For Each it In list
            i += 1
            RaiseEvent Progress(i, total, it.RelPath)
            Await Task.Run(Sub() ExportOne(it, baseOut))
        Next


    End Function

    Private Sub ExportOne(it As ScanItem, baseOut As String)
        Try
            Dim rel = If(it.RelPath, Path.GetFileName(it.FullPath))
            Dim relDir = Path.GetDirectoryName(rel)
            Dim baseName = Path.GetFileNameWithoutExtension(rel)

            Dim outFolder = If(String.IsNullOrEmpty(relDir), baseOut, Path.Combine(baseOut, relDir))
            Directory.CreateDirectory(outFolder)

            Dim outPdf = Path.Combine(outFolder, baseName & ".pdf")

            ' Se la pagina è troppo lunga, non fare PDF: copia TIFF
            Dim hMm As Double = GetHeightMm(it.FullPath)
            RaiseEvent LogLine($"DEBUG HeightMm={Math.Round(hMm)}  Max={MaxPdfHeightMm}  File={rel}")
            If hMm > 0 AndAlso hMm > MaxPdfHeightMm Then
                RaiseEvent LogLine($"SKIP PDF (troppo lungo: {Math.Round(hMm)}mm) -> copio TIFF: {rel}")
                If CopyTiffOnFailure Then CopyTiffFallback(it.FullPath, rel, outFolder)
                Return
            End If
            RaiseEvent LogLine("EXPORT: " & rel)

            Dim tmpPdf = Path.Combine(Path.GetTempPath(), baseName & "__tmp_" & Guid.NewGuid().ToString("N") & ".pdf")

            ' 1) TIFF -> PDF (Magick) con rotazione della riga (0/90/180/270)
            Dim rotateArg As String = ""
            Dim deg = it.Rotate
            If deg <> 0 Then rotateArg = $" -rotate {deg} "

            Dim args1 = $"""{it.FullPath}""{rotateArg} -define pdf:fit-page=true ""{tmpPdf}"""
            Dim r1 = RunProcess(MagickExe, args1)

            If r1.ExitCode <> 0 OrElse Not File.Exists(tmpPdf) Then
                RaiseEvent LogLine("ERR Magick: " & TrimOutput(r1.AllOutput))
                If CopyTiffOnFailure Then CopyTiffFallback(it.FullPath, rel, outFolder)
                SafeDelete(tmpPdf)
                Return
            End If

            ' 2) compress PDF (Ghostscript) come nel converter :contentReference[oaicite:1]{index=1}
            Dim q = Math.Max(30, Math.Min(95, JpegQ))
            Dim args2 =
                "-sDEVICE=pdfwrite -dNOPAUSE -dBATCH -dNOSAFER " &
                "-dCompatibilityLevel=1.7 " &
                "-dDetectDuplicateImages=true " &
                "-dDownsampleColorImages=false " &
                $"-dColorImageFilter=/DCTEncode -dJPEGQ={q} " &
                "-dFastWebView=true " &
                $"-sOutputFile=""{outPdf}"" ""{tmpPdf}"""

            Dim r2 = RunProcess(GhostscriptExe, args2)

            SafeDelete(tmpPdf)

            If r2.ExitCode <> 0 OrElse Not File.Exists(outPdf) Then
                RaiseEvent LogLine("ERR Ghostscript: " & TrimOutput(r2.AllOutput))

                ' ✅ fallback migliore: salva il PDF “grezzo” creato da Magick
                Try
                    File.Copy(tmpPdf, outPdf, True)
                    RaiseEvent LogLine("FALLBACK -> salvato PDF NON compresso: " & outPdf)
                    SafeDelete(tmpPdf)
                    Return
                Catch ex As Exception
                    RaiseEvent LogLine("ERR fallback PDF grezzo: " & ex.Message)
                End Try

                ' se anche questo fallisce, allora TIFF
                If CopyTiffOnFailure Then CopyTiffFallback(it.FullPath, rel, outFolder)
                Return
            End If

            RaiseEvent LogLine("OK -> " & outPdf)

        Catch ex As Exception
            RaiseEvent LogLine("ERR ExportOne: " & ex.Message)
        End Try
    End Sub

    Private Structure ProcResult
        Public ExitCode As Integer
        Public AllOutput As String
    End Structure

    Private Function RunProcess(exe As String, args As String) As ProcResult
        Dim psi As New ProcessStartInfo With {
            .FileName = exe,
            .Arguments = args,
            .UseShellExecute = False,
            .CreateNoWindow = True,
            .RedirectStandardOutput = True,
            .RedirectStandardError = True
        }

        Using p As New Process()
            p.StartInfo = psi
            p.Start()
            Dim stdout = p.StandardOutput.ReadToEnd()
            Dim stderr = p.StandardError.ReadToEnd()
            p.WaitForExit()

            Return New ProcResult With {
                .ExitCode = p.ExitCode,
                .AllOutput = stdout & Environment.NewLine & stderr
            }
        End Using
    End Function

    Private Function TrimOutput(s As String) As String
        If String.IsNullOrWhiteSpace(s) Then Return ""
        Dim t = s.Trim()
        If t.Length > 1500 Then t = t.Substring(0, 1500) & "..."
        Return t
    End Function

    Private Sub SafeDelete(path As String)
        Try
            If File.Exists(path) Then File.Delete(path)
        Catch
        End Try
    End Sub

    Private Function GetHeightMm(path As String) As Double
        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using img = Image.FromStream(fs, False, False)
                    Dim hPx = img.Height
                    Dim dpiY = img.VerticalResolution
                    If dpiY > 1 Then
                        Return (hPx / dpiY) * 25.4
                    End If
                End Using
            End Using
        Catch
        End Try
        Return 0
    End Function

    Private Sub CopyTiffFallback(fullPath As String, rel As String, outFolder As String)
        Try
            Directory.CreateDirectory(outFolder)

            Dim dest = Path.Combine(outFolder, Path.GetFileName(fullPath))

            ' sovrascrive se esiste
            File.Copy(fullPath, dest, True)

            RaiseEvent LogLine("TIFF fallback salvato -> " & dest)
        Catch ex As Exception
            RaiseEvent LogLine("ERR copia TIFF fallback: " & ex.Message)
        End Try
    End Sub
End Class
