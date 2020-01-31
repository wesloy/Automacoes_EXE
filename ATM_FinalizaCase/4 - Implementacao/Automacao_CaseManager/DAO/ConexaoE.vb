Imports System.Configuration
Imports System.Data
Imports System.Data.OleDb
Imports ADODB
Public Class ConexaoE
    Private conexao As New ADODB.Connection 'conexao usando ADO
    Private cmd As New ADODB.Command 'Command
    Private isTran As Boolean
    Public banco_dados As String = "db_Fraude_Amex"

    Public Function GetStringConexao() As String

        Dim strconexao As String = ""
        'SQL Server , usando SQL Server OLE DB Provider
        strconexao = "Provider=SQLOLEDB;"
        strconexao += "Data Source=UDPCRPDB03;"
        strconexao += "Initial Catalog=db_Fraude_Amex;"
        strconexao += "User Id=usr_Fraude_Amex;"
        strconexao += "Password=Gw2cTJ@WhCM05k;"
        strconexao += "Connect Timeout=60000"
        Return strconexao

    End Function

    'Metodo para efetuar uma conexão
    'Optional ByVal senha As Boolean = False
    Public Function Conectar() As Boolean
        Dim bln As Boolean
        Try
            If conexao.State = ConnectionState.Closed Then
                With conexao
                    .ConnectionString = GetStringConexao()
                    .Mode = ConnectModeEnum.adModeReadWrite 'modo de conexao leitura e escrita
                    .Open()
                End With
            End If
            bln = True
        Catch ex As Exception 'ex As OleDbException
            Desconectar()
            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

            bln = False
            Application.Exit()
        End Try
        Return bln
    End Function

    ' Procedimento para desconectar do banco de dados.
    Public Sub Desconectar()
        Try
            If Not conexao Is Nothing Then
                If Not conexao.State = ConnectionState.Closed Then
                    conexao.Close()
                End If
            End If
        Catch ex As Exception
            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _
            '                       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _
            '                                          & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _
            '                                                             & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

        End Try

    End Sub



    ' Procedimento para testar conexão com o banco de dados.

    Public Sub Testaconexao()

        Try

            Conectar()

            MsgBox("Conexão realizada com sucesso!!!")

        Catch ex As Exception

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

            conexao = Nothing

        End Try

        Desconectar()

    End Sub





    'Executa um comando SQL e retorna um boleano

    Public Function ExecutaQuery(ByVal strSql As String, Optional ByRef qtRegistroBlock As Long = 0) As Boolean

        Try

            'verifica se a conexao esta fechada

            If conexao.State = ConnectionState.Closed Then

                Conectar()

            End If

            'executa a consulta

            With conexao

                .Execute(strSql, qtRegistroBlock)

            End With



            'retorna verdadeiro

            ExecutaQuery = True

            Desconectar()



        Catch ex As Exception

            Desconectar()

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

            'Application.Exit()

            Return False

        End Try

    End Function



    Public Function RetornaDataTable(ByVal strSQL As String) As DataTable

        Dim objDA As New OleDbDataAdapter

        Dim objDT As New DataTable

        Dim rsObjt As ADODB.Recordset

        Try

            If conexao.State = ConnectionState.Closed Then

                Conectar()

            End If

            rsObjt = RetornaRs(strSQL)

            objDT = RecordSetToDataTable(rsObjt)

            Desconectar()

        Catch ex As Exception

            Desconectar()

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

            Application.Exit()

        End Try

        RetornaDataTable = objDT

    End Function



    'Retorna um recordset

    Public Function RetornaRs(ByVal strSQL As String) As ADODB.Recordset

        Dim ADORecordset As New ADODB.Recordset

        Try

            If conexao.State = ConnectionState.Closed Then

                Conectar()

            End If

            With ADORecordset

                .CursorLocation = CursorLocationEnum.adUseClient

            End With

            ADORecordset.Open(strSQL, conexao, CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

            RetornaRs = ADORecordset

            ADORecordset = Nothing

        Catch ex As Exception

            Desconectar()

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

            Application.Exit()

            Return Nothing

        End Try

    End Function



    'Procedimento para retornar um Objeto do tipo DataTable através de um recordset

    Public Function RecordSetToDataTable(ByVal objRS As ADODB.Recordset) As DataTable

        Dim objDA As New OleDbDataAdapter()

        Dim objDT As New DataTable()

        objDA.Fill(objDT, objRS)

        Desconectar()

        Return objDT

    End Function



    'Inicia uma transação;

    Public Sub BeginTransaction()

        Try

            If conexao.State = ConnectionState.Closed Then

                Conectar()

            End If

            conexao.BeginTransaction()

        Catch ex As Exception

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

        End Try

    End Sub



    'Faz um commit na transação;

    Public Sub CommitTransaction()

        Try

            conexao.CommitTrans()

            conexao.Close()

        Catch ex As Exception

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

        End Try

    End Sub



    'Cancela a transação

    Public Sub RollBackTransaction()

        Try

            conexao.RollbackTrans()

            conexao.Close()

        Catch ex As Exception

            'MsgBox("Falha de comunicação com a rede. Tente outra vez daqui alguns minutos!" _

            '       & vbNewLine & "Erro de conexão (" & Err.Number & ")" _

            '       & vbNewLine & "Descrição Do Erro (" & Err.Description & ")" _

            '       & vbNewLine & "Função (" & hlp.GetNomeFuncao & ")", vbInformation, TITULO_ALERTA)

        End Try

    End Sub



    Public Function logicoSql(ByVal argValor As Boolean) As String

        'Função que troca os valores lógicos Verdadeiro/Falso

        'para True/False para utilização em consultas SQL


        logicoSql = 1

    End Function



    Public Function pontoVirgula(ByVal varValor As Object) As String

        'Função que troca a vírgula de um valor decimal por

        'um ponto para utilização em consultas SQL



        Dim strValor As String

        Dim strInteiro As String

        Dim strDecimal As String

        Dim intPosicao As Integer



        'Converte o valor em string

        strValor = CStr(varValor)



        'Busca a posição da vírgula

        intPosicao = InStr(strValor, ",")



        'Se há uma vírgula em alguma posição

        If intPosicao > 0 Then

            'Retira a parte inteira

            strInteiro = Left(strValor, intPosicao - 1)

            'Retira a parte decimal

            strDecimal = Right(strValor, Len(strValor) - intPosicao)

            'Junta os dois novamente incluindo

            'agora o ponto no lugar da vírgula

            pontoVirgula = strInteiro & "." & strDecimal

        Else

            'Senão devolve o mesmo valor

            pontoVirgula = strValor

        End If



    End Function



    Public Function HoraSql(ByVal argData As DateTime) As String

        'Função que formata uma data para o modo SQL

        'com a cerquilha: #YYYY-MM-DD HH:MM:SS#

        'sempre retorna uma string

        Dim strDataCompleta As String

        'Remonta no formato adequado (Padrão banco de dados)

        strDataCompleta = CDate(argData).ToString("HH:mm:ss")

        HoraSql = strDataCompleta

    End Function



    Public Function dataSql(ByVal argData As DateTime) As String

        'Função que formata uma data para o modo SQL

        'com a cerquilha: #YYYY-MM-DD HH:MM:SS#

        'sempre retorna uma string

        Dim strDataCompleta As String

        'Remonta no formato adequado (Padrão banco de dados)

        strDataCompleta = CDate(argData).ToString("yyyy-MM-dd HH:mm:ss")

        dataSql = strDataCompleta

    End Function



    Public Function dataSqlAbreviada(ByVal argData As DateTime) As String

        'Função que formata uma data para o modo SQL

        'com a cerquilha: #YYYY-MM-DD HH:MM:SS#

        'sempre retorna uma string

        Dim strDataCompleta As String

        'Remonta no formato adequado (Padrão banco de dados)

        strDataCompleta = CDate(argData).ToString("yyyy-MM-dd")

        dataSqlAbreviada = strDataCompleta

    End Function





    Public Function valorSql(ByVal argValor As Object) As String

        'Função que formata valores para utilização

        'em consultas SQL

        valorSql = Nothing



        If argValor = Nothing Then

            valorSql = "Null"

        End If

        'Seleciona o tipo de valor informado

        Select Case VarType(argValor)

            'Caso seja vazio ou nulo apenas

            'devolve a string Null

            Case vbEmpty, vbNull

                valorSql = "Null"

                'Caso seja inteiro ou longo apenas

                'converte em string

            Case vbInteger, vbLong

                valorSql = CStr(argValor)

                'Caso seja simples, duplo, decimal ou moeda

                'substitui a vírgula por ponto

            Case vbSingle, vbDouble, vbDecimal, vbCurrency

                valorSql = pontoVirgula(argValor)

                'Caso seja data chama a função dataSql()

            Case vbDate

                'verifica se esta vazio e retorna Null

                'Or argValor = "00:00:00" Or argValor = "12:00:00 AM"

                Dim dataVazia As DateTime = Nothing

                If CDate(argValor).ToString("yyyy-MM-dd HH:mm:ss") = CDate(dataVazia).ToString("yyyy-MM-dd HH:mm:ss") Then

                    valorSql = "Null"

                Else

                    valorSql = dataSql(argValor)

                End If

                'Caso seja string acrescenta aspas simples

            Case vbString

                If String.IsNullOrEmpty(argValor) Or argValor = "" Then

                    'devolve a string Null

                    valorSql = "Null"

                Else

                    'acrescenta aspas simples para valores diferentes de vazio

                    valorSql = "'" & argValor & "'"

                End If

                'Caso seja lógico chama a função logicoSql()

            Case vbBoolean

                valorSql = logicoSql(argValor)

        End Select

        Return valorSql

    End Function

    'Função para retornar um valor vazio ao invés de nulo.

    'para utilização nas classes DTO

    'Para setar campo data como null/nothing:

    'campoDeData = objCon.retornaVazioParaValorNulo(drRow("data_inicial_viagem"), Nothing)

    Public Function retornaVazioParaValorNulo(ByVal valor As Object, Optional ByVal valorRetorno As Object = "") As Object

        'verificamos se a variavel esta vazia ou nulla e retornamos vazio e/ou nothing nos casos de data vazia

        If String.IsNullOrEmpty(If(IsDBNull(valor), valorRetorno, valor)) Then

            Return valorRetorno

        ElseIf IsDBNull(valor) Then 'novo

            Return valorRetorno

        Else

            Return valor

        End If

    End Function



    Public Sub salvaDataTable(tabela As String, dt As DataTable)

        Try

            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor

            Dim qtdcolunas As Integer = 0

            Dim i As Integer = 0 'contador de colunas

            Dim irow As Integer = 0 'contador de linhas

            Dim sql As String = ""

            Dim sqlValues As String = ""

            Dim sqlInto As String = ""

            Dim sqlFinal As String = ""

            Dim column As DataColumn

            Dim cont As Long = 0 'contador

            Dim totMX_ALRegistros As Long = dt.Rows.Count

            Dim linhas As Integer

            Dim colunas As Integer

            'percorre toda a datatable e incluir na tabela correspondente

            If dt.Rows.Count > 0 Then 'verifica se existem registros

                qtdcolunas = dt.Columns.Count 'calcular a qtd de colunas

                'cria sql

                sql = "Insert into [" & tabela & "] "

                sql = sql & "("

                'percorremos as colunas

                For Each column In dt.Columns

                    sqlInto = sqlInto & "[" & column.ToString & "]"

                    If i < (qtdcolunas - 1) Then

                        sqlInto = sqlInto & "," 'Incluimos a virgula em todos menos na ultima coluna

                    Else

                        sqlInto = sqlInto & ") " 'fechamos o parentese

                    End If

                    i = i + 1

                Next

                sql = sql & sqlInto & "Values("

                'percorremos as linhas

                For linhas = 0 To totMX_ALRegistros - 1 'total de registros do datatable

                    For colunas = 0 To qtdcolunas - 1 'total de colunas

                        sqlValues = sqlValues & valorSql(dt.Rows(linhas).Item(colunas))

                        If colunas < qtdcolunas - 1 Then

                            sqlValues = sqlValues & ", " 'Incluimos a virgula em todos menos na ultima coluna

                        Else

                            sqlValues = sqlValues & ") " 'fechamos o parentese

                        End If

                        'efetua o insert da linha completa

                        If colunas = qtdcolunas - 1 Then

                            cont = cont + 1 'contador geral

                            'linhas = linhas + 1 'pula para a proxima linha

                            sqlFinal = sql & sqlValues

                            ExecutaQuery(sqlFinal)

                            Application.DoEvents()

                            'colunas = 0

                            sqlValues = ""

                            sqlFinal = ""

                        End If

                    Next

                Next

            End If
            Cursor.Current = System.Windows.Forms.Cursors.Default

        Catch ex As Exception

            '       MsgBox("Ocorreu um Erro: " & Err.Number & " " & ex.Message, vbCritical, TITULO_ALERTA)

        End Try

    End Sub



    'Função utilizada para retornar a relação de usuários conectados no banco de dados

    Public Function ShowUserRosterMultipleUsers() As String

        Try

            'Dim cn As New clsConexao

            Dim rs As New ADODB.Recordset

            Dim strLista As String = ""

            Dim NrEspacos As String = ""

            Dim i As Long = 0

            Dim a As String = ""



            If conexao.State = ConnectionState.Closed Then

                Conectar()

            End If



            For i = 1 To 15

                NrEspacos = NrEspacos & " "

            Next i

            rs = conexao.OpenSchema(SchemaEnum.adSchemaProviderSpecific, , "{947bb102-5d43-11d1-bdbf-00c04fb92675}")

            'Set rs = con.ShowUsersAcess

            'Lista os usuários conectados ao database informado.

            strLista = Left(Trim(rs.Fields(0).Name) & NrEspacos, Len(NrEspacos))

            strLista = strLista & "|" & Left(Trim(rs.Fields(1).Name) & NrEspacos, Len(NrEspacos))

            strLista = strLista & "|" & Left(Trim(rs.Fields(2).Name) & NrEspacos, Len(NrEspacos))

            strLista = strLista & "|" & Left(Trim(rs.Fields(3).Name) & NrEspacos, Len(NrEspacos))

            While Not rs.EOF

                strLista = strLista & vbNewLine

                strLista = strLista & Left(Trim(rs.Fields(0).Value) & NrEspacos, Len(NrEspacos))

                strLista = strLista & "|" & Left(Trim(rs.Fields(1).Value) & NrEspacos, Len(NrEspacos))

                strLista = strLista & "|" & Left(Trim(rs.Fields(2).Value) & NrEspacos, Len(NrEspacos))

                If String.IsNullOrEmpty(Trim(rs.Fields(3).Value.ToString)) Then a = "Não"

                strLista = strLista & "|" & Left(a & NrEspacos, Len(NrEspacos))

                rs.MoveNext()

            End While

            ShowUserRosterMultipleUsers = strLista

            rs = Nothing

            Desconectar()

        Catch ex As Exception

            Desconectar()

            ' MsgBox("Ocorreu um Erro: " & Err.Number & " " & ex.Message, vbCritical, TITULO_ALERTA)

            Return ""

            Application.Exit()

        End Try

    End Function

End Class
