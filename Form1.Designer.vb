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
        dgvQueue = New DataGridView()
        GroupBox1 = New GroupBox()
        Label4 = New Label()
        Label2 = New Label()
        GroupBox2 = New GroupBox()
        Label5 = New Label()
        Label6 = New Label()
        btnOpenOut = New Button()
        btnOpenIn = New Button()
        btnOpenArchive = New Button()
        btnOpenOutJob = New Button()
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        CType(dgvQueue, ComponentModel.ISupportInitialize).BeginInit()
        GroupBox1.SuspendLayout()
        GroupBox2.SuspendLayout()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(19, 15)
        Label3.Margin = New Padding(2, 0, 2, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(78, 15)
        Label3.TabIndex = 6
        Label3.Text = "Sotto Cartella"
        ' 
        ' txtSubDir
        ' 
        txtSubDir.Location = New Point(18, 31)
        txtSubDir.Margin = New Padding(2, 1, 2, 1)
        txtSubDir.Name = "txtSubDir"
        txtSubDir.Size = New Size(108, 23)
        txtSubDir.TabIndex = 3
        ' 
        ' btnStart
        ' 
        btnStart.Location = New Point(515, 20)
        btnStart.Margin = New Padding(2, 1, 2, 1)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(81, 28)
        btnStart.TabIndex = 17
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnStop
        ' 
        btnStop.Enabled = False
        btnStop.Location = New Point(515, 53)
        btnStop.Margin = New Padding(2, 1, 2, 1)
        btnStop.Name = "btnStop"
        btnStop.Size = New Size(81, 28)
        btnStop.TabIndex = 18
        btnStop.Text = "Stop"
        btnStop.UseVisualStyleBackColor = True
        ' 
        ' dgvFiles
        ' 
        dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFiles.Location = New Point(22, 190)
        dgvFiles.Margin = New Padding(2, 1, 2, 1)
        dgvFiles.MultiSelect = False
        dgvFiles.Name = "dgvFiles"
        dgvFiles.RowHeadersWidth = 82
        dgvFiles.ScrollBars = ScrollBars.Vertical
        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.Size = New Size(574, 571)
        dgvFiles.TabIndex = 10
        ' 
        ' picPreview
        ' 
        picPreview.Location = New Point(616, 190)
        picPreview.Margin = New Padding(2, 1, 2, 1)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(472, 571)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 11
        picPreview.TabStop = False
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(329, 29)
        btnDelete.Margin = New Padding(2, 1, 2, 1)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(122, 23)
        btnDelete.TabIndex = 6
        btnDelete.Text = "Cancella Selezione"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnSettings
        ' 
        btnSettings.Location = New Point(515, 133)
        btnSettings.Margin = New Padding(2, 1, 2, 1)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Size(81, 23)
        btnSettings.TabIndex = 20
        btnSettings.Text = "Setting"
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' btnExport
        ' 
        btnExport.Location = New Point(515, 87)
        btnExport.Margin = New Padding(2, 1, 2, 1)
        btnExport.Name = "btnExport"
        btnExport.Size = New Size(81, 40)
        btnExport.TabIndex = 19
        btnExport.Text = "Esporta"
        btnExport.UseVisualStyleBackColor = True
        ' 
        ' cmbRotateAll
        ' 
        cmbRotateAll.DropDownStyle = ComboBoxStyle.DropDownList
        cmbRotateAll.FormattingEnabled = True
        cmbRotateAll.Items.AddRange(New Object() {"0,90,180,270"})
        cmbRotateAll.Location = New Point(143, 30)
        cmbRotateAll.Margin = New Padding(2, 1, 2, 1)
        cmbRotateAll.Name = "cmbRotateAll"
        cmbRotateAll.Size = New Size(78, 23)
        cmbRotateAll.TabIndex = 4
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(145, 15)
        Label1.Margin = New Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(68, 15)
        Label1.TabIndex = 20
        Label1.Text = "Rot. default"
        ' 
        ' chkAnteprima
        ' 
        chkAnteprima.Appearance = Appearance.Button
        chkAnteprima.AutoSize = True
        chkAnteprima.Location = New Point(238, 30)
        chkAnteprima.Margin = New Padding(2, 1, 2, 1)
        chkAnteprima.Name = "chkAnteprima"
        chkAnteprima.Size = New Size(73, 25)
        chkAnteprima.TabIndex = 5
        chkAnteprima.Text = "Anteprima"
        chkAnteprima.UseVisualStyleBackColor = True
        ' 
        ' cmbLavoro
        ' 
        cmbLavoro.FormattingEnabled = True
        cmbLavoro.Location = New Point(160, 31)
        cmbLavoro.Margin = New Padding(2, 1, 2, 1)
        cmbLavoro.Name = "cmbLavoro"
        cmbLavoro.Size = New Size(200, 23)
        cmbLavoro.TabIndex = 1
        ' 
        ' cmbSede
        ' 
        cmbSede.DropDownStyle = ComboBoxStyle.DropDownList
        cmbSede.FormattingEnabled = True
        cmbSede.Items.AddRange(New Object() {"Cervia", "Russi", "Altro"})
        cmbSede.Location = New Point(18, 31)
        cmbSede.Margin = New Padding(2, 1, 2, 1)
        cmbSede.Name = "cmbSede"
        cmbSede.Size = New Size(126, 23)
        cmbSede.TabIndex = 0
        ' 
        ' btnReset
        ' 
        btnReset.Location = New Point(385, 30)
        btnReset.Margin = New Padding(2, 1, 2, 1)
        btnReset.Name = "btnReset"
        btnReset.Size = New Size(66, 23)
        btnReset.TabIndex = 2
        btnReset.Text = "Reset"
        btnReset.UseVisualStyleBackColor = True
        ' 
        ' dgvQueue
        ' 
        dgvQueue.AllowUserToAddRows = False
        dgvQueue.AllowUserToDeleteRows = False
        dgvQueue.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvQueue.Location = New Point(708, 20)
        dgvQueue.Margin = New Padding(2, 1, 2, 1)
        dgvQueue.MultiSelect = False
        dgvQueue.Name = "dgvQueue"
        dgvQueue.ReadOnly = True
        dgvQueue.RowHeadersWidth = 82
        dgvQueue.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvQueue.Size = New Size(380, 136)
        dgvQueue.TabIndex = 25
        ' 
        ' GroupBox1
        ' 
        GroupBox1.Controls.Add(Label4)
        GroupBox1.Controls.Add(Label2)
        GroupBox1.Controls.Add(cmbSede)
        GroupBox1.Controls.Add(btnReset)
        GroupBox1.Controls.Add(cmbLavoro)
        GroupBox1.Location = New Point(22, 12)
        GroupBox1.Name = "GroupBox1"
        GroupBox1.Size = New Size(471, 69)
        GroupBox1.TabIndex = 26
        GroupBox1.TabStop = False
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(160, 15)
        Label4.Margin = New Padding(2, 0, 2, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(84, 15)
        Label4.TabIndex = 25
        Label4.Text = "Pratica\Lavoro"
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(18, 15)
        Label2.Margin = New Padding(2, 0, 2, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(53, 15)
        Label2.TabIndex = 24
        Label2.Text = "Comune"
        ' 
        ' GroupBox2
        ' 
        GroupBox2.Controls.Add(txtSubDir)
        GroupBox2.Controls.Add(Label3)
        GroupBox2.Controls.Add(chkAnteprima)
        GroupBox2.Controls.Add(cmbRotateAll)
        GroupBox2.Controls.Add(Label1)
        GroupBox2.Controls.Add(btnDelete)
        GroupBox2.Location = New Point(22, 87)
        GroupBox2.Name = "GroupBox2"
        GroupBox2.Size = New Size(471, 69)
        GroupBox2.TabIndex = 27
        GroupBox2.TabStop = False
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(22, 171)
        Label5.Margin = New Padding(2, 0, 2, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(79, 15)
        Label5.TabIndex = 28
        Label5.Text = "Elenco Tavole"
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(616, 171)
        Label6.Margin = New Padding(2, 0, 2, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(63, 15)
        Label6.TabIndex = 29
        Label6.Text = "Anteprima"
        ' 
        ' btnOpenOut
        ' 
        btnOpenOut.Location = New Point(603, 20)
        btnOpenOut.Margin = New Padding(2, 1, 2, 1)
        btnOpenOut.Name = "btnOpenOut"
        btnOpenOut.Size = New Size(81, 28)
        btnOpenOut.TabIndex = 30
        btnOpenOut.Text = "Cartella Out"
        btnOpenOut.UseVisualStyleBackColor = True
        ' 
        ' btnOpenIn
        ' 
        btnOpenIn.Location = New Point(603, 53)
        btnOpenIn.Margin = New Padding(2, 1, 2, 1)
        btnOpenIn.Name = "btnOpenIn"
        btnOpenIn.Size = New Size(81, 28)
        btnOpenIn.TabIndex = 31
        btnOpenIn.Text = "Cartella IN"
        btnOpenIn.UseVisualStyleBackColor = True
        ' 
        ' btnOpenArchive
        ' 
        btnOpenArchive.Location = New Point(603, 87)
        btnOpenArchive.Margin = New Padding(2, 1, 2, 1)
        btnOpenArchive.Name = "btnOpenArchive"
        btnOpenArchive.Size = New Size(81, 40)
        btnOpenArchive.TabIndex = 32
        btnOpenArchive.Text = "Cartella Archivio"
        btnOpenArchive.UseVisualStyleBackColor = True
        ' 
        ' btnOpenOutJob
        ' 
        btnOpenOutJob.Location = New Point(603, 133)
        btnOpenOutJob.Margin = New Padding(2, 1, 2, 1)
        btnOpenOutJob.Name = "btnOpenOutJob"
        btnOpenOutJob.Size = New Size(81, 23)
        btnOpenOutJob.TabIndex = 33
        btnOpenOutJob.Text = "Corrente"
        btnOpenOutJob.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1109, 786)
        Controls.Add(btnOpenOutJob)
        Controls.Add(btnOpenArchive)
        Controls.Add(btnOpenIn)
        Controls.Add(btnOpenOut)
        Controls.Add(Label6)
        Controls.Add(Label5)
        Controls.Add(GroupBox2)
        Controls.Add(GroupBox1)
        Controls.Add(dgvQueue)
        Controls.Add(btnExport)
        Controls.Add(btnSettings)
        Controls.Add(picPreview)
        Controls.Add(dgvFiles)
        Controls.Add(btnStop)
        Controls.Add(btnStart)
        Icon = CType(resources.GetObject("$this.Icon"), Icon)
        Margin = New Padding(2, 1, 2, 1)
        Name = "Form1"
        StartPosition = FormStartPosition.CenterScreen
        Text = "ScannerTiff  -  Ver. 2.0"
        CType(dgvFiles, ComponentModel.ISupportInitialize).EndInit()
        CType(picPreview, ComponentModel.ISupportInitialize).EndInit()
        CType(dgvQueue, ComponentModel.ISupportInitialize).EndInit()
        GroupBox1.ResumeLayout(False)
        GroupBox1.PerformLayout()
        GroupBox2.ResumeLayout(False)
        GroupBox2.PerformLayout()
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
    Friend WithEvents dgvQueue As DataGridView
    Friend WithEvents GroupBox1 As GroupBox
    Friend WithEvents Label4 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents GroupBox2 As GroupBox
    Friend WithEvents Label5 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents btnOpenOut As Button
    Friend WithEvents btnOpenIn As Button
    Friend WithEvents btnOpenArchive As Button
    Friend WithEvents btnOpenOutJob As Button

End Class
