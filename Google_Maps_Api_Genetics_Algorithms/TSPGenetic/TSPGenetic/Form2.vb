Public Class Form2

    Private Sub MyApp_Click(sender As Object, e As EventArgs) Handles MyApp_button.Click

        Me.Hide()
        MyApp.Show()

    End Sub

    Private Sub Manage_Click(sender As Object, e As EventArgs) Handles Manage.Click

        Me.Hide()
        Admin.Show()

    End Sub

End Class