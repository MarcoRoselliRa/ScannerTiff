Imports System.IO
Imports System.Threading


Public Class Form1
    Private _logBuffer As New List(Of String)()
    Private Const MaxLogLines As Integer = 500
    Private _worker As ScanMonitorWorker
    Private _items As New BindingSource()
    Private _list As New System.ComponentModel.BindingList(Of ScanItem)()
    Private _ui As SynchronizationContext
    Private _isMonitoring As Boolean = False
    Private _subDirColors As New Dictionary(Of String, Color)
    Private _colorPalette As Color() = {
    Color.FromArgb(235, 245, 255), ' azzurro
    Color.FromArgb(235, 255, 235), ' verde chiaro
    Color.FromArgb(255, 245, 230), ' arancio chiaro
    Color.FromArgb(245, 235, 255), ' lilla
    Color.FromArgb(255, 235, 235)  ' rosino
}
    Private _nextColorIndex As Integer = 0
    Private _isExporting As Boolean = False
    Private _loadingSettings As Boolean = False
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        _ui = SynchronizationContext.Current
        _items.DataSource = _list
        txtSubDir.Text = My.Settings.SubDir


        dgvFiles.DataSource = _items

        dgvFiles.Columns.Clear()

        dgvFiles.Columns.Clear()


        Dim c1 As New DataGridViewTextBoxColumn With {
            .Name = "colRelPath",
            .DataPropertyName = "RelPath",
            .HeaderText = "File",
            .Width = 178,
            .ReadOnly = True
        }
        dgvFiles.Columns.Add(c1)

        Dim c2 As New DataGridViewTextBoxColumn With {
            .Name = "colSize",
            .DataPropertyName = "FileSizeBytes",
            .HeaderText = "FileSize",
            .Width = 100,
            .ReadOnly = True
        }
        c2.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight
        dgvFiles.Columns.Add(c2)

        Dim c3 As New DataGridViewTextBoxColumn With {
            .Name = "colPage",
            .DataPropertyName = "PageInfo",
            .HeaderText = "Pagina",
            .Width = 135,
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

        cmbRotateAll.Items.Clear()
        cmbRotateAll.Items.AddRange(New Object() {0, 90, 180, 270})
        _loadingSettings = True
        Try
            Dim v As Integer = My.Settings.DefaultRotate

            ' fallback se per qualche motivo non è valido
            If v <> 0 AndAlso v <> 90 AndAlso v <> 180 AndAlso v <> 270 Then v = 0

            cmbRotateAll.SelectedItem = v
        Finally
            _loadingSettings = False
        End Try
    End Sub
    Private Sub cmbRotateAll_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRotateAll.SelectedIndexChanged
        If _loadingSettings Then Return
        Dim def = GetDefaultRotate()
        My.Settings.DefaultRotate = def
        My.Settings.Save()

        For Each it In _list
            it.Rotate = def
        Next

        ' refresh griglia
        dgvFiles.Refresh()

        ' aggiorna anteprima riga selezionata
        Dim sel = GetSelectedItem()
        If sel IsNot Nothing Then ShowPreview(sel.FullPath, sel.Rotate)
    End Sub

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            If _worker Is Nothing Then
                _worker = New ScanMonitorWorker()
            Else
                ' se per caso era rimasto attivo, lo fermiamo e ripartiamo puliti
                Try : _worker.Stop() : Catch : End Try
            End If
            ' ora esiste, quindi posso assegnare
            _worker.InDir = My.Settings.InDir
            _worker.WorkDir = My.Settings.WorkDir
            _worker.SubDir = txtSubDir.Text.Trim()

            My.Settings.SubDir = txtSubDir.Text.Trim()
            My.Settings.Save()

            ' (ri)collega sempre gli eventi
            RemoveHandler _worker.LogLine, AddressOf Worker_LogLine
            RemoveHandler _worker.ItemAdded, AddressOf Worker_ItemAdded
            RemoveHandler _worker.LastScanChanged, AddressOf Worker_LastScanChanged

            AddHandler _worker.LogLine, AddressOf Worker_LogLine
            AddHandler _worker.ItemAdded, AddressOf Worker_ItemAdded
            AddHandler _worker.LastScanChanged, AddressOf Worker_LastScanChanged

            _worker.InDir = My.Settings.InDir
            _worker.WorkDir = My.Settings.WorkDir
            _worker.SubDir = txtSubDir.Text.Trim()

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

            Dim work = My.Settings.WorkDir
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
                .Rotate = GetDefaultRotate(),
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
                         _logBuffer.Add(text)
                         If _logBuffer.Count > MaxLogLines Then
                             _logBuffer.RemoveRange(0, _logBuffer.Count - MaxLogLines)
                         End If
                     Catch
                     End Try
                 End Sub, Nothing)
    End Sub


    Private Sub Worker_ItemAdded(item As ScanItem)
        If _ui Is Nothing Then Return

        _ui.Post(Sub(state)
                     Try
                         item.Rotate = GetDefaultRotate()
                         item.PageInfo = GetPageInfo(item.FullPath)
                         _list.Add(item)

                         ' ✅ aggiorna anteprima subito
                         ShowPreview(item.FullPath, item.Rotate)

                         ' ✅ seleziona l’ultima riga
                         If dgvFiles.Rows.Count > 0 Then
                             dgvFiles.ClearSelection()
                             dgvFiles.Rows(dgvFiles.Rows.Count - 1).Selected = True
                             dgvFiles.FirstDisplayedScrollingRowIndex = dgvFiles.Rows.Count - 1
                         End If
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
        Dim root = My.Settings.WorkDir
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

    Private Sub dgvFiles_CellPainting(sender As Object, e As DataGridViewCellPaintingEventArgs) Handles dgvFiles.CellPainting
        If e.RowIndex < 0 Then Return
        If e.ColumnIndex < 0 Then Return

        If dgvFiles.Columns(e.ColumnIndex).Name <> "colRotate" Then Return

        Dim isSelected As Boolean = (e.State And DataGridViewElementStates.Selected) = DataGridViewElementStates.Selected
        Dim isAlt As Boolean = (e.RowIndex Mod 2 = 1)

        Dim back As Color = If(isSelected, SystemColors.Highlight,
                               If(isAlt, Color.FromArgb(220, 220, 220), Color.White))
        Dim fore As Color = If(isSelected, SystemColors.HighlightText, dgvFiles.ForeColor)

        ' 1) sfondo
        Using br As New SolidBrush(back)
            e.Graphics.FillRectangle(br, e.CellBounds)
        End Using

        ' 2) bordo cella
        Using p As New Pen(dgvFiles.GridColor)
            e.Graphics.DrawRectangle(p, New Rectangle(e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width - 1, e.CellBounds.Height - 1))
        End Using

        ' 3) area pulsante dropdown (a destra)
        Dim btnW As Integer = 30
        Dim btnRect As New Rectangle(e.CellBounds.Right - btnW - 2, e.CellBounds.Y + 2, btnW, e.CellBounds.Height - 4)

        ' pulsante combo
        ControlPaint.DrawComboButton(e.Graphics, btnRect, ButtonState.Normal)

        ' 4) testo (valore)
        Dim textRect As New Rectangle(e.CellBounds.X + 6, e.CellBounds.Y + 2, e.CellBounds.Width - btnW - 12, e.CellBounds.Height - 4)
        Dim txt As String = If(e.FormattedValue Is Nothing, "", e.FormattedValue.ToString())

        TextRenderer.DrawText(e.Graphics, txt, dgvFiles.Font, textRect, fore,
                              TextFormatFlags.VerticalCenter Or TextFormatFlags.Left Or TextFormatFlags.EndEllipsis)

        e.Handled = True
    End Sub
    Private Sub txtSubDir_TextChanged(sender As Object, e As EventArgs) Handles txtSubDir.TextChanged
        Dim subd = txtSubDir.Text.Trim()

        ' aggiorna subito il worker (vale "da lì in poi")
        If _worker IsNot Nothing Then
            _worker.SubDir = subd
        End If

        ' salva anche nelle impostazioni
        My.Settings.SubDir = subd
        My.Settings.Save()
    End Sub

    Private Sub dgvFiles_RowPrePaint(sender As Object, e As DataGridViewRowPrePaintEventArgs) Handles dgvFiles.RowPrePaint
        If e.RowIndex < 0 Then Return

        Dim row = dgvFiles.Rows(e.RowIndex)
        Dim item = TryCast(row.DataBoundItem, ScanItem)
        If item Is Nothing Then Return

        ' Se è nella root (nessuna sottocartella)
        If Not item.RelPath.Contains("\"c) Then
            Dim isAlt As Boolean = (e.RowIndex Mod 2 = 1)
            row.DefaultCellStyle.BackColor =
                If(isAlt, Color.FromArgb(220, 220, 220), Color.White)
            Return
        End If

        ' Estrai nome sottocartella (prima parte prima dello slash)
        Dim subDirName = item.RelPath.Split("\"c)(0)

        ' Se non ha ancora colore, assegnane uno
        If Not _subDirColors.ContainsKey(subDirName) Then
            Dim col = _colorPalette(_nextColorIndex Mod _colorPalette.Length)
            _subDirColors(subDirName) = col
            _nextColorIndex += 1
        End If

        row.DefaultCellStyle.BackColor = _subDirColors(subDirName)
    End Sub


    Private Sub btnSettings_Click(sender As Object, e As EventArgs) Handles btnSettings.Click
        Dim f As New FrmSettings With {
        .InDir = My.Settings.InDir,
        .WorkDir = My.Settings.WorkDir,
        .OutDir = My.Settings.OutDir,
        .LogText = String.Join(Environment.NewLine, _logBuffer)
    }
        f.ShowDialog(Me)
    End Sub

    Private Async Sub btnExport_Click(sender As Object, e As EventArgs) Handles btnExport.Click
        Dim oldTitle As String = Me.Text
        If String.IsNullOrWhiteSpace(My.Settings.ArchiveDir) Then
            MessageBox.Show("Imposta ARCHIVIO nelle Impostazioni (⚙).")
            Return
        End If
        If Not Directory.Exists(My.Settings.ArchiveDir) Then
            MessageBox.Show("La cartella ARCHIVIO non esiste:" & Environment.NewLine & My.Settings.ArchiveDir)
            Return
        End If

        Try
            If String.IsNullOrWhiteSpace(My.Settings.OutDir) Then
                MessageBox.Show("Imposta OUT nelle Impostazioni (⚙).")
                Return
            End If

            Dim folderName = InputBox("Nome cartella export (dentro OUT):", "Esporta",
                                  "Export_" & DateTime.Now.ToString("yyyyMMdd_HHmm")).Trim()
            If folderName = "" Then Return

            ' UI: mostra che sta lavorando
            SetUiBusyForExport(True)

            Dim exporter As New PdfExportService With {
            .OutRoot = My.Settings.OutDir,
            .ExportFolderName = folderName,
            .JpegQ = My.Settings.JpegQ,
            .MagickExe = My.Settings.MagickExe,
            .GhostscriptExe = My.Settings.GhostscriptExe,
            .MaxPdfHeightMm = 5000,
            .CopyTiffOnFailure = True
        }

            AddHandler exporter.LogLine, AddressOf Worker_LogLine

            AddHandler exporter.Progress,
                    Sub(cur As Integer, tot As Integer, relp As String)
                        If _ui Is Nothing Then Return

                        Dim rowIndex As Integer = cur - 1 ' cur è 1-based

                        _ui.Post(Sub()
                                     Me.Text = $"ScannerTiff - Export {cur}/{tot} - {relp}"
                                     SelectRowInDgv(rowIndex)
                                 End Sub, Nothing)
                    End Sub

            ' snapshot lista
            Dim items = _list.ToList()
            Await exporter.ExportAllAsync(items)

            ' 1) copia originali in Archivio
            ArchiveOriginalTiffs(items, My.Settings.ArchiveDir, folderName)

            ' 2) ferma il monitor (programma in STOP)
            StopMonitoring()

            ' 3) cancella da WORK
            DeleteWorkFiles(items)

            ' 4) svuota lista e anteprima (opzionale ma consigliato)
            _list.Clear()
            If picPreview.Image IsNot Nothing Then
                picPreview.Image.Dispose()
                picPreview.Image = Nothing
            End If

            MessageBox.Show("Export completato in: " & Path.Combine(My.Settings.OutDir, folderName))
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore export")
        Finally
            ' UI: ripristina
            SetUiBusyForExport(False)
            Me.Text = oldTitle
        End Try
    End Sub
    Private Sub SetUiBusyForExport(isBusy As Boolean)
        Me.UseWaitCursor = isBusy

        btnExport.Enabled = Not isBusy
        btnStart.Enabled = Not isBusy AndAlso Not _isMonitoring
        btnStop.Enabled = Not isBusy AndAlso _isMonitoring

        btnDelete.Enabled = Not isBusy
        btnSettings.Enabled = Not isBusy
        txtSubDir.Enabled = Not isBusy

        'dgvFiles.Enabled = Not isBusy  ' opzionale: se vuoi evitare click mentre esporta
    End Sub
    Private Sub SelectRowInDgv(index As Integer)
        If index < 0 OrElse index >= dgvFiles.Rows.Count Then Return

        dgvFiles.ClearSelection()
        dgvFiles.Rows(index).Selected = True

        If dgvFiles.Columns.Count > 0 Then
            dgvFiles.CurrentCell = dgvFiles.Rows(index).Cells(0)
        End If

        dgvFiles.FirstDisplayedScrollingRowIndex = Math.Max(0, index)

    End Sub
    Private Function GetDefaultRotate() As Integer
        Dim v As Integer = 0
        If cmbRotateAll IsNot Nothing AndAlso cmbRotateAll.SelectedItem IsNot Nothing Then
            Integer.TryParse(cmbRotateAll.SelectedItem.ToString(), v)
        End If
        Return v
    End Function
    Private Sub ArchiveOriginalTiffs(items As List(Of ScanItem), archiveRoot As String, folderName As String)
        Dim baseArchive = Path.Combine(archiveRoot, folderName.Trim())
        Directory.CreateDirectory(baseArchive)

        For Each it In items
            ' it.RelPath = sottocartelle + nome file
            Dim rel = If(it.RelPath, Path.GetFileName(it.FullPath))
            Dim dest = Path.Combine(baseArchive, rel)

            Dim destDir = Path.GetDirectoryName(dest)
            If Not String.IsNullOrEmpty(destDir) Then Directory.CreateDirectory(destDir)

            ' Copia (sovrascrive)
            File.Copy(it.FullPath, dest, True)
        Next
    End Sub
    Private Sub StopMonitoring()
        Try
            If _worker IsNot Nothing Then _worker.Stop()
        Catch
        End Try
        _isMonitoring = False
        btnStart.Enabled = True
        btnStop.Enabled = False
    End Sub

    Private Sub DeleteWorkFiles(items As List(Of ScanItem))
        For Each it In items
            Try
                If File.Exists(it.FullPath) Then File.Delete(it.FullPath)
            Catch
            End Try
        Next
    End Sub
End Class
