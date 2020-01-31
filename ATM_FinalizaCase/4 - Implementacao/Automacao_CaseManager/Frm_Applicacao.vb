Imports System.Diagnostics
Imports System.ComponentModel
Imports White.Core.UIItems.WindowItems
Imports System.Configuration
Imports System.Threading
Imports System.Text
Imports System.IO

Public Class Frm_Applicacao
    Public JanelaInicialCliente As White.Core.UIItems.WindowItems.Window
    Public listaMatrix As List(Of ClasseCasos)
    Private _MandeiParar As Boolean
    Public Property MandeiParar() As Boolean
        Get
            Return _MandeiParar
        End Get
        Set(ByVal value As Boolean)
            _MandeiParar = value
        End Set
    End Property

    Private sqlAdicional As String
    Public Property _sqlAdicional() As String
        Get
            Return sqlAdicional
        End Get
        Set(ByVal value As String)
            sqlAdicional = value
        End Set
    End Property
    Private _ingles As Boolean
    Public Property ingles() As Boolean
        Get
            Return _ingles
        End Get
        Set(ByVal value As Boolean)
            _ingles = value
        End Set
    End Property
#Region "Botões"
    Private Sub BtnInicio_Click(sender As Object, e As EventArgs) Handles BtnIniciar.Click

        PictureBox1.Image = My.Resources.Ligado
        BtnIniciar.Text = "Iniciado"
        Application.DoEvents()
        '  meupro.iniciar()
        'trd = New Thread(AddressOf meupro.iniciar)

        'trd.IsBackground = True
        'trd.Start()

        Try
            Dim meupro As New Funcoes
            sqlAdicional = ConfigurationManager.AppSettings("sqlAdicional")
            MandeiParar = False
            meupro.iniciar()

        Catch ex As Exception
            ListComandos.Items.Add("Erro ao iniciar")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            Application.DoEvents()
            'enviando.getMensageSms("Alerta Case Manager: Erro ao iniciar Case Manager: OFF")
            ListComandos.Items.Add("Envio SMS ""Alerta Finaliza Case 1.2.2: Servico parado.""")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
        End Try
    End Sub

    Private Sub BtnFim_Click(sender As Object, e As EventArgs) Handles BtnFim.Click
        Dim enviando As New EnvioM
        Try
            Dim locallog As String
            locallog = ConfigurationManager.AppSettings("LocalLog")
            TmpTick.Stop()
            Application.DoEvents()
            sqlAdicional = ""
            MandeiParar = True
            BtnIniciar.Enabled = True
            BtnIniciar.Text = "Iniciar"
            PictureBox1.Image = My.Resources.Desligado
            Application.DoEvents()
            'enviando.getMensageSms("Alerta Case Manager: Servico parado manualmente")
            ListComandos.Items.Add("Envio SMS ""Alerta Finaliza Case 1.2.2: Servico parado.""")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            Funcoes.geraTxtExcecao("Alerta Case Manager: Servico parado manualmente ")
            BtnFim.Text = "Aguarde..."
            BtnFim.Enabled = False
            Application.Exit()
        Catch ex As Exception
            ListComandos.Items.Add("Erro ao parar atividade")
            'enviando.getMensageSms("Alerta Case Manager: Erro ao parar atividade")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
        End Try

    End Sub
#End Region

    Private Sub TMPTick_start(sender As Object, e As EventArgs) Handles TmpTick.Tick
        'Dim enviando As New EnvioM
        Dim cont As Integer = 0
        Try
            TmpTick.Stop()
            Application.DoEvents()
            If ListComandos.Items.Count > 20000 Then
                ListComandos.Items.Clear()
            End If
            PictureBox1.Image = My.Resources.Ligado
            BtnIniciar.Text = "Iniciado"
            Me.lbQuantEncerrados.Text = BLLClasseCasos.RetornarSQL("select count(*) as quant from MX_bMaTRiX where DataFinalizacaoCase between '" & Now.ToString("yyyy-MM-dd") & " 00:00:00' and '" & Now.ToString("yyyy-MM-dd") & "  23:59:00' ", 1).FirstOrDefault.iCodMatrix

            Application.DoEvents()
            listaMatrix = New List(Of ClasseCasos)
            'oficial
            listaMatrix = BLLClasseCasos.RetornarSQL("Select top 20 c.finalizar_Case_Especifico, c.valorDespesa,c.id,c.cartao,c.dataRegistro,c.estabelecimentoCodigo,f.Finalizacaocase,sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0 and not f.finalizacaoCase is null and c.origemregistro = 'case' " & sqlAdicional & "  order by c.horafinal asc", 0)
            'para testes
            'listaMatrix = BLLClasseCasos.RetornarSQL("Select top 20 c.finalizar_Case_Especifico, c.valorDespesa,c.id,c.cartao,c.dataRegistro,c.estabelecimentoCodigo,f.Finalizacaocase,sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.id = 1159834 order by c.horafinal asc ")


            Application.DoEvents()
            If IsNothing(JanelaInicialCliente) Then
reabrindo:      Dim meupro As New Funcoes
                Dim uma As New Funcoes
                Application.DoEvents()
                BarGeral.Value = 5
                ListComandos.Items.Add("O Case Manager está fechado!")
                'enviando.getMensageSms("Alerta Case Manager: Aplicacao fechada, Reabrindo...")
                ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                Application.DoEvents()
                ListComandos.Items.Add("Abrindo novamente...")
                ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                If MandeiParar = True Then
                    TmpTick.Stop()
                Else
                    If meupro.iniciar() = False Then
                        Exit Sub
                    End If
                End If
            Else
                Application.DoEvents()
                BarGeral.Value = 5
                Dim meupro As New Funcoes
                Dim janelaAnalise As White.Core.UIItems.Label
                janelaAnalise = JanelaInicialCliente.Get(Of White.Core.UIItems.Label)("Label9")
                If IsNothing(janelaAnalise) Then
                    JanelaInicialCliente = Funcoes.PegaJanela("Case Manager Cliente")
                    ListComandos.Items.Add("Abrindo Pesquisa em tela de Cliente")
                    ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                    Application.DoEvents()
                    If Funcoes.AbrirPesquisar(JanelaInicialCliente) = True Then
                        'enviando.getMensageSms("Alerta Case Manager: Tela de pesquisa Reaberta")
                        ListComandos.Items.Add("Tela de pesquisa Reaberta")
                    Else
                        TmpTick.Stop()
                        Application.DoEvents()
                        'enviando.getMensageSms("Alerta Case Manager: Automator parado, nao foi possivel abrir pesquisa.")
                        ListComandos.Items.Add("Automator parado, nao foi possivel abrir pesquisa.")
                        Application.DoEvents()
                        Exit Sub
                    End If
                    If IsNothing(JanelaInicialCliente) = True Then
                        GoTo reabrindo
                    End If
                End If
                Dim CartaoNaoLocalizado As Integer
                For Each item In listaMatrix
                    colocarFormNaFrente()
                    BarGeral.Value = 6
                    Dim contaErroCartao As Integer = 0
                    Threading.Thread.Sleep(2000)
                    CartaoNaoLocalizado = 0
BuscaNovamente:     If Funcoes.PesquisarCartao(JanelaInicialCliente, item) = False Then
                        contaErroCartao = contaErroCartao + 1

                        Dim BtnOK As White.Core.UIItems.Button
                        BtnOK = JanelaInicialCliente.Get(Of White.Core.UIItems.Button)("2") 'cmd_salvar
                        If IsNothing(BtnOK) = False Then
                            BtnOK.Click()
                        End If
                        If contaErroCartao > 3 Then
                            ListComandos.Items.Add("Erro ao buscar cartão")
                            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                            'enviando.getMensageSms("Alerta Case Manager: Erro grave ao buscar cartão")
                            ListComandos.Items.Add("Aplicação parada devido erro ao buscar cartão")
                            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                            'enviando.getMensageSms("Aplicação parada devido erro ao buscar cartao")
                            GoTo reabrindo
                            TmpTick.Stop()
                            Application.DoEvents()
                            Throw New Exception("Erro ao buscar cartão")
                        End If
                        GoTo BuscaNovamente
                    End If

                    Dim BtnFechar As White.Core.UIItems.Panel
                    If MandeiParar = True Then
                        TmpTick.Stop()
                        Exit Sub
                    End If
                    BtnFechar = JanelaInicialCliente.Get(Of White.Core.UIItems.Panel)("PictureBox2")

                    If IsNothing(BtnFechar) = False Then
                        If BtnFechar.Name = "O Cartão | C.P.F. | Caso |  informado não foi encontrado." Then
                            Dim saindo As White.Core.UIItems.Button
                            CartaoNaoLocalizado = CartaoNaoLocalizado + 1
                            saindo = JanelaInicialCliente.Get(Of White.Core.UIItems.Button)("cmd_sair")
                            saindo.Click()
                            Dim obscase As String
                            ListComandos.Items.Add("Cartão (" & item.sNroDoCartao.Substring(0, 10) & "XXXXX" & item.sNroDoCartao.Substring(14, 5) & ") não localizado")
                            ListComandos.Items.Add("2 tentativa de localizar cartão...")
                            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                            If CartaoNaoLocalizado > 3 Then
                                obscase = "Cartão (" & item.sNroDoCartao.Substring(0, 10) & "XXXXX" & item.sNroDoCartao.Substring(14, 5) & ") não localizado"
                                ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                                item.ierroFinalizaCase = 1
                                item.iFinalizarCase = 0
                                item.Tratado_Automacao_CASE = 0
                                item.finalizaCaseOBS = obscase
                                item.sDataFinalizacaoCase = Now
                                BLLClasseCasos.Atualizar(item)
                                GoTo netx
                            End If
                            GoTo BuscaNovamente
                        End If
                        If BtnFechar.Name = "O cartão informado já está sendo analisado por outro analista." Then
                            Dim saindo As White.Core.UIItems.Button
                            saindo = JanelaInicialCliente.Get(Of White.Core.UIItems.Button)("cmd_sair")
                            saindo.Click()
                            Dim obscase As String
                            ListComandos.Items.Add("Cartão (" & item.sNroDoCartao.Substring(0, 10) & "XXXXX" & item.sNroDoCartao.Substring(14, 5) & ") está sendo analisado por outro analista")
                            obscase = "Cartão (" & item.sNroDoCartao.Substring(0, 10) & "XXXXX" & item.sNroDoCartao.Substring(14, 5) & ") está sendo analisado por outro analista"
                            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
                            item.ierroFinalizaCase = 0
                            item.iFinalizarCase = 1
                            item.Tratado_Automacao_CASE = 0
                            item.finalizaCaseOBS = obscase
                            item.sDataFinalizacaoCase = "01/01/2001 00:00:01"
                            BLLClasseCasos.Atualizar(item)
                            GoTo netx
                        End If
                    End If
                    cont = cont + 1
                    If MandeiParar = True Then
                        Exit Sub
                    End If
                    Me.lbDtBuscada.Text = ""
                    If ingles = True Then
                        Me.lbDtBuscada.Text = CDate(item.sDataTransmissao).ToString("dd/MM/yyyy HH:mm:ss")
                    Else
                        Me.lbDtBuscada.Text = CDate(item.sDataTransmissao.Substring(6, 4) & "/" & item.sDataTransmissao.Substring(3, 2) & "/" & item.sDataTransmissao.Substring(0, 2) & item.sDataTransmissao.Substring(10, 9)).ToString("dd/MM/yyyy HH:mm:ss")
                    End If

                    Me.lbDtBuscada.BackColor = Color.Transparent
                    Application.DoEvents()
                    If Funcoes.SelecionarPendencias(JanelaInicialCliente, item, listaMatrix.Count, cont) = False Then
                        Exit For
                    End If
                    If MandeiParar = True Then
                        TmpTick.Stop()
                        Application.DoEvents()
                        Exit Sub
                    End If
                    CartaoNaoLocalizado = 0
netx:           Next
                BarGeral.Value = 7
            End If
            BarGeral.Value = 8
            Application.DoEvents()
            BarGeral.Value = 9
            Application.DoEvents()
            BarGeral.Value = 10
            'ListComandos.Items.Add("Concluído " & Now)
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            Application.DoEvents()
            listaMatrix = New List(Of ClasseCasos)
            listaMatrix = BLLClasseCasos.RetornarSQL("Select top 20 c.finalizar_Case_Especifico, c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE  FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0 and not f.finalizacaoCase is null and c.origemregistro = 'case'  " & sqlAdicional & "  order by c.horafinal asc", 0)
            'para testes
            'listaMatrix = BLLClasseCasos.RetornarSQL("Select top 20 c.finalizar_Case_Especifico, c.valorDespesa,c.id,c.cartao,c.dataRegistro,c.estabelecimentoCodigo,f.Finalizacaocase,sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase, c.Tratado_Automacao_CASE FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.id = 1159834 order by c.horafinal asc ")



            Application.DoEvents()
            ListComandos.Items.Add("Lista MATRIX retornou " & listaMatrix.Count & " casos a serem encerrados." & Now)
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            TmpTick.Start()
            PictureBox1.Image = My.Resources.Ligado
            Application.DoEvents()
        Catch ex As Exception
            ListComandos.Items.Add("Erro ao executar consulta")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            'enviando.getMensageSms("Alerta Case Manager: Erro ao executar consulta")
            TmpTick.Stop()
            Funcoes.geraTxtExcecao(ex.Message)
            PictureBox1.Image = My.Resources.Desligado
            Application.DoEvents()
        End Try
    End Sub
    Public Sub colocarFormNaFrente()

        Me.TopMost = True

        Me.TopMost = False

    End Sub
    'Private trd As Thread
    Private Sub Frm_Applicacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.DesktopLocation = New Point(630, 720)
        Dim enviando As New EnvioM
        Dim meupro As New Funcoes
        Try
            If Thread.CurrentThread.CurrentCulture.Name = "pt-BR" Then
                '    Me.ForeColor = System.Drawing.Color.DarkBlue
                lbIdioma.ForeColor = Color.DarkBlue
                lbIdioma.Text = UCase(Thread.CurrentThread.CurrentCulture.Name.ToString)
                ingles = False
            Else
                lbIdioma.ForeColor = Color.Red
                'Me.ForeColor = System.Drawing.Color.Red
                lbIdioma.Text = UCase(Thread.CurrentThread.CurrentCulture.Name.ToString)
                ingles = True
                '    MsgBox("Atenção, sua máquina está com a região / idioma diferente do qual o Gestor Manager foi desenvolvido, favor contatar o suporte para configurá-la para pt-BR", MsgBoxStyle.Critical, "Região / Idioma")
            End If

            sqlAdicional = ConfigurationManager.AppSettings("sqlAdicional")
            lbLOG.Text = "Log salvo em: " & ConfigurationManager.AppSettings("LocalLog") & "LOGGestor AAAA-MM-DD HH.MM.SS.txt"
            If ConfigurationManager.AppSettings("LocalLog") = "" Then
                lbLOG.Text = "Log salvo em: " & "C:\Users\" & UCase(Environ("USERNAME")) & "\Downloads\"
                Application.DoEvents()
            End If

            If sqlAdicional = "" Then
                lbNotificacao.Visible = False
            Else
                lbNotificacao.Visible = True
                lbNotificacao.Text = "Parametro de SQL: " & sqlAdicional
            End If

            ' TmpTick.Enabled = True
            'meupro.iniciar()
        Catch ex As Exception
            ListComandos.Items.Add("Erro ao iniciar")
            enviando.getMensageSms("Alerta Finaliza Case 1.2.2: Erro ao iniciar")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
        End Try

        'trd = New Thread(AddressOf meupro.iniciar)

        'trd.IsBackground = True

        'trd.Start()
    End Sub

    Private Sub Frm_Applicacao_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Dim enviando As New EnvioM
        Try
            Dim locallog As String
            locallog = ConfigurationManager.AppSettings("LocalLog")
            TmpTick.Stop()
            Application.DoEvents()
            sqlAdicional = ""
            MandeiParar = True
            BtnIniciar.Enabled = True
            BtnIniciar.Text = "Iniciar"
            PictureBox1.Image = My.Resources.Desligado
            Application.DoEvents()
            'enviando.getMensageSms("Alerta Case Manager: Servico parado manualmente")
            ListComandos.Items.Add("Envio SMS ""Alerta Finaliza Case 1.2.2: Servico parado.""")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
            Funcoes.geraTxtExcecao("Alerta Case Manager: Servico parado manualmente ")
            BtnFim.Text = "Aguarde..."
            BtnFim.Enabled = False
            Application.Exit()
        Catch ex As Exception
            ListComandos.Items.Add("Erro ao parar atividade")
            'enviando.getMensageSms("Alerta Case Manager: Erro ao parar atividade")
            ListComandos.SetSelected(ListComandos.Items.Count - 1, True)
        End Try
    End Sub

    Private Sub NotifyIcon1_DoubleClick(sender As Object, e As EventArgs) Handles NotifyIcon1.DoubleClick
        Me.MandeiParar = True
        Funcoes.geraTxtExcecao("Aplicação encerrada.")
        Application.Exit()
    End Sub

    Private Sub Frm_Applicacao_FormClosed(sender As Object, e As FormClosedEventArgs) Handles MyBase.FormClosed
        Dim enviando As New EnvioM
        enviando.getMensageSms("Alerta Finaliza Case 1.2.2: Servico parado.")
    End Sub
End Class


