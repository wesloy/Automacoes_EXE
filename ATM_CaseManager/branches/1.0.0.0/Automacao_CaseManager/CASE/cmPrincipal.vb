Public Class cmPrincipal
    Private CM As New funcoes_CM
    Private Sub btnCM_Click(sender As Object, e As EventArgs) Handles btnCM.Click

        txtUsuarioCM.Text = "UB057219"
        txtSenhaCM.Text = "123"

        'Verificar se o Case já está aberto
        If CM.validarTelaPrincipalAberta() Then
            CM.capturarAlertas()
        End If

        'Verificar se tela de Login está aberta
        If CM.validarTelaLoginAberta() Then
            'Logando
            CM.login(Me.txtUsuarioCM.Text, Me.txtSenhaCM.Text, True)
        Else
            'Abrindo o CM
            If CM.iniciarCM Then
                If CM.login(Me.txtUsuarioCM.Text, Me.txtSenhaCM.Text, True) Then
                    'Após logar corretamente capturar os registros
                    CM.capturarAlertas()
                End If
            End If
        End If


    End Sub
End Class