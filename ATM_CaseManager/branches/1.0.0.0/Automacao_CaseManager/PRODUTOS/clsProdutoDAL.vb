Public Class clsProdutoDAL
    Private sql As String
    Private objCon As New conexao
    Private dt As DataTable
    Private hlp As New helpers

    Public Function getProdutoPorBin(strBin As String) As String 'Função para capturar a descricao do produto por bin
        Dim produto As String = "N/I"
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                produto = drRow("produto")
            Next drRow
        Else
            produto = "N/I"
        End If
        Return produto
    End Function

    'Função para capturar o ID do produto fornecendo o BIN para busca
    Public Function getIdProdutoPorBIN(strBin As String) As Integer
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                Return drRow("id")
            Next drRow
        Else
            Return 1
        End If
        Return 1
    End Function

    'Função para capturar a EPS do produto por bin
    Public Function getEpsPorBin(strBin As String) As String
        Dim eps As String = "N/I"
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                eps = drRow("eps").ToString
            Next drRow
        Else
            eps = "N/I"
        End If
        Return eps
    End Function

    'Função para capturar a Prioridade do produto por bin
    Public Function getPrioridadePorBin(strBin As String) As Long
        Dim x As Long = "0"
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                x = drRow("prioridade").ToString
            Next drRow
        Else
            x = 0
        End If
        Return x
    End Function

    'Função para capturar a Bandeira por bin
    Public Function getBandeiraPorBin(strBin As String) As String
        Dim ban As String = "N/I"
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                ban = drRow("emissor").ToString
            Next drRow
        Else
            ban = "N/I"
        End If
        Return ban
    End Function

    'Função para capturar a Tipo do Cartão por bin
    Public Function getTipoCartaoPorBin(strBin As String) As String
        Dim x As String = "N/I"
        sql = "select * from sysProdutos where bin = " & objCon.valorSql(strBin)
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                x = drRow("tipoCartao").ToString
            Next drRow
        Else
            x = "N/I"
        End If
        Return x
    End Function



    'Função para capturar o Produto pelo ID da tabela
    Public Function getProdutoPorId(ByVal idPRoduto As Integer) As String
        Dim x As String = "N/I"
        sql = "select * from sysProdutos where id = " & objCon.valorSql(idPRoduto)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                x = drRow("produto").ToString.ToUpper
            Next drRow
        Else
            x = "N/I"
        End If
        Return x
    End Function
    Public Function getFullTipoCartao() As DataTable
        Try
            sql = "select tipoCartao from sysProdutos Group By tipoCartao where produto <> 'DECOMISSIONADO' order by tipoCartao"
            dt = objCon.RetornaDataTable(sql)
        Catch ex As Exception
            dt = Nothing
        End Try
        Return dt
    End Function

    Public Function getFullProduto() As DataTable
        Try
            sql = "select produto from sysProdutos Group By produto where produto <> 'DECOMISSIONADO' order by produto"
            dt = objCon.RetornaDataTable(sql)
        Catch ex As Exception
            dt = Nothing
        End Try
        Return dt
    End Function

    Public Sub GetComboboxProduto(frm As Form, cb As ComboBox)
        hlp.carregaComboBox("select 1 as id_reg, produto from sysProdutos Where produto <> 'DECOMISSIONADO' Group By produto order by produto", frm, cb)
    End Sub
    Public Sub GetComboboxTipoCartao(frm As Form, cb As ComboBox)
        hlp.carregaComboBox("select 1 as id_reg, tipoCartao from sysProdutos Where produto <> 'DECOMISSIONADO' Group By tipoCartao order by tipoCartao", frm, cb)
    End Sub
End Class

