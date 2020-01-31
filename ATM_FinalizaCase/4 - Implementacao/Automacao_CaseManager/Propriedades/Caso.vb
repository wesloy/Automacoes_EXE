Public Class Caso

    Public Property id As Integer

    Public Property resposta As String

    Public Property nome As String

    Public Property existeAdicional As Boolean

    Public Property nomeAdicional As String

    Public Property cpf As String

    Public Property telefone As String

    Public Property finalCartao As String

    Public Property produto As String

    Public Property filaDeTrabalho As String

    Public Property idFilaDeTrabalho As String

    Public Property dataCriacao As String

    Public Property dataRespondida As String

    Public Property status As String

    Public Property despesas() As List(Of Despesa)

End Class
