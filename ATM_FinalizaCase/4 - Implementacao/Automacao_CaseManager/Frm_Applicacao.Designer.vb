<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Frm_Applicacao
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Frm_Applicacao))
        Me.BtnIniciar = New System.Windows.Forms.Button()
        Me.BarGeral = New System.Windows.Forms.ProgressBar()
        Me.ListComandos = New System.Windows.Forms.ListBox()
        Me.BtnFim = New System.Windows.Forms.Button()
        Me.TmpTick = New System.Windows.Forms.Timer(Me.components)
        Me.PictureBox1 = New System.Windows.Forms.PictureBox()
        Me.NotifyIcon1 = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.lbNotificacao = New System.Windows.Forms.Label()
        Me.lbLOG = New System.Windows.Forms.Label()
        Me.lbDtBuscada = New System.Windows.Forms.Label()
        Me.lbIdioma = New System.Windows.Forms.Label()
        Me.lbEnr = New System.Windows.Forms.Label()
        Me.lbQuantEncerrados = New System.Windows.Forms.Label()
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'BtnIniciar
        '
        Me.BtnIniciar.Enabled = False
        Me.BtnIniciar.Location = New System.Drawing.Point(62, 29)
        Me.BtnIniciar.Name = "BtnIniciar"
        Me.BtnIniciar.Size = New System.Drawing.Size(75, 23)
        Me.BtnIniciar.TabIndex = 0
        Me.BtnIniciar.Text = "Iniciar"
        Me.BtnIniciar.UseVisualStyleBackColor = True
        '
        'BarGeral
        '
        Me.BarGeral.Location = New System.Drawing.Point(143, 29)
        Me.BarGeral.Name = "BarGeral"
        Me.BarGeral.Size = New System.Drawing.Size(495, 14)
        Me.BarGeral.TabIndex = 1
        '
        'ListComandos
        '
        Me.ListComandos.FormattingEnabled = True
        Me.ListComandos.Location = New System.Drawing.Point(143, 49)
        Me.ListComandos.Name = "ListComandos"
        Me.ListComandos.Size = New System.Drawing.Size(495, 56)
        Me.ListComandos.TabIndex = 3
        '
        'BtnFim
        '
        Me.BtnFim.Location = New System.Drawing.Point(62, 58)
        Me.BtnFim.Name = "BtnFim"
        Me.BtnFim.Size = New System.Drawing.Size(75, 23)
        Me.BtnFim.TabIndex = 4
        Me.BtnFim.Text = "Encerrar"
        Me.BtnFim.UseVisualStyleBackColor = True
        '
        'TmpTick
        '
        Me.TmpTick.Enabled = True
        Me.TmpTick.Interval = 6000
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.Image = Global.Automacao_CaseManager.My.Resources.Resources.industrye1474201555531
        Me.PictureBox1.Location = New System.Drawing.Point(4, 30)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(58, 50)
        Me.PictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.PictureBox1.TabIndex = 5
        Me.PictureBox1.TabStop = False
        '
        'NotifyIcon1
        '
        Me.NotifyIcon1.Icon = CType(resources.GetObject("NotifyIcon1.Icon"), System.Drawing.Icon)
        Me.NotifyIcon1.Text = "Parar Gestor Manager"
        Me.NotifyIcon1.Visible = True
        '
        'lbNotificacao
        '
        Me.lbNotificacao.AutoSize = True
        Me.lbNotificacao.Font = New System.Drawing.Font("Microsoft Sans Serif", 7.2!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbNotificacao.Location = New System.Drawing.Point(3, 134)
        Me.lbNotificacao.Name = "lbNotificacao"
        Me.lbNotificacao.Size = New System.Drawing.Size(0, 13)
        Me.lbNotificacao.TabIndex = 6
        '
        'lbLOG
        '
        Me.lbLOG.AutoSize = True
        Me.lbLOG.Location = New System.Drawing.Point(2, 115)
        Me.lbLOG.Name = "lbLOG"
        Me.lbLOG.Size = New System.Drawing.Size(0, 13)
        Me.lbLOG.TabIndex = 7
        '
        'lbDtBuscada
        '
        Me.lbDtBuscada.AutoSize = True
        Me.lbDtBuscada.Location = New System.Drawing.Point(8, 88)
        Me.lbDtBuscada.Name = "lbDtBuscada"
        Me.lbDtBuscada.Size = New System.Drawing.Size(0, 13)
        Me.lbDtBuscada.TabIndex = 8
        '
        'lbIdioma
        '
        Me.lbIdioma.AutoSize = True
        Me.lbIdioma.Location = New System.Drawing.Point(2, 147)
        Me.lbIdioma.Name = "lbIdioma"
        Me.lbIdioma.Size = New System.Drawing.Size(38, 13)
        Me.lbIdioma.TabIndex = 9
        Me.lbIdioma.Text = "Idioma"
        '
        'lbEnr
        '
        Me.lbEnr.AutoSize = True
        Me.lbEnr.Location = New System.Drawing.Point(140, 9)
        Me.lbEnr.Name = "lbEnr"
        Me.lbEnr.Size = New System.Drawing.Size(89, 13)
        Me.lbEnr.TabIndex = 10
        Me.lbEnr.Text = "Encerrados Hoje:"
        '
        'lbQuantEncerrados
        '
        Me.lbQuantEncerrados.AutoSize = True
        Me.lbQuantEncerrados.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbQuantEncerrados.Location = New System.Drawing.Point(235, 4)
        Me.lbQuantEncerrados.Name = "lbQuantEncerrados"
        Me.lbQuantEncerrados.Size = New System.Drawing.Size(0, 20)
        Me.lbQuantEncerrados.TabIndex = 11
        '
        'Frm_Applicacao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.ClientSize = New System.Drawing.Size(650, 164)
        Me.Controls.Add(Me.lbQuantEncerrados)
        Me.Controls.Add(Me.lbEnr)
        Me.Controls.Add(Me.lbIdioma)
        Me.Controls.Add(Me.lbDtBuscada)
        Me.Controls.Add(Me.lbLOG)
        Me.Controls.Add(Me.lbNotificacao)
        Me.Controls.Add(Me.PictureBox1)
        Me.Controls.Add(Me.BtnFim)
        Me.Controls.Add(Me.ListComandos)
        Me.Controls.Add(Me.BtnIniciar)
        Me.Controls.Add(Me.BarGeral)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimizeBox = False
        Me.Name = "Frm_Applicacao"
        Me.Text = "Gestor Manager 1.2.2"
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents BtnIniciar As Button
    Friend WithEvents BarGeral As ProgressBar
    Friend WithEvents ListComandos As ListBox
    Friend WithEvents BtnFim As Button
    Friend WithEvents TmpTick As Timer
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents NotifyIcon1 As NotifyIcon
    Friend WithEvents lbNotificacao As Label
    Friend WithEvents lbLOG As Label
    Friend WithEvents lbDtBuscada As Label
    Friend WithEvents lbIdioma As Label
    Friend WithEvents lbEnr As Label
    Friend WithEvents lbQuantEncerrados As Label
End Class
