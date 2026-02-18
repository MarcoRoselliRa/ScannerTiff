Imports System.IO
Imports System.Threading


Public Class Form1
    Private _worker As ScanMonitorWorker
    Private _items As New BindingSource()
    Private _list As New System.ComponentModel.BindingList(Of ScanItem)()
    Private _ui As SynchronizationContext

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _ui = SynchronizationContext.Current
        _items.DataSource = _list
        txtInDir.Text = My.Settings.InDir
        txtWorkDir.Text = My.Settings.WorkDir
        txtSubDir.Text = My.Settings.SubDir
        ' txtOutDir.Text = My.Settings.OutDir   ' lo useremo quando aggiungi OUT
        txtOutDir.Text = My.Settings.OutDir


        dgvFiles.DataSource = _items

        dgvFiles.Columns.Clear()

        dgvFiles.Columns.Add(New DataGridViewTextBoxColumn With {
        .DataPropertyName = "RelPath",
        .HeaderText = "File",
        .Width = 220
    })

        dgvFiles.Columns.Add(New DataGridViewTextBoxColumn With {
        .DataPropertyName = "FileSizeBytes",
        .HeaderText = "Bytes",
        .Width = 90
    })

        dgvFiles.Columns.Add(New DataGridViewTextBoxColumn With {
        .DataPropertyName = "PageInfo",
        .HeaderText = "Pagina",
        .Width = 120
    })

        Dim colRot As New DataGridViewComboBoxColumn With {
        .DataPropertyName = "Rotate",
        .HeaderText = "Ruota",
        .Width = 80
    }
        colRot.Items.AddRange(New Object() {0, 90, 180, 270})
        dgvFiles.Columns.Add(colRot)
    End Sub

    Private Sub btnBrowseIn_Click(sender As Object, e As EventArgs) Handles btnBrowseIn.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtInDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnBrowseWork_Click(sender As Object, e As EventArgs) Handles btnBrowseWork.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtWorkDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub
    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            If _worker Is Nothing Then
                _worker = New ScanMonitorWorker()
                AddHandler _worker.LogLine, AddressOf Worker_LogLine
                AddHandler _worker.ItemAdded, AddressOf Worker_ItemAdded
                AddHandler _worker.LastScanChanged, AddressOf Worker_LastScanChanged

            End If

            _worker.InDir = txtInDir.Text
            _worker.WorkDir = txtWorkDir.Text
            _worker.SubDir = txtSubDir.Text
            My.Settings.InDir = txtInDir.Text
            My.Settings.WorkDir = txtWorkDir.Text
            My.Settings.SubDir = txtSubDir.Text
            ' My.Settings.OutDir = txtOutDir.Text

            My.Settings.Save()

            _worker.Start()

            btnStart.Enabled = False
            btnStop.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            If _worker IsNot Nothing Then _worker.Stop()
        Catch
        End Try

        btnStart.Enabled = True
        btnStop.Enabled = False
    End Sub

    Private Sub Worker_LogLine(text As String)
        If _ui Is Nothing Then Return
        _ui.Post(Sub(state)
                     Try
                         If txtLog IsNot Nothing Then
                             txtLog.AppendText(text & Environment.NewLine)
                         End If
                     Catch
                     End Try
                 End Sub, Nothing)
    End Sub


    Private Sub Worker_ItemAdded(item As ScanItem)
        If _ui Is Nothing Then Return
        _ui.Post(Sub(state)
                     Try
                         _list.Add(item)
                     Catch
                     End Try
                 End Sub, Nothing)
    End Sub



    Private Sub Form1_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try
            If _worker IsNot Nothing Then _worker.Stop()
        Catch
        End Try
    End Sub

    Private Sub Worker_LastScanChanged(item As ScanItem)
        If _ui Is Nothing Then Return
        _ui.Post(Sub(state)
                     Try
                         ShowPreview(item.FullPath)

                         If dgvFiles.Rows.Count > 0 Then
                             dgvFiles.ClearSelection()
                             dgvFiles.Rows(dgvFiles.Rows.Count - 1).Selected = True
                             dgvFiles.FirstDisplayedScrollingRowIndex = dgvFiles.Rows.Count - 1
                         End If
                     Catch
                     End Try
                 End Sub, Nothing)
    End Sub



    Private Sub ShowPreview(path As String)
        Try
            If Not File.Exists(path) Then Return

            ' evita blocchi file: carica in memoria
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using img = Image.FromStream(fs)
                    picPreview.Image?.Dispose()
                    picPreview.Image = New Bitmap(img)
                End Using
            End Using
        Catch ex As Exception
            ' TIFF multipagina a volte non piace a GDI+: lo gestiamo nel passo successivo con Magick.NET
            Debug.WriteLine("Preview error: " & ex.Message)
        End Try
    End Sub

    Private Sub btnBrowseOut_Click(sender As Object, e As EventArgs) Handles btnBrowseOut.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtOutDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub


End Class
