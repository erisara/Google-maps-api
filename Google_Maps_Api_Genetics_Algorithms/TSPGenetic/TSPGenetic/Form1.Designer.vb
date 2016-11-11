<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form1
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
        Me.Text1 = New System.Windows.Forms.TextBox()
        Me.btnStart = New System.Windows.Forms.Button()
        Me.chkCoords = New System.Windows.Forms.CheckBox()
        Me.btnInfo = New System.Windows.Forms.Button()
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Text1
        '
        Me.Text1.Location = New System.Drawing.Point(24, 634)
        Me.Text1.Multiline = True
        Me.Text1.Name = "Text1"
        Me.Text1.ScrollBars = System.Windows.Forms.ScrollBars.Both
        Me.Text1.Size = New System.Drawing.Size(735, 66)
        Me.Text1.TabIndex = 3
        '
        'btnStart
        '
        Me.btnStart.Location = New System.Drawing.Point(765, 634)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Size = New System.Drawing.Size(86, 23)
        Me.btnStart.TabIndex = 4
        Me.btnStart.Text = "Έναρξη"
        Me.btnStart.UseVisualStyleBackColor = True
        '
        'chkCoords
        '
        Me.chkCoords.AutoSize = True
        Me.chkCoords.Location = New System.Drawing.Point(858, 634)
        Me.chkCoords.Name = "chkCoords"
        Me.chkCoords.Size = New System.Drawing.Size(106, 17)
        Me.chkCoords.TabIndex = 5
        Me.chkCoords.Text = "Συντεταγμένες"
        Me.chkCoords.UseVisualStyleBackColor = True
        '
        'btnInfo
        '
        Me.btnInfo.Location = New System.Drawing.Point(858, 664)
        Me.btnInfo.Name = "btnInfo"
        Me.btnInfo.Size = New System.Drawing.Size(106, 23)
        Me.btnInfo.TabIndex = 7
        Me.btnInfo.Text = "Πληροφορίες"
        Me.btnInfo.UseVisualStyleBackColor = True
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.SystemColors.ControlDarkDark
        Me.PictureBox1.InitialImage = Nothing
        Me.PictureBox1.Location = New System.Drawing.Point(24, 12)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(984, 616)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.PictureBox1.TabIndex = 2
        Me.PictureBox1.TabStop = False
        '
        'Form1
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(1020, 729)
        Me.Controls.Add(Me.btnInfo)
        Me.Controls.Add(Me.chkCoords)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Text1)
        Me.Controls.Add(Me.PictureBox1)
        Me.DoubleBuffered = True
        Me.Name = "Form1"
        Me.Text = "Travelling Salesman Problem"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents Text1 As System.Windows.Forms.TextBox
    Friend WithEvents btnStart As System.Windows.Forms.Button
    Friend WithEvents chkCoords As System.Windows.Forms.CheckBox
    Friend WithEvents btnInfo As System.Windows.Forms.Button

End Class
