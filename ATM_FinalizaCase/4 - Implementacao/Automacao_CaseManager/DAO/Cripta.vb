Public Class Cripta
    Public Function Decrypt(str As String) As String

        Dim b As Byte() = Convert.FromBase64String(str)

        Dim decryp As String = System.Text.ASCIIEncoding.ASCII.GetString(b)

        Return decryp

    End Function

    Public Function Encrypt(str As String) As String

        Dim b As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(str)

        Dim encryp As String = Convert.ToBase64String(b)

        Return encryp

    End Function

End Class
