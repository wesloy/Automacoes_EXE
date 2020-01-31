
Imports System.Configuration
Imports White.Core.UIItems.WindowItems
Imports White.Core.Factory
Imports White.Core.UIItems.Finders
Imports White.Core.InputDevices


''' <summary>
''' Exemplo de utilização da biblioteca... White.Core.UIItems.WindowItems
''' https://www.codeproject.com/articles/289028/white-an-ui-automation-tool-for-windows-applicatio
''' </summary>
Public Class funcoes_CM

    Private Shared hlp As New helpers

    Public Function iniciarCM() As Boolean

        Try
            'Objetivo é validar se o CASE MANAGER está aberto
            Dim p As List(Of Process)
            Dim caminho As String
            p = Process.GetProcessesByName("Case_Manager_brd_prd").ToList

            If p.Count = 0 Then 'se o processo não está iniciado
                'Abrir aplicação
                caminho = "C:\Users\" & UCase(Environ("USERNAME")) & ""
                caminho += "\AppData\Roaming\Microsoft\Windows\Start Menu\Programs\2rp Net\Case Manager v"
                caminho += VERSAO_CM
                caminho += " - Produção(Bradesco).appref-ms"
                Process.Start(caminho)
                Threading.Thread.Sleep(14000) 'Tempo médio que o CM demora para iniciar

                p = Process.GetProcessesByName("Case_Manager_brd_prd").ToList
                If p.Count = 0 Then
                    Return False
                Else
                    Return True
                End If
            Else
                Return False
            End If

        Catch ex As Exception
            Return False
        End Try


    End Function

    ''' <summary>
    ''' O procedimento de matar o processo do CASE loca o usuário do CM
    ''' Sendo necessário pedir para um usuário MASTER do CM desbloquear o usuário
    ''' </summary>
    ''' <returns></returns>
    Public Function Kill_CM() As Boolean
        Try
            Dim startInfo As New ProcessStartInfo("Case_Manager_brd_prd.exe")
            Dim aplicacao As White.Core.Application
            aplicacao = White.Core.Application.AttachOrLaunch(startInfo)
            aplicacao.Kill()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Function fecharCM() As Boolean
        Try
            'Verificando qual tela está aberta..
            'se tela de login, pode matar o processo
            'se tela principal é necessário sair do CM
            If validarTelaLoginAberta() Then
                Kill_CM()
                Return True
            End If

            If validarTelaPrincipalAberta() Then

                Dim JanelaCaseManagerCliente As White.Core.UIItems.WindowItems.Window
                Dim cmd_sair As White.Core.UIItems.Button 'Botão de sair do CM dentro da janela principal
                Dim cmd_sim As White.Core.UIItems.Button 'Box de confirmação de fechar o CM

                JanelaCaseManagerCliente = localizarJanelaTituloDaJanela("Case Manager Cliente")
                cmd_sair = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.Button)("cmd_sair")
                cmd_sair.Click()

                cmd_sim = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.Button)("6")
                cmd_sim.Click()

                Return True
            End If
            Return False
        Catch ex As Exception
            Return False
        End Try

    End Function

    Public Function tratativaDeErros() As Boolean
        Try
            'Fechando qualquer excel aberto e o CM
            hlp.fecharProcesso("EXCEL")
            fecharCM()
            tratativaDeErros += 1
            Return True
        Catch ex As Exception
            hlp.fecharAplicativo(False)
            Return False
        End Try
    End Function

    Public Function localizarJanelaTituloDaJanela(tituloJanela As String) As White.Core.UIItems.WindowItems.Window
        Try

            Dim startInfo As New ProcessStartInfo("Case_Manager_brd_prd.exe")
            Dim aplicacao As White.Core.Application
            Dim janela As White.Core.UIItems.WindowItems.Window

            aplicacao = White.Core.Application.AttachOrLaunch(startInfo)
            janela = aplicacao.GetWindow(tituloJanela)

            Return janela

        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Public Function login(ByVal usuario As String, ByVal senha As String, Optional ByVal msgParaUsuario As Boolean = False) As Boolean

        Try
            Dim janela As White.Core.UIItems.WindowItems.Window
            Dim txtUsuario As White.Core.UIItems.TextBox
            Dim txtSenha As White.Core.UIItems.TextBox
            Dim btnlogin As White.Core.UIItems.Button
            Dim btnClose As White.Core.UIItems.Button
            Dim btnOk As White.Core.UIItems.Button
            Dim lbStatus As White.Core.UIItems.Label

            'Localizando a Janela e os itens do formulário aberto
            janela = localizarJanelaTituloDaJanela("Identificação")
            txtUsuario = janela.Get(Of White.Core.UIItems.TextBox)("txt_usuario")
            txtSenha = janela.Get(Of White.Core.UIItems.TextBox)("txt_senha")
            btnlogin = janela.Get(Of White.Core.UIItems.Button)("cmd_login")
            btnClose = janela.Get(Of White.Core.UIItems.Button)("cmd_sair")

            'Inserindo informações na janela e clicando em ok
            txtUsuario.Text = usuario
            txtSenha.Text = senha
            'btnlogin.Enter("cmd_login")
            btnlogin.Click()
            Threading.Thread.Sleep(5000) 'Tempo médio que o CM demora para iniciar

            Try

                lbStatus = janela.Get(Of White.Core.UIItems.Label)("65535")
                msgDeErros = lbStatus.Name
                btnOk = janela.Get(Of White.Core.UIItems.Button)("2")
                btnOk.Click()
                btnClose.Click()
                tratativaDeErros()
                'MsgBox(msg, vbInformation, TITULO_ALERTA)
                Return False

            Catch ex As Exception
                'Login completado com sucesso
                Return True
            End Try

        Catch ex As Exception
            msgDeErros = "Erro ao tentar logar no Case Manager! (" & Err.Description & " - " & Err.Number & ")"
            tratativaDeErros()
            'MsgBox(msgDeErros, vbCritical, TITULO_ALERTA)
            Return False
        End Try

    End Function

    Public Function capturarAlertas() As Boolean

        'variaveis de controle e cálculo
        Dim tempoEspera As Integer = 0 'Calcular em segundos o tempo de espera
        Dim inicioProcesso As Date = hlp.DataHoraAtual

        'Declarações de Itens do Case COM OS MESMOS NOMES DADOS NA APLICACAO
        Dim JanelaCaseManagerCliente As White.Core.UIItems.WindowItems.Window
        Dim menuResumos As Object
        Dim subMenuResumos As Object
        Dim painel As White.Core.UIItems.Panel
        Dim cbo_empresa As White.Core.UIItems.ListBoxItems.ComboBox '005 : BRADESCO (2º item)
        Dim cbo_filtro As White.Core.UIItems.ListBoxItems.ComboBox '00000000 - TODAS AS FILAS (1º item)
        Dim rdb_filaAtiva As White.Core.UIItems.RadioButton 'Selecionar apenas filas Ativas
        Dim msk_data_ini As White.Core.UIItems.TextBox 'Data Inicial
        Dim msk_data_fim As White.Core.UIItems.TextBox 'Data Final
        Dim msk_intervalo_min As White.Core.UIItems.TextBox 'Hora Inicial
        Dim msk_intervalo_max As White.Core.UIItems.TextBox 'Hora Final
        Dim cmd_pesquisa As White.Core.UIItems.Button 'Botão de pesquisa
        Dim cmd_exportar As White.Core.UIItems.Button 'Botão de exportar
        Dim cmd_sair As White.Core.UIItems.Button 'Botão de fechar da janela de Lista de Alertas por Fila
        Dim btnSalvar As White.Core.UIItems.Button 'Botão da janela Salvar Como

        'Dim Tab_0 As White.Core.UIItems.TabItems.TabPage
        'Dim dgAlerta As White.Core.UIItems.TableItems.Table 'Registros gerados
        inicioProcesso = hlp.DataHoraAtual()

        JanelaCaseManagerCliente = localizarJanelaTituloDaJanela("Case Manager Cliente")
        menuResumos = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.MenuItems.Menu)(White.Core.UIItems.Finders.SearchCriteria.ByText("Resumos      "))
        menuResumos.click()
        subMenuResumos = menuResumos.childMenus
        subMenuResumos.item(2).click() 'Lista de Alertas por Fila
        Threading.Thread.Sleep(1000)

        'Carregando componentes
        painel = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.Panel)("pan_pesquisa")
        cbo_empresa = painel.Get(Of White.Core.UIItems.ListBoxItems.ComboBox)("cbo_empresa")
        rdb_filaAtiva = painel.Get(Of White.Core.UIItems.RadioButton)("rdbFilaAtiva")
        cbo_filtro = painel.Get(Of White.Core.UIItems.ListBoxItems.ComboBox)("cbo_filtro")
        msk_data_ini = painel.Get(Of White.Core.UIItems.TextBox)("msk_data_ini")
        msk_data_fim = painel.Get(Of White.Core.UIItems.TextBox)("msk_data_fim")
        msk_intervalo_min = painel.Get(Of White.Core.UIItems.TextBox)("msk_intervalo_min")
        msk_intervalo_max = painel.Get(Of White.Core.UIItems.TextBox)("msk_intervalo_max")
        cmd_pesquisa = painel.Get(Of White.Core.UIItems.Button)("cmd_pesquisar")
        'Salvar Como
        painel = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.Panel)("Panel3")
        cmd_exportar = painel.Get(Of White.Core.UIItems.Button)("cmd_exportar")
        cmd_sair = painel.Get(Of White.Core.UIItems.Button)("cmd_sair")

        Try

            'Derrubar todos os processos de excel que estiverem abertos
            hlp.fecharProcesso("EXCEL")

            'Realizando o filtro
            cbo_empresa.Select(1) '005 : BRADESCO (2º item)
            rdb_filaAtiva.Click() 'Selecionar apenas filas Ativas
            cbo_filtro.Select(0) '00000000 - TODAS AS FILAS (1º item)

            'msk_data_ini.Text = "2018-06-12"
            'msk_data_fim.Text = "2018-06-12"
            'msk_intervalo_min.Text = "1:00 AM"
            'msk_intervalo_max.Text = "22"
            cmd_pesquisa.Click()
            Threading.Thread.Sleep(1000)
            tempoEspera = DateDiff(DateInterval.Second, inicioProcesso, hlp.DataHoraAtual) * 1000

            'Carregando componentes
            'Tab_0 = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.TabItems.TabPage)("Tab_0")
            'dgAlerta = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.TableItems.Table)("dgAlerta")
            'Ordenando do mais recente para o mais antigo
            'dgAlerta.Header.Columns("DT. Transação").Click()
            'dgAlerta.Header.Columns("DT. Transação").Click()

            cmd_exportar.Click()
            Threading.Thread.Sleep(5000) 'Aguardar a janela de salvar como carregar..

            'Janela Salvar Como
            btnSalvar = JanelaCaseManagerCliente.Get(Of White.Core.UIItems.Button)("1")
            Threading.Thread.Sleep(10000) 'Aguardar a janela de salvar como carregar..
            btnSalvar.Click()
            Threading.Thread.Sleep(tempoEspera) 'Aguardando o processo do botão salvar

            'Testar se existe excel aberto.. senão aguardar a abertura para depois seguir o processo
            'Necessário, vide o CM abrir automaticamente o excel exportado
            Do While Not hlp.verificarProcessoSeEstaAberto("EXCEL")
                'frmGestorImportacao.mensageRun(frmGestorImportacao.listBoxProcedimentos, "AGUARDE PROCESSANDO EXCEL", 0, 0)
                Threading.Thread.Sleep(2000)
            Loop
            'frmGestorImportacao.mensageRun(frmGestorImportacao.listBoxProcedimentos, "AGUARDE PROCESSANDO EXCEL", 0, 0)
            Threading.Thread.Sleep(tempoEspera) 'Aguardar excel abrir...

            'Derrubar todos os processos de excel que estiverem abertos
            hlp.fecharProcesso("EXCEL")

            'Fechar a janela de Lista de Alertas por fila
            'Necessário para que o próximo ciclo não trave e também o horário seja atualizado para o mais recente
            cmd_sair.Click()



            Return True
        Catch ex As Exception

            hlp.fecharProcesso("EXCEL")
            msgDeErros = "Erro ao tentar abrir Lista de Alertas por Fila. (" & Err.Description & " - " & Err.Number & ")"
            hlp.registrarLOG(Err.Number, Err.Description, "CM IMPORTADOR", "CAPTURANDO DESPESAS")
            cmd_sair.Click()
            tratativaDeErros()
            'MsgBox(msgDeErros, vbCritical, TITULO_ALERTA)
            Return False
        End Try
    End Function

    Public Function validarTelaPrincipalAberta() As Boolean
        Dim janela As White.Core.UIItems.WindowItems.Window
        janela = localizarJanelaTituloDaJanela("Case Manager Cliente")
        If IsNothing(janela) Then
            Return False
        Else
            Return True
        End If
    End Function

    Public Function validarTelaLoginAberta() As Boolean
        Dim janela As White.Core.UIItems.WindowItems.Window
        janela = localizarJanelaTituloDaJanela("Identificação")
        If IsNothing(janela) Then
            Return False
        Else
            Return True
        End If
    End Function



End Class


