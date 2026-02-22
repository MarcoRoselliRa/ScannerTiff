Imports System.IO
Imports System.Threading


Public Class Form1
    Private _worker As ScanMonitorWorker
    Private _items As New BindingSource()
    Private _list As New System.ComponentModel.BindingList(Of ScanItem)()
    Private _ui As SynchronizationContext
    Private _isMonitoring As Boolean = False

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _ui = SynchronizationContext.Current
        _items.DataSource = _list
        txtInDir.Text = My.Settings.InDir
        txtWorkDir.Text = My.Settings.WorkDir
        txtSubDir.Text = My.Settings.SubDir
        txtOutDir.Text = My.Settings.OutDir


        dgvFiles.DataSource = _items

        dgvFiles.Columns.Clear()

        dgvFiles.Columns.Clear()


        Dim c1 As New DataGridViewTextBoxColumn With {
            .Name = "colRelPath",
            .DataPropertyName = "RelPath",
            .HeaderText = "File",
            .Width = 220,
            .ReadOnly = True
        }
        dgvFiles.Columns.Add(c1)

        Dim c2 As New DataGridViewTextBoxColumn With {
            .Name = "colSize",
            .DataPropertyName = "FileSizeBytes",
            .HeaderText = "FileSize",
            .Width = 115,
            .ReadOnly = True
        }
        c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvFiles.Columns.Add(c2)

        Dim c3 As New DataGridViewTextBoxColumn With {
            .Name = "colPage",
            .DataPropertyName = "PageInfo",
            .HeaderText = "Pagina",
            .Width = 200,
            .ReadOnly = True
        }
        dgvFiles.Columns.Add(c3)

        Dim colRot As New DataGridViewComboBoxColumn With {
            .Name = "colRotate",
            .DataPropertyName = "Rotate",
            .HeaderText = "Ruota",
            .Width = 90,
            .ReadOnly = False
        }
        colRot.Items.AddRange(New Object() {0, 90, 180, 270})
        colRot.DefaultCellStyle.SelectionBackColor = dgvFiles.DefaultCellStyle.SelectionBackColor
        colRot.DefaultCellStyle.SelectionForeColor = dgvFiles.DefaultCellStyle.SelectionForeColor
        dgvFiles.Columns.Add(colRot)

        dgvFiles.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(220, 220, 220)
        dgvFiles.EnableHeadersVisualStyles = False
        dgvFiles.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(230, 230, 230)
        dgvFiles.ColumnHeadersDefaultCellStyle.Font = New Font(dgvFiles.Font, FontStyle.Bold)
        dgvFiles.RowHeadersWidth = 40
        dgvFiles.AllowUserToAddRows = False

        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.MultiSelect = False
        dgvFiles.RowsDefaultCellStyle.BackColor = Color.White
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
            ' salva settings
            My.Settings.InDir = txtInDir.Text
            My.Settings.WorkDir = txtWorkDir.Text
            My.Settings.SubDir = txtSubDir.Text
            My.Settings.OutDir = txtOutDir.Text
            My.Settings.Save()

            If _worker Is Nothing Then
                _worker = New ScanMonitorWorker()
            Else
                ' se per caso era rimasto attivo, lo fermiamo e ripartiamo puliti
                Try : _worker.Stop() : Catch : End Try
            End If

            ' (ri)collega sempre gli eventi
            RemoveHandler _worker.LogLine, AddressOf Worker_LogLine
            RemoveHandler _worker.ItemAdded, AddressOf Worker_ItemAdded
            RemoveHandler _worker.LastScanChanged, AddressOf Worker_LastScanChanged

            AddHandler _worker.LogLine, AddressOf Worker_LogLine
            AddHandler _worker.ItemAdded, AddressOf Worker_ItemAdded
            AddHandler _worker.LastScanChanged, AddressOf Worker_LastScanChanged

            _worker.InDir = txtInDir.Text
            _worker.WorkDir = txtWorkDir.Text
            _worker.SubDir = txtSubDir.Text

            ' log di controllo
            Worker_LogLine("START premuto")
            Worker_LogLine("IN: " & _worker.InDir)
            Worker_LogLine("WORK: " & _worker.WorkDir)
            Worker_LogLine("SUB: " & _worker.SubDir)

            ' mostra subito eventuali file già presenti in WORK (così capisci che la griglia funziona)
            LoadExistingFiles()

            _worker.Start()
            _isMonitoring = True

            btnStart.Enabled = False
            btnStop.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub LoadExistingFiles()
        Try
            _list.Clear()

            Dim work = txtWorkDir.Text
            If String.IsNullOrWhiteSpace(work) Then Return
            If Not Directory.Exists(work) Then Return

            For Each f In Directory.EnumerateFiles(work, "*.tif*", SearchOption.AllDirectories)
                Dim rel = f.Substring(work.Length).TrimStart("\"c)
                Dim fi As New FileInfo(f)

                _list.Add(New ScanItem With {
                .RelPath = rel,
                .FullPath = f,
                .FileSizeBytes = fi.Length,
                .PageInfo = GetPageInfo(f),
                .Rotate = 0,
                .CreatedAt = fi.CreationTime
            })
            Next

            ' anteprima ultimo, se c'è
            If _list.Count > 0 Then
                ShowPreview(_list(_list.Count - 1).FullPath)
            End If
        Catch ex As Exception
            Worker_LogLine("LoadExistingFiles error: " & ex.Message)
        End Try
    End Sub


    Private Sub btnStop_Click(sender As Object, e As EventArgs) Handles btnStop.Click
        Try
            If _worker IsNot Nothing Then _worker.Stop() : _isMonitoring = False
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
                         item.PageInfo = GetPageInfo(item.FullPath)
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
                         ShowPreview(item.FullPath, item.Rotate)

                         If dgvFiles.Rows.Count > 0 Then
                             dgvFiles.ClearSelection()
                             dgvFiles.Rows(dgvFiles.Rows.Count - 1).Selected = True
                             dgvFiles.FirstDisplayedScrollingRowIndex = dgvFiles.Rows.Count - 1
                         End If
                     Catch
                     End Try
                 End Sub, Nothing)
    End Sub



    Private Sub ShowPreview(path As String, Optional rotate As Integer = 0)
        Try
            If Not File.Exists(path) Then Return

            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using img = Image.FromStream(fs)
                    Dim bmp As New Bitmap(img)

                    Select Case rotate
                        Case 90
                            bmp.RotateFlip(RotateFlipType.Rotate90FlipNone)
                        Case 180
                            bmp.RotateFlip(RotateFlipType.Rotate180FlipNone)
                        Case 270
                            bmp.RotateFlip(RotateFlipType.Rotate270FlipNone)
                    End Select

                    picPreview.Image?.Dispose()
                    picPreview.Image = bmp
                End Using
            End Using
        Catch ex As Exception
            Debug.WriteLine("Preview error: " & ex.Message)
        End Try
    End Sub


    Private Sub btnBrowseOut_Click(sender As Object, e As EventArgs) Handles btnBrowseOut.Click
        If FolderBrowserDialog1.ShowDialog() = DialogResult.OK Then
            txtOutDir.Text = FolderBrowserDialog1.SelectedPath
        End If
    End Sub

    Private Sub btnDelete_Click(sender As Object, e As EventArgs) Handles btnDelete.Click
        If dgvFiles.CurrentRow Is Nothing Then Return

        Dim item = TryCast(dgvFiles.CurrentRow.DataBoundItem, ScanItem)
        If item Is Nothing Then Return

        Dim wasMonitoring = _isMonitoring

        If MessageBox.Show("Eliminare questo file?" & Environment.NewLine & item.RelPath,
                       "Conferma",
                       MessageBoxButtons.YesNo,
                       MessageBoxIcon.Question) <> DialogResult.Yes Then Return

        Try
            ' Se stava monitorando lo fermiamo temporaneamente
            If wasMonitoring AndAlso _worker IsNot Nothing Then
                _worker.Stop()
            End If

            ' Libera anteprima
            If picPreview.Image IsNot Nothing Then
                picPreview.Image.Dispose()
                picPreview.Image = Nothing
            End If

            ' Elimina file
            If File.Exists(item.FullPath) Then File.Delete(item.FullPath)

            ' Rinumera
            RenumberAllWorkFiles()

            ' Ricarica lista
            LoadExistingFiles()

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ripristina stato originale
            If wasMonitoring AndAlso _worker IsNot Nothing Then
                _worker.Start()
            End If
        End Try
    End Sub


    Private Sub RenumberAllWorkFiles()
        Dim root = txtWorkDir.Text
        If String.IsNullOrWhiteSpace(root) Then Return
        If Not Directory.Exists(root) Then Return

        ' Prendi tutti i tif (anche in sottocartelle)
        Dim files = Directory.EnumerateFiles(root, "*.tif*", SearchOption.AllDirectories).ToList()

        ' Ordina per numero se è numerico, altrimenti per nome
        files.Sort(Function(a, b)
                       Dim na As Integer, nb As Integer
                       Dim sa = Path.GetFileNameWithoutExtension(a)
                       Dim sb = Path.GetFileNameWithoutExtension(b)

                       Dim ha = Integer.TryParse(sa, na)
                       Dim hb = Integer.TryParse(sb, nb)

                       If ha AndAlso hb Then Return na.CompareTo(nb)
                       Return String.Compare(a, b, StringComparison.OrdinalIgnoreCase)
                   End Function)

        ' 1) Rinominare tutto in temporanei (per evitare collisioni)
        Dim tempList As New List(Of (tempPath As String, finalFolder As String))()

        For Each f In files
            Dim folder = Path.GetDirectoryName(f)
            Dim ext = If(Path.GetExtension(f).ToLowerInvariant() = ".tiff", ".tif", ".tif") ' uniformiamo a .tif
            Dim tmp = Path.Combine(folder, "__tmp__" & Guid.NewGuid().ToString("N") & ext)
            File.Move(f, tmp)
            tempList.Add((tmp, folder))
        Next

        ' 2) Assegna nuovi numeri globali
        Dim n As Integer = 1
        For Each t In tempList
            Dim finalName = n.ToString("D5") & ".tif"
            Dim finalPath = Path.Combine(t.finalFolder, finalName)
            File.Move(t.tempPath, finalPath)
            n += 1
        Next
    End Sub

    Private Sub dgvFiles_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFiles.SelectionChanged
        Dim item = GetSelectedItem()
        If item Is Nothing Then Return
        ShowPreview(item.FullPath, item.Rotate)
    End Sub

    Private Sub dgvFiles_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFiles.CellValueChanged
        If e.RowIndex < 0 Then Return
        If dgvFiles.Columns(e.ColumnIndex).DataPropertyName <> "Rotate" Then Return

        Dim item = TryCast(dgvFiles.Rows(e.RowIndex).DataBoundItem, ScanItem)
        If item Is Nothing Then Return

        ' aggiorna anteprima con nuova rotazione
        ShowPreview(item.FullPath, item.Rotate)
    End Sub

    Private Sub dgvFiles_CurrentCellDirtyStateChanged(sender As Object, e As EventArgs) Handles dgvFiles.CurrentCellDirtyStateChanged
        ' importante: fa scattare subito CellValueChanged quando cambi combobox
        If dgvFiles.IsCurrentCellDirty Then
            dgvFiles.CommitEdit(DataGridViewDataErrorContexts.Commit)
        End If
    End Sub

    Private Function GetSelectedItem() As ScanItem
        If dgvFiles.CurrentRow Is Nothing Then Return Nothing
        Return TryCast(dgvFiles.CurrentRow.DataBoundItem, ScanItem)
    End Function
    Private Function GetPageInfo(path As String) As String
        Try
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite)
                Using img = Image.FromStream(fs, False, False)
                    Dim wPx = img.Width
                    Dim hPx = img.Height

                    Dim dpiX = img.HorizontalResolution
                    Dim dpiY = img.VerticalResolution

                    ' se DPI validi -> mm reali
                    If dpiX > 1 AndAlso dpiY > 1 Then
                        Dim wMm = (wPx / dpiX) * 25.4
                        Dim hMm = (hPx / dpiY) * 25.4
                        Return $"{Math.Round(wMm)}×{Math.Round(hMm)} mm"
                    End If

                    ' fallback
                    Return $"{wPx}×{hPx} px"
                End Using
            End Using
        Catch
            Return ""
        End Try
    End Function

    Private Function FormatBytes(bytes As Long) As String
        If bytes < 1024 Then Return bytes & " B"

        Dim kb As Double = bytes / 1024.0
        If kb < 1024 Then Return kb.ToString("0.0") & " KB"

        Dim mb As Double = kb / 1024.0
        If mb < 1024 Then Return mb.ToString("0.0") & " MB"

        Dim gb As Double = mb / 1024.0
        Return gb.ToString("0.00") & " GB"
    End Function
    Private Sub dgvFiles_CellFormatting(sender As Object, e As DataGridViewCellFormattingEventArgs) Handles dgvFiles.CellFormatting
        If e.RowIndex < 0 Then Return

        ' 1) dimensione file (se già lo usi, lascia pure il tuo blocco e aggiungi solo la parte Rotate)
        If dgvFiles.Columns(e.ColumnIndex).DataPropertyName = "FileSizeBytes" AndAlso e.Value IsNot Nothing Then
            Dim b As Long
            If Long.TryParse(e.Value.ToString(), b) Then
                e.Value = FormatBytes(b)
                e.FormattingApplied = True
            End If
        End If

        ' 2) FORZA alternanza + selezione sulla ComboBox Ruota
        If dgvFiles.Columns(e.ColumnIndex).Name = "colRotate" Then
            Dim isAlt As Boolean = (e.RowIndex Mod 2 = 1)

            e.CellStyle.BackColor = If(isAlt, Color.FromArgb(220, 220, 220), Color.White)
            e.CellStyle.SelectionBackColor = SystemColors.Highlight
            e.CellStyle.SelectionForeColor = SystemColors.HighlightText
        End If
    End Sub

End Class
