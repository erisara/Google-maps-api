<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form2
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
        Me.Manage = New System.Windows.Forms.Button()
        Me.MyApp_button = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'Manage
        '
        Me.Manage.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, CType((System.Drawing.FontStyle.Bold Or System.Drawing.FontStyle.Italic), System.Drawing.FontStyle), System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.Manage.Location = New System.Drawing.Point(12, 14)
        Me.Manage.Name = "Manage"
        Me.Manage.Size = New System.Drawing.Size(150, 150)
        Me.Manage.TabIndex = 0
        Me.Manage.Text = "Access Management"
        Me.Manage.UseVisualStyleBackColor = True
        '
        'MyApp_button
        '
        Me.MyApp_button.Font = New System.Drawing.Font("Microsoft Sans Serif", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(161, Byte))
        Me.MyApp_button.Location = New System.Drawing.Point(172, 14)
        Me.MyApp_button.Name = "MyApp_button"
        Me.MyApp_button.Size = New System.Drawing.Size(150, 150)
        Me.MyApp_button.TabIndex = 1
        Me.MyApp_button.Text = "MyApp"
        Me.MyApp_button.UseVisualStyleBackColor = True
        '
        'Form2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(334, 176)
        Me.Controls.Add(Me.MyApp_button)
        Me.Controls.Add(Me.Manage)
        Me.Name = "Form2"
        Me.Text = "Adminform"
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Manage As Button
    Friend WithEvents MyApp_button As Button
End Class
