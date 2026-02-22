Public Class FrmSettings
    Public Property InDir As String
    Public Property WorkDir As String
    Public Property OutDir As String
    Public Property SubDir As String
    Public Property LogText As String

    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtInDir.Text = InDir
        txtWorkDir.Text = WorkDir
        txtOutDir.Text = OutDir
        txtLog.Text = LogText
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        InDir = txtInDir.Text
        WorkDir = txtWorkDir.Text
        OutDir = txtOutDir.Text
        LogText = txtLog.Text

        My.Settings.InDir = txtInDir.Text
        My.Settings.WorkDir = txtWorkDir.Text
        My.Settings.OutDir = txtOutDir.Text
        My.Settings.Save()

        Me.DialogResult = DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Me.DialogResult = DialogResult.Cancel
        Me.Close()
    End Sub
    Private Sub btnBrowseIn_Click(sender As Object, e As EventArgs) Handles btnBrowseIn.Click
        Using f As New FolderBrowserDialog()
            If f.ShowDialog() = DialogResult.OK Then txtInDir.Text = f.SelectedPath
        End Using
    End Sub

    Private Sub btnBrowseWork_Click(sender As Object, e As EventArgs) Handles btnBrowseWork.Click
        Using f As New FolderBrowserDialog()
            If f.ShowDialog() = DialogResult.OK Then txtWorkDir.Text = f.SelectedPath
        End Using
    End Sub

    Private Sub btnBrowseOut_Click(sender As Object, e As EventArgs) Handles btnBrowseOut.Click
        Using f As New FolderBrowserDialog()
            If f.ShowDialog() = DialogResult.OK Then txtOutDir.Text = f.SelectedPath
        End Using
    End Sub
End Class