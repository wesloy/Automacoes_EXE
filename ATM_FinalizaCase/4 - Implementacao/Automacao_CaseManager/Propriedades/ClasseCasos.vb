Public Class ClasseCasos
    Public Property sNroDoCartao As String
    Public Property sEstabelecimento As String
    Public Property sValor As String
    Public Property sDataTransmissao As String

#Region "Categoria Matrix"
    Public Property sCategoriaMatrix As String
    Public Property iCodCategoriaMatrix
#End Region

#Region "Subcategoria Matrix"
    Public Property iCodSubCategoriaMatrix As Integer
    Public Property sSubCategoriaMatrix As String
#End Region

#Region "Categoria Case"
    Public Property iCodCategoriaCase As Integer
    Public Property sCategoriaCase As String
#End Region

#Region "Categoria Case"
    Public Property iCodSubCategoriaCase As Integer
    Public Property sSubcategoriaCase As String
#End Region

    Public Property iCodMatrix As Integer
    Public Property sDataFinalizacaoCase As String
    Public Property iFinalizarCase As Integer
    Public Property ierroFinalizaCase As Integer
    Public Property Tratado_Automacao_CASE As Integer
    Public Property finalizaCaseOBS As String
    Public Property finalizar_Case_Especifico As Boolean
End Class
