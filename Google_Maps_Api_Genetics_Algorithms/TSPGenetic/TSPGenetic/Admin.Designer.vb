<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Admin
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.Save = New System.Windows.Forms.Button()
        Me.Update = New System.Windows.Forms.Button()
        Me.Delete = New System.Windows.Forms.Button()
        Me.SignOut = New System.Windows.Forms.Button()
        Me.Id = New System.Windows.Forms.Label()
        Me.Username = New System.Windows.Forms.Label()
        Me.Password = New System.Windows.Forms.Label()
        Me.TextId = New System.Windows.Forms.TextBox()
        Me.TextUsername = New System.Windows.Forms.TextBox()
        Me.TextPassword = New System.Windows.Forms.TextBox()
        Me.ComboBox1 = New System.Windows.Forms.ComboBox()
        Me.ListBox1 = New System.Windows.Forms.ListBox()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.Load_table = New System.Windows.Forms.Button()
        Me.Search_txt = New System.Windows.Forms.TextBox()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Save
        '
        Me.Save.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Save.Location = New System.Drawing.Point(32, 223)
        Me.Save.Name = "Save"
        Me.Save.Size = New System.Drawing.Size(100, 50)
        Me.Save.TabIndex = 0
        Me.Save.Text = "Save"
        Me.Save.UseVisualStyleBackColor = True
        '
        'Update
        '
        Me.Update.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Update.Location = New System.Drawing.Point(138, 223)
        Me.Update.Name = "Update"
        Me.Update.Size = New System.Drawing.Size(100, 50)
        Me.Update.TabIndex = 1
        Me.Update.Text = "Update"
        Me.Update.UseVisualStyleBackColor = True
        '
        'Delete
        '
        Me.Delete.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Delete.Location = New System.Drawing.Point(244, 223)
        Me.Delete.Name = "Delete"
        Me.Delete.Size = New System.Drawing.Size(100, 50)
        Me.Delete.TabIndex = 2
        Me.Delete.Text = "Delete"
        Me.Delete.UseVisualStyleBackColor = True
        '
        'SignOut
        '
        Me.SignOut.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.SignOut.Location = New System.Drawing.Point(12, 12)
        Me.SignOut.Name = "SignOut"
        Me.SignOut.Size = New System.Drawing.Size(100, 50)
        Me.SignOut.TabIndex = 3
        Me.SignOut.Text = "SignOut"
        Me.SignOut.UseVisualStyleBackColor = True
        '
        'Id
        '
        Me.Id.AutoSize = True
        Me.Id.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Id.Location = New System.Drawing.Point(28, 95)
        Me.Id.Name = "Id"
        Me.Id.Size = New System.Drawing.Size(27, 24)
        Me.Id.TabIndex = 4
        Me.Id.Text = "Id"
        '
        'Username
        '
        Me.Username.AutoSize = True
        Me.Username.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Username.Location = New System.Drawing.Point(28, 132)
        Me.Username.Name = "Username"
        Me.Username.Size = New System.Drawing.Size(105, 24)
        Me.Username.TabIndex = 5
        Me.Username.Text = "Username"
        '
        'Password
        '
        Me.Password.AutoSize = True
        Me.Password.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Password.Location = New System.Drawing.Point(28, 167)
        Me.Password.Name = "Password"
        Me.Password.Size = New System.Drawing.Size(100, 24)
        Me.Password.TabIndex = 6
        Me.Password.Text = "Password"
        '
        'TextId
        '
        Me.TextId.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.TextId.Location = New System.Drawing.Point(148, 90)
        Me.TextId.Name = "TextId"
        Me.TextId.Size = New System.Drawing.Size(196, 29)
        Me.TextId.TabIndex = 7
        '
        'TextUsername
        '
        Me.TextUsername.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.TextUsername.Location = New System.Drawing.Point(148, 129)
        Me.TextUsername.Name = "TextUsername"
        Me.TextUsername.Size = New System.Drawing.Size(196, 29)
        Me.TextUsername.TabIndex = 8
        '
        'TextPassword
        '
        Me.TextPassword.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.TextPassword.Location = New System.Drawing.Point(148, 164)
        Me.TextPassword.Name = "TextPassword"
        Me.TextPassword.Size = New System.Drawing.Size(196, 29)
        Me.TextPassword.TabIndex = 9
        '
        'ComboBox1
        '
        Me.ComboBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 15.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.ComboBox1.FormattingEnabled = True
        Me.ComboBox1.Location = New System.Drawing.Point(162, 12)
        Me.ComboBox1.Name = "ComboBox1"
        Me.ComboBox1.Size = New System.Drawing.Size(182, 33)
        Me.ComboBox1.TabIndex = 10
        '
        'ListBox1
        '
        Me.ListBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.ListBox1.FormattingEnabled = True
        Me.ListBox1.ItemHeight = 24
        Me.ListBox1.Location = New System.Drawing.Point(377, 5)
        Me.ListBox1.Name = "ListBox1"
        Me.ListBox1.Size = New System.Drawing.Size(88, 28)
        Me.ListBox1.TabIndex = 11
        '
        'DataGridView1
        '
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.DataGridView1.Location = New System.Drawing.Point(377, 39)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.Size = New System.Drawing.Size(387, 204)
        Me.DataGridView1.TabIndex = 12
        '
        'Load_table
        '
        Me.Load_table.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Load_table.Location = New System.Drawing.Point(486, 249)
        Me.Load_table.Name = "Load_table"
        Me.Load_table.Size = New System.Drawing.Size(150, 50)
        Me.Load_table.TabIndex = 13
        Me.Load_table.Text = "Load Table"
        Me.Load_table.UseVisualStyleBackColor = True
        '
        'Search_txt
        '
        Me.Search_txt.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Search_txt.Location = New System.Drawing.Point(486, 5)
        Me.Search_txt.Name = "Search_txt"
        Me.Search_txt.Size = New System.Drawing.Size(188, 29)
        Me.Search_txt.TabIndex = 14
        '
        'Admin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(776, 341)
        Me.Controls.Add(Me.Search_txt)
        Me.Controls.Add(Me.Load_table)
        Me.Controls.Add(Me.DataGridView1)
        Me.Controls.Add(Me.ListBox1)
        Me.Controls.Add(Me.ComboBox1)
        Me.Controls.Add(Me.TextPassword)
        Me.Controls.Add(Me.TextUsername)
        Me.Controls.Add(Me.TextId)
        Me.Controls.Add(Me.Password)
        Me.Controls.Add(Me.Username)
        Me.Controls.Add(Me.Id)
        Me.Controls.Add(Me.SignOut)
        Me.Controls.Add(Me.Delete)
        Me.Controls.Add(Me.Update)
        Me.Controls.Add(Me.Save)
        Me.Name = "Admin"
        Me.Text = "Admin"
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Save As Button
    Friend WithEvents Update As Button
    Friend WithEvents Delete As Button
    Friend WithEvents SignOut As Button
    Friend WithEvents Id As Label
    Friend WithEvents Username As Label
    Friend WithEvents Password As Label
    Friend WithEvents TextId As TextBox
    Friend WithEvents TextUsername As TextBox
    Friend WithEvents TextPassword As TextBox
    Friend WithEvents ComboBox1 As ComboBox
    Friend WithEvents ListBox1 As ListBox
    Friend WithEvents DataGridView1 As DataGridView
    Friend WithEvents Load_table As Button
    Friend WithEvents Search_txt As TextBox
End Class
