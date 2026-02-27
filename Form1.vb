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
    Private _previewEnabled As Boolean = False
    Private _currentJobPath As String = ""
    Private _rotationMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)

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

        chkAnteprima.Checked = My.Settings.PreviewEnabled
        _previewEnabled = chkAnteprima.Checked
        LoadJobsCombo()
    End Sub
    'Percorso medadata
    Private Function GetMetadataPath(jobPath As String) As String
        Return Path.Combine(jobPath, "metadata.json")
    End Function
    'Salva metadata Json
    Private Sub SaveMetadata(jobPath As String)
        Try
            Dim path = GetMetadataPath(jobPath)
            Dim json = System.Text.Json.JsonSerializer.Serialize(_rotationMap,
                    New System.Text.Json.JsonSerializerOptions With {
                        .WriteIndented = True
                    })
            File.WriteAllText(path, json)
        Catch ex As Exception
            Worker_LogLine("Errore salvataggio metadata: " & ex.Message)
        End Try
    End Sub
    'Carica i metadata
    Private Sub LoadMetadata(jobPath As String)
        _rotationMap.Clear()

        Try
            Dim path = GetMetadataPath(jobPath)

            ' ✅ Se il file NON esiste → crealo con rotazione standard
            If Not File.Exists(path) Then
                Dim defaultRotate As Integer = 0

                ' se vuoi prendere la rotazione dal cmbRotateAll
                If cmbRotateAll.SelectedItem IsNot Nothing Then
                    Integer.TryParse(cmbRotateAll.SelectedItem.ToString(), defaultRotate)
                End If

                ' inizializza mappa con tutti i file presenti
                For Each f In Directory.EnumerateFiles(jobPath, "*.tif*", SearchOption.AllDirectories)
                    Dim rel = f.Substring(jobPath.Length).TrimStart("\"c)
                    _rotationMap(rel) = defaultRotate
                Next

                SaveMetadata(jobPath)
                Return
            End If

            ' ✅ Se esiste → caricalo
            Dim json = File.ReadAllText(path)
            _rotationMap =
            System.Text.Json.JsonSerializer.Deserialize(Of Dictionary(Of String, Integer))(json)

            If _rotationMap Is Nothing Then
                _rotationMap = New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
            End If

        Catch ex As Exception
            Worker_LogLine("Errore caricamento metadata: " & ex.Message)
        End Try
    End Sub
    'carica le cartelle da Archivio
    Private Sub LoadJobsCombo()
        cmbLavoro.Items.Clear()

        Dim arch As String = My.Settings.ArchiveDir
        If String.IsNullOrWhiteSpace(arch) Then Return
        If Not Directory.Exists(arch) Then Return

        Dim dirs = Directory.GetDirectories(arch)
        Array.Sort(dirs, StringComparer.OrdinalIgnoreCase)

        For Each d In dirs
            cmbLavoro.Items.Add(Path.GetFileName(d))
        Next
    End Sub
    'evento combobox lavori
    Private Sub cmbLavoro_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbLavoro.SelectedIndexChanged
        If cmbLavoro.SelectedIndex < 0 Then Return

        Dim jobName As String = cmbLavoro.SelectedItem.ToString()
        _currentJobPath = Path.Combine(My.Settings.ArchiveDir, jobName)

        LoadMetadata(_currentJobPath)
        LoadJobFiles(_currentJobPath)
    End Sub
    'creo directori nuova dal combobox
    Private Sub cmbLavoro_KeyDown(sender As Object, e As KeyEventArgs) Handles cmbLavoro.KeyDown
        If e.KeyCode <> Keys.Enter Then Return
        e.SuppressKeyPress = True

        Dim name As String = cmbLavoro.Text.Trim()
        If name = "" Then Return

        ' pulizia base nome cartella
        For Each ch In Path.GetInvalidFileNameChars()
            name = name.Replace(ch, "_"c)
        Next

        Dim arch As String = My.Settings.ArchiveDir
        If String.IsNullOrWhiteSpace(arch) OrElse Not Directory.Exists(arch) Then
            MessageBox.Show("Archivio non impostato o non esiste.")
            Return
        End If

        Dim jobPath = Path.Combine(arch, name)

        Try
            If Not Directory.Exists(jobPath) Then
                Directory.CreateDirectory(jobPath)
                LoadJobsCombo()
            End If

            cmbLavoro.SelectedItem = name
            LoadJobFiles(jobPath)

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore creazione cartella")
        End Try
    End Sub
    'Carico i lavori nella lista
    Private Sub LoadJobFiles(jobPath As String)
        _list.Clear()

        If Not Directory.Exists(jobPath) Then Return

        For Each f In Directory.EnumerateFiles(jobPath, "*.tif*", SearchOption.AllDirectories)
            Dim fi As New FileInfo(f)

            ' Percorso relativo rispetto alla cartella lavoro
            Dim rel = f.Substring(jobPath.Length).TrimStart("\"c)

            Dim rot As Integer = 0
            If _rotationMap.ContainsKey(rel) Then rot = _rotationMap(rel)

            _list.Add(New ScanItem With {
            .RelPath = rel,
            .FullPath = f,
            .FileSizeBytes = fi.Length,
            .PageInfo = GetPageInfo(f),
            .Rotate = rot,
            .CreatedAt = fi.CreationTime
        })
        Next
    End Sub
    'Prossimo numero files
    Private Function GetNextProgressiveFileName(jobPath As String, ext As String) As String
        Dim maxN As Integer = 0

        If Directory.Exists(jobPath) Then
            For Each f In Directory.EnumerateFiles(jobPath, "*.tif*", SearchOption.AllDirectories)
                Dim name = Path.GetFileNameWithoutExtension(f)
                Dim n As Integer
                If Integer.TryParse(name, n) Then
                    If n > maxN Then maxN = n
                End If
            Next
        End If

        Dim nextN = maxN + 1
        Return nextN.ToString("D5") & ext
    End Function
    Private Sub chkAnteprima_CheckedChanged(sender As Object, e As EventArgs) Handles chkAnteprima.CheckedChanged
        _previewEnabled = chkAnteprima.Checked

        My.Settings.PreviewEnabled = _previewEnabled
        My.Settings.Save()

        If Not _previewEnabled Then
            If picPreview.Image IsNot Nothing Then
                picPreview.Image.Dispose()
                picPreview.Image = Nothing
            End If
        Else
            ' se riattivi, mostra subito la preview della riga selezionata (se c'è)
            Dim it = GetSelectedItem()
            If it IsNot Nothing Then ShowPreview(it.FullPath, it.Rotate)
        End If
    End Sub
    'Ruota tutto
    Private Sub cmbRotateAll_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbRotateAll.SelectedIndexChanged
        If String.IsNullOrWhiteSpace(_currentJobPath) Then Return
        If cmbRotateAll.SelectedItem Is Nothing Then Return

        Dim deg As Integer
        If Not Integer.TryParse(cmbRotateAll.SelectedItem.ToString(), deg) Then Return

        ' aggiorna tutte le righe + mappa rotazioni
        For Each si As ScanItem In _list
            si.Rotate = deg
            _rotationMap(si.RelPath) = deg
        Next

        ' salva json
        SaveMetadata(_currentJobPath)

        ' refresh griglia (BindingList di solito aggiorna, ma questo aiuta)
        dgvFiles.Refresh()
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

            ' ricarica il lavoro selezionato (Archivio), così Stop/Start non svuota la lista
            If Not String.IsNullOrWhiteSpace(_currentJobPath) AndAlso Directory.Exists(_currentJobPath) Then
                LoadJobFiles(_currentJobPath)
            End If

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

    'controllo la directori work
    Private Sub Worker_ItemAdded(item As ScanItem)

        If String.IsNullOrWhiteSpace(_currentJobPath) Then
            Worker_LogLine("Nessun lavoro selezionato.")
            Return
        End If

        If Not Directory.Exists(_currentJobPath) Then
            Worker_LogLine("Cartella lavoro non valida.")
            Return
        End If

        Try
            ' attesa breve per sicurezza (file ancora in scrittura)
            Threading.Thread.Sleep(200)

            Dim ext = Path.GetExtension(item.FullPath)
            If String.IsNullOrWhiteSpace(ext) Then ext = ".tif"
            ext = ext.ToLowerInvariant()

            ' --- 1) Manteniamo la sottocartella relativa ---
            Dim relDir As String = Path.GetDirectoryName(item.RelPath)
            Dim targetDir As String

            If String.IsNullOrWhiteSpace(relDir) Then
                targetDir = _currentJobPath
            Else
                targetDir = Path.Combine(_currentJobPath, relDir)
            End If

            Directory.CreateDirectory(targetDir)

            ' --- 2) Nome progressivo globale sul lavoro ---
            Dim newName = GetNextProgressiveFileName(_currentJobPath, ext)

            ' --- 3) Destinazione finale ---
            Dim destPath = Path.Combine(targetDir, newName)

            File.Move(item.FullPath, destPath)

            ' --- 4) Aggiornamento UI ---
            If _ui IsNot Nothing Then
                _ui.Post(Sub(state)
                             LoadJobFiles(_currentJobPath)
                         End Sub, Nothing)
            End If

        Catch ex As Exception
            Worker_LogLine("Errore spostamento: " & ex.Message)
        End Try

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
        If Not _previewEnabled Then Return   ' <<< BLOCCO GLOBALE ANTEPRIMA

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
    'Tasto cancella files
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

            If Not String.IsNullOrWhiteSpace(_currentJobPath) Then
                ' Rinumera + riallinea metadata.json
                RebuildMetadataAfterRenumber(_currentJobPath)

                ' Ricarica (ruote + lista)
                LoadMetadata(_currentJobPath)
                LoadJobFiles(_currentJobPath)
            End If

        Catch ex As Exception
            MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ripristina stato originale
            If wasMonitoring AndAlso _worker IsNot Nothing Then
                _worker.Start()
            End If
        End Try
    End Sub
    'Seleziono un file nella lista e aggiorno anteprima
    Private Sub dgvFiles_SelectionChanged(sender As Object, e As EventArgs) Handles dgvFiles.SelectionChanged
        Dim item = GetSelectedItem()
        If item Is Nothing Then Return
        ShowPreview(item.FullPath, item.Rotate)
    End Sub
    'Cambia la rotazione
    Private Sub dgvFiles_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles dgvFiles.CellValueChanged
        If e.RowIndex < 0 Then Return
        If dgvFiles.Columns(e.ColumnIndex).DataPropertyName <> "Rotate" Then Return

        Dim si = TryCast(dgvFiles.Rows(e.RowIndex).DataBoundItem, ScanItem)
        If si Is Nothing Then Return

        ' aggiorna anteprima con nuova rotazione (se la usi ancora)
        ShowPreview(si.FullPath, si.Rotate)

        ' --- salva rotazione nel metadata ---
        _rotationMap(si.RelPath) = si.Rotate

        If Not String.IsNullOrWhiteSpace(_currentJobPath) Then
            SaveMetadata(_currentJobPath)
        End If
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

            ' 0) export PDF
            Await exporter.ExportAllAsync(items)

            ' 1) copia originali in Archivio
            ArchiveOriginalTiffs(items, My.Settings.ArchiveDir, folderName)

            ' 2) ferma il monitor (programma in STOP)
            StopMonitoring()

            ' 3) libera anteprima (evita lock GDI+ su file in WORK)
            If picPreview.Image IsNot Nothing Then
                picPreview.Image.Dispose()
                picPreview.Image = Nothing
            End If

            ' 4) svuota COMPLETAMENTE WORK (file + cartelle)
            ClearWorkFolder()

            ' 5) pulisci lista UI
            _list.Clear()

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

    Private Sub SafeDeleteFile(path As String, Optional tries As Integer = 6, Optional delayMs As Integer = 250)
        For t = 1 To tries
            Try
                If File.Exists(path) Then
                    File.SetAttributes(path, FileAttributes.Normal)
                    File.Delete(path)
                End If
                Exit Sub
            Catch
                Threading.Thread.Sleep(delayMs)
            End Try
        Next
    End Sub

    Private Sub DeleteEmptyDirectories(root As String)
        If Not Directory.Exists(root) Then Return

        Dim dirs = Directory.EnumerateDirectories(root, "*", SearchOption.AllDirectories).ToList()
        dirs.Sort(Function(a, b) b.Length.CompareTo(a.Length)) ' più profonde prima

        For Each d In dirs
            Try
                If Directory.Exists(d) AndAlso Not Directory.EnumerateFileSystemEntries(d).Any() Then
                    Directory.Delete(d, False)
                End If
            Catch
            End Try
        Next
    End Sub

    Private Sub ClearWorkFolder()
        Dim root = My.Settings.WorkDir
        If String.IsNullOrWhiteSpace(root) Then Return
        If Not Directory.Exists(root) Then Return

        ' 1) cancella tutti i file
        For Each f In Directory.EnumerateFiles(root, "*", SearchOption.AllDirectories)
            SafeDeleteFile(f)
        Next

        ' 2) cancella tutte le cartelle rimaste vuote
        DeleteEmptyDirectories(root)
    End Sub
    'Rinomina la lista files
    Private Sub RenumberAllJobFiles(root As String)
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
        Dim tempList As New List(Of (tempPath As String, finalFolder As String, ext As String))()

        For Each f In files
            Dim folder = Path.GetDirectoryName(f)
            Dim ext = Path.GetExtension(f).ToLowerInvariant()
            If ext = ".tiff" Then ext = ".tif" ' uniformiamo
            Dim tmp = Path.Combine(folder, "__tmp__" & Guid.NewGuid().ToString("N") & ext)
            File.Move(f, tmp)
            tempList.Add((tmp, folder, ext))
        Next

        ' 2) Assegna nuovi numeri globali (attraverso tutte le sottocartelle)
        Dim n As Integer = 1
        For Each t In tempList
            Dim finalName = n.ToString("D5") & t.ext
            Dim finalPath = Path.Combine(t.finalFolder, finalName)
            File.Move(t.tempPath, finalPath)
            n += 1
        Next
    End Sub
    'Rinomino i files dentro Json
    Private Sub RebuildMetadataAfterRenumber(jobPath As String)
        If String.IsNullOrWhiteSpace(jobPath) OrElse Not Directory.Exists(jobPath) Then Return

        ' 1) elenco file PRIMA (ordinato come rinumerazione)
        Dim beforeFiles = Directory.EnumerateFiles(jobPath, "*.tif*", SearchOption.AllDirectories).ToList()
        beforeFiles.Sort(Function(a, b)
                             Dim na As Integer, nb As Integer
                             Dim sa = Path.GetFileNameWithoutExtension(a)
                             Dim sb = Path.GetFileNameWithoutExtension(b)
                             Dim ha = Integer.TryParse(sa, na)
                             Dim hb = Integer.TryParse(sb, nb)
                             If ha AndAlso hb Then Return na.CompareTo(nb)
                             Return String.Compare(a, b, StringComparison.OrdinalIgnoreCase)
                         End Function)

        ' 2) rotazioni in ordine (se manca => 0)
        Dim rotations As New List(Of Integer)
        For Each f In beforeFiles
            Dim rel = f.Substring(jobPath.Length).TrimStart("\"c)
            Dim r As Integer = 0
            If _rotationMap.ContainsKey(rel) Then r = _rotationMap(rel)
            rotations.Add(r)
        Next

        ' 3) rinumera
        RenumberAllJobFiles(jobPath)

        ' 4) elenco file DOPO (stesso ordinamento)
        Dim afterFiles = Directory.EnumerateFiles(jobPath, "*.tif*", SearchOption.AllDirectories).ToList()
        afterFiles.Sort(Function(a, b)
                            Dim na As Integer, nb As Integer
                            Dim sa = Path.GetFileNameWithoutExtension(a)
                            Dim sb = Path.GetFileNameWithoutExtension(b)
                            Dim ha = Integer.TryParse(sa, na)
                            Dim hb = Integer.TryParse(sb, nb)
                            If ha AndAlso hb Then Return na.CompareTo(nb)
                            Return String.Compare(a, b, StringComparison.OrdinalIgnoreCase)
                        End Function)

        ' 5) ricostruisci mappa (per posizione)
        Dim newMap As New Dictionary(Of String, Integer)(StringComparer.OrdinalIgnoreCase)
        Dim count = Math.Min(afterFiles.Count, rotations.Count)

        For i As Integer = 0 To count - 1
            Dim relNew = afterFiles(i).Substring(jobPath.Length).TrimStart("\"c)
            newMap(relNew) = rotations(i)
        Next

        _rotationMap = newMap
        SaveMetadata(jobPath)
    End Sub
End Class
