Public Class clsProdutoBLL
    Private dt As DataTable
    Private dal As New clsProdutoDAL

    Public Function GetNomeProdutoPorID(ByVal idProduto As Integer) As String 'Retorna o nome do produto por ID da tabela
        Return dal.getProdutoPorId(idProduto)
    End Function
    'Retorna o ID do produto por BIN
    Public Function getIdProdutoPorBIN(strBin As String) As Integer
        Return dal.getIdProdutoPorBIN(strBin)
    End Function

    'Retorna o produto
    Public Function getNomeProdutoPorBIN(strBin As String) As String
        Return dal.getProdutoPorBin(strBin)
    End Function

    'Retorna a EPS do produto
    Public Function getEpsProduto(strBin As String) As String
        Return dal.getEpsPorBin(strBin)
    End Function

    'Retorna a PRIORIDADE do produto
    Public Function getPrioridadeProduto(strBin As String) As Long
        Return dal.getPrioridadePorBin(strBin)
    End Function

    'Retorna a EMISSOR (Bandeira) do produto
    Public Function getEmissorProduto(strBin As String) As String
        Return dal.getBandeiraPorBin(strBin)
    End Function

    'Retorna a tipo do cartão pf ou cnpj do produto
    Public Function getTipoCartao(strBin As String) As String
        Return dal.getTipoCartaoPorBin(strBin)
    End Function

    Public Function getFullTipoCartao() As DataTable
        Return dal.getFullTipoCartao
    End Function
    Public Function getFullProduto() As DataTable
        Return dal.getFullProduto
    End Function
    Public Sub PreencheComboProduto(frm As Form, cb As ComboBox)
        dal.GetComboboxProduto(frm, cb)
    End Sub
    Public Sub PreencheComboTipoCartao(frm As Form, cb As ComboBox)
        dal.GetComboboxTipoCartao(frm, cb)
    End Sub
End Class
