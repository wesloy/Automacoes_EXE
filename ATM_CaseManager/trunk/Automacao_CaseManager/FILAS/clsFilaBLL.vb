Public Class clsFilaBLL
    Private dal As New clsFilaDAL
    Private dt As New DataTable
    Private dto As New clsFilaDTO

    Public Function getIdFilaPorPrioridade(ByVal prioridade As Integer, ByVal idArea As Integer) As Integer
        Return dal.getIdFilaPorPrioridade(prioridade, idArea)
    End Function

    Public Function GetIdFilaPorNome(ByVal NomeFila As String) As Integer
        Return dal.GetIdFilaPorNome(NomeFila)
    End Function

    Public Function GetDescricaoFilaPorID(IDFILA As Integer) As String
        Dim fdto As New clsFilaDTO
        fdto = GetFilaPorCodigo(IDFILA)
        Return fdto.Fila
    End Function
    Public Function GetFinalizaCasePorID(id_fila As Integer) As Boolean
        Dim fdto As New clsFilaDTO
        fdto = GetFilaPorCodigo(id_fila)
        Return fdto.FinalizaCase
    End Function

    Public Sub PreencheComboFila(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0)
        dal.GetComboboxFila(frm, cb, area)
    End Sub
    Public Sub PreencheComboFilasImportacaoManual(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0)
        dal.GetComboboxFilasAberturaProducaoManual(frm, cb, area)
    End Sub
    Public Sub PreencheComboFilasAuditoria(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0, Optional somenteComVolume As Boolean = True)
        dal.GetComboboxFilasAuditoria(frm, cb, area, somenteComVolume)
    End Sub
    Public Sub PreencheComboFilasMonitoriaEspecial(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0, Optional somenteComVolume As Boolean = True)
        dal.GetComboboxFilasMonitoriaEspecial(frm, cb, area, somenteComVolume)
    End Sub

    Public Sub PreencheComboFilaAutomatica(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0, Optional somenteComVolume As Boolean = True, Optional siglaFila As String = "sigla")
        dal.GetComboboxFilaAutomatica(frm, cb, area, somenteComVolume, siglaFila)
    End Sub
    Public Sub PreencherComboProdutoParaFilasComVolume(frm As Form, cb As ComboBox, Optional ByVal fila_id As Integer = 0)
        dal.GetComboboxProdutosParaFilaComVolume(frm, cb, fila_id)
    End Sub
    Public Sub PreencheComboFilaManual(frm As Form, cb As ComboBox, Optional ByVal area As Integer = 0)
        dal.GetComboboxFilaManual(frm, cb, area)
    End Sub
    Public Function GetFilaPorCodigo(ByVal _filaId As Integer) As clsFilaDTO
        Return dal.GetFilaPorId(_filaId)
    End Function
    Public Function GetIdAreaPorFila(ByVal _fila As String) As Long
        Return dal.GetIdAreaFila(_fila)
    End Function
    Public Function GetIdNeppoPorFila(ByVal _fila As String) As Long
        Return dal.GetIdNeppoFila(_fila)
    End Function
    Public Function DeletaFila(ByVal _filaId As Integer) As Boolean
        Return dal.DeletaFilaPorId(_filaId)
    End Function
    Public Function AlterarStatusTodasFilas(ByVal Situacao As Boolean, ByVal idArea As Integer) As Boolean
        Return dal.AlterarStatusTodasFilas(Situacao, idArea)
    End Function

    Public Function AlterarPrioridadeFilas(ByVal prioridade As Integer, ByVal idfila As Integer) As Boolean
        Return dal.alterarPrioridadeFilas(prioridade, idfila)
    End Function

    Public Function getMenorPrioridade() As Integer
        Dim dtoFila As New clsFilaDTO
        dtoFila = dal.getFilaDeMenorPrioridade 'Captura a fila de menor prioridade 
        If IsNothing(dtoFila) Then
            Return 1
        Else
            Return dtoFila.Prioridade
        End If

    End Function

    Public Function propagarAlteracaoIdArea(ByVal nomeTabela As String, ByVal idArea As Integer, ByVal idFila As Integer) As Boolean
        Return dal.propagarAlteracaoIdArea(nomeTabela, idArea, idFila)
    End Function

End Class
