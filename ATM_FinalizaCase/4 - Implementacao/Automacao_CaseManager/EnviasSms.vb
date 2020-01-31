Imports System.IO
Imports System.Net
Imports System.Web.Script.Serialization
Imports Bradesco.Fraudes.Componentes.WSReferences.SmsAlgar
Imports RestSharp
'Public Class EnviasSms
'    'Shared Sub AlertaParadaAutomacao(motivo As String)
'    '    Try
'    '        Dim EnviaMens As New EnviasSms
'    '        Dim xml As String
'    '        Dim dataAtual As String = CDate(Now).ToString("dd/MM/yyyy")
'    '        Dim json As String = ""
'    '        Dim retorno As Boolean = False
'    '        Dim dt As DataTable
'    '        Dim con As New ConexaoE
'    '        con.banco_dados = "db_atm.accdb"
'    '        dt = con.RetornaDataTable("select telefone from ATM_sysTelefoneAlertas where ativo = 1")

'    '        If dt.Rows.Count > 0 Then 'verifica se existem registros                    
'    '            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim                    
'    '                EnviaMens.EnviaSms(motivo.ToString, "3499950") 'motivo.tostring, drRow("telefone").ToString)
'    '            Next drRow
'    '            con.Desconectar()
'    '        End If

'    '    Catch ex As Exception
'    '        Frm_Applicacao.ListComandos.Items.Add("Erro ao enviar Alerta de SMS no COREO!" & vbNewLine & ex.Message)
'    '        Frm_Applicacao.ListComandos.SetSelected(Frm_Applicacao.ListComandos.Items.Count - 1, True)
'    '        Environment.Exit(1)
'    '    End Try
'    'End Sub



'    'Public Function EnviaSms(ByVal motivo As String, telefone As String) As String

'    '    'IDS:
'    '    'OPERAÇÃO           = d7777e34-30f5-e611-80d5-000c29872643
'    '    'attendanceTypeId   = 70234668-9b77-e711-999a-bc305bce3806
'    '    'CELULAR            = 94f4ba8d-9977-e711-999a-bc305bce3806
'    '    'MENSAGEM           = 95f4ba8d-9977-e711-999a-bc305bce3806

'    '    Try
'    '        Dim h As HandlerSmsAlgar(Of ClasseCasos)


'    '        Dim objAtendimento As New AttendanceView
'    '        Dim retorno As String = ""
'    '        Dim mensagem As String
'    '        Dim json As New JavaScriptSerializer()
'    '        json.MaxJsonLength = Int32.MaxValue
'    '        mensagem = "Alerta de ATM: "
'    '        mensagem += motivo.ToString
'    '        mensagem += "."

'    '        With objAtendimento
'    '            .attendanceTypeId = "70234668-9b77-e711-999a-bc305bce3806"
'    '            .fields = New List(Of FieldValueView)

'    '            'CELULAR
'    '            .fields.Add(New FieldValueView With {
'    '                                            .fieldId = "94f4ba8d-9977-e711-999a-bc305bce3806",
'    '                                            .value = telefone,
'    '                                            .groupId = "",
'    '                                            .groupNumber = 0})
'    '            'TOKEN
'    '            .fields.Add(New FieldValueView With {
'    '                                            .fieldId = "95f4ba8d-9977-e711-999a-bc305bce3806",
'    '                                            .value = mensagem,
'    '                                            .groupId = "",
'    '                                            .groupNumber = 0})

'    '            sJson = json.Serialize(objAtendimento)
'    '            json = PostData(SMS_URL_ATTEND, sJson, getToken)
'    '            If Left(sJson, 1) = "{" Then
'    '                retorno = True
'    '            Else
'    '                retorno = False
'    '            End If
'    '            Return retorno
'    '        End With
'    '    Catch ex As Exception
'    '        Return False
'    '    End Try

'    'End Function
'    Public Function PostData(ByVal url As String, ByVal strJSON As String, ByVal token As String) As String
'        Try
'            Dim strURL As String
'            Dim myWebReq As HttpWebRequest
'            Dim myWebResp As HttpWebResponse
'            Dim encoding As New System.Text.UTF8Encoding
'            Dim getData__1 As String = ""
'            Dim sr As StreamReader
'            getData__1 = getData__1 & strJSON
'            Dim data As Byte() = encoding.GetBytes(getData__1)
'            strURL = url
'            myWebReq = DirectCast(WebRequest.Create(strURL), HttpWebRequest)
'            myWebReq.ContentType = "application/json; charset=utf-8"
'            myWebReq.Headers.Add("Authorization", "Bearer " & token)
'            myWebReq.ContentLength = data.Length
'            myWebReq.Method = "POST"
'            myWebReq.KeepAlive = True
'            If myWebReq.Proxy IsNot Nothing Then
'                myWebReq.Proxy.Credentials = CredentialCache.DefaultCredentials
'            End If
'            Dim myStream As Stream = myWebReq.GetRequestStream()
'            If data.Length > 0 Then
'                myStream.Write(data, 0, data.Length)
'                myStream.Close()
'            End If
'            myWebResp = DirectCast(myWebReq.GetResponse(), HttpWebResponse)
'            sr = New StreamReader(myWebResp.GetResponseStream())
'            Dim strJSON__2 As String = sr.ReadToEnd()
'            Dim HTTP_Status_Code As Integer = myWebResp.StatusCode
'            '201: OK
'            If HTTP_Status_Code = 201 Then
'                'Dim j As Object = New JavaScriptSerializer().Deserialize(Of Object)(strJSON__2)
'                'j.("nomecampo")
'                Return strJSON__2
'            Else
'                Return ""
'            End If
'        Catch e As Exception
'            'MsgBox("An error occurred: " & e.Message, vbCritical)
'            Return ""
'        End Try
'    End Function

'    'Public Function getToken() As String
'    '    Dim json As New JavaScriptSerializer()
'    '    json.MaxJsonLength = Int32.MaxValue
'    '    Dim obj As New Token
'    '    sJson = GetKey(SMS_URL, param)
'    '    obj = json.Deserialize(Of Token)(sJson)
'    '    Return obj.access_token.ToString
'    'End Function

'End Class

'IDS:

'CELULAR = d9777e34 - 30.0F5-e611-80d5-000c29872643

'NOME = da777e34 - 30.0F5-e611-80d5-000c29872643

'CPF = db777e34 - 30.0F5-e611-80d5-000c29872643

'FINALCARTAO = dc777e34 - 30.0F5-e611-80d5-000c29872643

'PRODUTO = dd777e34 - 30.0F5-e611-80d5-000c29872643

'FERRAMENTA = de777e34 - 30.0F5-e611-80d5-000c29872643



'DATA = e1777e34 - 30.0F5-e611-80d5-000c29872643

'VALOR = e2777e34 - 30.0F5-e611-80d5-000c29872643

'STATUS = e3777e34 - 30.0F5-e611-80d5-000c29872643

'ESTAB = e4777e34 - 30.0F5-e611-80d5-000c29872643

'groupid = e0777e34 - 30.0F5-e611-80d5-000c29872643

'groupNumber = 0,1,2,3 (NRO DE DESPESAS)
