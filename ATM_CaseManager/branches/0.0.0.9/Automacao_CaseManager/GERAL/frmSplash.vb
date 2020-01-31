Public Class frmSplash
    Private hlp As New helpers
    Private time As New Timer()

    Private Sub frmSplash_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Version.Text = "Versão: " & My.Application.Info.Version.ToString
        Copyright.Text = My.Application.Info.Copyright.ToString
        Company.Text = My.Application.Info.CompanyName.ToString
        desenvolvido.Text = My.Application.Info.Description.ToString

        Try
            'Validação se a máquina é a correta para se inicializar a automação
            If HOSTNAME_ATM <> hlp.hostnameLocal Then
                MsgBox("Não é possível iniciar a automação nesta máquina!", vbInformation, TITULO_ALERTA)
                hlp.registrarLOG("PADRAO: " & HOSTNAME_ATM, "TENTATIVA: " & hlp.hostnameLocal, "CM IMPORTADOR", "INICIAR EM MÁQUINA NÃO CADASTRADA")
                hlp.fecharAplicativo(False)
            Else
                'Enviando SMS de inicialização da aplicação
                InitializeMyTimer()
                EnviaAlerta("INICIADO")
            End If
        Catch ex As Exception
            hlp.fecharAplicativo(False)
        End Try

    End Sub

    Private Sub IncreaseProgressBar(ByVal sender As Object, ByVal e As EventArgs)
        Try
            'ProgressBar1.Increment(10)
            ProgressBar1.Value = ProgressBar1.Value + 10
            ProgressBar1.Minimum = 0
            ProgressBar1.Maximum = 100
            'Debug.Print(ProgressBar1.Value)
            If ProgressBar1.Value >= ProgressBar1.Maximum Then
                hlp.abrirForm(frmGestorImportacao)
                time.Stop()
            End If
        Catch ex As Exception
            hlp.fecharAplicativo(False)
        End Try

    End Sub

    Private Sub InitializeMyTimer()
        Try
            time.Enabled = True
            time.Interval = 110
            'milisegundos 1000 = 1 segundo
            AddHandler time.Tick, AddressOf IncreaseProgressBar
            time.Start()
        Catch ex As Exception
            hlp.fecharAplicativo(False)
        End Try
    End Sub


End Class
