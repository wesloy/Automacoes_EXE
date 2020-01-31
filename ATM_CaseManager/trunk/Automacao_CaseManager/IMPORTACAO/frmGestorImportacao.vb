Imports System.Threading
'Imports System.Runtime.InteropServices
Public Class frmGestorImportacao

    Private hlp As New helpers
    Private objLogImport As New clsLogImportacaoBLL
    Private atmImpCase As New atmImportacaoCASE
    Private CM As New funcoes_CM

    Private thread_1 As Thread
    Private thread_1_status As String = ""
    Private thread_1_runing As Boolean = False


#Region "Funções do Formúlário"

    Private Sub colocarFormNaFrente()
        Me.TopMost = True
        Me.TopMost = False
    End Sub

    Private Sub frmGestorImportacao_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Try
            'Validação se a máquina é a correta para se inicializar a automação
            If HOSTNAME_ATM <> hlp.hostnameLocal Then
                MsgBox("Não é possível iniciar a automação nesta máquina!", vbInformation, TITULO_ALERTA)
                hlp.registrarLOG("PADRAO: " & HOSTNAME_ATM, "TENTATIVA: " & hlp.hostnameLocal, "CM IMPORTADOR", "INICIAR EM MÁQUINA NÃO CADASTRADA")
                Me.Close()
                Exit Sub
            End If

            'scheduleRobo()
            'lblTitulo.Parent = header
            lbVersao.Text = "Versão: " & Application.ProductVersion
            Control.CheckForIllegalCrossThreadCalls = False
            hlp.LimparCampos(controleGuias)
            hlp.LimparCampos(Me)
            AtualizarListView()
            Me.lblDataHora.Text = ExibeData(Now)

            txtUsuarioCM.Text = UsuarioLogado
            txtSenhaCM.Text = SenhaUsuarioLogado '"bradesco01"

            'Alerta de usuário sem acesso ao C: e fechando aplicação
            If Not hlp.capturaIdRede() Like "A053463" And Not hlp.capturaIdRede() Like "A058572" And Not hlp.capturaIdRede() Like "A074915*" Then
                MsgBox("O usuário " & hlp.capturaIdRede() & " não possui privilégios suficientes para importação do CASE MANAGER, favor reiniciar o computador com um usuário com permissões para esta tarefa.", vbCritical, TITULO_ALERTA)
                hlp.registrarLOG(, "TENTATIVA: " & hlp.capturaIdRede, "CM IMPORTADOR", "INICIAR COM USUÁRIO SEM ACESSO AO C:")
                hlp.fecharAplicativo(False)
                Me.Close()
                Exit Sub
            End If

            ''Aviso por SMS que a aplicação foi iniciada
            'Me.txtStatus.Text = "Enviando SMS de INICIALIZAÇÃO da aplicação!"

        Catch ex As Exception
            hlp.fecharAplicativo(False)
        End Try

    End Sub

    Private Sub btnExportarLog_Click(sender As Object, e As EventArgs) Handles btnExportarLog.Click
        hlp.exportarListViewParaExcel(ListView1)
    End Sub

    Private Sub btnImportarCase_Click(sender As Object, e As EventArgs) Handles btnAtmCaseIniciar.Click

        Dim validador As Boolean = False
        Dim caminho As String = ""
        Dim sairFuncao As Boolean = False

        'Derrubar todos os processos de excel que estiverem abertos
        hlp.fecharProcesso("EXCEL")

        'Validando preenchimento das informações no fomulário
        If txtUsuarioCM.Text = "" Or IsNothing(txtUsuarioCM.Text) Then
            sairFuncao = True
        End If
        If txtSenhaCM.Text = "" Or IsNothing(txtSenhaCM.Text) Then
            sairFuncao = True
        End If
        If sairFuncao Then
            MsgBox("Favor preencher as informações de Usuário/Senha antes de iniciar o processo!", vbInformation, TITULO_ALERTA)
            Exit Sub
        End If

        If pararAplicacao Then
            stepAtual = "PARANDO A APLICAÇÃO"
            btnAtmCaseParar_Click(Nothing, Nothing)
            Exit Sub
        End If

        'Iniciar Procedimentos
        Me.txtStatus.Text = "Enviando SMS de inicialização da automação."
        'EnviaAlerta("INICIADO")
        Executar_thread_1()

    End Sub
#End Region

#Region "ListView"
    Public Sub AtualizarListView()
        objLogImport.AtualizaListViewLogImportacao()
    End Sub

    Private Sub ListView1_ColumnClick(sender As Object, e As ColumnClickEventArgs) Handles ListView1.ColumnClick
        If Me.ListView1.Sorting = SortOrder.Ascending Then
            Me.ListView1.Sorting = SortOrder.Descending
        Else
            Me.ListView1.Sorting = SortOrder.Ascending
        End If
        Me.ListView1.ListViewItemSorter = New mdlOrdenacaoListView(e.Column, Me.ListView1.Sorting)
    End Sub

    Private Sub ListView1_DoubleClick(sender As Object, e As EventArgs) Handles ListView1.DoubleClick
        Dim nomeArquivo() As String

        If Me.ListView1.SelectedItems(0).SubItems(2).Text Like "Triar" Then
            MsgBox("Para validar os e-mails triados, consulte o outlook na subpasta IMPORTADOS VB", vbInformation, TITULO_ALERTA)
            Exit Sub
        End If

        If MsgBox("Deseja abrir o arquivo de log?", vbQuestion + vbYesNo, TITULO_ALERTA) = vbYes Then
            nomeArquivo = Split(Replace(Me.ListView1.SelectedItems(0).SubItems(5).Text, ")", ""), "(") 'captura informações da primeira coluna selecionada
            Call hlp.abrirArquivo(PATH_LOG_IMPORT & nomeArquivo(1))
        End If
    End Sub
#End Region

#Region "funcoes"
    Public Function ExibeData(data As DateTime) As String
        Try

            Dim dia_semana As String = Weekday(data)
            Select Case dia_semana
                Case 1 : dia_semana = "Domingo"
                Case 2 : dia_semana = "Segunda-Feira"
                Case 3 : dia_semana = "Terça-Feira"
                Case 4 : dia_semana = "Quarta-Feira"
                Case 5 : dia_semana = "Quinta-Feira"
                Case 6 : dia_semana = "Sexta-Feira"
                Case 7 : dia_semana = "Sábado"
            End Select
            Dim mes As String = Month(data)
            Select Case mes
                Case 1 : mes = "Janeiro"
                Case 2 : mes = "Fevereiro"
                Case 3 : mes = "Março"
                Case 4 : mes = "Abril"
                Case 5 : mes = "Maio"
                Case 6 : mes = "Junho"
                Case 7 : mes = "Julho"
                Case 8 : mes = "Agosto"
                Case 9 : mes = "Setembro"
                Case 10 : mes = "Outubro"
                Case 11 : mes = "Novembro"
                Case 12 : mes = "Dezembro"
            End Select
            ExibeData = dia_semana & ", " & Microsoft.VisualBasic.Day(data) & " de " & mes & " de " & Year(data) & " " & data.ToString("HH:mm:ss")
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Function Atualizadata() As String
        Return ExibeData(Now)
    End Function

    Public Function calculaPorcentagem(valor1 As Double, valor2 As Double) As String
        Try
            If valor1 = 0 And valor2 = 0 Then
                Return " (0,00%)"
            End If
            Dim retorno As String = " (" & FormatNumber(((valor1 / valor2) * 100), 2).ToString & "%)"
            Return retorno.ToString
        Catch ex As Exception
            Return ""
        End Try
    End Function

    Public Sub deletarArquivosDeImportacoesAntigas()
        Try
            Dim deletar As Boolean = True
            Dim caminho As String = ""

            Do While deletar = True
                caminho = hlp.localizaArquivoPastaEspecifica(hlp.retornaDirPessoal, "FRM_RESUMO")
                If caminho = "" Or IsNothing(caminho) Then
                    deletar = False
                Else
                    hlp.CriarCopiarMoverDeletarAquivo(caminho, "Deletar") 'Deletando
                    deletar = True
                End If
            Loop
        Catch ex As Exception
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", hlp.GetNomeFuncao)
        End Try


    End Sub


#End Region

#Region "Acompanhamento_Execução_Robô"

    Public Sub mensageRun(ctrl As ListBox, processo As String, nrProcessoAtual As Long, nrProcessoTotal As Long)
        Try
            ctrl.Items.Add("Task: " & nrProcessoAtual & " de " & nrProcessoTotal & "  " &
                            Now.ToString("dd/MM HH:mm:ss") & " : " & vbNewLine &
                            processo.ToString & vbNewLine)

            If ctrl.Items.Count > 0 Then
                ctrl.SelectedIndex = ctrl.Items.Count - 1
                'LIMITA A EXIBIÇÃO DO LOG A 500
                If ctrl.Items.Count > 500 Then
                    ctrl.Items.Clear()
                End If
            End If

            'atualiza a data do formulário sempre que concluir um step
            Me.lblDataHora.Text = ExibeData(Now)
        Catch ex As Exception
            ctrl.Items.Clear()
        End Try
    End Sub
#End Region

#Region "Execução de Threads"
    ''' <summary>
    ''' Execução de todas as threads da aplicação
    ''' </summary>
    Private Sub Executar_thread_1()
        Try
            If pararAplicacao Then
                btnAtmCaseParar_Click(Nothing, Nothing)
                Exit Sub
            End If
            thread_1_runing = True
            thread_1 = New Thread(AddressOf Me.thread_1_procedimentos)
            thread_1.IsBackground = True
            thread_1.Start()
        Catch ex As Exception

        End Try

    End Sub
#End Region

#Region "Procedimentos"
    ''' <summary>
    ''' Region de chamada dos STEPs e retornos
    ''' </summary>
    ''' <returns></returns>
    Private Function thread_1_procedimentos() As Boolean

        Try
            'Controles de etadas executadas
            Dim t1_NroProcessos As Long = 2
            Dim t1_NroProcessoAtual As Long = 1
            Dim ciclos As Long = 0
            'Controles de informações entre Steps
            Dim endArquivo As String = ""
            Dim step2ok As Boolean = False

            Do While thread_1_runing And (Not pararAplicacao And Not encerrarAplicacao)
                'Enquanto o botão de Parar/Fechar não foi acionado

                'Atualizando informações
                If Me.ListView1.InvokeRequired Then
                    Me.ListView1.Invoke(New MethodInvoker(AddressOf AtualizarListView))
                End If
                Application.DoEvents()
                ciclos += 1

                Me.lbCiclos.Text = "Total de Ciclos: " & ciclos

                'Etapa 0 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                'Reiniciando o CASE para evitar erros na aplicação
                If ciclos Mod ciclos_maximo = 0 Then
                    CM.fecharCM()
                End If

                'Etapa 1 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                mensageRun(Me.listBoxProcedimentos, "CAPTURANDO REGISTROS", t1_NroProcessoAtual, t1_NroProcessos)
                System.Threading.Thread.Sleep(1000)
                thread_1_status = step_1()

                'VALIDAÇÃO DE ERROS
                If thread_1_status Like "Erro:*" Then
                    thread_1_runing = False
                    mensageRun(Me.listBoxProcedimentos, thread_1_status, 0, 0) 'Enviando msg para a lista de procedimentos
                    If thread_1.IsAlive Then thread_1.Abort()
                    Exit Do
                End If

                'Etapa 2 <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<
                If Not IsNothing(thread_1_status) Then
                    t1_NroProcessoAtual += 1
                    mensageRun(Me.listBoxProcedimentos, "IMPORTANDO REGISTROS", t1_NroProcessoAtual, t1_NroProcessos)
                    System.Threading.Thread.Sleep(1000)
                    If step_2(thread_1_status) Then 'Realizando a importação
                        mensageRun(Me.listBoxProcedimentos, "IMPORTADO: " & Me.txtVolumeImportado.Text & " / " & Me.txtVolumeAnalisado.Text, t1_NroProcessoAtual, t1_NroProcessos)
                        'Zerando variáveis
                        t1_NroProcessoAtual = 1
                        t1_NroProcessos = 2
                        endArquivo = ""
                    Else 'erro na importacao
                        mensageRun(Me.listBoxProcedimentos, msgDeErros, 0, 0) 'Enviando msg para a lista de procedimentos
                        If thread_1.IsAlive Then thread_1.Abort()
                        Exit Do
                    End If
                End If
            Loop

            If reiniciarAplicacao And pararAplicacao = False And encerrarAplicacao = False Then
                hlp.fecharProcesso("EXCEL")
                reiniciarAplicacao = False
                pararAplicacao = False
                encerrarAplicacao = False
                stepAtual = "Iniciando o processo"
                Executar_thread_1() 'Reiniciando o processo caso seja passível
                Return True
            ElseIf reiniciarAplicacao = False And pararAplicacao = False And encerrarAplicacao = False Then

                Me.txtStatus.Text = "Enviando SMS de parada forçada da aplicação!"
                EnviaAlerta("PARADA FORÇADA - elseIf")
                Environment.Exit(1)

                'Reiniciando a máquina
                'hlp.desligarReiniciarWindows("R")
                Return False
            Else
                Return True
            End If

        Catch ex As Exception

            If reiniciarAplicacao Then
                hlp.fecharProcesso("EXCEL")
                reiniciarAplicacao = False
                pararAplicacao = False
                encerrarAplicacao = False
                stepAtual = "Iniciando o processo"
                Executar_thread_1() 'Reiniciando o processo caso seja passível
            Else

                Me.txtStatus.Text = "Enviando SMS de parada forçada da aplicação!"
                EnviaAlerta("PARADA FORÇADA - catch")
                Environment.Exit(1)

                'Reiniciando a máquina
                'Me.txtStatus.Text = "Enviando SMS de reinicialização da máquina!"
                'EnviaAlerta("REINICIANDO MAQUINA")
                'Application.DoEvents()
                'hlp.desligarReiniciarWindows("R")
                Return False
            End If

            Return False

        End Try


    End Function
#End Region

#Region "Steps"

    ''' <summary>
    ''' Logar e capturar dados do CASE MANAGER
    ''' </summary>
    ''' <returns></returns>
    Private Function step_1() As String

        Try
            Dim validador As Boolean = False
            Dim telaPrincial As Boolean = False
            Dim caminho As String = ""

            'Primeira ação é garantir que não existe planilhas antigas baixadas do CASE no Documents do usuário
            stepAtual = "Deletando planilhas antigas"
            Me.txtStatus.Text = stepAtual
            deletarArquivosDeImportacoesAntigas()

            'Garantindo que o CASE MANAGER esteja na tela principal para capturas de despesas
            'Tela Principal do Case é o ponto de partida para captura despesas
            stepAtual = "Logando"
            Me.txtStatus.Text = stepAtual
            If Not CM.validarTelaPrincipalAberta() Then

                If Not CM.validarTelaLoginAberta() Then

                    If CM.iniciarCM Then 'Iniciando o Case Manager
                        If CM.login(Me.txtUsuarioCM.Text, Me.txtSenhaCM.Text, True) Then
                            telaPrincial = True
                        Else
                            'Necessário para validar o tipo de erro, se passível de tentar outra vez automaticamente ou não
                            If msgDeErros Like "*Senha*" Then
                                reiniciarAplicacao = False
                            Else
                                reiniciarAplicacao = True
                            End If
                            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "LOGAR CM")
                            Return "Erro: " & msgDeErros
                        End If
                    Else 'Em caso de erro de inicialização do Case Manager
                        reiniciarAplicacao = False
                        hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "INICIAR CM")
                        Return "Erro: Case Manager não instalado. NÃO TEM COMO PROSSEGUIR!"
                    End If

                Else 'Qdo tela de login já está aberta
                    If CM.login(Me.txtUsuarioCM.Text, Me.txtSenhaCM.Text, True) Then
                        telaPrincial = True
                    End If
                End If

            Else 'Qdo tela principal já está aberta
                telaPrincial = True
            End If

            'Capturando despesas...
            stepAtual = "Capturando despesas"
            Me.txtStatus.Text = stepAtual
            If telaPrincial Then
                validador = CM.capturarAlertas()
            End If

            'Validando se foi solicitada o ENCERRAMENTO/PARADA do robô
            If encerrarAplicacao Or pararAplicacao Then
                stepAtual = "PARANDO A APLICAÇÃO"
                Me.txtStatus.Text = stepAtual
                If encerrarAplicacao Then
                    frmGestorImportacao_FormClosing(Nothing, Nothing)
                End If
                If pararAplicacao Then
                    btnAtmCaseParar_Click(Nothing, Nothing)
                End If
                Return stepAtual
            End If

            'Identificando a planilha exportada da captura despesas
            stepAtual = "Localizando planilha exportada"
            Me.txtStatus.Text = stepAtual
            If validador Then
                Return hlp.localizaArquivoPastaEspecifica(hlp.retornaDirPessoal, "FRM_RESUMO")
            Else
                reiniciarAplicacao = True
                hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "LOCALIZAR PLAN EXPORTADA")
                Return "Erro: Localizar planilha para importação!"
            End If

        Catch ex As Exception
            If Not pararAplicacao Then reiniciarAplicacao = True
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "ERRO GERAL STEP 1")
            Return "Erro: Desconhecido - " & Err.Description & "(" & Err.Number & ")"
        End Try


    End Function

    ''' <summary>
    ''' Função de Importação do arquivo extraído do CM
    ''' O caminho do arquivo é fornecido do STEP 1 após o termino de seu processo, caso ocorra tudo certo
    ''' </summary>
    ''' <param name="caminho"></param>
    ''' <returns></returns>
    Private Function step_2(caminho As String) As Boolean
        Try
            stepAtual = "Importando planilha"
            Me.txtStatus.Text = stepAtual
            Dim validador As Boolean = True

            If Not caminho Is Nothing Then
                colocarFormNaFrente()
                hlp.fecharProcesso("EXCEL") 'Ação de fechar todos os excels abertos (Esteja visível ou não)
                'Importando plan e carregando o validador
                If Not atmImpCase.importarCase(caminho, txtStatus, txtVolumeImportado, txtVolumeNaoImportado, txtVolumeAnalisado) Then
                    hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", msgDeErros)
                    mensageRun(Me.listBoxProcedimentos, msgDeErros, 0, 0)
                    validador = False
                End If
            Else
                reiniciarAplicacao = True
                msgDeErros = "Erro: Imp. do Excel - caminho não localizado"
                hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "IMPORTAR ARQUIVO CAMINHO VAZIO")
                validador = False
            End If


            If validador Then 'Validando se a importação aconteceu corretamente
                stepAtual = "Copiando xls p/ rede"
                Me.txtStatus.Text = stepAtual
                
                hlp.fecharProcesso("EXCEL") 'Ação de fechar todos os excels abertos (Esteja visível ou não)
                Threading.Thread.Sleep(3000) 'Aguardando o windows matar o processo do Excel
                If Not hlp.CriarCopiarMoverDeletarAquivo(caminho, "Deletar") Then
                    reiniciarAplicacao = True
                    msgDeErros = "Erro: Copiar plan para rede!"
                    mensageRun(Me.listBoxProcedimentos, msgDeErros, 0, 0)
                End If 'Deletando 
            Else
                reiniciarAplicacao = True
                deletarArquivosDeImportacoesAntigas()
            End If

            'Validando se foi solicitada o ENCERRAMENTO/PARADA do robô
            If encerrarAplicacao Or pararAplicacao Then
                stepAtual = "PARANDO A APLICAÇÃO"
                Me.txtStatus.Text = stepAtual
                If encerrarAplicacao Then
                    frmGestorImportacao_FormClosing(Nothing, Nothing)
                End If
                If pararAplicacao Then
                    btnAtmCaseParar_Click(Nothing, Nothing)
                End If
                Return True
            End If

            Return validador

        Catch ex As Exception
            If Not pararAplicacao Then reiniciarAplicacao = True
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "ERRO GERAL STEP 2")
            msgDeErros = "Erro: Durante a importação do Excel"
            Return False
        End Try

    End Function

    Private Sub btnAtmCaseParar_Click(sender As Object, e As EventArgs) Handles btnAtmCaseParar.Click
        Try

            'Validando se é o step de capturar despesa, caso sim, não se pode parar enquanto estiver rodando
            'Dentro do step CAPTURANDO DESPESA existe uma validação para a execução da parada, caso tenha sido invocada
            pararAplicacao = True
            mensageRun(listBoxProcedimentos, "Iniciado parada do Robô!", 0, 0)
            If stepAtual = "Capturando despesas" Then
                Exit Sub
            End If

            If stepAtual = "PARANDO A APLICAÇÃO" Then
                Dim nrThreadsTotal As Long = 2
                frmBarraProgresso_v2.Show()
                frmBarraProgresso_v2.ProcessaBarra(1, nrThreadsTotal)

                'AlertaParadaAutomacao("Macro interrompida! (Btn Parar Acionado)")
                If thread_1_runing Then
                    thread_1_runing = False
                    If thread_1.IsAlive Then thread_1.Abort()
                End If


                frmBarraProgresso_v2.ProcessaBarra(2, nrThreadsTotal, "Por favor, aguarde")
                System.Threading.Thread.Sleep(1000)
                frmBarraProgresso_v2.Close()
                Application.DoEvents()

                mensageRun(listBoxProcedimentos, "Robô parado com sucesso!", 0, 0)
                CM.fecharCM()
            End If


        Catch ex As Exception
            Me.txtStatus.Text = "Enviando SMS de PARADA da aplicação!"
            EnviaAlerta("Clique no botao PARAR")
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", hlp.GetNomeFuncao)
            Environment.Exit(1)
        End Try
    End Sub

    Private Sub btnAtualizarListView_Click(sender As Object, e As EventArgs) Handles btnAtualizarListView.Click
        AtualizarListView()
    End Sub

    Private Sub btnExibir_Click(sender As Object, e As EventArgs) Handles btnExibir.Click
        If btnExibir.Text = "Exibir" Then
            txtSenhaCM.PasswordChar = ""
            btnExibir.Text = "Ocultar"
        Else
            txtSenhaCM.PasswordChar = "*"
            btnExibir.Text = "Exibir"
        End If

    End Sub

    Private Sub frmGestorImportacao_FormClosing(sender As Object, e As FormClosingEventArgs) Handles MyBase.FormClosing
        Try


            If HOSTNAME_ATM <> hlp.hostnameLocal Then
                hlp.fecharAplicativo(False)
                Exit Sub
            End If

            'Validando se é o step de capturar despesa, caso sim, não se pode fechar enquanto estiver rodando
            'Dentro do step CAPTURANDO DESPESA existe uma validação para a execução de fechamento, caso tenha sido invocada
            encerrarAplicacao = True
            mensageRun(listBoxProcedimentos, "Iniciado encerramento do Robô!", 0, 0)
            If stepAtual = "Capturando despesas" Then
                Exit Sub
            End If

            Dim nrThreadsTotal As Long = 2
            frmBarraProgresso_v2.Show()
            frmBarraProgresso_v2.ProcessaBarra(1, nrThreadsTotal)

            'AlertaParadaAutomacao("Macro interrompida! (Btn Parar Acionado)")
            If thread_1_runing Then
                thread_1_runing = False
                If thread_1.IsAlive Then thread_1.Abort()
            End If

            frmBarraProgresso_v2.ProcessaBarra(2, nrThreadsTotal, "Por favor, aguarde")
            System.Threading.Thread.Sleep(1000)
            frmBarraProgresso_v2.Close()
            Application.DoEvents()

            hlp.fecharAplicativo(False)
            mensageRun(listBoxProcedimentos, "Robô encerrado!", 0, 0)
            CM.fecharCM()

        Catch ex As Exception
            Me.txtStatus.Text = "Enviando SMS de ENCERRAMENTO da aplicação!"
            Application.DoEvents()
            EnviaAlerta("ENCERRADO")
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", hlp.GetNomeFuncao)
            Environment.Exit(1)
        End Try
    End Sub




#End Region

End Class