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
        Label3 = New Label()
        txtSubDir = New TextBox()
        btnStart = New Button()
        btnStop = New Button()
        FolderBrowserDialog1 = New FolderBrowserDialog()
        dgvFiles = New DataGridView()
        picPreview = New PictureBox()
        btnDelete = New Button()
        btnSettings = New Button()
        CType(dgvFiles, ComponentModel.ISupportInitialize).BeginInit()
        CType(picPreview, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(782, 14)
        Label3.Name = "Label3"
        Label3.Size = New Size(99, 32)
        Label3.TabIndex = 6
        Label3.Text = "Sottodir"
        ' 
        ' txtSubDir
        ' 
        txtSubDir.Location = New Point(782, 49)
        txtSubDir.Name = "txtSubDir"
        txtSubDir.Size = New Size(134, 39)
        txtSubDir.TabIndex = 7
        ' 
        ' btnStart
        ' 
        btnStart.Location = New Point(1062, 28)
        btnStart.Name = "btnStart"
        btnStart.Size = New Size(91, 60)
        btnStart.TabIndex = 8
        btnStart.Text = "Start"
        btnStart.UseVisualStyleBackColor = True
        ' 
        ' btnStop
        ' 
        btnStop.Enabled = False
        btnStop.Location = New Point(1176, 26)
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
        dgvFiles.RowHeadersWidth = 82
        dgvFiles.ScrollBars = ScrollBars.Vertical
        dgvFiles.SelectionMode = DataGridViewSelectionMode.FullRowSelect
        dgvFiles.Size = New Size(747, 494)
        dgvFiles.TabIndex = 10
        ' 
        ' picPreview
        ' 
        picPreview.Location = New Point(846, 266)
        picPreview.Name = "picPreview"
        picPreview.Size = New Size(430, 489)
        picPreview.SizeMode = PictureBoxSizeMode.Zoom
        picPreview.TabIndex = 11
        picPreview.TabStop = False
        ' 
        ' btnDelete
        ' 
        btnDelete.Location = New Point(820, 209)
        btnDelete.Name = "btnDelete"
        btnDelete.Size = New Size(154, 38)
        btnDelete.TabIndex = 16
        btnDelete.Text = "Cancella"
        btnDelete.UseVisualStyleBackColor = True
        ' 
        ' btnSettings
        ' 
        btnSettings.Location = New Point(1086, 118)
        btnSettings.Name = "btnSettings"
        btnSettings.Size = New Size(118, 43)
        btnSettings.TabIndex = 17
        btnSettings.Text = "Setting"
        btnSettings.UseVisualStyleBackColor = True
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(13.0F, 32.0F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1309, 1040)
        Controls.Add(btnSettings)
        Controls.Add(btnDelete)
        Controls.Add(picPreview)
        Controls.Add(dgvFiles)
        Controls.Add(btnStop)
        Controls.Add(btnStart)
        Controls.Add(txtSubDir)
        Controls.Add(Label3)
        Name = "Form1"
        Text = "Form1"
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

End Class
