Public Class clsFilaDTO
    'Utilizando a nova sintaxe do .Net
    'Propriedades autoimplementadas permitem que você 
    'especifique uma propriedade de uma classe rapidamente sem precisar 
    'escrever código para os Gets e Sets.
    'Fonte: https://msdn.microsoft.com/pt-br/library/dd293589.aspx

    Public Property ID As Integer
    Public Property Fila As String
    Public Property SiglaFila As String
    Public Property Situacao As Boolean
    Public Property CapturaAutomatica As Boolean
    Public Property IDArea As Integer
    Public Property Prioridade As Integer
    Public Property Grupo As String
    Public Property enviarSMS As Boolean
    Public Property permitirAberturaManual As Boolean
    Public Property finalizaCase As Boolean
    Public Property Acao As Byte
End Class