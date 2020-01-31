
Imports excel = Microsoft.Office.Interop.Excel
Public Class atmImportacaoCASE
    'Variáveis diversas
    Private Objcon As New conexao
    Private hlp As New helpers
    Private sql As String
    Private dt As DataTable

    'BLLs
    Private logImport As New clsLogImportacaoBLL
    Private produtoBLL As New clsProdutoBLL
    Private filaBLL As New clsFilaBLL
    Public Function importarCase(diretorio As String, ctrl_status As Control, ctrl_cont_importados As Control, ctrl_cont_naoImportados As Control, ctrl_cont_total As Control, Optional ByVal CaseON As Boolean = True, Optional ByVal acaoContingencial As Boolean = False) As Boolean

        'Fechando qualquer excel aberto
        hlp.fecharProcesso("EXCEL")

        'Instanciando o excel
        Dim appExcel As New excel.Application
        Dim appExcelBook As excel.Workbook
        Dim appExcelWorkSheet As excel.Worksheet

        Dim importarArq As Boolean = True

        'variáveis de controle
        Dim linha As Long = 0, linhaInicial As Long = 0
        Dim rsMaTrix As New ADODB.Recordset, rsRobo As New ADODB.Recordset, rsChave As New ADODB.Recordset, rsCase As New ADODB.Recordset
        Dim hora_i As Date, difHoraTrabalhado As Date
        Dim importar As Boolean = True, registrarDescarte As Boolean = False
        Dim codFilaPlanilhaCase As String = "", dupInd As String = "", validaBIN As String = "", idMaTriX As Long, campoChaveATM As String
        Dim cartao As String = "", bin As String = "", tipoCartao As String = "", produto As String = "", bandeira As String = "", legadoB2K As String = "", origemCaso As String = ""
        Dim contadorFOR As Long = 0, cont_importados As Long = 0, cont_naoImportados As Long = 0, cont_total As Long = 0, totalRegistros As Long = 0
        Dim i As Long = 0, cont As Long = 0
        Dim dataRegistro As Date
        Dim nomeArquivo As String = "", stepProgress As String = "Passo 2 de 4, aguarde..."
        Dim distribuicaoCasos As Integer = 0
        Dim status As Integer = 0, areaID As Integer = 1
        Dim filaNome_CM As String = "", filaID_CM As Integer = 0, filaNome_MX As String = "", filaID_MX As Integer = 0, riscoFila As String = "", fluxo_ATM As String = "", fluxo_XS As String = "", aplicar_Matriz As Boolean = False
        Dim neppoID As Integer = 0, codFila As Integer = 0
        Dim key_matrix As String
        Dim key_case As String

        ctrl_status.Text = ""
        ctrl_cont_importados.Text = ""
        ctrl_cont_naoImportados.Text = ""
        ctrl_cont_total.Text = ""

        Try


            'acaoContingencial = False
            appExcelBook = appExcel.Workbooks.Open(Filename:=diretorio)
            appExcelWorkSheet = appExcelBook.Sheets("Alerta_Gerado")

            'para calcular o tempo de importação
            hora_i = Now()
            linhaInicial = 10 'linha inicial
            appExcel.Sheets("Alerta_Gerado").Select()
            ctrl_status.Text = "Por favor, aguarde..."

            'antes de importar efetuamos uma validação do layout
            'verificamos a primeira e ultima coluna
            If appExcel.Cells(linhaInicial - 1, 1).Value <> "Org [Empresa]" Or appExcel.Cells(linhaInicial - 1, 53).Value <> "Usuário Resp." Then
                MsgBox("Este layout está divergente." & vbNewLine & "Por favor, utilize o layout correto.", vbCritical, TITULO_ALERTA)
                ctrl_status.Text = ""
                hlp.registrarLOG(, , hlp.GetNomeFuncao, "Layout Incorreto")
                appExcel.Visible = True
                appExcel.Quit()
                Return False
                Exit Function
            End If

            linha = linhaInicial
            'Percorrendo toda a sheet que contenha informacoes na primeira coluna
            'Validando se todo o volume do Excel pertence a um único dia
            dataRegistro = hlp.FormataDataAbreviada(appExcel.Cells(linha, 10).Value.ToString.Trim)
            While Not String.IsNullOrEmpty(appExcel.Range("A" & linha).Value)
                hlp.CursorPointer(True)
                totalRegistros = totalRegistros + 1
                ctrl_cont_total.Text = totalRegistros
                'Blindando para 1 dia de importação de cada vez
                If Not hlp.FormataDataAbreviada(appExcel.Cells(linha, 10).Value.ToString.Trim) = dataRegistro Then
                    frmBarraProgresso_v2.Close()
                    Application.DoEvents()
                    MsgBox("Este arquivo possui mais que 1 dia para importação!" _
                        & vbNewLine & "Certifique que haja apenas 1 dia para importação de cada vez. Obrigado!", vbInformation, TITULO_ALERTA)
                    appExcel.Visible = True
                    appExcel.Quit()
                    Return False
                End If
                Application.DoEvents()
                linha = linha + 1
            End While

            ctrl_cont_total.Text = "" 'limpa o total

            'Conforme orientado VERBALMENTE pelo Adrino em 10/8 é para transferir todo volume
            'direcionado para HUMANO em CASE ON que não foi trabalho para o Robô em CASE PEND
            If Not CaseON Then
                sql = "Delete from MX_bMaTRiX Where fila_id = 42 And status = 0 And format(dataRegistro,'yyyy-MM-dd') = '" & Format(dataRegistro, "yyyy-MM-dd") & "' "
                Objcon.ExecutaQuery(sql)
            End If

            'Iniciar a barra de progresso
            frmBarraProgresso_v2.Show()
            frmBarraProgresso_v2.ProcessaBarra(1, totalRegistros, stepProgress)
            linha = linhaInicial 'reseta posição inicial para novo while

            'Carregando as filas parametrizadas com DE/PARA (CASE P/ MATRIX)
            contadorFOR = 0 'Zerando contador
            dt = Nothing
            sql = "Select * from MX_CaseFilasImportacao Where ativa = " & Objcon.valorSql(True) & ""
            dt = Objcon.RetornaDataTable(sql)
            Dim CM_NomeFilaCM(0 To dt.Rows.Count) As String
            Dim CM_IDFilaCM(0 To dt.Rows.Count) As String
            Dim CM_RiscoFila(0 To dt.Rows.Count) As String
            Dim CM_NomeFilaMX(0 To dt.Rows.Count) As String
            Dim CM_IDFilaMX(0 To dt.Rows.Count) As String
            Dim CM_FluxoATM(0 To dt.Rows.Count) As String
            Dim CM_FluxoBloqueio(0 To dt.Rows.Count) As String
            Dim CM_AplicarMatriz(0 To dt.Rows.Count) As String
            If dt.Rows.Count > 0 Then 'Validando se tem volume
                For Each drRow As DataRow In dt.Rows 'Carregando Array
                    CM_NomeFilaCM(contadorFOR) = drRow("nome_Fila")
                    CM_IDFilaCM(contadorFOR) = drRow("id_Fila")
                    CM_RiscoFila(contadorFOR) = drRow("risco_fila")
                    CM_NomeFilaMX(contadorFOR) = drRow("filaMatrix_Nome")
                    CM_IDFilaMX(contadorFOR) = drRow("filaMatrix_Numero")
                    CM_FluxoATM(contadorFOR) = drRow("fluxo_ATM")
                    CM_FluxoBloqueio(contadorFOR) = drRow("fluxo_Bloqueio")
                    CM_AplicarMatriz(contadorFOR) = drRow("aplicar_Matriz")
                    contadorFOR = contadorFOR + 1
                Next
            End If

            'Carregando todos os BINs passíveis de serem importados
            contadorFOR = 0 'Zerando contador
            dt = Nothing
            sql = "Select * from MX_B2K_BINsImportacao Where ativa = " & Objcon.valorSql(True) & ""
            dt = Objcon.RetornaDataTable(sql)
            Dim BINsImportacao(0 To dt.Rows.Count) As String
            If dt.Rows.Count > 0 Then 'Validando se tem volume
                For Each drRow As DataRow In dt.Rows 'Carregando Array
                    BINsImportacao(contadorFOR) = drRow("BIN")
                    contadorFOR = contadorFOR + 1
                Next
            End If

            'Carregando os produtos/tipo cartão
            contadorFOR = 0 'Zerando contador
            dt = Nothing
            sql = "Select * from sysProdutos"
            dt = Objcon.RetornaDataTable(sql)
            Dim binArray(0 To dt.Rows.Count) As String
            Dim produtoArray(0 To dt.Rows.Count) As String
            Dim tipoCartaoArray(0 To dt.Rows.Count) As String
            Dim bandeiraArray(0 To dt.Rows.Count) As String
            If dt.Rows.Count > 0 Then 'Validando se tem volume
                For Each drRow As DataRow In dt.Rows 'Carregando Array
                    binArray(contadorFOR) = IIf(IsDBNull(drRow("bin")), Nothing, drRow("bin"))
                    produtoArray(contadorFOR) = IIf(IsDBNull(drRow("produto")), Nothing, drRow("produto"))
                    tipoCartaoArray(contadorFOR) = IIf(IsDBNull(drRow("tipoCartao")), Nothing, drRow("tipoCartao"))
                    bandeiraArray(contadorFOR) = IIf(IsDBNull(drRow("emissor")), Nothing, drRow("emissor"))
                    contadorFOR = contadorFOR + 1
                Next
            End If

            'Carregando cartões cancelados
            contadorFOR = 0 'Zerando contador
            dt = Nothing
            sql = "Select * from MX_bCartoesCancelados"
            Objcon.banco_dados = "db_MaTRiX.accdb"
            dt = Objcon.RetornaDataTable(sql)
            Dim canceladosArray(0 To dt.Rows.Count) As String
            If dt.Rows.Count > 0 Then 'Validando se tem volume
                For Each drRow As DataRow In dt.Rows 'Carregando Array
                    canceladosArray(contadorFOR) = drRow("cartao")
                    contadorFOR = contadorFOR + 1
                Next
            End If

            'Carregamento de base para pesquisa de duplicidade
            sql = "SELECT * FROM MX_bMaTRiX inner join MX_sysfilas on MX_bMaTRiX.fila_id = MX_sysFilas.id "
            sql += "WHERE 1 = 1 "
            sql += "and MX_sysFilas.sigla_fila = 'CM' "
            sql += "and MX_sysFilas.fila not like 'CM FOLLOW' "
            sql += "and MX_sysFilas.fila not like '%TRATATIVA%' "
            sql += "and MX_sysFilas.fila not like '%REENTRADA%' "
            sql += "and MX_sysFilas.fila not like '%ROTEAMENTO%' "
            sql += "and (dataImportacao >= " & Objcon.dataSql(DateAdd(DateInterval.Day, -7, Now)) & ") "
            sql += "Order by MX_bMaTRiX.id desc"
            rsMaTrix = Nothing
            rsMaTrix = Objcon.RetornaRs(sql)

            linha = totalRegistros + 9 '+ 9, pois são as linhas de cabeçalho
            'Percorrendo todo os dados válidos, do último para o primeiro, devido ordenação da planilha extraída
            While Not linha = 9

                importar = True
                registrarDescarte = False
                riscoFila = ""
                filaID_CM = 0
                filaNome_CM = ""
                filaID_MX = 0
                filaNome_MX = "ALERTA DIGITAL"
                fluxo_ATM = "SMS+URA"
                fluxo_XS = "BLOQUEAR NO INICIO"
                aplicar_Matriz = False
                produto = ""
                tipoCartao = ""
                bandeira = ""
                cartao = Microsoft.VisualBasic.Right(Replace(appExcel.Cells(linha, 2).Value, " ", ""), 16)
                cartao = IIf(Microsoft.VisualBasic.Left(cartao, 1) = 0, Microsoft.VisualBasic.Right(Replace(appExcel.Cells(linha, 2).Value, " ", ""), 15), cartao)
                dataRegistro = hlp.FormataDataHoraCompleta(appExcel.Cells(linha, 10).Value.ToString.Trim)
                validaBIN = Microsoft.VisualBasic.Left(cartao, 6).Trim
                codFilaPlanilhaCase = appExcel.Cells(linha, 3).Value

                'Higienização 1
                'Retirada de cartões cancelados
                If importar Then
                    For i = LBound(canceladosArray) To UBound(canceladosArray)
                        If cartao = canceladosArray(i) Then
                            importar = False
                            Exit For
                        End If
                    Next
                End If

                'Higienização 2
                'Apenas BINs cadastrados para importação
                If importar Then
                    For i = LBound(BINsImportacao) To UBound(BINsImportacao)
                        If validaBIN = BINsImportacao(i) Then
                            importar = True
                            Exit For
                        Else
                            importar = False
                        End If
                    Next
                End If

                'Higienização 3
                'Apenas FILAS cadastradas para importacao e suas parametrizações
                If importar Then
                    For i = LBound(CM_IDFilaCM) To UBound(CM_IDFilaCM)
                        If codFilaPlanilhaCase = CM_IDFilaCM(i) And appExcel.Cells(linha, 1).Value.ToString.Trim.ToUpper Like "5" Then
                            riscoFila = CM_RiscoFila(i)
                            filaID_CM = CM_IDFilaCM(i)
                            filaNome_CM = CM_NomeFilaCM(i)
                            filaID_MX = CM_IDFilaMX(i)
                            filaNome_MX = CM_NomeFilaMX(i)
                            fluxo_ATM = CM_FluxoATM(i)
                            fluxo_XS = CM_FluxoBloqueio(i)
                            aplicar_Matriz = CM_AplicarMatriz(i)
                            importar = True
                            Exit For
                        Else
                            importar = False
                        End If
                    Next
                End If

                'Higienização 4
                'Permitir a entrada de casos novos do mesmo cartão apenas que tenham dt/hms > do que a última transação finalizada
                'Carregar RS com tbl que será alimentada e também validar se a transação pode subir para automação
                If importar Then
                    rsMaTrix.Filter = ""
                    rsMaTrix.Filter = "Cartao = '" & cartao & "'"

                    With rsMaTrix
                        If .RecordCount > 0 Then
                            .MoveFirst()
                            Do While .EOF = False 'Looping de todos os registros encontrados


                                'Registro já trabalhdo é mais recente do que o atual que está sendo importado
                                'Não deve ser importado, pois considera-se que já foi validado em uma importação anterior
                                If .Fields("dataRegistro").Value >= dataRegistro Then
                                    importar = False
                                    Exit Do
                                End If


                                'Validando se a transação atual é igual a alguma já importada e finalizada
                                'Caso sim, deverá subir finalizada como DESCARTE, para que o Robô do Case Manager feche o registro
                                'Dados para validação para duplicidade: 
                                'cartao (já está no filter do RS), COD FILA, GRUPO REGRA, REGRA, dt TRANS (DD/MM/AAAA), VL LOCAL, COD EC
                                key_matrix = Nothing
                                key_case = Nothing

                                key_matrix = hlp.FormataDataAbreviada(.Fields("dataRegistro").Value).ToString
                                key_matrix += .Fields("regraFraude").Value.ToString
                                key_matrix += .Fields("estabelecimentoCodigo").Value.ToString
                                key_matrix += .Fields("valorDespesa").Value.ToString

                                key_case = hlp.FormataDataAbreviada(appExcel.Cells(linha, 10).Value.ToString.Trim).ToString
                                key_case += Replace(Trim(appExcel.Cells(linha, 7).Value) & "-" & Trim(appExcel.Cells(linha, 8).Value), " ", "").ToString
                                key_case += Replace(appExcel.Cells(linha, 17).Value, " ", "").ToString
                                key_case += CStr(hlp.transformarMoeda(Replace(appExcel.Cells(linha, 12).Value, " ", "").ToString))

                                key_matrix = hlp.RetornaSoNumeroDeString(key_matrix)
                                key_case = hlp.RetornaSoNumeroDeString(key_case)

                                'Antes de criar a FINALIZAÇÃO ESPECÍFICA era necessário esperar até que o caso tivesse sido trabalhado, para evitar DESCARTAR TODOS OS REGISTROS
                                'If .Fields("status").Value = 3 And key_matrix = key_case Then
                                If key_matrix = key_case Then

                                    'Garantindo que o mesmo REGISTRO não seja importado mais que 1x
                                    rsMaTrix.Filter = "dataRegistro = '" & dataRegistro & "'"

                                    If rsMaTrix.RecordCount = 0 Then 'Se for > 0 o registro avaliado é igual ao que já foi importado
                                        importar = True
                                        registrarDescarte = True
                                        status = 0 'No registro de DESCARTE será alterado para 3
                                        Exit Do 'Necessário para não cair na função ABAIXO de validação de cartão ainda aguardando ser trabalhado
                                    Else
                                        importar = False
                                        registrarDescarte = True
                                        status = 0 'No registro de DESCARTE será alterado para 3
                                        Exit Do 'Necessário para não cair na função ABAIXO de validação de cartão ainda aguardando ser trabalhado
                                    End If
                                End If

                                'Cartão ainda aguardando ser trabalhado
                                If .Fields("status").Value = 0 Or .Fields("status").Value = 5 Then
                                    importar = False
                                    Exit Do
                                End If

                                .MoveNext()
                            Loop
                        End If
                    End With
                End If

                'Higienização 5
                'Identificação BIN / PRODUTO / TIPO CARTAO / ORIGEM CASO
                If importar Then
                    bin = Microsoft.VisualBasic.Left(cartao, 6).Trim
                    For i = LBound(binArray) To UBound(binArray)
                        If bin = binArray(i) Then
                            produto = produtoArray(i)
                            bandeira = bandeiraArray(i)
                            tipoCartao = tipoCartaoArray(i)
                            origemCaso = "CASE"
                            Exit For
                        End If
                    Next
                End If

                'Higienização 6
                'Definir fila e área de trabalho e capturar ID das mesmas e qual status 
                'Fixei os dados das filas no código devido a velocidade de importação que estava lenta qdo fazia consulta
                'registro à registro buscando as informações
                If importar Then
                    If produto.ToUpper Like "*CORPORATE*" Then 'TODO corporate vai para fila de contato, trabalhado por humano de forma direta
                        '44 'CM ON CONTATO'
                        '45 'CM PEND CONTATO'
                        codFila = IIf(CaseON, 44, 45)
                        status = 0 'Status para Humano
                    Else

                        'Validando se deve seguir o para o ALERTA DIGITAL ou para uma fila expecífica
                        If filaNome_MX.ToUpper Like "ALERTA DIGITAL" Then
                            '42 'CM ON SMS ENVIO'
                            '43 'CM PEND SMS ENVIO'
                            codFila = IIf(CaseON, 42, 43)
                            status = IIf(acaoContingencial, 0, 5) 'Bloqueado para o robô
                        Else
                            codFila = filaID_MX 'Id da fila Matrix que foi carregado da tabela de FILAS ATIVAS E PARAMETRIZACOES 
                            status = 0 'Status para Humano
                        End If
                    End If
                End If

                campoChaveATM = ""
                idMaTriX = 0

                If importar Then

                    With rsMaTrix
                        .AddNew()
                        .Fields("status").Value = IIf(acaoContingencial, 0, status) 'Status é definido conforme a fila, porém qdo tem ação contingencial sempre será 0 para encaminhar para Humano
                        .Fields("fila_ID").Value = codFila
                        .Fields("area_ID").Value = areaID
                        .Fields("risco").Value = riscoFila
                        .Fields("tipoRegistro").Value = "AUTOMATICO"
                        .Fields("origemRegistro").Value = "CASE"
                        .Fields("dataRegistro").Value = hlp.FormataDataHoraCompleta(appExcel.Cells(linha, 10).Value.ToString.Trim)
                        .Fields("descricaoRegistro").Value = "MCC: " & hlp.RemoverSimbolos(Trim(appExcel.Cells(linha, 16).Value.ToString.ToUpper))
                        .Fields("regraFraude").Value = Replace(Trim(appExcel.Cells(linha, 7).Value) & "-" & Trim(appExcel.Cells(linha, 8).Value), " ", "")
                        .Fields("cartao").Value = cartao
                        .Fields("bin").Value = bin
                        .Fields("produto").Value = produto
                        .Fields("tipoCartao").Value = tipoCartao
                        .Fields("estabelecimentoCodigo").Value = Replace(appExcel.Cells(linha, 17).Value, " ", "")
                        .Fields("estabelecimentoNome").Value = hlp.RemoverSimbolos(appExcel.Cells(linha, 18).Value.Trim)
                        .Fields("valorDespesa").Value = hlp.transformarMoeda(Replace(appExcel.Cells(linha, 12).Value, " ", ""))
                        .Fields("IdImportador").Value = hlp.capturaIdRede
                        .Fields("dataImportacao").Value = Format(hora_i, "yyyy-MM-dd")
                        .Fields("horaImportacao").Value = Format(hora_i, "HH:mm:ss")

                        'Importando caso finalizado, para que o Case Manager também seja fechado
                        If registrarDescarte Then
                            .Fields("fila_ID").Value = IIf(CaseON, 44, 45) '44 CM ON CONTATO / 45 CM PEND CONTATO
                            .Fields("area_ID").Value = 1
                            .Fields("status").Value = 3
                            .Fields("finalizacao_id").Value = IIf(CaseON, 246, 254) 'DESCARTE
                            .Fields("subfinalizacao_id").Value = 1
                            .Fields("idCat").Value = "MATRIX"
                            .Fields("dataCat").Value = hlp.FormataDataAbreviada(Now())
                            .Fields("horaInicial").Value = hora_i
                            .Fields("horaFinal").Value = Now()
                            difHoraTrabalhado = hlp.converterSegundos(Microsoft.VisualBasic.DateDiff(DateInterval.Second, .Fields("horaInicial").Value, .Fields("horaFinal").Value))
                            .Fields("tempoTotalAnalise").Value = Format(difHoraTrabalhado, "HH:mm:ss")
                            .Fields("tempoTotalAnaliseSeg").Value = Microsoft.VisualBasic.DateDiff(DateInterval.Second, .Fields("horaInicial").Value, .Fields("horaFinal").Value)
                            .Fields("FinalizarCase").Value = True
                            .Fields("finalizar_Case_Especifico").Value = True 'FINALIZAR APENAS ESTE REGISTRO
                        End If


                        .Fields("campoChaveATM").Value = .Fields("cartao").Value & .Fields("estabelecimentoCodigo").Value & Now
                        campoChaveATM = .Fields("campoChaveATM").Value
                        .Update()

                        'Capturando o ID Matrix após importacao..
                        sql = "Select id from MX_bMaTRiX Where campoChaveATM = '" & campoChaveATM & "' "
                        rsChave = Objcon.RetornaRs(sql)
                        If rsChave.RecordCount > 0 Then
                            idMaTriX = rsChave.Fields("id").Value
                        End If
                        rsChave = Nothing

                    End With

                    'Inserindo caso na tabela MX_CaseManager_AlertasGerados
                    sql = "Select top 1 * from MX_CaseManager_AlertasGerados"
                    rsCase = Objcon.RetornaRs(sql)

                    With rsCase
                        .AddNew()
                        .Fields("id_Matrix").Value = idMaTriX
                        .Fields("Org_Empresa").Value = Trim(appExcel.Cells(linha, 1).Value)
                        .Fields("Nr_Cartao").Value = cartao
                        .Fields("Cod_Fila_Atual").Value = Trim(appExcel.Cells(linha, 3).Value)
                        .Fields("Fila_Atual").Value = Trim(appExcel.Cells(linha, 4).Value)
                        .Fields("Cod_Fila_Ant").Value = Trim(appExcel.Cells(linha, 5).Value)
                        .Fields("Fila_Ant").Value = Trim(appExcel.Cells(linha, 6).Value)
                        .Fields("Grupo_Regra").Value = Trim(appExcel.Cells(linha, 7).Value)
                        .Fields("Regra").Value = Trim(appExcel.Cells(linha, 8).Value)
                        .Fields("Score").Value = Trim(appExcel.Cells(linha, 9).Value)
                        .Fields("DT_Transacao").Value = hlp.FormataDataHoraCompleta(Trim(appExcel.Cells(linha, 10).Value))
                        .Fields("Valor_Local").Value = hlp.transformarMoeda(Trim(appExcel.Cells(linha, 11).Value))
                        .Fields("Valor_Faturado").Value = hlp.transformarMoeda(Trim(appExcel.Cells(linha, 12).Value))
                        .Fields("POS").Value = Trim(appExcel.Cells(linha, 13).Value)
                        .Fields("Resp").Value = Trim(appExcel.Cells(linha, 14).Value)
                        .Fields("MCC").Value = Trim(appExcel.Cells(linha, 15).Value)
                        .Fields("Campo16").Value = Trim(appExcel.Cells(linha, 16).Value)
                        .Fields("Cod_Estab").Value = Trim(appExcel.Cells(linha, 17).Value)
                        .Fields("Estab").Value = Trim(appExcel.Cells(linha, 18).Value)
                        .Fields("Cidade_Estab").Value = Trim(appExcel.Cells(linha, 19).Value)
                        .Fields("UF_Estab").Value = Trim(appExcel.Cells(linha, 20).Value)
                        .Fields("País").Value = Trim(appExcel.Cells(linha, 21).Value)
                        .Fields("Moeda").Value = Trim(appExcel.Cells(linha, 22).Value)
                        .Fields("NR_POS").Value = Trim(appExcel.Cells(linha, 23).Value)
                        .Fields("Token_requestor_ID").Value = Trim(appExcel.Cells(linha, 24).Value)
                        .Fields("Flag_Token").Value = Trim(appExcel.Cells(linha, 25).Value)
                        .Fields("Tipo_Mensagem").Value = Trim(appExcel.Cells(linha, 26).Value)
                        .Fields("Token_Provisioning_Score").Value = Trim(appExcel.Cells(linha, 27).Value)
                        .Fields("Device_Type").Value = Trim(appExcel.Cells(linha, 28).Value)
                        .Fields("Token_Account_Score").Value = Trim(appExcel.Cells(linha, 29).Value)
                        .Fields("Token_Device_Score").Value = Trim(appExcel.Cells(linha, 30).Value)
                        .Fields("Token_Reason_Code").Value = Trim(appExcel.Cells(linha, 31).Value)
                        .Fields("Device_Number").Value = Trim(appExcel.Cells(linha, 32).Value)
                        .Fields("IP_Address").Value = Trim(appExcel.Cells(linha, 33).Value)
                        .Fields("ID_Requisitante_Token").Value = Trim(appExcel.Cells(linha, 34).Value)
                        .Fields("Nivel_Segurança_Token").Value = Trim(appExcel.Cells(linha, 35).Value)
                        .Fields("PAN").Value = Trim(appExcel.Cells(linha, 36).Value)
                        .Fields("Token_PSN").Value = Trim(appExcel.Cells(linha, 37).Value)
                        .Fields("Token_Expiration_Dat").Value = Trim(appExcel.Cells(linha, 38).Value)
                        .Fields("Token_Status").Value = Trim(appExcel.Cells(linha, 39).Value)
                        .Fields("Token_Cryptogram_Verification_Results").Value = Trim(appExcel.Cells(linha, 40).Value)
                        .Fields("EMV_Token_Cryptogram_Verificarion_Results").Value = Trim(appExcel.Cells(linha, 41).Value)
                        .Fields("Token_Constraints_Verification_Status").Value = Trim(appExcel.Cells(linha, 42).Value)
                        .Fields("Transaction_Date_Time_Constraint").Value = Trim(appExcel.Cells(linha, 43).Value)
                        .Fields("Transaction_Amount_Constraint").Value = Trim(appExcel.Cells(linha, 44).Value)
                        .Fields("Usage_Constraint").Value = Trim(appExcel.Cells(linha, 45).Value)
                        .Fields("Token_ATC_Verification_Result").Value = Trim(appExcel.Cells(linha, 46).Value)
                        .Fields("CVE2_Token_Cryptogram_Verification_Status").Value = Trim(appExcel.Cells(linha, 47).Value)
                        .Fields("MCC_Constraints").Value = Trim(appExcel.Cells(linha, 48).Value)

                        'alteração do layout em 23/02  [ rafael alvarenga ]
                        'Type Process' | 'Cliente UHV'
                        .Fields("Type_Process").Value = Trim(appExcel.Cells(linha, 49).Value)
                        .Fields("Cliente_UHV").Value = Trim(appExcel.Cells(linha, 50).Value)
                        .Fields("Resposta").Value = Trim(appExcel.Cells(linha, 51).Value)
                        .Fields("Data_Registro").Value = hlp.FormataDataHoraCompleta(Trim(appExcel.Cells(linha, 52).Value))
                        .Fields("Usuario_Resp").Value = Trim(appExcel.Cells(linha, 53).Value)

                        .Fields("dataImportacao").Value = hlp.FormataDataHoraCompleta(Now())
                        .Fields("idImportador").Value = hlp.capturaIdRede
                        .Update()
                    End With

                    'Inserindo caso no robô
                    'Status igual a 5 deve ser registrada na base do robô
                    If status = 5 And acaoContingencial = False And registrarDescarte = False Then

                        sql = "Select top 1 * from ATM_sysAtmBase"
                        rsRobo = Objcon.RetornaRs(sql)
                        With rsRobo

                            'gerando divisão do volume
                            distribuicaoCasos = IIf(distribuicaoCasos = 0, 1, 0)
                            legadoB2K = IIf(distribuicaoCasos = 0, "B2K", "B2K_" & distribuicaoCasos)

                            .AddNew()
                            .Fields("id_Matrix").Value = idMaTriX
                            .Fields("area_ID").Value = areaID
                            .Fields("fila_ID").Value = codFila
                            .Fields("fila_IDNeppo").Value = neppoID
                            .Fields("cartao").Value = cartao
                            .Fields("bin").Value = bin
                            .Fields("produto").Value = produto
                            .Fields("tipoProduto").Value = tipoCartao
                            .Fields("se_numero").Value = rsMaTrix.Fields("estabelecimentoCodigo").Value
                            .Fields("se_nome").Value = rsMaTrix.Fields("estabelecimentoNome").Value
                            .Fields("valor").Value = rsMaTrix.Fields("valorDespesa").Value
                            .Fields("dtTrans").Value = Format(rsMaTrix.Fields("dataRegistro").Value, "yyyy-MM-dd")
                            .Fields("hrTrans").Value = Format(rsMaTrix.Fields("dataRegistro").Value, "HH:mm:ss")
                            .Fields("legado_b2k").Value = legadoB2K

                            'Novos campos alimentados à partir da importação do CASE (não tinha no Winter)
                            .Fields("org_empresa").Value = Trim(appExcel.Cells(linha, 1).Value)
                            .Fields("grupo_regra").Value = Trim(appExcel.Cells(linha, 7).Value)
                            .Fields("pos").Value = Trim(appExcel.Cells(linha, 13).Value)
                            .Fields("cod_resp").Value = Trim(appExcel.Cells(linha, 14).Value)
                            .Fields("cod_pais").Value = Trim(appExcel.Cells(linha, 21).Value)
                            .Fields("bandeira").Value = Trim(bandeira)
                            .Fields("cod_moeda").Value = rsCase.Fields("Moeda").Value

                            'Campos de flexibilidade criados à partir de 18/4/18
                            .Fields("fluxo_ATM").Value = fluxo_ATM
                            .Fields("fluxo_Bloqueio").Value = fluxo_XS
                            .Fields("aplicar_Matriz").Value = aplicar_Matriz


                            If riscoFila.ToUpper Like "*ULTRA*" Then
                                .Fields("risco").Value = "ULTRA"
                            ElseIf riscoFila.ToUpper Like "*ALTO*" Then
                                .Fields("risco").Value = "ALTO"
                            ElseIf riscoFila.ToUpper Like "*MÉDIO*" Or riscoFila.ToUpper Like "*MEDIO*" Then
                                .Fields("risco").Value = "MEDIO"
                            ElseIf riscoFila.ToUpper Like "*BAIXO*" Then
                                .Fields("risco").Value = "BAIXO"
                            Else
                                .Fields("risco").Value = "ULTRA"
                            End If

                            .Fields("IdImportador").Value = hlp.capturaIdRede
                            .Fields("dataImportacao").Value = rsMaTrix.Fields("dataImportacao").Value
                            .Fields("horaImportacao").Value = rsMaTrix.Fields("horaImportacao").Value
                            .Update()
                        End With
                    End If


                    cont_importados += 1
                Else
                    cont_naoImportados += 1
                End If

                cont_total += 1
                'mostra o volume analisado/importado/não importado
                ctrl_cont_total.Text = cont_total
                ctrl_cont_importados.Text = cont_importados
                ctrl_cont_naoImportados.Text = cont_naoImportados

                'pulaR linha
                linha = linha - 1
                'refresh na barra de progresso
                frmBarraProgresso_v2.ProcessaBarra(cont_total, totalRegistros, stepProgress)
            End While


            'registra log de importacao
            Dim total As Long
            total = cont_importados
            If total > 0 Then
                ctrl_status.Text = "Salvando uma copia do arquivo importado..."
                Dim novoNome As String = ""
                'salva uma copia do arquivo utilizado para importar
                nomeArquivo = Dir(diretorio)
                If Not CaseON Then
                    novoNome = hlp.CopiaArquivoRetornaNome(origem:=diretorio, destino:=PATH_LOG_IMPORT, arquivo:=nomeArquivo, id:="CASE")
                Else
                    novoNome = "SEM ARQ BACKUP"
                End If

                logImport.registrarLogImportacao(, , hlp.GetCurrentMethodName, "Importados: " & total & ";" & "Arquivo: (" & novoNome & ")")
                Application.DoEvents()
                ctrl_status.Text = "Foram importados: " & total & " registros."
            Else
                ctrl_status.Text = "Não existem novos registros para importar."
            End If
            frmBarraProgresso_v2.Close()
            Application.DoEvents()
            appExcel.Visible = True
            appExcelBook.Close()
            appExcelBook = Nothing
            appExcel.Quit()
            rsCase = Nothing
            rsChave = Nothing
            rsMaTrix = Nothing
            rsRobo = Nothing
            Objcon.Desconectar()

            '>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> AÇÃO CONTIGENCIAL - TRANSFERENCIA DOS CASOS AINDA NÃO TRABALHADOS PELO ROBÔ PARA HUMANO
            'If acaoContingencial Then
            '    If contigencia("B2K", CaseON) Then
            '        Return True
            '    Else
            '        Return False
            '    End If
            'Else
            '    Return True
            'End If

            Return True

        Catch ex As Exception
            Objcon.Desconectar()
            'MsgBox("Este layout não é permitido." & vbNewLine & "Por favor, utilize o layout correto." & vbNewLine & vbNewLine & "Erro: " & ex.Message & " " & Err.Number, vbCritical, TITULO_ALERTA)
            appExcel.Visible = True
            appExcel.Quit()
            Return False
        End Try
        Exit Function

    End Function


End Class
