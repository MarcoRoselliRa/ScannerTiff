<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Form1))
        Label3 = New Label()
        txtSubDir = New TextBox()
        btnStart = New Button()
        btnStop = New Button()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        dgvFiles = New DataGridView()
        picPreview = New PictureBox()
        btnDelete = New Button()
        btnSettings = New Button()
        btnExport = New Button()
        cmbRotateAll = New ComboBox()
        Label1 = New Label()
        chkAnteprima = New CheckBox()
        cmbLavoro = New ComboBox()
        cmbSede = New ComboBox()
        btnReset = New Button()
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(20, 19)
        Label3.Margin = New Padding(4, 0, 4, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(99, 32)
        Label3.TabIndex = 6
        Label3.Text = "Sottodir"
        ' 
        ' txtSubDir
        ' 
        txtSubDir.Location = New Point(212, 13)
        txtSubDir.Margin = New Padding(4, 2, 4, 2)
        txtSubDir.Name = "txtSubDir"
        txtSubDir.Size = New Size(141, 39)
        txtSubDir.TabIndex = 7
        ' 
        ' btnStart
        ' 
        btnStart.Location = New Point(897, 58)
        btnStart.Margin = New Padding(4, 2, 4, 2)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(91, 60)
        btnStart.TabIndex = 8
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnStop
        ' 
        btnStop.Enabled = False
        btnStop.Location = New Point(995, 58)
        btnStop.Margin = New Padding(4, 2, 4, 2)
        btnStop.Name = "btnStop"
        btnStop.Size = New Size(91, 60)
        btnStop.TabIndex = 9
        btnStop.Text = "Stop"
        btnStop.UseVisualStyleBackColor = True
        ' 
        ' dgvFiles
        ' 
        dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFiles.Location = New Point(20, 141)
        dgvFiles.Margin = New Padding(4, 2, 4, 2)
        dgvFiles.MultiSelect = False
        dgvFiles.Name = "dgvFiles"
        dgvFiles.RowHeadersWidth = 82
        dgvFiles.ScrollBars = ScrollBars.Vertical
        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.Size = New Size(1066, 1436)
        dgvFiles.TabIndex = 10
        ' 
        ' picPreview
        ' 
        picPreview.Location = New Point(1148, 141)
        picPreview.Margin = New Padding(4, 2, 4, 2)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(1055, 723)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 11
        picPreview.TabStop = False
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(384, 70)
        btnDelete.Margin = New Padding(4, 2, 4, 2)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(227, 49)
        btnDelete.TabIndex = 16
        btnDelete.Text = "Cancella Selezione"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnSettings
        ' 
        btnSettings.Location = New Point(967, 2)
        btnSettings.Margin = New Padding(4, 2, 4, 2)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Size(119, 49)
        btnSettings.TabIndex = 17
        btnSettings.Text = "Setting"
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnExport
        ' 
        btnExport.Location = New Point(689, 70)
        btnExport.Margin = New Padding(4, 2, 4, 2)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(160, 47)
        btnExport.TabIndex = 18
        btnExport.Text = "Esporta"
        btnExport.UseVisualStyleBackColor = True
        ' 
        ' cmbRotateAll
        ' 
        cmbRotateAll.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRotateAll.FormattingEnabled = True
        cmbRotateAll.Items.AddRange(New Object() {"0,90,180,270"})
        cmbRotateAll.Location = New Point(212, 73)
        cmbRotateAll.Margin = New Padding(4, 2, 4, 2)
        cmbRotateAll.Name = "cmbRotateAll"
        cmbRotateAll.Size = New Size(141, 40)
        cmbRotateAll.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(20, 90)
        Label1.Margin = New Padding(4, 0, 4, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(201, 32)
        Label1.TabIndex = 20
        Label1.Text = "Rotazione default"
        ' 
        ' chkAnteprima
        ' 
        chkAnteprima.AutoSize = True
        chkAnteprima.Location = New Point(400, 21)
        chkAnteprima.Name = "chkAnteprima"
        chkAnteprima.Size = New Size(157, 36)
        chkAnteprima.TabIndex = 21
        chkAnteprima.Text = "Anteprima"
        chkAnteprima.UseVisualStyleBackColor = True
        ' 
        ' cmbLavoro
        ' 
        cmbLavoro.FormattingEnabled = True
        cmbLavoro.Location = New Point(563, 17)
        cmbLavoro.Name = "cmbLavoro"
        cmbLavoro.Size = New Size(368, 40)
        cmbLavoro.TabIndex = 22
        ' 
        ' cmbSede
        ' 
        cmbSede.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSede.FormattingEnabled = True
        cmbSede.Items.AddRange(New Object() {"Cervia", "Russi", "Altro"})
        cmbSede.Location = New Point(1207, 30)
        cmbSede.Name = "cmbSede"
        cmbSede.Size = New Size(231, 40)
        cmbSede.TabIndex = 23
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(1526, 37)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(122, 58)
        btnReset.TabIndex = 24
        btnReset.Text = "Reset"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(2243, 1598)
        Controls.Add(btnReset)
        Controls.Add(cmbSede)
        Controls.Add(cmbLavoro)
        Controls.Add(chkAnteprima)
        Controls.Add(Label1)
        Controls.Add(cmbRotateAll)
        Controls.Add(btnExport)
        Controls.Add(btnSettings)
        Controls.Add(btnDelete)
        Controls.Add(picPreview)
        Controls.Add(dgvFiles)
        Controls.Add(btnStop)
        Controls.Add(btnStart)
        Controls.Add(txtSubDir)
        Controls.Add(Label3)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(4, 2, 4, 2)
        Name = "Form1"
        Text = "ScannerTiff"
        CType(dgvFiles, ComponentModel.ISupportInitialize).EndInit()
        CType(picPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSubDir As TextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents dgvFiles As DataGridView
    Friend WithEvents picPreview As PictureBox
    Friend WithEvents btnDelete As Button
    Friend WithEvents btnSettings As Button
    Friend WithEvents btnExport As Button
    Friend WithEvents cmbRotateAll As ComboBox
    Friend WithEvents Label1 As Label
    Friend WithEvents chkAnteprima As CheckBox
    Friend WithEvents cmbLavoro As ComboBox
    Friend WithEvents cmbSede As ComboBox
    Friend WithEvents btnReset As Button

End Class
