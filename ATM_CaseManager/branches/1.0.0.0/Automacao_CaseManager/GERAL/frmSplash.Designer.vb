<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSplash
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmSplash))
        Me.Version = New System.Windows.Forms.Label()
        Me.Company = New System.Windows.Forms.Label()
        Me.desenvolvido = New System.Windows.Forms.Label()
        Me.Copyright = New System.Windows.Forms.Label()
        Me.ProgressBar1 = New System.Windows.Forms.ProgressBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'Version
        '
        Me.Version.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Version.BackColor = System.Drawing.Color.Transparent
        Me.Version.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Version.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.Version.Location = New System.Drawing.Point(13, 199)
        Me.Version.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Version.Name = "Version"
        Me.Version.Size = New System.Drawing.Size(227, 20)
        Me.Version.TabIndex = 2
        Me.Version.Text = "Versão"
        Me.Version.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Version.UseWaitCursor = True
        '
        'Company
        '
        Me.Company.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Company.BackColor = System.Drawing.Color.Transparent
        Me.Company.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Company.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.Company.Location = New System.Drawing.Point(13, 219)
        Me.Company.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.Company.Name = "Company"
        Me.Company.Size = New System.Drawing.Size(227, 20)
        Me.Company.TabIndex = 3
        Me.Company.Text = "Empresa"
        Me.Company.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Company.UseWaitCursor = True
        '
        'desenvolvido
        '
        Me.desenvolvido.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.desenvolvido.BackColor = System.Drawing.Color.Transparent
        Me.desenvolvido.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.desenvolvido.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.desenvolvido.Location = New System.Drawing.Point(13, 239)
        Me.desenvolvido.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.desenvolvido.Name = "desenvolvido"
        Me.desenvolvido.Size = New System.Drawing.Size(227, 20)
        Me.desenvolvido.TabIndex = 4
        Me.desenvolvido.Text = "Desenvolvido por"
        Me.desenvolvido.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.desenvolvido.UseWaitCursor = True
        '
        'Copyright
        '
        Me.Copyright.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.Copyright.BackColor = System.Drawing.Color.Transparent
        Me.Copyright.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Copyright.ForeColor = System.Drawing.Color.LightSeaGreen
        Me.Copyright.Location = New System.Drawing.Point(12, 259)
        Me.Copyright.Name = "Copyright"
        Me.Copyright.Size = New System.Drawing.Size(228, 20)
        Me.Copyright.TabIndex = 5
        Me.Copyright.Text = "Copyright"
        Me.Copyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Copyright.UseWaitCursor = True
        '
        'ProgressBar1
        '
        Me.ProgressBar1.Location = New System.Drawing.Point(8, 282)
        Me.ProgressBar1.Name = "ProgressBar1"
        Me.ProgressBar1.Size = New System.Drawing.Size(682, 19)
        Me.ProgressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous
        Me.ProgressBar1.TabIndex = 6
        Me.ProgressBar1.UseWaitCursor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Verdana", 24.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.Turquoise
        Me.Label1.Location = New System.Drawing.Point(1, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(364, 76)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "  .:: Importador ::." & Global.Microsoft.VisualBasic.ChrW(13) & Global.Microsoft.VisualBasic.ChrW(10) & ".:: Case Manager ::."
        Me.Label1.UseWaitCursor = True
        '
        'frmSplash
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Azure
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(702, 311)
        Me.ControlBox = False
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.ProgressBar1)
        Me.Controls.Add(Me.Copyright)
        Me.Controls.Add(Me.desenvolvido)
        Me.Controls.Add(Me.Company)
        Me.Controls.Add(Me.Version)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSplash"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.UseWaitCursor = True
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Version As System.Windows.Forms.Label
    Friend WithEvents Company As System.Windows.Forms.Label
    Friend WithEvents desenvolvido As System.Windows.Forms.Label
    Friend WithEvents Copyright As System.Windows.Forms.Label
    Friend WithEvents ProgressBar1 As System.Windows.Forms.ProgressBar
    Friend WithEvents Label1 As Label
    'Friend WithEvents Timer1 As System.Windows.Forms.Timer

End Class
