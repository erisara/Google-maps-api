Imports MySql.Data.MySqlClient

Public Class Admin

    Dim cn As New MySqlConnection
    Dim com As MySqlCommand
    Dim read As MySqlDataReader
    Dim dbDataSet As New DataTable
    Dim query As String


    Private Sub SignOut_Click(sender As Object, e As EventArgs) Handles SignOut.Click

        Me.Hide()
        Form2.Show()

    End Sub

    Private Sub Save_Click(sender As Object, e As EventArgs) Handles Save.Click

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "insert into user.login (id,username,password) values ('" & TextId.Text & "', '" & TextUsername.Text & "','" & TextPassword.Text & "')"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader
            MessageBox.Show("Data Saved")

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

        loadtable()

    End Sub

    Private Sub Update_Click(sender As Object, e As EventArgs) Handles Update.Click

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "update user.login set id='" & TextId.Text & "',username= '" & TextUsername.Text & "',password='" & TextPassword.Text & "' where id='" & TextId.Text & "'"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader
            MessageBox.Show("Data Updated")

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

        loadtable()

    End Sub

    Private Sub Delete_Click(sender As Object, e As EventArgs) Handles Delete.Click

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "Delete from user.login where id='" & TextId.Text & "'"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader
            MessageBox.Show("Data Deleted")

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

        loadtable()

    End Sub

    Private Sub Admin_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        loadtable()

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "select * from user.login"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader

            While read.Read
                Dim sName = read.GetString("username")
                ComboBox1.Items.Add(sName)
                ListBox1.Items.Add(sName)
            End While

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

    End Sub

    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "select * from user.login where username='" & ComboBox1.Text & "'"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader

            While read.Read

                TextId.Text = read.GetInt32("id")
                TextUsername.Text = read.GetString("username")
                TextPassword.Text = read.GetString("password")

            End While

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"
        Try
            cn.Open()

            query = "select * from user.login where username='" & ListBox1.Text & "'"
            com = New MySqlCommand(query, cn)
            read = com.ExecuteReader

            While read.Read

                TextId.Text = read.GetInt32("id")
                TextUsername.Text = read.GetString("username")
                TextPassword.Text = read.GetString("password")

            End While

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

    End Sub
    Private Sub loadtable()

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"

        Dim SDA As New MySqlDataAdapter
        Dim bSource As New BindingSource

        Try
            cn.Open()

            query = "select id,username,password from user.login"
            com = New MySqlCommand(query, cn)

            SDA.SelectCommand = com
            SDA.Fill(dbDataSet)
            bSource.DataSource = dbDataSet
            DataGridView1.DataSource = bSource
            SDA.Update(dbDataSet)

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

    End Sub


    Private Sub Load_table_Click(sender As Object, e As EventArgs) Handles Load_table.Click

        cn.ConnectionString = "server=localhost; username=root; password=1234; database=user;"

        Dim SDA As New MySqlDataAdapter
        Dim bSource As New BindingSource

        Try
            cn.Open()

            query = "select id,username,password from user.login"
            com = New MySqlCommand(query, cn)

            SDA.SelectCommand = com
            SDA.Fill(dbDataSet)
            bSource.DataSource = dbDataSet
            DataGridView1.DataSource = bSource
            SDA.Update(dbDataSet)

            cn.Close()

        Catch ex As MySqlException
            MessageBox.Show(ex.Message)
        Finally
            cn.Dispose()
        End Try

    End Sub

    Private Sub DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles DataGridView1.CellContentClick

        If e.RowIndex >= 0 Then

            Dim row As DataGridViewRow
            row = Me.DataGridView1.Rows(e.RowIndex)
            TextId.Text = row.Cells("id").Value.ToString
            TextUsername.Text = row.Cells("username").Value.ToString
            TextPassword.Text = row.Cells("password").Value.ToString

        End If

    End Sub

    Private Sub Search_txt_TextChanged(sender As Object, e As EventArgs) Handles Search_txt.TextChanged

        Dim DV As New DataView(dbDataSet)
        DV.RowFilter = String.Format("username Like '%{0}%'", Search_txt.Text)
        DataGridView1.DataSource = DV

    End Sub

End Class
