Module SMS

    Private hlp As New helpers
    'Retorna as configurações dos IDS do portal SMS
    Public Function retornaIDCamposSMS(ambiente As String, categoria As String) As Algar.SMS.objetos.IDCamposSMS
        Try
            Dim conRN_RCSMS As New Algar.Utils.Conexao(Algar.Utils.Conexao.FLAG_SGBD.SQL, ALGAR_PWD, ALGAR_BD, ALGAR_SERVIDOR, ALGAR_USER, "")
            Dim sql As String
            'Dim X As New Algar.SMS.funcoes
            'Dim O As New Algar.SMS.objetos

            Dim fieldsSMS As New Algar.SMS.objetos.IDCamposSMS
            Dim dt As DataTable
            sql = "SELECT * FROM ATM_SYSATENDIMENTOSMS WHERE ambiente = '" & ambiente.ToString & "' AND categoria = '" & categoria.ToString & "' ORDER BY ID ASC"
            dt = conRN_RCSMS.retornaDataTable(sql)
            If dt.Rows.Count > 0 Then 'verifica se existem registros
                fieldsSMS.fields = New List(Of Algar.SMS.objetos.CamposSMS)
                For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim                
                    With fieldsSMS
                        .fields.Add(New Algar.SMS.objetos.CamposSMS With {
                                .campo = drRow("campo").ToString,
                                .attendanceTypeId = drRow("attendanceTypeId").ToString,
                                .fieldId = drRow("fieldId").ToString,
                                .value = drRow("value").ToString,
                                .groupId = drRow("groupId").ToString,
                                .groupNumber = 0})
                    End With
                Next drRow
            End If
            Return fieldsSMS
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'RETORNA UMA LISTA DAS FILAS
    Public Function listagemFilasSMS(ByVal grupo As String, Optional filtraSomenteFilaDeEnvio As Boolean = False) As List(Of Algar.SMS.objetos.SmsFilas)
        Try
            Dim Conect As New Algar.Utils.Conexao(Algar.Utils.Conexao.FLAG_SGBD.SQL, ALGAR_PWD, ALGAR_BD, ALGAR_SERVIDOR, ALGAR_USER, "")
            Dim ListFilas As New List(Of Algar.SMS.objetos.SmsFilas)
            Dim dt As DataTable
            Dim sql As String
            Dim fnc As New Algar.SMS.funcoes
            sql = "select * from ATM_sysFilasSMS "
            sql += "where ativo = 1 "
            sql += "and grupo = '" & grupo.ToString & "' "
            sql += "and ambiente = '" & AMBIENTE.ToString & "' "
            If filtraSomenteFilaDeEnvio Then
                sql += "and filadeEnvio = " & Conect.logicoSql(filtraSomenteFilaDeEnvio) & " "
            End If
            dt = Conect.retornaDataTable(sql)
            ListFilas = fnc.listagemFilasSMS(dt)
            Return ListFilas
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'ENVIA UMA MENSAGEM DE ALERTA
    Public Function EnviaAlerta(mensagem As String) As Boolean
        Try
            Dim dataAtual As String = CDate(Now).ToString("dd/MM/yyyy")
            Dim json As String = ""
            Dim retorno As Boolean = False
            Dim fnc As New Algar.SMS.funcoes
            Dim dt As DataTable
            Dim listaTelefone As New List(Of Object)
            Dim hostname As String = Environ("COMPUTERNAME").ToString.ToUpper
            Dim con As New Algar.Utils.Conexao(Algar.Utils.Conexao.FLAG_SGBD.SQL, ALGAR_PWD, ALGAR_BD, ALGAR_SERVIDOR, ALGAR_USER, "")
            dt = con.retornaDataTable("select telefone from ATM_sysTelefoneAlertas where ativo = 1")

            Dim mensagemFormatada As String
            mensagemFormatada = "Alerta! IMPORTADOR CM: " & mensagem.ToString

            'Pulando processo de envio se a máq for a de construção
            If hostname = "BRA-MAC001" Then
                Return True
            End If

            'Apenas para o cel do Eloy
            'With listaTelefone
            '    .Add("34991772053") 'TIM
            'End With
            'fnc.enviaAlerta(SMS_USER, SMS_PWD, mensagemFormatada, listaTelefone, idCAMPOS_ENVIO_TOKEN)

            'Captura a lista de telefones para o envio
            If dt.Rows.Count > 0 Then
                For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim                
                    With listaTelefone
                        .Add(drRow("telefone").ToString)
                    End With
                Next
                fnc.enviaAlerta(SMS_USER, SMS_PWD, mensagemFormatada, listaTelefone, idCAMPOS_ENVIO_TOKEN)
            End If
            Return True
        Catch ex As Exception
            MsgBox("Erro ao enviar Alerta de SMS!" & vbNewLine & ex.Message, vbCritical, TITULO_ALERTA)
            Environment.Exit(1)
            Return False
        End Try
    End Function

End Module

