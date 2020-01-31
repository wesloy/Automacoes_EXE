Imports System.Data.OleDb
Imports System.Data.Odbc
Imports System.Data.SqlClient
Imports System.Configuration

Public Class Conexao

    Dim HoraInicial
    Dim HoraFinal
    Dim HoraCon


    Public Function GetConexao() As ADODB.Connection

        Dim Conexao As New ADODB.Connection

        Try
            Conexao.Open(ConfigurationManager.ConnectionStrings("stringconexao").ConnectionString)

        Catch ex As Exception

            HoraFinal = Format(Now, "HH:mm:ss")
            HoraCon = (TimeSpan.Parse(HoraFinal) - TimeSpan.Parse(HoraInicial)).ToString

            If MsgBox("Erro Interno: Não foi possível estabelecer uma conexão com o banco de dados, clique em Retry para tentar novamente ou cancele para fechar o aplicativo. " & vbCrLf & ex.Message & vbCrLf & "Tempo de Conexão: " & HoraCon, MsgBoxStyle.RetryCancel Or MsgBoxStyle.Exclamation) = MsgBoxResult.Retry Then

                GetConexao()

            Else

                Application.Exit()

            End If

        End Try

        Return Conexao

    End Function

    Public Shared Sub FechaConexao(ByVal Conexao As ADODB.Connection)
        Try

            If Conexao IsNot Nothing AndAlso Conexao.State <> ConnectionState.Closed Then
                Conexao.Close()
            End If


        Catch ex As Exception

            Console.WriteLine("Erro para fechar conexão " & Conexao.ToString * "|" & ex.Message)

        End Try

    End Sub

    Public Function Conectar(ByVal sql As String, ByVal Conexao As ADODB.Connection) As ADODB.Recordset

        Dim RST As New ADODB.Recordset

        Try

            HoraInicial = Format(Now, "HH:mm:ss")
            RST.Open(sql, Conexao, ADODB.CursorTypeEnum.adOpenKeyset, ADODB.LockTypeEnum.adLockOptimistic)

        Catch ex As Exception

            HoraFinal = Format(Now, "HH:mm:ss")
            HoraCon = (TimeSpan.Parse(HoraFinal) - TimeSpan.Parse(HoraInicial)).ToString
            MsgBox("Erro Interno: Não foi possível estabelecer uma conexão com o banco de dados atraves da RecordSet, por favor tente novamente. " & vbCrLf & ex.Message & vbCrLf & "Tempo de Conexão: " & HoraCon, MsgBoxStyle.Exclamation)

        End Try

        Return RST

    End Function

End Class
