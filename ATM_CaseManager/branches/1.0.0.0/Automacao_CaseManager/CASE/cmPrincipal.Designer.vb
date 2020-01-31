<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class cmPrincipal
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
        Me.txtSenhaCM = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtUsuarioCM = New System.Windows.Forms.TextBox()
        Me.btnCM = New System.Windows.Forms.Button()
        Me.SuspendLayout()
        '
        'txtSenhaCM
        '
        Me.txtSenhaCM.Location = New System.Drawing.Point(103, 48)
        Me.txtSenhaCM.Name = "txtSenhaCM"
        Me.txtSenhaCM.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaCM.Size = New System.Drawing.Size(139, 20)
        Me.txtSenhaCM.TabIndex = 15
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(21, 51)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Senha CM:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.Location = New System.Drawing.Point(21, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(76, 13)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Usuário CM:"
        '
        'txtUsuarioCM
        '
        Me.txtUsuarioCM.Location = New System.Drawing.Point(103, 22)
        Me.txtUsuarioCM.Name = "txtUsuarioCM"
        Me.txtUsuarioCM.Size = New System.Drawing.Size(139, 20)
        Me.txtUsuarioCM.TabIndex = 12
        '
        'btnCM
        '
        Me.btnCM.Location = New System.Drawing.Point(103, 74)
        Me.btnCM.Name = "btnCM"
        Me.btnCM.Size = New System.Drawing.Size(139, 23)
        Me.btnCM.TabIndex = 11
        Me.btnCM.Text = "Case Manager"
        Me.btnCM.UseVisualStyleBackColor = True
        '
        'cmPrincipal
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(271, 303)
        Me.Controls.Add(Me.txtSenhaCM)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtUsuarioCM)
        Me.Controls.Add(Me.btnCM)
        Me.Name = "cmPrincipal"
        Me.Text = "::: ATM CM :::"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtSenhaCM As MaskedTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label2 As Label
    Friend WithEvents txtUsuarioCM As TextBox
    Friend WithEvents btnCM As Button
End Class
