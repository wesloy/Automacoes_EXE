Imports System.Configuration
Imports System.Net
Imports Bradesco.Fraude.VO.VO
Imports Bradesco.Fraudes.Componentes.WSReferences.SmsAlgar
Imports System.Runtime.Serialization
Imports Utilitario

Public Class EnvioM

    Function getValueSms() As ResquetParameterSmsAlgar
        Dim b As New ResquetParameterSmsAlgar
        Dim crip As New Cripta
        b.uri = "https://apismsalgar.algartech.com/api/token"
        b.username = ConfigurationManager.AppSettings("SMS_USER").ToString '"automacao_fraude"
        b.passWord = crip.Decrypt(ConfigurationManager.AppSettings("SMS_PWD").ToString) '"NHhQMUtscVJQSGxlc2xUNw==")
        b.aplicationDesc = "application/x-www-form-urlencoded"
        b.credential = CredentialCache.DefaultNetworkCredentials
        Return b
    End Function

    Sub getMensageSms(Mensagem As String)

        'Dim enviaMensagem As New Utilitario.Slack()
        'enviaMensagem.PostaMensagem("#alertacasemanager", Mensagem, "xoxb-46637047718-jelcVyv10hEjuqd3DP4InLNS")
        If UCase(Environ("USERNAME")) = "A003440" Then
        Else
            Try

                Dim ListaTelefone As New List(Of String)

                For i As Integer = 1 To 10
                    If ConfigurationManager.AppSettings("CelularEnvio" & i).ToString <> "" Then
                        ListaTelefone.Add(ConfigurationManager.AppSettings("CelularEnvio" & i).ToString)
                    End If
                Next

                For Each celular As String In ListaTelefone
                    Dim sms As ResquetParameterSmsAlgar = getValueSms()
                    'desativado solicitação Wladmir
                    Dim rest As String = HandlerSmsAlgar(Of ResquetParameterSmsAlgar).getTokenSms(sms)

                    Dim aut As AuthenticationToken = HandlerSmsAlgar(Of AuthenticationToken).getResponseByRequet(rest)

                    HandlerSmsAlgar(Of String).getTokenSms(sms)

                    Dim mensages As MensageSms = New MensageSms()
                    mensages.attendanceTypeId = "70234668-9b77-e711-999a-bc305bce3806"
                    mensages.authenticationToken = aut
                    mensages.field = New Fields()
                    mensages.field.fieldId = "94f4ba8d-9977-e711-999a-bc305bce3806"
                    mensages.field.value = celular ' "DDD999112150"
                    mensages.field2 = New Fields()
                    mensages.field2.fieldId = "95f4ba8d-9977-e711-999a-bc305bce3806"
                    mensages.field2.value = Mensagem ' "Alerta Case Manager: ON"
                    mensages.uri = "https://apismsalgar.algartech.com/api/attendances"
                    HandlerSmsAlgar(Of ResquetParameterSmsAlgar).getTokenSms(mensages)
                Next
            Catch ex As Exception
                Frm_Applicacao.ListComandos.Items.Add("Erro: " & Mensagem)
        End Try

        End If
    End Sub

End Class
