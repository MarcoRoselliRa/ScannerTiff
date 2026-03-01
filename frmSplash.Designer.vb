<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
    Inherits System.Windows.Forms.Form

    'Form esegue l'override del metodo Dispose per pulire l'elenco dei componenti.
    <System.Diagnostics.DebuggerNonUserCode()> _
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmSplash))
        PicLogo = New PictureBox()
        Label = New Label()
        CType(PicLogo, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PicLogo
        ' 
        PicLogo.Image = CType(resources.GetObject("PicLogo.Image"), Image)
        PicLogo.Location = New Point(-314, -224)
        PicLogo.Margin = New Padding(4, 2, 4, 2)
        PicLogo.Name = "PicLogo"
        PicLogo.Size = New Size(1369, 1118)
        PicLogo.TabIndex = 0
        PicLogo.TabStop = False
        ' 
        ' Label
        ' 
        Label.AutoSize = True
        Label.Location = New Point(994, 1015)
        Label.Margin = New Padding(6, 0, 6, 0)
        Label.Name = "Label"
        Label.Size = New Size(312, 32)
        Label.TabIndex = 1
        Label.Text = "Versione 2.0 del 01/03/2026"
        ' 
        ' FrmSplash
        ' 
        AutoScaleDimensions = New SizeF(13F, 32F)
        AutoScaleMode = AutoScaleMode.Font
        AutoSize = True
        ClientSize = New Size(1293, 1067)
        ControlBox = False
        Controls.Add(Label)
        Controls.Add(PicLogo)
        FormBorderStyle = FormBorderStyle.None
        Margin = New Padding(4, 2, 4, 2)
        Name = "FrmSplash"
        StartPosition = FormStartPosition.CenterScreen
        Text = "frmSplash"
        CType(PicLogo, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
        PerformLayout()
    End Sub

    Friend WithEvents PicLogo As PictureBox
    Friend WithEvents Label As Label
End Class
