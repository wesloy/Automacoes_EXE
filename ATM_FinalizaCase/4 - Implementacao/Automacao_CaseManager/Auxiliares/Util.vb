Public Class Util
    Public Shared Function TratarNuloData(ByVal Conteudo As Object) As Object

        If IsDBNull(Conteudo) = True Then

            Return "0001/01/01 00:00:00"

        Else
            If (Conteudo.ToString() = String.Empty) Then
                Return "0001/01/01 00:00:00"
                '  Return Now
            Else
                Return Conteudo
            End If

        End If

    End Function
    Public Shared Function TratarNuloString(ByVal Conteudo As Object) As Object
        If IsDBNull(Conteudo) = True Then
            Return ""

        Else
            Return Conteudo

        End If

    End Function



End Class
