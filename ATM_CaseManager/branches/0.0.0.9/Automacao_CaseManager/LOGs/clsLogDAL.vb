'Classe DAL
Public Class clsLogDAL
    Private dto As New clsLogDTO
    Private objCon As New conexao
    Private sql As String

    Public Function Incluir(ByVal log As clsLogDTO) As Boolean
        sql = "Insert into sysLOG ("
        sql = sql & "[data], "
        sql = sql & "[funcaoExecutada], "
        sql = sql & "[erroNumero], "
        sql = sql & "[erroDescricao], "
        sql = sql & "[idUsuario], "
        sql = sql & "[versaoSis], "
        sql = sql & "[idioma], "
        sql = sql & "[hostname], "
        sql = sql & "[acao], "
        sql = sql & "[ferramenta]) "
        sql = sql & "values ( "
        sql = sql & objCon.valorSql(log.data) & ", "
        sql = sql & objCon.valorSql(log.funcaoExecutada) & ", "
        sql = sql & objCon.valorSql(log.erroNumero) & ", "
        sql = sql & objCon.valorSql(log.erroDescricao) & ", "
        sql = sql & objCon.valorSql(log.idUsuario) & ", "
        sql = sql & objCon.valorSql(log.versaoSis) & ", "
        sql = sql & objCon.valorSql(log.idiomaPC) & ", "
        sql = sql & objCon.valorSql(log.hostname) & ", "
        sql = sql & objCon.valorSql(log.acao) & ", "
        sql = sql & objCon.valorSql(log.ferramenta) & ") "
        Incluir = objCon.ExecutaQuery(sql)
    End Function
End Class