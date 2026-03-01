<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmSettings
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Richiesto da Progettazione Windows Form
    Private components As System.ComponentModel.IContainer

    'NOTA: la procedura che segue è richiesta da Progettazione Windows Form
    'Può essere modificata in Progettazione Windows Form.  
    'Non modificarla mediante l'editor del codice.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        btnBrowseOut = New Button()
        txtOutDir = New TextBox()
        Label4 = New Label()
        txtLog = New TextBox()
        btnBrowseWork = New Button()
        txtWorkDir = New TextBox()
        Label2 = New Label()
        btnBrowseIn = New Button()
        Label1 = New Label()
        txtInDir = New TextBox()
        btnOk = New Button()
        btnClose = New Button()
        MagickExe = New TextBox()
        Label3 = New Label()
        Label5 = New Label()
        GhostscriptExe = New TextBox()
        NumericUpDown = New NumericUpDown()
        Label6 = New Label()
        btnTestMagick = New Button()
        btnTestGhost = New Button()
        txtArchiveDir = New TextBox()
        Label7 = New Label()
        btnBrowseArchivio = New Button()
        Label8 = New Label()
        CType(NumericUpDown, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnBrowseOut
        ' 
        btnBrowseOut.Location = New Point(406, 111)
        btnBrowseOut.Margin = New Padding(2, 1, 2, 1)
        btnBrowseOut.Name = "btnBrowseOut"
        btnBrowseOut.Size = New Size(74, 23)
        btnBrowseOut.TabIndex = 25
        btnBrowseOut.Text = "Open"
        btnBrowseOut.UseVisualStyleBackColor = True
        ' 
        ' txtOutDir
        ' 
        txtOutDir.Location = New Point(13, 111)
        txtOutDir.Margin = New Padding(2, 1, 2, 1)
        txtOutDir.Name = "txtOutDir"
        txtOutDir.Size = New Size(389, 23)
        txtOutDir.TabIndex = 24
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(13, 95)
        Label4.Margin = New Padding(2, 0, 2, 0)
        Label4.Name = "Label4"
        Label4.Size = New Size(74, 15)
        Label4.TabIndex = 23
        Label4.Text = "Cartella OUT"
        ' 
        ' txtLog
        ' 
        txtLog.Location = New Point(13, 289)
        txtLog.Margin = New Padding(2, 1, 2, 1)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(570, 237)
        txtLog.TabIndex = 22
        ' 
        ' btnBrowseWork
        ' 
        btnBrowseWork.Location = New Point(406, 71)
        btnBrowseWork.Margin = New Padding(2, 1, 2, 1)
        btnBrowseWork.Name = "btnBrowseWork"
        btnBrowseWork.Size = New Size(74, 23)
        btnBrowseWork.TabIndex = 21
        btnBrowseWork.Text = "Open"
        btnBrowseWork.UseVisualStyleBackColor = True
        ' 
        ' txtWorkDir
        ' 
        txtWorkDir.Location = New Point(13, 71)
        txtWorkDir.Margin = New Padding(2, 1, 2, 1)
        txtWorkDir.Name = "txtWorkDir"
        txtWorkDir.Size = New Size(389, 23)
        txtWorkDir.TabIndex = 20
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(13, 52)
        Label2.Margin = New Padding(2, 0, 2, 0)
        Label2.Name = "Label2"
        Label2.Size = New Size(84, 15)
        Label2.TabIndex = 19
        Label2.Text = "Cartella WORK"
        ' 
        ' btnBrowseIn
        ' 
        btnBrowseIn.Location = New Point(406, 28)
        btnBrowseIn.Margin = New Padding(2, 1, 2, 1)
        btnBrowseIn.Name = "btnBrowseIn"
        btnBrowseIn.Size = New Size(74, 23)
        btnBrowseIn.TabIndex = 18
        btnBrowseIn.Text = "Open"
        btnBrowseIn.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(13, 9)
        Label1.Margin = New Padding(2, 0, 2, 0)
        Label1.Name = "Label1"
        Label1.Size = New Size(60, 15)
        Label1.TabIndex = 17
        Label1.Text = "Cartella In"
        ' 
        ' txtInDir
        ' 
        txtInDir.Location = New Point(13, 28)
        txtInDir.Margin = New Padding(2, 1, 2, 1)
        txtInDir.Name = "txtInDir"
        txtInDir.Size = New Size(389, 23)
        txtInDir.TabIndex = 16
        ' 
        ' btnOk
        ' 
        btnOk.Location = New Point(510, 182)
        btnOk.Margin = New Padding(2, 1, 2, 1)
        btnOk.Name = "btnOk"
        btnOk.Size = New Size(64, 39)
        btnOk.TabIndex = 26
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(510, 225)
        btnClose.Margin = New Padding(2, 1, 2, 1)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(64, 35)
        btnClose.TabIndex = 27
        btnClose.Text = "Chiudi"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' MagickExe
        ' 
        MagickExe.Location = New Point(13, 191)
        MagickExe.Margin = New Padding(2, 1, 2, 1)
        MagickExe.Name = "MagickExe"
        MagickExe.Size = New Size(282, 23)
        MagickExe.TabIndex = 28
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(13, 175)
        Label3.Margin = New Padding(2, 0, 2, 0)
        Label3.Name = "Label3"
        Label3.Size = New Size(94, 15)
        Label3.TabIndex = 29
        Label3.Text = "Convertitore Tiff"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(13, 215)
        Label5.Margin = New Padding(2, 0, 2, 0)
        Label5.Name = "Label5"
        Label5.Size = New Size(67, 15)
        Label5.TabIndex = 31
        Label5.Text = "Ghostscript"
        ' 
        ' GhostscriptExe
        ' 
        GhostscriptExe.Location = New Point(13, 231)
        GhostscriptExe.Margin = New Padding(2, 1, 2, 1)
        GhostscriptExe.Name = "GhostscriptExe"
        GhostscriptExe.Size = New Size(282, 23)
        GhostscriptExe.TabIndex = 30
        ' 
        ' NumericUpDown
        ' 
        NumericUpDown.Location = New Point(359, 231)
        NumericUpDown.Margin = New Padding(2, 1, 2, 1)
        NumericUpDown.Name = "NumericUpDown"
        NumericUpDown.Size = New Size(59, 23)
        NumericUpDown.TabIndex = 32
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(359, 209)
        Label6.Margin = New Padding(2, 0, 2, 0)
        Label6.Name = "Label6"
        Label6.Size = New Size(107, 15)
        Label6.TabIndex = 33
        Label6.Text = "Compressione PDF"
        ' 
        ' btnTestMagick
        ' 
        btnTestMagick.Location = New Point(302, 191)
        btnTestMagick.Margin = New Padding(2, 1, 2, 1)
        btnTestMagick.Name = "btnTestMagick"
        btnTestMagick.Size = New Size(40, 23)
        btnTestMagick.TabIndex = 34
        btnTestMagick.Text = "Test"
        btnTestMagick.UseVisualStyleBackColor = True
        ' 
        ' btnTestGhost
        ' 
        btnTestGhost.Location = New Point(302, 231)
        btnTestGhost.Margin = New Padding(2, 1, 2, 1)
        btnTestGhost.Name = "btnTestGhost"
        btnTestGhost.Size = New Size(40, 23)
        btnTestGhost.TabIndex = 35
        btnTestGhost.Text = "Test"
        btnTestGhost.UseVisualStyleBackColor = True
        ' 
        ' txtArchiveDir
        ' 
        txtArchiveDir.Location = New Point(13, 151)
        txtArchiveDir.Margin = New Padding(2, 1, 2, 1)
        txtArchiveDir.Name = "txtArchiveDir"
        txtArchiveDir.Size = New Size(389, 23)
        txtArchiveDir.TabIndex = 37
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(13, 135)
        Label7.Margin = New Padding(2, 0, 2, 0)
        Label7.Name = "Label7"
        Label7.Size = New Size(104, 15)
        Label7.TabIndex = 36
        Label7.Text = "Cartella ARCHIVIO"
        ' 
        ' btnBrowseArchivio
        ' 
        btnBrowseArchivio.Location = New Point(406, 150)
        btnBrowseArchivio.Margin = New Padding(2, 1, 2, 1)
        btnBrowseArchivio.Name = "btnBrowseArchivio"
        btnBrowseArchivio.Size = New Size(74, 22)
        btnBrowseArchivio.TabIndex = 38
        btnBrowseArchivio.Text = "Open"
        btnBrowseArchivio.UseVisualStyleBackColor = True
        ' 
        ' Label8
        ' 
        Label8.AutoSize = True
        Label8.Location = New Point(13, 273)
        Label8.Margin = New Padding(2, 0, 2, 0)
        Label8.Name = "Label8"
        Label8.Size = New Size(27, 15)
        Label8.TabIndex = 39
        Label8.Text = "Log"
        ' 
        ' FrmSettings
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(605, 536)
        Controls.Add(Label8)
        Controls.Add(btnBrowseArchivio)
        Controls.Add(txtArchiveDir)
        Controls.Add(Label7)
        Controls.Add(btnTestGhost)
        Controls.Add(btnTestMagick)
        Controls.Add(Label6)
        Controls.Add(NumericUpDown)
        Controls.Add(Label5)
        Controls.Add(GhostscriptExe)
        Controls.Add(Label3)
        Controls.Add(MagickExe)
        Controls.Add(btnClose)
        Controls.Add(btnOk)
        Controls.Add(btnBrowseOut)
        Controls.Add(txtOutDir)
        Controls.Add(Label4)
        Controls.Add(txtLog)
        Controls.Add(btnBrowseWork)
        Controls.Add(txtWorkDir)
        Controls.Add(Label2)
        Controls.Add(btnBrowseIn)
        Controls.Add(Label1)
        Controls.Add(txtInDir)
        FormBorderStyle = FormBorderStyle.FixedToolWindow
        Margin = New Padding(2, 1, 2, 1)
        Name = "FrmSettings"
        Text = "Settaggi"
        CType(NumericUpDown, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents btnBrowseOut As Button
    Friend WithEvents txtOutDir As TextBox
    Friend WithEvents Label4 As Label
    Friend WithEvents txtLog As TextBox
    Friend WithEvents btnBrowseWork As Button
    Friend WithEvents txtWorkDir As TextBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnBrowseIn As Button
    Friend WithEvents Label1 As Label
    Friend WithEvents txtInDir As TextBox
    Friend WithEvents btnOk As Button
    Friend WithEvents btnClose As Button
    Friend WithEvents MagickExe As TextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents GhostscriptExe As TextBox
    Friend WithEvents NumericUpDown As NumericUpDown
    Friend WithEvents Label6 As Label
    Friend WithEvents btnTestMagick As Button
    Friend WithEvents btnTestGhost As Button
    Friend WithEvents txtArchiveDir As TextBox
    Friend WithEvents Label7 As Label
    Friend WithEvents btnBrowseArchivio As Button
    Friend WithEvents Label8 As Label
End Class
