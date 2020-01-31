
Public Class mdlOrdenacaoListView

    'INSERIR ESTE CÓDIGO NA AÇÃO: CLICK COLUNA do LISTVIEW

    'If Me.ListView1.Sorting = SortOrder.Ascending Then
    '    Me.ListView1.Sorting = SortOrder.Descending
    'Else
    '    Me.ListView1.Sorting = SortOrder.Ascending
    'End If
    'Me.ListView1.ListViewItemSorter = New mdlOrdenacaoListView(e.Column, Me.ListView1.Sorting)


    Implements IComparer
    Private Coluna As Integer
    Private Ordem As SortOrder

    Public Sub New()
        Me.Coluna = 0
        Me.Ordem = System.Windows.Forms.SortOrder.Ascending
    End Sub

    Public Sub New(ByVal pColuna As Integer)
        Me.Coluna = pColuna
        Me.Ordem = System.Windows.Forms.SortOrder.Ascending
    End Sub

    Public Sub New(ByVal column As Integer, ByVal s As SortOrder)
        Me.Coluna = column
        Me.Ordem = s
    End Sub

    Public Function Compare(ByVal x As Object, ByVal y As Object) As Integer Implements IComparer.Compare
        If Me.Ordem = System.Windows.Forms.SortOrder.Ascending Then
            Return String.Compare(CType(x, ListViewItem).SubItems(Me.Coluna).Text, CType(y, ListViewItem).SubItems(Me.Coluna).Text)
        Else
            Return String.Compare(CType(y, ListViewItem).SubItems(Me.Coluna).Text, CType(x, ListViewItem).SubItems(Me.Coluna).Text)
        End If

    End Function
End Class
