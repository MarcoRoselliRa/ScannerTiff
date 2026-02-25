Imports System.Diagnostics
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
        MagickExe.Text = My.Settings.MagickExe
        GhostscriptExe.Text = My.Settings.GhostscriptExe

        NumericUpDown.Minimum = 30
        NumericUpDown.Maximum = 95
        NumericUpDown.Value = My.Settings.JpegQ
    End Sub
    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        InDir = txtInDir.Text
        WorkDir = txtWorkDir.Text
        OutDir = txtOutDir.Text
        LogText = txtLog.Text

        My.Settings.ArchiveDir = txtArchiveDir.Text.Trim()
        My.Settings.MagickExe = MagickExe.Text.Trim()
        My.Settings.GhostscriptExe = GhostscriptExe.Text.Trim()
        My.Settings.JpegQ = CInt(NumericUpDown.Value)
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
        Using f As New FolderBrowserDialog
            If f.ShowDialog = DialogResult.OK Then txtOutDir.Text = f.SelectedPath
        End Using
    End Sub
    Private Sub btnTestMagick_Click(sender As Object, e As EventArgs) Handles btnTestMagick.Click
        TestExe(MagickExe.Text.Trim(), "-version", "ImageMagick")
    End Sub

    Private Sub btnTestGhost_Click(sender As Object, e As EventArgs) Handles btnTestGhost.Click
        TestExe(GhostscriptExe.Text.Trim(), "-version", "Ghostscript")
    End Sub
    Private Sub TestExe(exePath As String, args As String, friendlyName As String)
        Try
            If String.IsNullOrWhiteSpace(exePath) Then
                MessageBox.Show("Percorso non valido.")
                Return
            End If

            Dim psi As New ProcessStartInfo With {
                .FileName = exePath,
                .Arguments = args,
                .UseShellExecute = False,
                .RedirectStandardOutput = True,
                .RedirectStandardError = True,
                .CreateNoWindow = True
            }

            Using p As New Process()
                p.StartInfo = psi
                p.Start()

                Dim output = p.StandardOutput.ReadToEnd()
                Dim err = p.StandardError.ReadToEnd()

                p.WaitForExit()

                If p.ExitCode = 0 Then
                    MessageBox.Show($"{friendlyName} OK ✅" & Environment.NewLine &
                                    output.Split(Environment.NewLine)(0),
                                    "Test riuscito",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information)
                Else
                    MessageBox.Show($"{friendlyName} ERRORE ❌" & Environment.NewLine & err,
                                    "Errore",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error)
                End If
            End Using

        Catch ex As Exception
            MessageBox.Show($"{friendlyName} non trovato." & Environment.NewLine & ex.Message,
                            "Errore",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error)
        End Try
    End Sub
End Class