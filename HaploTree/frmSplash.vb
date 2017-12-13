Public Class frmSplash

    Private Sub frmSplash_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.picLogo.Image = imglstSplash.Images(0)
    End Sub

    Private Sub btnClose_Click(sender As Object, e As EventArgs) Handles btnClose.Click
        Dim frmMain As New frmMain
        frmMain.Show()
        Me.Visible = False

    End Sub
End Class