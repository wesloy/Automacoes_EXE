Public Class clsLogImportacaoDAL
    Private dto As New clsLogImportacaoDTO
    Private objCon As New conexao
    Private sql As String
    Private hlp As New helpers
    Private dt As DataTable
    Private rs As ADODB.Recordset

    Public Function Incluir(ByVal log As clsLogImportacaoDTO) As Boolean
        sql = "Insert into MX_sysLogImportacao ("
        sql = sql & "[data], "
        sql = sql & "[funcaoExecutada], "
        sql = sql & "[erroNumero], "
        sql = sql & "[erroDescricao], "
        sql = sql & "[idUsuario], "
        sql = sql & "[versaoSis], "
        sql = sql & "[acao])"

        sql = sql & "values ( "
        sql = sql & objCon.valorSql(log.data) & ", "
        sql = sql & objCon.valorSql(log.funcaoExecutada) & ", "
        sql = sql & objCon.valorSql(log.erroNumero) & ", "
        sql = sql & objCon.valorSql(log.erroDescricao) & ", "
        sql = sql & objCon.valorSql(log.idUsuario) & ", "
        sql = sql & objCon.valorSql(log.versaoSis) & ", "
        sql = sql & objCon.valorSql(log.acao) & ") "
        Incluir = objCon.ExecutaQuery(sql)
    End Function

    'Função para capturar o log das ultimas importações no periodo de 30 dias por ação
    Public Function GetUltimosRegistrosLogImportacao() As DataTable
        sql = "SELECT L.id, L.data, L.funcaoExecutada, U.id_rede, U.nome, L.acao " 'TOP 100,
        sql = sql & "FROM MX_sysLogImportacao L INNER JOIN MX_sysUsuarios U ON L.idUsuario = U.id_rede "
        sql = sql & "WHERE format(L.data,'yyyy-MM-dd') BETWEEN " & objCon.dataSqlAbreviada(hlp.DataAbreviada.AddDays(-7)) & " AND " & objCon.dataSqlAbreviada(hlp.DataAbreviada) & " "
        sql = sql & "AND L.funcaoExecutada LIKE 'ImportarCase%' "
        sql = sql & "GROUP BY L.id, U.id_rede, L.data, L.funcaoExecutada, U.nome, L.acao "
        sql = sql & "ORDER BY L.data DESC "
        GetUltimosRegistrosLogImportacao = objCon.RetornaDataTable(sql)
    End Function

    'Função para capturar data e hora da úlitma importação
    Public Function getUltimaDataHoraUlitmaImportacao() As String
        sql = "SELECT TOP 1 MX_sysLogImportacao.data "
        sql += "FROM MX_sysLogImportacao "
        sql += "ORDER BY MX_sysLogImportacao.data DESC "
        objCon.banco_dados = "db_MaTRiX.accdb"
        rs = objCon.RetornaRs(sql)
        If rs.RecordCount > 0 Then
            getUltimaDataHoraUlitmaImportacao = rs.Fields("data").Value
        Else
            getUltimaDataHoraUlitmaImportacao = (Nothing)
        End If

        rs.Close()
        objCon.Desconectar()

    End Function

End Class