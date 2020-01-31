
Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Configuration
Public Class BLLClasseCasos

    Shared Function Add(obj As ClasseCasos) As Boolean
        Try
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Shared Function RetornarSQL(sql As String, Optional Quant As Integer = 0) As List(Of ClasseCasos)
        Try
            Dim con As New SqlConnection(ConfigurationManager.ConnectionStrings("stringconexao").ConnectionString)
            Dim cmd As New SqlCommand(sql, con)

            Dim list As New List(Of ClasseCasos)
            'valorDespesa,	id,	cartao,	dataRegistro,	estabelecimentoCodigo,	Finalizacao_ID,	Subfinalizacao_ID 
            con.Open()
            If Quant <> 0 Then
                Dim quantCMD As New SqlCommand(sql, con)
                Dim QuantRd As SqlDataReader = cmd.ExecuteReader()

                While QuantRd.Read()
                    Dim wun As New ClasseCasos
                    If Frm_Applicacao.MandeiParar = True Then
                        Return Nothing
                    End If
                    wun.iCodMatrix = QuantRd.Item("quant")
                    list.Add(wun)
                    con.Close()
                    Return list
                End While
            End If

            Dim rd As SqlDataReader = cmd.ExecuteReader()

            While rd.Read()
                Dim Caso As New ClasseCasos
                If Frm_Applicacao.MandeiParar = True Then
                    Return Nothing
                End If
                Caso.iCodMatrix = rd.Item("id")
                Caso.sCategoriaCase = Util.TratarNuloString(rd.Item("Finalizacaocase"))
                Caso.sSubcategoriaCase = Util.TratarNuloString(rd.Item("Subfinalizacaocase"))
                Caso.sDataTransmissao = rd.Item("dataRegistro").ToString
                Caso.sEstabelecimento = rd.Item("estabelecimentoCodigo")
                Caso.sNroDoCartao = rd.Item("cartao").PadLeft(19, "0")
                If Caso.sNroDoCartao.Length < 19 Then
                    Caso.sNroDoCartao = Caso.sNroDoCartao.PadLeft(19, "0")
                End If
                Caso.sValor = rd.Item("valorDespesa")
                Caso.iFinalizarCase = rd.Item("FinalizarCase")
                Caso.sDataFinalizacaoCase = Util.TratarNuloData(rd.Item("DataFinalizacaoCase"))
                Caso.ierroFinalizaCase = rd.Item("erroFinalizaCase")
                Caso.Tratado_Automacao_CASE = rd.Item("Tratado_Automacao_CASE")
                Caso.finalizar_Case_Especifico = rd.Item("finalizar_Case_Especifico")
                list.Add(Caso)
            End While
            con.Close()
            Return list
        Catch ex As Exception
            Return Nothing
        End Try
    End Function


    Shared Function Atualizar(obj As ClasseCasos) As Boolean
        'Dim enviando As New EnvioM
        Try
            Dim cone As New ConexaoE
            Dim sql As String
            Dim CartaoSemZeros As Long
            CartaoSemZeros = obj.sNroDoCartao
            sql = "UPDATE MX_bMaTRiX SET Finalizarcase = " & obj.iFinalizarCase & ",DataFinalizacaoCase = '" & cone.dataSql(obj.sDataFinalizacaoCase) & "' , erroFinalizaCase = " & obj.ierroFinalizaCase & ", Tratado_Automacao_CASE = " & obj.Tratado_Automacao_CASE & ", finalizaCaseOBS= '" & obj.finalizaCaseOBS & "' WHERE id = " & obj.iCodMatrix & ""
            Threading.Thread.Sleep(2000)
            cone.ExecutaQuery(sql)
            Return True
            'LIMPAR ESSES CASOS " Select c.id, c.valorDespesa,		c.cartao,	c.dataRegistro,	c.estabelecimentoCodigo, 
            'f.Finalizacaocase,	sf.Subfinalizacaocase,c.FinalizarCase,c.erroFinalizaCase,
            'c.DataFinalizacaoCase FROM MX_bMaTRiX c 
            'left join MX_sysFinalizacao f on f.id = c.Finalizacao_ID 
            'left join MX_sysSubFinalizacao sf on sf.id = c.Subfinalizacao_ID 
            'where c.finalizacao_id > 200"
        Catch ex As Exception
            'MsgBox("Erro ao atualizar")
            Frm_Applicacao.ListComandos.Items.Add("Erro ao atualizar no MATRIX")
            Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
            'enviando.getMensageSms("Alerta Case Manager ERRO: Erro ao atualizar no MATRIX")
            Return False
        End Try

    End Function
End Class

