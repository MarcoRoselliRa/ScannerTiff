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
        txtInDir = New TextBox()
        Label1 = New Label()
        btnBrowseIn = New Button()
        Label2 = New Label()
        txtWorkDir = New TextBox()
        btnBrowseWork = New Button()
        Label3 = New Label()
        txtSubDir = New TextBox()
        btnStart = New Button()
        btnStop = New Button()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        dgvFiles = New DataGridView()
        picPreview = New PictureBox()
        txtLog = New TextBox()
        Label4 = New Label()
        txtOutDir = New TextBox()
        btnBrowseOut = New Button()
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' txtInDir
        ' 
        txtInDir.Location = New Point(42, 44)
        txtInDir.Name = "txtInDir"
        txtInDir.Size = New Size(188, 39)
        txtInDir.TabIndex = 0
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(42, 9)
        Label1.Name = "Label1"
        Label1.Size = New Size(121, 32)
        Label1.TabIndex = 1
        Label1.Text = "Cartella In"
        ' 
        ' btnBrowseIn
        ' 
        btnBrowseIn.Location = New Point(295, 50)
        btnBrowseIn.Name = "btnBrowseIn"
        btnBrowseIn.Size = New Size(137, 42)
        btnBrowseIn.TabIndex = 2
        btnBrowseIn.Text = "Button1"
        btnBrowseIn.UseVisualStyleBackColor = True
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(51, 99)
        Label2.Name = "Label2"
        Label2.Size = New Size(169, 32)
        Label2.TabIndex = 3
        Label2.Text = "Cartella WORK"
        ' 
        ' txtWorkDir
        ' 
        txtWorkDir.Location = New Point(50, 143)
        txtWorkDir.Name = "txtWorkDir"
        txtWorkDir.Size = New Size(236, 39)
        txtWorkDir.TabIndex = 4
        ' 
        ' btnBrowseWork
        ' 
        btnBrowseWork.Location = New Point(310, 143)
        btnBrowseWork.Name = "btnBrowseWork"
        btnBrowseWork.Size = New Size(121, 43)
        btnBrowseWork.TabIndex = 5
        btnBrowseWork.Text = "Button1"
        btnBrowseWork.UseVisualStyleBackColor = True
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(536, 25)
        Label3.Name = "Label3"
        Label3.Size = New Size(99, 32)
        Label3.TabIndex = 6
        Label3.Text = "Sottodir"
        ' 
        ' txtSubDir
        ' 
        txtSubDir.Location = New Point(537, 65)
        txtSubDir.Name = "txtSubDir"
        txtSubDir.Size = New Size(134, 39)
        txtSubDir.TabIndex = 7
        ' 
        ' btnStart
        ' 
        btnStart.Location = New Point(526, 136)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(91, 60)
        btnStart.TabIndex = 8
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnStop
        ' 
        btnStop.Enabled = False
        btnStop.Location = New Point(640, 134)
        btnStop.Name = "btnStop"
        btnStop.Size = New Size(91, 60)
        btnStop.TabIndex = 9
        btnStop.Text = "Stop"
        btnStop.UseVisualStyleBackColor = True
        ' 
        ' dgvFiles
        ' 
        dgvFiles.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize
        dgvFiles.Location = New Point(52, 266)
        dgvFiles.MultiSelect = False
        dgvFiles.Name = "dgvFiles"
        dgvFiles.ReadOnly = True
        dgvFiles.RowHeadersWidth = 82
        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.Size = New Size(497, 633)
        dgvFiles.TabIndex = 10
        ' 
        ' picPreview
        ' 
        picPreview.Location = New Point(768, 271)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(430, 639)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 11
        picPreview.TabStop = False
        ' 
        ' txtLog
        ' 
        txtLog.Location = New Point(596, 750)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(381, 191)
        txtLog.TabIndex = 12
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(52, 185)
        Label4.Name = "Label4"
        Label4.Size = New Size(148, 32)
        Label4.TabIndex = 13
        Label4.Text = "Cartella OUT"
        ' 
        ' txtOutDir
        ' 
        txtOutDir.Location = New Point(50, 221)
        txtOutDir.Name = "txtOutDir"
        txtOutDir.Size = New Size(236, 39)
        txtOutDir.TabIndex = 14
        ' 
        ' btnBrowseOut
        ' 
        btnBrowseOut.Location = New Point(324, 221)
        btnBrowseOut.Name = "btnBrowseOut"
        btnBrowseOut.Size = New Size(116, 47)
        btnBrowseOut.TabIndex = 15
        btnBrowseOut.Text = "Button1"
        btnBrowseOut.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1309, 982)
        Controls.Add(btnBrowseOut)
        Controls.Add(txtOutDir)
        Controls.Add(Label4)
        Controls.Add(txtLog)
        Controls.Add(picPreview)
        Controls.Add(dgvFiles)
        Controls.Add(btnStop)
        Controls.Add(btnStart)
        Controls.Add(txtSubDir)
        Controls.Add(Label3)
        Controls.Add(btnBrowseWork)
        Controls.Add(txtWorkDir)
        Controls.Add(Label2)
        Controls.Add(btnBrowseIn)
        Controls.Add(Label1)
        Controls.Add(txtInDir)
        Name = "Form1"
        Text = "Form1"
        CType(dgvFiles, ComponentModel.ISupportInitialize).EndInit()
        CType(picPreview, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents txtInDir As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnBrowseIn As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents txtWorkDir As TextBox
    Friend WithEvents btnBrowseWork As Button
    Friend WithEvents Label3 As Label
    Friend WithEvents txtSubDir As TextBox
    Friend WithEvents btnStart As Button
    Friend WithEvents btnStop As Button
    Friend WithEvents FolderBrowserDialog1 As FolderBrowserDialog
    Friend WithEvents dgvFiles As DataGridView
    Friend WithEvents picPreview As PictureBox
    Friend WithEvents txtLog As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtOutDir As TextBox
    Friend WithEvents btnBrowseOut As Button

End Class
