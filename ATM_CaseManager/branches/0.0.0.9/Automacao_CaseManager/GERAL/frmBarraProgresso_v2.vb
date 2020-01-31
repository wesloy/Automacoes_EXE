'O formulário está preparado para criar todos os controles em runtime
'Começamos por importar bibliotecas necessárias
Imports System.Drawing
Imports System.Drawing.Drawing2D

Public Class frmBarraProgresso_v2

    'exemplo de utilização:
    '               frmBarraProgresso_v2.Show()
    '               frmBarraProgresso_v2.ProcessaBarra(nroRegistroAtual, nroTotalDeRegistros)
    '               frmBarraProgresso_v2.Close()

    'Instanciamos um novo bitmap, com as dimensões do painel que faz de barra de progresso
    'Este bitmap vai ser usado como sua imagem de fundo e por isso é importante ter as mesmas dimensões
    Dim Progresso As New Bitmap(250, 40)
    'Instanciamos o nosso painel, que é no fundo a nossa barra de progresso
    Dim BarraProgresso As New Panel
    'Instanciamos a classe graphics para se desenhar directamente no bitmap que é agora o fundo do painel
    Dim G As Graphics = Graphics.FromImage(Progresso)

    'redimenciona o tamanho e adiciona a barra de progresso ao iniciar o form
    Private Sub Form1_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Preparamos o form para que a barra fique visível
        Me.Width = 265 '265
        Me.Height = 75 '75
        Me.Text = "Aguarde..."
        'Por fim, adicionamos o controle ao form
        Me.Controls.Add(BarraProgresso)
    End Sub

    Public Sub ProcessaBarra(ByVal i As Long, ByVal limite As Long, Optional ByVal status As String = "Aguarde...")
        'ALTERAMOS O CURSOR PARA WAIT
        Me.Text = status
        Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        'E alteramos as propriedades para a colocar na posição certa e com as dimensões certas
        With BarraProgresso
            .Width = 250
            .Height = 40
            .Location = New Point(0, 0)
            .BackgroundImage = Progresso
        End With
        'Criamos uma simulação de processo demorado, 
        'como um ciclo for do i sendo o indice até o limite e um atraso de 0 msecs por ciclo.
        'Threading.Thread.Sleep(0)
        'Calculamos o novo comprimento do rectângulo colorido (a barra de progresso)
        Dim ProgWid As Integer = Math.Round((i * 250) / limite, 0) '500
        G.Clear(Color.White)
        'Criamos um brush gradiente, só para ser mais bonito
        Dim LGF As New LinearGradientBrush(New Point(0, 0), New Point(ProgWid + 1, 0), Color.LightCyan, Color.LightSteelBlue) ' Color.LightSteelBlue
        'Desenhamos o retângulo com o gradiente e na posição précalculada
        G.FillRectangle(LGF, New Rectangle(0, 0, ProgWid, 40))
        'Para desenhar o valor da percentagem, comecemos por instanciar uma nova fonte
        Dim F As New Font("Arial", 14, FontStyle.Bold)
        'De seguida efectuamos os cálculos necessários para apresentar o valor em percentagem
        Dim ProgLabel As String = CStr(Math.Round((ProgWid / 250) * 100, 0)) & "%"
        'Medimos o seu tamanho final, essencial para entrar o valor na barra
        Dim Tam As SizeF = G.MeasureString(ProgLabel, F)
        'E desenhamos o valor por cima da barra, com os cálculos de posição que o façam ficar ao centro
        G.DrawString(ProgLabel, F, Brushes.Black, (250 / 2) - (Tam.Width / 2), (40 / 2) - (Tam.Height / 2))
        'Refresh na barra, para que possa ser atualizada a cada ciclo
        BarraProgresso.Refresh()
        'E por fim, forçamos a aplicação a fazer o que tem a fazer, mesmo que isso signifique perda de performance
        'Isto é importante para que a aplicação não fique sem responder durante o processo
        Application.DoEvents()
        'Cursor.Current = System.Windows.Forms.Cursors.Default
    End Sub
End Class