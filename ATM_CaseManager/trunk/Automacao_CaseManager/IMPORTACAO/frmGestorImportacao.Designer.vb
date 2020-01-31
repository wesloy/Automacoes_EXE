<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmGestorImportacao
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGestorImportacao))
        Me.controleGuias = New System.Windows.Forms.TabControl()
        Me.pgCase = New System.Windows.Forms.TabPage()
        Me.btnExibir = New System.Windows.Forms.Button()
        Me.btnAtmCaseParar = New System.Windows.Forms.Button()
        Me.txtSenhaCM = New System.Windows.Forms.MaskedTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtUsuarioCM = New System.Windows.Forms.TextBox()
        Me.btnAtmCaseIniciar = New System.Windows.Forms.Button()
        Me.txtVolumeNaoImportado = New System.Windows.Forms.TextBox()
        Me.txtVolumeImportado = New System.Windows.Forms.TextBox()
        Me.txtVolumeAnalisado = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtStatus = New System.Windows.Forms.TextBox()
        Me.ListView1 = New System.Windows.Forms.ListView()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.btnExportarLog = New System.Windows.Forms.Button()
        Me.lblDataHora = New System.Windows.Forms.Label()
        Me.listBoxProcedimentos = New System.Windows.Forms.ListBox()
        Me.lbCiclos = New System.Windows.Forms.Label()
        Me.btnAtualizarListView = New System.Windows.Forms.Button()
        Me.lbVersao = New System.Windows.Forms.Label()
        Me.controleGuias.SuspendLayout()
        Me.pgCase.SuspendLayout()
        Me.SuspendLayout()
        '
        'controleGuias
        '
        Me.controleGuias.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.controleGuias.Controls.Add(Me.pgCase)
        Me.controleGuias.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.controleGuias.Location = New System.Drawing.Point(15, 22)
        Me.controleGuias.Name = "controleGuias"
        Me.controleGuias.SelectedIndex = 0
        Me.controleGuias.Size = New System.Drawing.Size(840, 109)
        Me.controleGuias.TabIndex = 0
        '
        'pgCase
        '
        Me.pgCase.Controls.Add(Me.btnExibir)
        Me.pgCase.Controls.Add(Me.btnAtmCaseParar)
        Me.pgCase.Controls.Add(Me.txtSenhaCM)
        Me.pgCase.Controls.Add(Me.Label3)
        Me.pgCase.Controls.Add(Me.Label4)
        Me.pgCase.Controls.Add(Me.txtUsuarioCM)
        Me.pgCase.Controls.Add(Me.btnAtmCaseIniciar)
        Me.pgCase.Location = New System.Drawing.Point(4, 22)
        Me.pgCase.Name = "pgCase"
        Me.pgCase.Padding = New System.Windows.Forms.Padding(3)
        Me.pgCase.Size = New System.Drawing.Size(832, 83)
        Me.pgCase.TabIndex = 7
        Me.pgCase.Text = "Importação Automática do Case Manager"
        Me.pgCase.UseVisualStyleBackColor = True
        '
        'btnExibir
        '
        Me.btnExibir.Location = New System.Drawing.Point(269, 47)
        Me.btnExibir.Name = "btnExibir"
        Me.btnExibir.Size = New System.Drawing.Size(65, 23)
        Me.btnExibir.TabIndex = 140
        Me.btnExibir.Text = "Exibir"
        Me.btnExibir.UseVisualStyleBackColor = True
        '
        'btnAtmCaseParar
        '
        Me.btnAtmCaseParar.BackColor = System.Drawing.Color.PapayaWhip
        Me.btnAtmCaseParar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAtmCaseParar.Image = CType(resources.GetObject("btnAtmCaseParar.Image"), System.Drawing.Image)
        Me.btnAtmCaseParar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAtmCaseParar.Location = New System.Drawing.Point(672, 19)
        Me.btnAtmCaseParar.Name = "btnAtmCaseParar"
        Me.btnAtmCaseParar.Size = New System.Drawing.Size(142, 51)
        Me.btnAtmCaseParar.TabIndex = 139
        Me.btnAtmCaseParar.Text = "&Parar"
        Me.btnAtmCaseParar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAtmCaseParar.UseVisualStyleBackColor = False
        '
        'txtSenhaCM
        '
        Me.txtSenhaCM.Location = New System.Drawing.Point(99, 47)
        Me.txtSenhaCM.Name = "txtSenhaCM"
        Me.txtSenhaCM.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtSenhaCM.Size = New System.Drawing.Size(164, 20)
        Me.txtSenhaCM.TabIndex = 138
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(17, 50)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(69, 13)
        Me.Label3.TabIndex = 137
        Me.Label3.Text = "Senha CM:"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.Location = New System.Drawing.Point(17, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(76, 13)
        Me.Label4.TabIndex = 136
        Me.Label4.Text = "Usuário CM:"
        '
        'txtUsuarioCM
        '
        Me.txtUsuarioCM.Location = New System.Drawing.Point(99, 21)
        Me.txtUsuarioCM.Name = "txtUsuarioCM"
        Me.txtUsuarioCM.Size = New System.Drawing.Size(235, 20)
        Me.txtUsuarioCM.TabIndex = 135
        '
        'btnAtmCaseIniciar
        '
        Me.btnAtmCaseIniciar.BackColor = System.Drawing.Color.Honeydew
        Me.btnAtmCaseIniciar.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnAtmCaseIniciar.Image = CType(resources.GetObject("btnAtmCaseIniciar.Image"), System.Drawing.Image)
        Me.btnAtmCaseIniciar.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.btnAtmCaseIniciar.Location = New System.Drawing.Point(524, 19)
        Me.btnAtmCaseIniciar.Name = "btnAtmCaseIniciar"
        Me.btnAtmCaseIniciar.Size = New System.Drawing.Size(142, 51)
        Me.btnAtmCaseIniciar.TabIndex = 133
        Me.btnAtmCaseIniciar.Text = "&Iniciar"
        Me.btnAtmCaseIniciar.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText
        Me.btnAtmCaseIniciar.UseVisualStyleBackColor = False
        '
        'txtVolumeNaoImportado
        '
        Me.txtVolumeNaoImportado.BackColor = System.Drawing.Color.White
        Me.txtVolumeNaoImportado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVolumeNaoImportado.Location = New System.Drawing.Point(122, 198)
        Me.txtVolumeNaoImportado.Name = "txtVolumeNaoImportado"
        Me.txtVolumeNaoImportado.ReadOnly = True
        Me.txtVolumeNaoImportado.Size = New System.Drawing.Size(89, 20)
        Me.txtVolumeNaoImportado.TabIndex = 122
        Me.txtVolumeNaoImportado.Text = "0"
        Me.txtVolumeNaoImportado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtVolumeImportado
        '
        Me.txtVolumeImportado.BackColor = System.Drawing.Color.White
        Me.txtVolumeImportado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVolumeImportado.Location = New System.Drawing.Point(122, 174)
        Me.txtVolumeImportado.Name = "txtVolumeImportado"
        Me.txtVolumeImportado.ReadOnly = True
        Me.txtVolumeImportado.Size = New System.Drawing.Size(89, 20)
        Me.txtVolumeImportado.TabIndex = 119
        Me.txtVolumeImportado.Text = "0"
        Me.txtVolumeImportado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtVolumeAnalisado
        '
        Me.txtVolumeAnalisado.BackColor = System.Drawing.Color.White
        Me.txtVolumeAnalisado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtVolumeAnalisado.Location = New System.Drawing.Point(122, 150)
        Me.txtVolumeAnalisado.Name = "txtVolumeAnalisado"
        Me.txtVolumeAnalisado.ReadOnly = True
        Me.txtVolumeAnalisado.Size = New System.Drawing.Size(89, 20)
        Me.txtVolumeAnalisado.TabIndex = 117
        Me.txtVolumeAnalisado.Text = "0"
        Me.txtVolumeAnalisado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label5.Location = New System.Drawing.Point(11, 202)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(115, 21)
        Me.Label5.TabIndex = 121
        Me.Label5.Text = "# Não Importados:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label1.Location = New System.Drawing.Point(11, 176)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(115, 21)
        Me.Label1.TabIndex = 120
        Me.Label1.Text = "# Importado:"
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label2.Location = New System.Drawing.Point(11, 149)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(115, 21)
        Me.Label2.TabIndex = 118
        Me.Label2.Text = "# Analisado:"
        '
        'txtStatus
        '
        Me.txtStatus.BackColor = System.Drawing.Color.White
        Me.txtStatus.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtStatus.Enabled = False
        Me.txtStatus.Location = New System.Drawing.Point(217, 150)
        Me.txtStatus.Multiline = True
        Me.txtStatus.Name = "txtStatus"
        Me.txtStatus.ReadOnly = True
        Me.txtStatus.Size = New System.Drawing.Size(137, 69)
        Me.txtStatus.TabIndex = 123
        Me.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'ListView1
        '
        Me.ListView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.ListView1.GridLines = True
        Me.ListView1.Location = New System.Drawing.Point(14, 252)
        Me.ListView1.MultiSelect = False
        Me.ListView1.Name = "ListView1"
        Me.ListView1.Size = New System.Drawing.Size(841, 249)
        Me.ListView1.TabIndex = 124
        Me.ListView1.UseCompatibleStateImageBehavior = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.Image = CType(resources.GetObject("Label7.Image"), System.Drawing.Image)
        Me.Label7.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label7.ImeMode = System.Windows.Forms.ImeMode.NoControl
        Me.Label7.Location = New System.Drawing.Point(12, 236)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(236, 13)
        Me.Label7.TabIndex = 125
        Me.Label7.Text = "      Log de importação (últimos 30 dias):"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnExportarLog
        '
        Me.btnExportarLog.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExportarLog.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExportarLog.Location = New System.Drawing.Point(15, 507)
        Me.btnExportarLog.Name = "btnExportarLog"
        Me.btnExportarLog.Size = New System.Drawing.Size(101, 29)
        Me.btnExportarLog.TabIndex = 167
        Me.btnExportarLog.Text = "&Exportar Log"
        Me.btnExportarLog.UseVisualStyleBackColor = True
        '
        'lblDataHora
        '
        Me.lblDataHora.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblDataHora.BackColor = System.Drawing.Color.Transparent
        Me.lblDataHora.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.lblDataHora.Location = New System.Drawing.Point(506, 507)
        Me.lblDataHora.Name = "lblDataHora"
        Me.lblDataHora.Size = New System.Drawing.Size(349, 16)
        Me.lblDataHora.TabIndex = 168
        Me.lblDataHora.Text = "Hoje"
        Me.lblDataHora.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'listBoxProcedimentos
        '
        Me.listBoxProcedimentos.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.listBoxProcedimentos.BackColor = System.Drawing.Color.White
        Me.listBoxProcedimentos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.listBoxProcedimentos.FormattingEnabled = True
        Me.listBoxProcedimentos.Location = New System.Drawing.Point(360, 149)
        Me.listBoxProcedimentos.Name = "listBoxProcedimentos"
        Me.listBoxProcedimentos.Size = New System.Drawing.Size(495, 67)
        Me.listBoxProcedimentos.TabIndex = 169
        '
        'lbCiclos
        '
        Me.lbCiclos.AutoSize = True
        Me.lbCiclos.BackColor = System.Drawing.Color.Transparent
        Me.lbCiclos.Location = New System.Drawing.Point(357, 219)
        Me.lbCiclos.Name = "lbCiclos"
        Me.lbCiclos.Size = New System.Drawing.Size(89, 13)
        Me.lbCiclos.TabIndex = 170
        Me.lbCiclos.Text = "Total de Ciclos: 0"
        Me.lbCiclos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'btnAtualizarListView
        '
        Me.btnAtualizarListView.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnAtualizarListView.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAtualizarListView.Location = New System.Drawing.Point(122, 507)
        Me.btnAtualizarListView.Name = "btnAtualizarListView"
        Me.btnAtualizarListView.Size = New System.Drawing.Size(101, 29)
        Me.btnAtualizarListView.TabIndex = 171
        Me.btnAtualizarListView.Text = "&Atualizar Lista"
        Me.btnAtualizarListView.UseVisualStyleBackColor = True
        '
        'lbVersao
        '
        Me.lbVersao.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lbVersao.AutoSize = True
        Me.lbVersao.BackColor = System.Drawing.Color.Transparent
        Me.lbVersao.ForeColor = System.Drawing.SystemColors.ControlDark
        Me.lbVersao.Location = New System.Drawing.Point(769, 22)
        Me.lbVersao.Name = "lbVersao"
        Me.lbVersao.Size = New System.Drawing.Size(79, 13)
        Me.lbVersao.TabIndex = 172
        Me.lbVersao.Text = "Versão: 1.0.0.0"
        '
        'frmGestorImportacao
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(877, 555)
        Me.Controls.Add(Me.lbVersao)
        Me.Controls.Add(Me.btnAtualizarListView)
        Me.Controls.Add(Me.lbCiclos)
        Me.Controls.Add(Me.listBoxProcedimentos)
        Me.Controls.Add(Me.lblDataHora)
        Me.Controls.Add(Me.btnExportarLog)
        Me.Controls.Add(Me.ListView1)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtStatus)
        Me.Controls.Add(Me.txtVolumeNaoImportado)
        Me.Controls.Add(Me.txtVolumeImportado)
        Me.Controls.Add(Me.txtVolumeAnalisado)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.controleGuias)
        Me.DoubleBuffered = True
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmGestorImportacao"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "::: Robô de Importação"
        Me.controleGuias.ResumeLayout(False)
        Me.pgCase.ResumeLayout(False)
        Me.pgCase.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents controleGuias As System.Windows.Forms.TabControl
    Friend WithEvents txtVolumeNaoImportado As System.Windows.Forms.TextBox
    Friend WithEvents txtVolumeImportado As System.Windows.Forms.TextBox
    Friend WithEvents txtVolumeAnalisado As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtStatus As System.Windows.Forms.TextBox
    Friend WithEvents ListView1 As System.Windows.Forms.ListView
    Friend WithEvents Label7 As System.Windows.Forms.Label
    Friend WithEvents btnExportarLog As System.Windows.Forms.Button
    Friend WithEvents pgCase As TabPage
    Friend WithEvents btnAtmCaseIniciar As Button
    Friend WithEvents txtSenhaCM As MaskedTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents txtUsuarioCM As TextBox
    Friend WithEvents btnAtmCaseParar As Button
    Friend WithEvents lblDataHora As Label
    Friend WithEvents listBoxProcedimentos As ListBox
    Friend WithEvents lbCiclos As Label
    Friend WithEvents btnAtualizarListView As Button
    Friend WithEvents btnExibir As Button
    Friend WithEvents lbVersao As Label
End Class
