Public Class clsLogImportacaoBLL
    Private objDAL As New clsLogImportacaoDAL
    Private dt As New DataTable
    'Private ObjUsu As New clsUsuariosBLL
    Private hlp As New helpers

    Public Sub registrarLogImportacao(Optional ByVal erroNumero As String = "", Optional ByVal erroDescricao As String = "", Optional ByVal funcaoExecutada As Object = "", Optional ByVal acao As String = "")
        Dim logDTO As New clsLogImportacaoDTO
        Dim logDAL As New clsLogImportacaoDAL

        With logDTO
            .data = Now()
            .idUsuario = hlp.capturaIdRede()
            .erroNumero = erroNumero.ToString
            .erroDescricao = erroDescricao.ToString
            .funcaoExecutada = funcaoExecutada.ToString & "_ATM"
            .versaoSis = hlp.versaoSistema()
            .acao = acao.ToString
        End With
        logDAL.Incluir(logDTO)
    End Sub

    'Função para capturar última atualização da base
    Public Function ultimaImportacao() As String
        Return objDAL.getUltimaDataHoraUlitmaImportacao()
    End Function

    'Função para carregar o listview de usuario
    Public Function AtualizaListViewLogImportacao() As Boolean
        Dim cont As Long = 0
        dt = objDAL.GetUltimosRegistrosLogImportacao()
        frmGestorImportacao.ListView1.Clear()
        'AJUSTANDO AS COLUNAS
        With frmGestorImportacao.ListView1
            .View = View.Details
            .LabelEdit = False
            .CheckBoxes = False
            .SmallImageList = imglist() 'Utilizando um modulo publico
            .GridLines = True
            .FullRowSelect = True ' True
            .HideSelection = False
            .MultiSelect = False
            .Columns.Add("", 20, HorizontalAlignment.Left) 'ID
            .Columns.Add("Última Data", 130, HorizontalAlignment.Left)
            .Columns.Add("Função", 110, HorizontalAlignment.Left)
            .Columns.Add("ID Rede", 80, HorizontalAlignment.Left)
            .Columns.Add("Analista", 120, HorizontalAlignment.Left)
            .Columns.Add("Log", 500, HorizontalAlignment.Left)
        End With
        'Dim i As Integer = 0
        'POPULANDO
        If dt.Rows.Count > 0 Then 'verifica se existem registros
            For Each drRow As DataRow In dt.Rows
                Dim item As New ListViewItem()
                item.Text = drRow("id")
                item.SubItems.Add("" & drRow("data"))
                item.SubItems.Add("" & drRow("funcaoExecutada"))
                item.SubItems.Add("" & drRow("id_rede"))
                item.SubItems.Add("" & hlp.abreviaNome(drRow("nome")))
                item.SubItems.Add("" & drRow("acao"))
                item.ImageKey = 11
                frmGestorImportacao.ListView1.Items.Add(item)
            Next drRow
        Else
            'frmGestorImportacao.txtTotalCasosPendentes.Text = 0
        End If
        Return True
    End Function

End Class
