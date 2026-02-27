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
        CType(NumericUpDown, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' btnBrowseOut
        ' 
        btnBrowseOut.Location = New Point(782, 199)
        btnBrowseOut.Name = "btnBrowseOut"
        btnBrowseOut.Size = New Size(137, 47)
        btnBrowseOut.TabIndex = 25
        btnBrowseOut.Text = "Button1"
        btnBrowseOut.UseVisualStyleBackColor = True
        ' 
        ' txtOutDir
        ' 
        txtOutDir.Location = New Point(24, 207)
        txtOutDir.Name = "txtOutDir"
        txtOutDir.Size = New Size(719, 39)
        txtOutDir.TabIndex = 24
        ' 
        ' Label4
        ' 
        Label4.AutoSize = True
        Label4.Location = New Point(24, 172)
        Label4.Name = "Label4"
        Label4.Size = New Size(148, 32)
        Label4.TabIndex = 23
        Label4.Text = "Cartella OUT"
        ' 
        ' txtLog
        ' 
        txtLog.Location = New Point(24, 533)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(1056, 381)
        txtLog.TabIndex = 22
        ' 
        ' btnBrowseWork
        ' 
        btnBrowseWork.Location = New Point(782, 129)
        btnBrowseWork.Name = "btnBrowseWork"
        btnBrowseWork.Size = New Size(137, 43)
        btnBrowseWork.TabIndex = 21
        btnBrowseWork.Text = "Button1"
        btnBrowseWork.UseVisualStyleBackColor = True
        ' 
        ' txtWorkDir
        ' 
        txtWorkDir.Location = New Point(24, 131)
        txtWorkDir.Name = "txtWorkDir"
        txtWorkDir.Size = New Size(719, 39)
        txtWorkDir.TabIndex = 20
        ' 
        ' Label2
        ' 
        Label2.AutoSize = True
        Label2.Location = New Point(24, 96)
        Label2.Name = "Label2"
        Label2.Size = New Size(169, 32)
        Label2.TabIndex = 19
        Label2.Text = "Cartella WORK"
        ' 
        ' btnBrowseIn
        ' 
        btnBrowseIn.Location = New Point(782, 54)
        btnBrowseIn.Name = "btnBrowseIn"
        btnBrowseIn.Size = New Size(137, 42)
        btnBrowseIn.TabIndex = 18
        btnBrowseIn.Text = "Button1"
        btnBrowseIn.UseVisualStyleBackColor = True
        ' 
        ' Label1
        ' 
        Label1.AutoSize = True
        Label1.Location = New Point(24, 19)
        Label1.Name = "Label1"
        Label1.Size = New Size(121, 32)
        Label1.TabIndex = 17
        Label1.Text = "Cartella In"
        ' 
        ' txtInDir
        ' 
        txtInDir.Location = New Point(24, 54)
        txtInDir.Name = "txtInDir"
        txtInDir.Size = New Size(719, 39)
        txtInDir.TabIndex = 16
        ' 
        ' btnOk
        ' 
        btnOk.Location = New Point(954, 51)
        btnOk.Name = "btnOk"
        btnOk.Size = New Size(75, 45)
        btnOk.TabIndex = 26
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(969, 102)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(111, 41)
        btnClose.TabIndex = 27
        btnClose.Text = "Chiudi"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' MagickExe
        ' 
        MagickExe.Location = New Point(24, 364)
        MagickExe.Name = "MagickExe"
        MagickExe.Size = New Size(520, 39)
        MagickExe.TabIndex = 28
        ' 
        ' Label3
        ' 
        Label3.AutoSize = True
        Label3.Location = New Point(24, 329)
        Label3.Name = "Label3"
        Label3.Size = New Size(189, 32)
        Label3.TabIndex = 29
        Label3.Text = "Convertitore Tiff"
        ' 
        ' Label5
        ' 
        Label5.AutoSize = True
        Label5.Location = New Point(24, 406)
        Label5.Name = "Label5"
        Label5.Size = New Size(133, 32)
        Label5.TabIndex = 31
        Label5.Text = "Ghostscript"
        ' 
        ' GhostscriptExe
        ' 
        GhostscriptExe.Location = New Point(24, 441)
        GhostscriptExe.Name = "GhostscriptExe"
        GhostscriptExe.Size = New Size(520, 39)
        GhostscriptExe.TabIndex = 30
        ' 
        ' NumericUpDown
        ' 
        NumericUpDown.Location = New Point(721, 442)
        NumericUpDown.Name = "NumericUpDown"
        NumericUpDown.Size = New Size(102, 39)
        NumericUpDown.TabIndex = 32
        ' 
        ' Label6
        ' 
        Label6.AutoSize = True
        Label6.Location = New Point(608, 406)
        Label6.Name = "Label6"
        Label6.Size = New Size(215, 32)
        Label6.TabIndex = 33
        Label6.Text = "Compressione PDF"
        ' 
        ' btnTestMagick
        ' 
        btnTestMagick.Location = New Point(560, 369)
        btnTestMagick.Name = "btnTestMagick"
        btnTestMagick.Size = New Size(75, 38)
        btnTestMagick.TabIndex = 34
        btnTestMagick.Text = "Test"
        btnTestMagick.UseVisualStyleBackColor = True
        ' 
        ' btnTestGhost
        ' 
        btnTestGhost.Location = New Point(550, 443)
        btnTestGhost.Name = "btnTestGhost"
        btnTestGhost.Size = New Size(75, 38)
        btnTestGhost.TabIndex = 35
        btnTestGhost.Text = "Test"
        btnTestGhost.UseVisualStyleBackColor = True
        ' 
        ' txtArchiveDir
        ' 
        txtArchiveDir.Location = New Point(24, 284)
        txtArchiveDir.Name = "txtArchiveDir"
        txtArchiveDir.Size = New Size(719, 39)
        txtArchiveDir.TabIndex = 37
        ' 
        ' Label7
        ' 
        Label7.AutoSize = True
        Label7.Location = New Point(24, 249)
        Label7.Name = "Label7"
        Label7.Size = New Size(207, 32)
        Label7.TabIndex = 36
        Label7.Text = "Cartella ARCHIVIO"
        ' 
        ' btnBrowseArchivio
        ' 
        btnBrowseArchivio.Location = New Point(782, 280)
        btnBrowseArchivio.Name = "btnBrowseArchivio"
        btnBrowseArchivio.Size = New Size(137, 47)
        btnBrowseArchivio.TabIndex = 38
        btnBrowseArchivio.Text = "Button1"
        btnBrowseArchivio.UseVisualStyleBackColor = True
        ' 
        ' FrmSettings
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1119, 971)
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
End Class
