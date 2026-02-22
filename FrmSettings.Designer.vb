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
        txtLog.Location = New Point(24, 272)
        txtLog.Multiline = True
        txtLog.Name = "txtLog"
        txtLog.ScrollBars = ScrollBars.Vertical
        txtLog.Size = New Size(895, 381)
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
        btnOk.Location = New Point(975, 63)
        btnOk.Name = "btnOk"
        btnOk.Size = New Size(75, 45)
        btnOk.TabIndex = 26
        btnOk.Text = "Ok"
        btnOk.UseVisualStyleBackColor = True
        ' 
        ' btnClose
        ' 
        btnClose.Location = New Point(969, 144)
        btnClose.Name = "btnClose"
        btnClose.Size = New Size(111, 41)
        btnClose.TabIndex = 27
        btnClose.Text = "Chiudi"
        btnClose.UseVisualStyleBackColor = True
        ' 
        ' FrmSettings
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        ClientSize = New Size(1119, 665)
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
End Class
