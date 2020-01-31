
Imports White.Core.UIItems.WindowItems
Imports System.Configuration
Class Funcoes

    Public Function iniciar() As Boolean
        Dim enviando As New EnvioM
        Frm_Applicacao.ListComandos.Items.Add("Envio SMS ""Alerta Case Manager: ON "" " & Now)
        Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
        enviando.getMensageSms("Alerta Case Manager: Servico iniciado")

        Try
            Frm_Applicacao.BtnIniciar.Enabled = False
            With Frm_Applicacao

                .LbGeral.Text = "Iniciando!"
                .ListComandos.Items.Add("Iniciando " & Now)
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                .BarGeral.Maximum = 10
                .BarGeral.Value = 0

                Dim uma As New Funcoes
                Dim JanelaLogin As White.Core.UIItems.WindowItems.Window

                .BarGeral.Value = 1

                uma.AbrirAplicacao("C:\Users\a058572\Desktop\Case Manager v9.4.1 - Produção (Bradesco).appref-ms")
                .ListComandos.Items.Add("Logando... " & Now)
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                JanelaLogin = Funcoes.PegaJanela("Identificação")
                .BarGeral.Value = 2
                If Funcoes.Logando(JanelaLogin) = False Then
                    If Funcoes.Logando2Tentativa(JanelaLogin) = False Then
                        Dim FUUU As New EnvioM
                        Frm_Applicacao.ListComandos.Items.Add("Envio SMS ""Alerta Case Manager: OFF"" " & Now)
                        Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                        FUUU.getMensageSms("Alerta Case Manager: Erro ao fazer login")

                        MsgBox("Avisar Administrador", MsgBoxStyle.Critical)
                        Application.Exit()
                    End If
                End If
                .ListComandos.Items.Add("logado " & Now)
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                If VerificaLogon(JanelaLogin) = True Then
                    Dim FUUU As New EnvioM
                    Frm_Applicacao.ListComandos.Items.Add("Envio SMS ""Alerta Case Manager: OFF"" " & Now)
                    Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                    FUUU.getMensageSms("Alerta Case Manager: Erro ao fazer login")
                    Exit Function
                End If

                .BarGeral.Value = 3

                Frm_Applicacao.JanelaInicialCliente = Funcoes.PegaJanela("Case Manager Cliente")
                .ListComandos.Items.Add("Abrindo Pesquisa em tela de Cliente " & Now)
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)

                Dim TXTnroCartao As White.Core.UIItems.TextBox
                Application.DoEvents()
                TXTnroCartao = Frm_Applicacao.JanelaInicialCliente.Get(Of White.Core.UIItems.TextBox)("txt_cartao")

                If IsNothing(TXTnroCartao) Then
                    Frm_Applicacao.JanelaInicialCliente = Funcoes.PegaJanela("Case Manager Cliente")
                    Frm_Applicacao.ListComandos.Items.Add("Abrindo Pesquisa em tela de Cliente " & Now)
                    Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                    Funcoes.AbrirPesquisar(Frm_Applicacao.JanelaInicialCliente)
                    Frm_Applicacao.ListComandos.Items.Add("Analisando entrada... " & Now)
                End If
                .BarGeral.Value = 4

                .listaMatrix = New List(Of ClasseCasos)
                '.ListComandos.Items.Add("Consultando Banco de dados MATRIX")
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                'Frm_Applicacao.listaMatrix = BLLClasseCasos.RetornarSQL("Select c.valorDespesa,	c.id,	c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,c.DataFinalizacaoCase FROM MX_bMaTRiX c left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID where c.FinalizarCase = 1 and c.erroFinalizaCase = 0")
                '.ListComandos.Items.Add("Lista MATRIX retornou " & Frm_Applicacao.listaMatrix.Count & " casos a serem encerrados.")
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)

                .TmpTick.Start()
                .PictureBox1.Image = My.Resources.Ligado
                Application.DoEvents()

                Return True

            End With

        Catch ex As Exception
            Frm_Applicacao.BtnIniciar.Enabled = True
            Frm_Applicacao.ListComandos.Items.Add("Não foi possível iniciar a automação " & Now)
            enviando.getMensageSms("Alerta Case Manager: Nao foi possivel iniciar a automacao")
            Frm_Applicacao.TmpTick.Stop()
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Return False

        End Try
    End Function


    Private Function VerificaLogon(janela As Window) As Boolean

        Try

            Dim LbErro As White.Core.UIItems.Label

            LbErro = janela.Get(Of White.Core.UIItems.Label)("65535")
            If LbErro.Text = "O usuário informado não pode estar logado em mais
de uma estação de trabalho ao mesmo tempo!" Then
                Dim enviando As New EnvioM
                Frm_Applicacao.ListComandos.Items.Add("Envio SMS ""Alerta Case Manager: OFF"" " & Now)
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                enviando.getMensageSms("Alerta Case Manager ERRO: usuario logado em mais de uma estação")
                Application.DoEvents()
                'MsgBox("Avisar Administrador", MsgBoxStyle.Critical)
                Return False
            Else

                Return True

            End If

        Catch ex As Exception

            Return False

        End Try
        Return True

    End Function

    Friend Shared Function AbrirPesquisar(janelaInicialCliente As Window) As Boolean
        Dim enviando As New EnvioM
        Try

            Dim menuItemPesquisa As Object = janelaInicialCliente.Get(Of White.Core.UIItems.MenuItems.Menu)(White.Core.UIItems.Finders.SearchCriteria.ByText("Pesquisar    "))

            menuItemPesquisa.click()

            Dim menuItemCartao As Object = menuItemPesquisa.ChildMenus 'System.Collections.Generic.Mscorlib_CollectionDebugView(Of White.Core.UIItems.MenuItems.Menu)(DirectCast(menuItemPesquisa, Castle.Proxies.MenuProxy).ChildMenus).Items(0)

            menuItemCartao.item(0).click()
            Return True

        Catch ex As Exception

            Frm_Applicacao.ListComandos.Items.Add("Enviando SMS: ""Erro ao clicar em ""Pesquisa"" " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            enviando.getMensageSms("Alerta Case Manager ERRO: usuario logado em mais de uma estação")
            Return False
        End Try

    End Function

    Public Shared Function PesquisarCartao(janela As Window, Caso As ClasseCasos) As Boolean

        Try
            '    Frm_Applicacao.ListComandos.Focus()
            Dim TXTnroCartao As White.Core.UIItems.TextBox
            Application.DoEvents()
            TXTnroCartao = janela.Get(Of White.Core.UIItems.TextBox)("txt_cartao")
            ' TXTnroCartao.Focus()
            TXTnroCartao.Text = ""
            TXTnroCartao.Text = Caso.sNroDoCartao

            Dim btnpesquisa As White.Core.UIItems.Button
            Application.DoEvents()
            btnpesquisa = janela.Get(Of White.Core.UIItems.Button)("cmd_pesquisar")
            btnpesquisa.Click()

            Dim JanelaErro As White.Core.UIItems.WindowItems.Window

            JanelaErro = Funcoes.PegaJanela("Informação não encontrada.")

            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function

    Friend Shared Sub SelecionarPendencias(janela As Window, item As ClasseCasos, quanti As Integer, cont As Integer)
        Try

            Dim ListaCasos As White.Core.UIItems.ListView
            Dim desmarcador As White.Core.UIItems.ListViewCell
            Dim listaFechar As New List(Of ClasseCasos)
            Dim tabeladeItens As White.Core.UIItems.TabItems.Tab
            Dim caso As ClasseCasos

            ListaCasos = janela.Get(Of White.Core.UIItems.ListView)("lvw")
            tabeladeItens = janela.Get(Of White.Core.UIItems.TabItems.Tab)("Tab_2")


            Dim listaobj As Object = ListaCasos.Rows
            Dim passouSuspeita As Boolean = False
            For Each itens In listaobj

                If itens.name = "S U S P E I T A" Then
                    'If itens.name = "M O N I T O R A M E N T O" Then
                    caso = New ClasseCasos
                    caso.sEstabelecimento = itens.cells.item(21).name
                    caso.sDataTransmissao = itens.cells.item(15).name
                    caso.sValor = itens.cells.item(18).name
                    desmarcador = itens.cells.item(7)
                    If Frm_Applicacao.MandeiParar = True Then
                        Exit Sub
                    End If
                    If caso.sEstabelecimento = item.sEstabelecimento And caso.sDataTransmissao = item.sDataTransmissao Then

                        If FinalizarCaso(itens, janela, item) = False Then
                            passouSuspeita = True
                            Frm_Applicacao.ListComandos.Items.Add("Erro ao finalizar caso " & item.sNroDoCartao & ". " & Now)
                            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                        Else
                            Frm_Applicacao.ListComandos.Items.Add(cont & "/" & quanti & ". Cartão " & item.sNroDoCartao & " Finalizado. " & Now)
                            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                            Exit For
                        End If

                    Else
                        passouSuspeita = False
                        DesmarcarCaso(itens)
                    End If
                End If
            Next
            If passouSuspeita = False Then
                Frm_Applicacao.ListComandos.Items.Add("Cartão (" & item.sNroDoCartao & ") já tratado.")
                Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                item.ierroFinalizaCase = 0
                item.iFinalizarCase = 0
                item.Tratado_Automacao_CASE = 0
                item.sDataFinalizacaoCase = Now
                BLLClasseCasos.Atualizar(item)
            End If

        Catch ex As Exception
            Frm_Applicacao.ListComandos.Items.Add("Erro ao Selecionar pendencia " & Now)
        End Try

    End Sub

    Private Shared Function DesmarcarCaso(itens As Object) As Boolean
        Try
            itens.Focus()
            itens.doubleclick
            Return True
        Catch ex As Exception
            Return False
        End Try


    End Function

    Private Shared Function FinalizarCaso(itens As Object, janela As Window, caso As ClasseCasos) As Boolean
        Try

            If IsNothing(caso.sCategoriaCase) = False Then

                Dim categoriaselectada As Object = caso.sCategoriaCase.Split("-")

                caso.sCategoriaCase = categoriaselectada(0).ToString.Trim
            End If
            Dim ResponderCartao As White.Core.UIItems.Button
            ResponderCartao = janela.Get(Of White.Core.UIItems.Button)("Cmd_situacao_caso")
            ResponderCartao.Click()

            Dim CmboxRespostaCartao As White.Core.UIItems.ListBoxItems.ComboBox
            CmboxRespostaCartao = janela.Get(Of White.Core.UIItems.ListBoxItems.ComboBox)("cbo_resposta")
            Dim numerorecebido As Integer
            numerorecebido = Procuraindex(caso.sCategoriaCase)
            CmboxRespostaCartao.Select(numerorecebido)


            If IsNothing(caso.sSubcategoriaCase) = False Then
                Dim Subcategoriaselectada As Object = caso.sSubcategoriaCase.Split("-")
                caso.sSubcategoriaCase = Subcategoriaselectada(0).ToString.Trim
                Dim CmboxMotivo As White.Core.UIItems.ListBoxItems.ComboBox
                CmboxMotivo = janela.Get(Of White.Core.UIItems.ListBoxItems.ComboBox)("cbo_motivo")
                Dim SUBnumerorecebido As Integer
                SUBnumerorecebido = Procuraindexsub(caso.sCategoriaCase, caso.sSubcategoriaCase)
                CmboxMotivo.Select(SUBnumerorecebido)
            End If
            Dim CmboxNovaAnalise As White.Core.UIItems.ListBoxItems.ComboBox
            CmboxNovaAnalise = janela.Get(Of White.Core.UIItems.ListBoxItems.ComboBox)("cbo_nova_analise")
            CmboxNovaAnalise.Select(0)

            Dim Txtboxcomentario As White.Core.UIItems.MultilineTextBox
            Dim vari As String

            Txtboxcomentario = janela.Get(Of White.Core.UIItems.MultilineTextBox)("txt_comentario")
            vari = Txtboxcomentario.Text
            Txtboxcomentario.Text = vari & " - ALERTA DIGITAL ALGARTECH - #" & caso.iCodMatrix & " #TESTE DE FINALIZACAO CASE MIGRACAO RCP"

            If ClicarNoSalvar(janela) = False Then

                Return False

            Else
                caso.iFinalizarCase = 0
                caso.sDataFinalizacaoCase = Now
                caso.ierroFinalizaCase = 0
                caso.Tratado_Automacao_CASE = 1
                If BLLClasseCasos.Atualizar(caso) = True Then
                    Return True
                Else
                    Return False
                End If

            End If

        Catch ex As Exception

            Frm_Applicacao.ListComandos.Items.Add("Erro ao finalizar caso " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)

            Return False

        End Try
    End Function

    Private Shared Function Procuraindex(sCategoriaCase As String) As Integer
        Try
            Select Case sCategoriaCase
                Case "AGE"
                    Return 0
                Case "501"
                    Return 1
                Case "503"
                    Return 2
                Case "504"
                    Return 3
                Case "0010"
                    Return 4
                Case "008"
                    Return 5
                Case "5020"
                    Return 6
                Case "801"
                    Return 7
                Case "802"
                    Return 8
                Case "605"
                    Return 9
                Case "606"
                    Return 10
                Case "BD0"
                    Return 11
                Case "805"
                    Return 12
                Case "803"
                    Return 13
                Case "804"
                    Return 14
                Case "805"
                    Return 15
                Case "806"
                    Return 16
                Case "807"
                    Return 17
                Case "808"
                    Return 18
                Case "809"
                    Return 19
            End Select
        Catch ex As Exception
            Frm_Applicacao.ListComandos.Items.Add("Erro ao selecionar box de resposta do cartão " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Return 200
        End Try
        Return 0
    End Function
    Private Shared Function Procuraindexsub(sCategoriaCase As String, sSubcategoriaCase As String) As Integer
        Try
            Select Case sCategoriaCase
                Case "AGE"
                    Return 0
                Case "501"
                    Select Case sSubcategoriaCase
                        Case "1"
                            Return 0
                        Case "3"
                            Return 1
                        Case "4"
                            Return 2
                        Case "5"
                            Return 3
                        Case "6"
                            Return 4
                        Case "23"
                            Return 5
                        Case "45"
                            Return 6
                    End Select
                Case "503"
                    Return 0
                Case "504"
                    Select Case sSubcategoriaCase
                        Case "43"
                            Return 0
                        Case "44"
                            Return 1
                    End Select
                Case "0010"
                    Return 0
                Case "008"
                    Return 0
                Case "5020"
                    Return 0
                Case "801"

                    Select Case sSubcategoriaCase
                        Case "16"
                            Return 0
                        Case "17"
                            Return 1
                        Case "18"
                            Return 2
                        Case "19"
                            Return 3
                        Case "21"
                            Return 4
                        Case "46"
                            Return 5
                        Case "47"
                            Return 6
                    End Select
                Case "802"
                    Return 0
                Case "605"
                    Select Case sSubcategoriaCase
                        Case "24"
                            Return 0
                        Case "25"
                            Return 1
                        Case "26"
                            Return 2
                        Case "27"
                            Return 3
                        Case "28"
                            Return 4
                        Case "48"
                            Return 5
                        Case "49"
                            Return 6
                    End Select
                Case "606"
                    Return 0
                Case "BD0"
                    Return 0
                Case "805"
                    Return 0
                Case "803"
                    Select Case sSubcategoriaCase
                        Case "29"
                            Return 0
                        Case "30"
                            Return 1
                        Case "31"
                            Return 2
                        Case "32"
                            Return 3
                        Case "34"
                            Return 4
                        Case "36"
                            Return 5
                        Case "50"
                            Return 6
                    End Select
                Case "804"
                    Return 0
                Case "805"
                    Return 0
                Case "806"
                    Return 0
                Case "807"
                    Select Case sSubcategoriaCase
                        Case "37"
                            Return 0
                        Case "38"
                            Return 1
                        Case "39"
                            Return 2
                        Case "40"
                            Return 3
                        Case "41"
                            Return 4
                        Case "42"
                            Return 5
                        Case "51"
                            Return 6
                    End Select
                Case "808"
                    Return 0
                Case "809"
                    Return 0
            End Select
        Catch ex As Exception
            Frm_Applicacao.ListComandos.Items.Add("Erro ao selecionar box de resposta do cartão " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Return 200
        End Try
        Return 0
    End Function

    Private Shared Function ClicarNoSalvar(janela As Window) As Boolean
        Try
            Dim Salvar As White.Core.UIItems.Button
            Salvar = janela.Get(Of White.Core.UIItems.Button)("cmd_salvar") 'cmd_salvar
            Salvar.Click()

            Dim BtnOK As White.Core.UIItems.Button
            BtnOK = janela.Get(Of White.Core.UIItems.Button)("2") 'cmd_salvar
            BtnOK.Click()
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function
    ' Abre aplicação
    Public Sub AbrirAplicacao(myFavoritesPath As String)
        Dim p As List(Of Process)

        p = Process.GetProcessesByName("Case_Manager_brd_prd").ToList

        If p.Count = 0 Then

            Process.Start(myFavoritesPath)
            Frm_Applicacao.ListComandos.Items.Add("Aguardando inicialização do Case Manager... " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Application.DoEvents()
            Threading.Thread.Sleep(14000)

        End If

    End Sub 'OpenApplication

    Public Shared Function PegaJanela(titulo As String) As White.Core.UIItems.WindowItems.Window
        Try

            Dim startInfo As New ProcessStartInfo("Case_Manager_brd_prd.exe")
            Dim aplicacao As White.Core.Application
            Dim janela As White.Core.UIItems.WindowItems.Window
            aplicacao = White.Core.Application.AttachOrLaunch(startInfo)
            janela = aplicacao.GetWindow(titulo)
            'Dim menuItemPesquisa As Object = aplicacao.Get(Of White.Core.UIItems.WindowItems.Window)(White.Core.UIItems.Finders.SearchCriteria.ByText(titulo))
            Frm_Applicacao.ListComandos.Items.Add("Janela """ & titulo & """ aberta " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Return janela

        Catch ex As Exception
            ' Frm_Applicacao.ListComandos.Items.Add("Erro ao abrir janela """ & titulo & """")
            Return Nothing
        End Try
    End Function

    Friend Shared Function Logando(janela As Window) As Boolean
        Dim TXTLogin As White.Core.UIItems.TextBox
        Dim txtSenha As White.Core.UIItems.TextBox
        Dim btnlogin As White.Core.UIItems.Button
        Dim enviando As New EnvioM
        Dim criptografa As New Cripta
        Try
            Try

                TXTLogin = janela.Get(Of White.Core.UIItems.TextBox)("txt_usuario")
                TXTLogin.Text = ConfigurationManager.AppSettings("UsuarioLogado").ToString  ' "RALVARENGA"
                txtSenha = janela.Get(Of White.Core.UIItems.TextBox)("txt_senha")
                txtSenha.Text = criptografa.Decrypt(ConfigurationManager.AppSettings("SenhaUsuarioLogado").ToString) '"algar@01"
                btnlogin = janela.Get(Of White.Core.UIItems.Button)("cmd_login")
            Catch ex As Exception
                Return True
            End Try

            btnlogin.Click()
            Try
                Dim LbErro As White.Core.UIItems.Label
                LbErro = janela.Get(Of White.Core.UIItems.Label)("65535")
                If LbErro.Text = "Usuário/Senha inválidos ou usuário não possui perfil associado. Verifique o usuário/senha informados e se possui perfil de acesso ao sistema." Then
                    Dim btnOK As White.Core.UIItems.Button
                    btnOK = janela.Get(Of White.Core.UIItems.Button)("2")
                    btnOK.Click()
                    Frm_Applicacao.ListComandos.Items.Add("Erro ao utilizar primeiro Login!" & Now)
                    Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
                    Application.DoEvents()
                    enviando.getMensageSms("Alerta Case Manager: Erro ao utilizar primeiro Login!")
                    Return False
                End If
            Catch ex As Exception
                Return True
            End Try
            Return True

        Catch ex As Exception
            enviando.getMensageSms("Alerta Case Manager: Erro ao utilizar primeiro Login!")
            Return False
        End Try

    End Function
    Friend Shared Function Logando2Tentativa(janela As Window) As Boolean
        Try

            Dim TXTLogin As White.Core.UIItems.TextBox
            Dim txtSenha As White.Core.UIItems.TextBox
            Dim btnlogin As White.Core.UIItems.Button
            Dim enviando As New EnvioM
            Dim criptografa As New Cripta
            enviando.getMensageSms("Alerta Case Manager: Segunda tentativa de Login")
            Frm_Applicacao.ListComandos.Items.Add("Segunda tentativa de Login " & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            TXTLogin = janela.Get(Of White.Core.UIItems.TextBox)("txt_usuario")
            TXTLogin.Text = ""
            TXTLogin.Text = ConfigurationManager.AppSettings("UsuarioLogado2").ToString  ' "RALVARENGA"
            txtSenha = janela.Get(Of White.Core.UIItems.TextBox)("txt_senha")
            txtSenha.Text = ""
            txtSenha.Text = criptografa.Decrypt(ConfigurationManager.AppSettings("SenhaUsuarioLogado2").ToString) '"algar@01"
            btnlogin = janela.Get(Of White.Core.UIItems.Button)("cmd_login")
            btnlogin.Click()
            enviando.getMensageSms("Alerta Case Manager: segunda tentativa de login realizado com sucesso!")
            Frm_Applicacao.ListComandos.Items.Add("segunda tentativa de login realizado com sucesso!" & Now)
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            Return True

        Catch ex As Exception

            Return False

        End Try

    End Function
End Class
