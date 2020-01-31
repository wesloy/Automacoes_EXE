Public Class clsFilaDAL
    Private objCon As New conexao
    Private rs As ADODB.Recordset
    Private sql As String
    Private fila As New clsFilaDTO
    Private dt As DataTable
    Private hlp As New helpers
    Private dto As New clsFilaDTO

    Public Function DeletaFilaPorId(ByVal _filaId As Integer) As Boolean 'deleta as informações por id

        Try
            sql = "Delete from MX_sysFilas where id=" & objCon.valorSql(_filaId)
            objCon.banco_dados = "db_MaTRiX.accdb"
            DeletaFilaPorId = objCon.ExecutaQuery(sql)
            If DeletaFilaPorId Then
                hlp.registrarLOG(_filaId, , hlp.GetNomeFuncao, "MANUTENÇÃO FILAS")
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function

    'parametro de filtro opcional
    Public Function GetFilas(Optional ByVal filtro As String = "") As DataTable
        sql = "Select MX_sysFilas.*, MX_sysAreas.area AS DescricaoArea "
        sql = sql & "from MX_sysFilas "
        sql = sql & "LEFT JOIN MX_sysAreas ON MX_sysFilas.idArea = MX_sysAreas.id "
        sql = sql & "where (fila like ('" & filtro & "%') " 'filtro opcional por fila
        sql = sql & "or sigla_fila like ('" & filtro & "%')) " 'filtro opcional por sigla
        sql = sql & "order by MX_sysFilas.idarea ASC, MX_sysFilas.captura_automatica asc "
        objCon.banco_dados = "db_MaTRiX.accdb"
        GetFilas = objCon.RetornaDataTable(sql)
    End Function

    'captura as informações por id e retorna o objeto
    Public Function GetFilaPorId(ByVal _filaId As Integer) As clsFilaDTO
        sql = "Select * from MX_sysFilas where id= " & objCon.valorSql(_filaId)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                With fila
                    .ID = objCon.retornaVazioParaValorNulo(drRow("id"))
                    .Fila = objCon.retornaVazioParaValorNulo(drRow("fila"))
                    .SiglaFila = objCon.retornaVazioParaValorNulo(drRow("sigla_fila"))
                    .Situacao = objCon.retornaVazioParaValorNulo(drRow("situacao"))
                    .CapturaAutomatica = objCon.retornaVazioParaValorNulo(drRow("captura_automatica"))
                    .IDArea = objCon.retornaVazioParaValorNulo(drRow("idArea"))
                    .Prioridade = objCon.retornaVazioParaValorNulo(drRow("prioridade"))
                    .Grupo = objCon.retornaVazioParaValorNulo(drRow("grupo"))
                    .enviarSMS = objCon.retornaVazioParaValorNulo(drRow("enviarSMS"))
                    .permitirAberturaManual = objCon.retornaVazioParaValorNulo(drRow("permitirAberturaManual"))
                    .finalizaCase = objCon.retornaVazioParaValorNulo(drRow("finalizaCase"))
                End With
            Next drRow
        End If
        Return fila
    End Function
    'captura o id da Area por Fila
    Public Function GetIdAreaFila(strFila As String) As Integer
        sql = "Select * from MX_sysFilas where fila = " & objCon.valorSql(strFila)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        Dim i As Long = 0
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                i = objCon.retornaVazioParaValorNulo(drRow("idArea"))
            Next drRow
        End If
        Return i
    End Function
    'captura o id da Area por Fila
    Public Function GetIdNeppoFila(strFila As String) As Integer
        sql = "Select * from MX_sysFilas where fila = " & objCon.valorSql(strFila)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        Dim i As Long = 0
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                i = objCon.retornaVazioParaValorNulo(drRow("fila_idNeppo"))
            Next drRow
        End If
        Return i
    End Function

    'incluir no banco de dados o objeto passado via parametro
    Public Function Incluir(ByVal _fila As clsFilaDTO) As Boolean
        Try
            sql = "Insert into MX_sysFilas "
            sql = sql & "(fila,"
            sql = sql & "sigla_fila,"
            sql = sql & "situacao,"
            sql = sql & "captura_automatica,"
            sql = sql & "idArea, "
            sql = sql & "grupo, "
            sql = sql & "enviarSMS, "
            sql = sql & "permitirAberturaManual, "
            sql = sql & "prioridade) "
            sql = sql & "values( "
            sql = sql & objCon.valorSql(_fila.Fila) & ","
            sql = sql & objCon.valorSql(_fila.SiglaFila) & ","
            sql = sql & objCon.valorSql(_fila.Situacao) & ","
            sql = sql & objCon.valorSql(_fila.CapturaAutomatica) & ","
            sql = sql & objCon.valorSql(_fila.IDArea) & ","
            sql = sql & objCon.valorSql(_fila.Grupo) & ", "
            sql = sql & objCon.valorSql(_fila.enviarSMS) & ", "
            sql = sql & objCon.valorSql(_fila.permitirAberturaManual) & ", "
            sql = sql & objCon.valorSql(_fila.Prioridade) & ")"
            objCon.banco_dados = "db_MaTRiX.accdb"
            Incluir = objCon.ExecutaQuery(sql)
            If Incluir Then
                hlp.registrarLOG(0, _fila.Fila.Trim, hlp.GetNomeFuncao, "MANUTENÇÃO FILAS")
            End If
        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function Atualizar(ByVal _fila As clsFilaDTO) As Boolean

        Try
            sql = "Update MX_sysFilas "
            sql = sql & "set fila = " & objCon.valorSql(_fila.Fila.Trim) & ","
            sql = sql & "sigla_fila = " & objCon.valorSql(_fila.SiglaFila.Trim) & ","
            sql = sql & "situacao = " & objCon.valorSql(_fila.Situacao) & ","
            sql = sql & "captura_automatica = " & objCon.valorSql(_fila.CapturaAutomatica) & ","
            sql = sql & "idArea = " & objCon.valorSql(_fila.IDArea) & ","
            sql = sql & "permitirAberturaManual = " & objCon.valorSql(_fila.permitirAberturaManual) & ","
            sql = sql & "enviarSMS = " & objCon.valorSql(_fila.enviarSMS) & ","
            sql = sql & "grupo = " & objCon.valorSql(_fila.Grupo) & " "
            sql = sql & "where id = " & objCon.valorSql(_fila.ID) & " "
            objCon.banco_dados = "db_MaTRiX.accdb"
            Atualizar = objCon.ExecutaQuery(sql)
            If Atualizar Then
                hlp.registrarLOG(_fila.ID, _fila.Fila.Trim, hlp.GetNomeFuncao, "MANUTENÇÃO FILAS")
            End If
            Return Atualizar
        Catch ex As Exception
            Return False
        End Try

    End Function

    'função que verifica se ja existe fila cadastrada
    Public Function ValidaDuplicidade(ByVal _fila As String, ByVal id_registro As Integer, ByVal area As Integer) As Boolean
        sql = "Select * from MX_sysFilas where fila = " & objCon.valorSql(_fila.Trim)
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                If id_registro = drRow("id") Or area <> drRow("idArea") Then
                    ValidaDuplicidade = True 'retorna true nos casos em que pode alterar
                Else
                    ValidaDuplicidade = False 'Não permite inclusão/alteração pois já existe
                End If
            Next drRow
        Else
            ValidaDuplicidade = True 'Deixa incluir
        End If
        Return ValidaDuplicidade
    End Function

    'Alterar PRIORIDADE de fila AUTOMÁTICA
    'Exemplo:
    'FILA DE MAIOR PRIORIDADE = 1
    'FILA SEQUENCIAL = 2
    'FILA SEQUENCIAL = 3
    'FILA DE MENOR PRIORIDADE = 4
    Public Function alterarPrioridadeFilas(ByVal prioridade As Integer, ByVal idFila As Integer) As Boolean
        sql = "Update MX_sysFilas SET "
        sql += "Prioridade = " & objCon.valorSql(prioridade) & " "
        sql += "WHERE MX_sysFilas.id = " & objCon.valorSql(idFila) & " "
        objCon.banco_dados = "db_MaTRiX.accdb"
        Return objCon.ExecutaQuery(sql)

    End Function

    'Capturar o ID da fila pela PRIORIDADE
    Public Function getIdFilaPorPrioridade(ByVal prioridade As Integer, ByVal idArea As Integer) As Integer
        sql = "Select ID from MX_sysFilas "
        sql = sql & "WHERE (MX_sysFilas.prioridade = " & objCon.valorSql(prioridade) & ") "
        sql = sql & "AND (MX_sysFilas.captura_automatica = " & objCon.valorSql(True) & ") "
        sql = sql & "AND (MX_sysFilas.idArea = " & idArea & ") "
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)

        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                Return drRow("ID")
            Next drRow
        End If
        Return 0
    End Function

    'Capturar o ID da Fila pelo nome
    Public Function GetIdFilaPorNome(ByVal NomeFila As String) As Integer
        sql = "Select ID from MX_sysFilas WHERE MX_sysFilas.Fila like " & objCon.valorSql(NomeFila) & " "
        objCon.banco_dados = "db_MaTRiX.accdb"
        dt = objCon.RetornaDataTable(sql)

        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows 'efetua o loop até o fim
                Return drRow("ID")
            Next drRow
        End If
        Return 0
    End Function
    'Todas as filas ou todas as filas por área
    Public Sub GetComboboxFila(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0)
        sql = "Select MX_sysFilas.id, MX_sysFilas.fila from MX_sysFilas where situacao = " & objCon.valorSql(True) & " "
        If area <> 0 Then
            sql = sql & "and idarea = " & area & " "
        End If
        sql = sql & "order by MX_sysFilas.fila asc"
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.carregaComboBox(sql, frm, cb)
    End Sub

    'Filas que podem ter abertura manual 
    Public Sub GetComboboxFilasAberturaProducaoManual(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0)
        sql = "Select MX_sysFilas.id, MX_sysFilas.fila from MX_sysFilas where situacao = " & objCon.valorSql(True) & " and permitirAberturaManual = " & objCon.valorSql(True) & " "
        If area <> 0 Then
            sql = sql & "and idarea = " & area & " "
        End If
        sql = sql & "order by MX_sysFilas.fila asc"
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.carregaComboBox(sql, frm, cb)
    End Sub

    'listagem de filas com volume para trabalho e também de todas as filas AUDITORIA
    Public Sub GetComboboxFilasAuditoria(frm As Form, cb As ComboBox, ByVal area As Integer, Optional somenteComVolume As Boolean = True)

        Dim data As Date = hlp.FormataDataAbreviada(Now)

        hlp.CursorPointer(True)

        sql = "Select MX_sysFilas.id, MX_sysFilas.fila "

        If somenteComVolume Then
            ''FILAS COM VOLUME
            Select Case area
                Case 5 'Auditoria
                    sql = sql & "FROM MX_bMaTRiX INNER JOIN MX_sysFilas ON MX_bMaTRiX.auditoriaFilaOrigem_id = MX_sysFilas.id "
                Case 6 'Monitoria
                    sql = sql & "FROM MX_bMaTRiX INNER JOIN MX_sysFilas ON MX_bMaTRiX.fila_ID = MX_sysFilas.id "
            End Select

            sql = sql & "where "
            sql = sql & "(MX_bMaTRiX.status = 0 ) " 'status de casos que ainda não foram trabalhados
            sql = sql & "and DateValue(MX_bMaTRiX.dataImportacao) <= " & objCon.dataSqlAbreviada(data) & " "
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_bMaTRiX.status, MX_bMaTRiX.area_ID "
            sql = sql & "HAVING (MX_bMaTRiX.area_ID = " & area & ")"


        Else
            ''RELACAO DAS FILAS
            sql = sql & "from MX_sysFilas "
            sql = sql & "where "
            sql = sql & "(MX_sysFilas.situacao = " & objCon.valorSql(True) & ") " 'Pode-se Auditar qualquer registro de qualquer fila, basta que a finalização/subFinalização esteja cadastrada para rotear
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_sysFilas.situacao "

        End If

        sql = sql & "order by MX_sysFilas.fila asc"
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.carregaComboBox(sql, frm, cb)
        hlp.CursorPointer(False)
    End Sub
    'Filas de MONITORIA ESPECIAL, cartões de Altos Executivos
    Public Sub GetComboboxFilasMonitoriaEspecial(frm As Form, cb As ComboBox, ByVal area As Integer, Optional somenteComVolume As Boolean = True)

        Dim data As Date = hlp.FormataDataAbreviada(Now)

        hlp.CursorPointer(True)

        sql = "Select MX_sysFilas.id, MX_sysFilas.fila "

        If somenteComVolume Then

            sql = sql & "FROM MX_bMaTRiX INNER JOIN MX_sysFilas ON MX_bMaTRiX.fila_ID = MX_sysFilas.id "
            sql = sql & "where "
            sql = sql & "(MX_bMaTRiX.status = 0 ) " 'status de casos que ainda não foram trabalhados
            sql = sql & "AND (MX_sysFilas.monitoriaEspecial = True) "
            sql = sql & "and DateValue(MX_bMaTRiX.dataImportacao) <= " & objCon.dataSqlAbreviada(data) & " "
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_bMaTRiX.status, MX_bMaTRiX.area_ID "
            sql = sql & "HAVING (MX_bMaTRiX.area_ID = " & area & ") "


        Else
            ''RELACAO DAS FILAS
            sql = sql & "from MX_sysFilas "
            sql = sql & "where "
            sql = sql & "(MX_sysFilas.situacao = " & objCon.valorSql(True) & ") " 'Pode-se monitorar qualquer registro de qualquer fila, basta que a finalização/subFinalização esteja cadastrada para rotear
            sql = sql & "AND (MX_sysFilas.monitoriaEspecial = True) "
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_sysFilas.situacao, MX_sysFilas.monitoriaEspecial "

        End If

        sql = sql & "order by MX_sysFilas.fila asc"
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.carregaComboBox(sql, frm, cb)
        hlp.CursorPointer(False)

    End Sub

    'listagem de produtos com volume para trabalho
    Public Sub GetComboboxProdutosParaFilaComVolume(frm As Form, cb As ComboBox, ByVal fila_id As Integer)
        Dim data As Date = hlp.FormataDataAbreviada(Now)

        hlp.CursorPointer(True)

        sql = "Select 1 as id, MX_bMaTRiX.produto "
        sql = sql & "FROM MX_bMaTRiX "
        sql = sql & "where "
        sql = sql & "(MX_bMaTRiX.status = 0) " 'status de casos que ainda não foram trabalhados
        sql = sql & "And (MX_bMaTRiX.Fila_id = " & fila_id & ") "
        sql = sql & "and Format(MX_bMaTRiX.dataImportacao,'yyyy-MM-dd') <= " & objCon.dataSqlAbreviada(data) & " "
        sql = sql & "GROUP BY MX_bMaTRiX.produto "
        sql = sql & "order by MX_bMaTRiX.produto desc"
        hlp.carregaComboBox(sql, frm, cb)
        hlp.CursorPointer(False)

    End Sub


    'listagem de filas com volume para trabalho e também de todas as filas
    Public Sub GetComboboxFilaAutomatica(frm As Form, cb As ComboBox, ByVal area As Integer, Optional somenteComVolume As Boolean = True, Optional siglaFila As String = "sigla")

        Dim data As Date = hlp.FormataDataAbreviada(Now)

        hlp.CursorPointer(True)

        sql = "Select MX_sysFilas.id, MX_sysFilas.fila "

        If somenteComVolume Then
            ''FILAS COM VOLUME
            sql = sql & "FROM MX_bMaTRiX INNER JOIN MX_sysFilas On MX_bMaTRiX.fila_id = MX_sysFilas.id "
            sql = sql & "where "
            sql = sql & "(MX_sysFilas.situacao = " & objCon.valorSql(True) & ") "
            sql = sql & "And (MX_sysFilas.captura_automatica = " & objCon.valorSql(True) & ") "
            sql = sql & "And (MX_bMaTRiX.status = 0 ) " 'status de casos que ainda não foram trabalhados
            sql = sql & "And (MX_sysFilas.idarea = " & area & ") "
            Select Case siglaFila 'Captura específica de grupos de filas
                Case "QPREA"
                    sql = sql & "And (MX_sysFilas.sigla_fila = " & objCon.valorSql(siglaFila) & ") "
                    sql = sql & "And (MX_sysFilas.ID = " & objCon.valorSql(56) & ") " 'Foi limitado o clean up para a fila ALT B2K
                    sql = sql & "And (MX_bMaTRiX.qpreaCleanUp_Executado = " & objCon.valorSql(False) & ") "
                Case Else
                    sql = sql & "And Format(MX_bMaTRiX.dataImportacao,'yyyy-MM-dd') <= " & objCon.dataSqlAbreviada(data) & " "
            End Select
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_sysFilas.situacao, MX_sysFilas.captura_automatica, MX_bMaTRiX.status, MX_sysFilas.idArea "

        Else
            ''RELACAO DAS FILAS
            sql = sql & "from MX_sysFilas "
            sql = sql & "where "
            sql = sql & "(MX_sysFilas.situacao = " & objCon.valorSql(True) & ") "
            sql = sql & "and (MX_sysFilas.captura_automatica = " & objCon.valorSql(True) & ") "
            sql = sql & "and (MX_sysFilas.idarea = " & area & ") "
            Select Case siglaFila 'Captura específica de grupos de filas
                Case "QPREA"
                    sql = sql & "And (MX_sysFilas.sigla_fila = " & objCon.valorSql(siglaFila) & ") "
                    sql = sql & "And (MX_sysFilas.ID = " & objCon.valorSql(56) & ") " 'Foi limitado o clean up para a fila ALT B2K
            End Select
            sql = sql & "GROUP BY MX_sysFilas.id, MX_sysFilas.fila, MX_sysFilas.situacao, MX_sysFilas.captura_automatica, MX_sysFilas.idArea "

        End If

        sql = sql & "order by MX_sysFilas.fila asc"
        hlp.carregaComboBox(sql, frm, cb)
        hlp.CursorPointer(False)
    End Sub

    Public Sub GetComboboxFilaManual(frm As Form, cb As ComboBox, ByVal area As Integer)

        hlp.CursorPointer(True)
        sql = "Select MX_sysFilas.id, MX_sysFilas.fila "
        sql = sql & "FROM MX_sysFilas "
        sql = sql & "where "
        sql = sql & "(MX_sysFilas.situacao = " & objCon.valorSql(True) & ") "
        sql = sql & "and (MX_sysFilas.idarea = " & area & ") "
        sql = sql & "and (MX_sysFilas.permitirAberturaManual = " & objCon.valorSql(True) & ") "
        'sql = sql & "and (MX_sysFilas.id <> 31 and MX_sysFilas.id <> 35) " 'Retirando filas de reentrada
        sql = sql & "order by MX_sysFilas.fila asc"
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.carregaComboBox(sql, frm, cb)
        hlp.CursorPointer(False)

    End Sub


    'captura MENOR prioridade cadastrada (MAIOR NÚMERO DE FILA)
    Public Function getFilaDeMenorPrioridade() As clsFilaDTO
        sql = "SELECT TOP 1 * FROM MX_sysFilas "
        sql = sql & "WHERE (MX_sysFilas.[situacao] = 1) And (MX_sysFilas.[captura_automatica] = 1) "
        sql = sql & "ORDER BY MX_sysFilas.[prioridade] DESC"
        objCon.banco_dados = "db_MaTRiX.accdb"
        rs = objCon.RetornaRs(sql)

        If rs.RecordCount = 0 Then
            rs.Close()
            objCon.Desconectar()
            Return Nothing
        End If

        If rs("id").Value > 0 Then
            Return GetFilaPorId(rs("id").Value)
        Else
            rs.Close()
            objCon.Desconectar()
            Return Nothing
        End If
    End Function

    Public Function AlterarStatusTodasFilas(ByVal situacao As Boolean, ByVal idArea As Integer) As Boolean
        sql = "Update MX_sysFilas Set "
        sql = sql & "Situacao = " & objCon.valorSql(situacao) & " "
        sql = sql & "WHERE idArea = " & objCon.valorSql(idArea) & " "
        objCon.banco_dados = "db_MaTRiX.accdb"
        hlp.registrarLOG(, , hlp.GetNomeFuncao, situacao)
        Return objCon.ExecutaQuery(sql)
    End Function

    'Esta manutenção foi necessária porque na criação foi estabelecido DUPLO ID nas tabelas (MX_bMaTRiX, MX_sysFinalizacao, MX_sysSubFinalizacao, MX_sysFilasDeAcessoUsuario, MX_sysPlanejamento)
    'de forma desnecessária, poderia apenas ser o ID de Fila. Como o programa já está em utilização e existe inúmeras amarrações, optei em fazer esta adequação.
    Public Function propagarAlteracaoIdArea(ByVal nomeTabela As String, ByVal idArea As Integer, ByVal idFila As Integer) As Boolean

        Select Case nomeTabela.ToUpper
            Case "MATRIX"
                sql = "UPDATE MX_bMaTRiX T SET T.area_ID = " & objCon.valorSql(idArea)
                sql += " WHERE (T.fila_ID = " & objCon.valorSql(idFila) & ") "

            Case "FINALIZACAO"
                sql = "UPDATE MX_sysFinalizacao T SET T.idArea = " & objCon.valorSql(idArea)
                sql += " WHERE (T.idFila = " & objCon.valorSql(idFila) & ") "

            Case "SUBFINALIZACAO"
                sql = "UPDATE MX_sysSubFinalizacao T SET T.idArea = " & objCon.valorSql(idArea)
                sql += " WHERE (T.idFila = " & objCon.valorSql(idFila) & ") "

            Case "FILASPORUSUARIOS"
                sql = "UPDATE MX_sysFilasDeAcessoUsuario T SET T.id_area = " & objCon.valorSql(idArea)
                sql += " WHERE (T.id_fila = " & objCon.valorSql(idFila) & ") "

            Case "VOLPLANEJADO"
                sql = "UPDATE MX_sysPlanejamento T SET T.areaID =  " & objCon.valorSql(idArea)
                sql += " WHERE (T.filaID = " & objCon.valorSql(idFila) & ") "

        End Select

        objCon.banco_dados = "db_MaTRiX.accdb"
        Return objCon.ExecutaQuery(sql)

    End Function


End Class


