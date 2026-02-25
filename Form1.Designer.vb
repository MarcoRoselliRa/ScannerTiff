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
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(11, 9)
        Label3.Margin = New Padding(2, 0, 2, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(49, 15)
        Label3.TabIndex = 6
        Label3.Text = "Sottodir"
        ' 
        ' txtSubDir
        ' 
        txtSubDir.Location = New Point(114, 6)
        txtSubDir.Margin = New Padding(2, 1, 2, 1)
        txtSubDir.Name = "txtSubDir"
        txtSubDir.Size = New Size(78, 23)
        txtSubDir.TabIndex = 7
        ' 
        ' btnStart
        ' 
        btnStart.Location = New Point(483, 27)
        btnStart.Margin = New Padding(2, 1, 2, 1)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(49, 28)
        btnStart.TabIndex = 8
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnStop
        ' 
        btnStop.Enabled = False
        btnStop.Location = New Point(536, 27)
        btnStop.Margin = New Padding(2, 1, 2, 1)
        btnStop.Name = "btnStop"
        btnStop.Size = New Size(49, 28)
        btnStop.TabIndex = 9
        btnStop.Text = "Stop"
        btnStop.UseVisualStyleBackColor = True
        ' 
        ' dgvFiles
        ' 
        dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFiles.Location = New Point(11, 66)
        dgvFiles.Margin = New Padding(2, 1, 2, 1)
        dgvFiles.MultiSelect = False
        dgvFiles.Name = "dgvFiles"
        dgvFiles.RowHeadersWidth = 82
        dgvFiles.ScrollBars = ScrollBars.Vertical
        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.Size = New Size(574, 673)
        dgvFiles.TabIndex = 10
        ' 
        ' picPreview
        ' 
        picPreview.Location = New Point(618, 66)
        picPreview.Margin = New Padding(2, 1, 2, 1)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(568, 673)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 11
        picPreview.TabStop = False
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(207, 33)
        btnDelete.Margin = New Padding(2, 1, 2, 1)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(122, 23)
        btnDelete.TabIndex = 16
        btnDelete.Text = "Cancella Selezione"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnSettings
        ' 
        btnSettings.Location = New Point(483, 2)
        btnSettings.Margin = New Padding(2, 1, 2, 1)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Size(64, 23)
        btnSettings.TabIndex = 17
        btnSettings.Text = "Setting"
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnExport
        ' 
        btnExport.Location = New Point(371, 33)
        btnExport.Margin = New Padding(2, 1, 2, 1)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(86, 22)
        btnExport.TabIndex = 18
        btnExport.Text = "Esporta"
        btnExport.UseVisualStyleBackColor = True
        ' 
        ' cmbRotateAll
        ' 
        cmbRotateAll.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRotateAll.FormattingEnabled = True
        cmbRotateAll.Items.AddRange(New Object() {"0,90,180,270"})
        cmbRotateAll.Location = New Point(114, 34)
        cmbRotateAll.Margin = New Padding(2, 1, 2, 1)
        cmbRotateAll.Name = "cmbRotateAll"
        cmbRotateAll.Size = New Size(78, 23)
        cmbRotateAll.TabIndex = 19
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(11, 42)
        Label1.Margin = New Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(99, 15)
        Label1.TabIndex = 20
        Label1.Text = "Rotazione default"
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1208, 749)
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
        Margin = New Padding(2, 1, 2, 1)
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

End Class
