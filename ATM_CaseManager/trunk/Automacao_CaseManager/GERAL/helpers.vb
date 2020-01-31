'Objetivo: oferecer funcionalidades de validação de dados e transformação de valores
'métodos genéricos
Imports System.Reflection
Imports System.Globalization
Imports System.Threading
Imports System.IO
Imports System.Text.RegularExpressions
Imports Scripting

Public Class helpers
    'Função para validar o preenchimento de campos obrigatórios
    'argForm = nome do formulario
    'strCamposObrigatorios = lista do com o nome dos campos separados por ";"
    'tituloCampos = lista dos titulos dos campos na mesma ordem e separados por ";"
    'validaCamposObrigatorios(Me, "nomeCampo1;nomeCampo2;etc", "TituloCampo1;TituloCampo2;etc")
    Public Function validaCamposObrigatorios(ByVal argForm As Control, ByVal strCamposObrigatorios As String, Optional ByVal tituloCampos As String = "") As Boolean
        Dim nomeCampos As Object
        Dim campos As Object
        Dim valor As Object
        Dim i As Long
        Dim inicio As Long
        Dim fim As Long
        Dim ctrl As String
        'Windows.Forms.Form
        'monta os arrays
        campos = Split(strCamposObrigatorios, ";")
        nomeCampos = Split(tituloCampos, ";")
        'captura o inicio e fim do array
        inicio = LBound(campos)
        fim = UBound(campos)
        i = inicio

        'inicia a validação uma a uma
        For i = inicio To fim
            'captura o nome do tipo de campo
            ctrl = argForm.Controls(campos(i)).GetType.Name
            Select Case ctrl
                'Caso seja ComboBox
                Case "ComboBox"
                    valor = argForm.Controls(campos(i)).Text
                    If String.IsNullOrEmpty(valor) Then
                        MsgBox("Uma opção: " & argForm.Controls(campos(i)).Tag & ". Deve ser selecionada.", MsgBoxStyle.Information, TITULO_ALERTA)
                        'argForm(campos(i)).SetFocus() 'Coloca o cursor no campo
                        argForm.Controls(campos(i)).Focus()
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                        validaCamposObrigatorios = False
                        Exit Function
                    Else
                        'Altera a cor de fundo para branco
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.White
                    End If
                    'Caso seja TextBox
                Case "TextBox"
                    valor = argForm.Controls(campos(i)).Text
                    If String.IsNullOrEmpty(valor) Then
                        MsgBox("O Campo: " & argForm.Controls(campos(i)).Tag & ". Deve ser preenchido.", MsgBoxStyle.Information, TITULO_ALERTA)
                        argForm.Controls(campos(i)).Focus()
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                        validaCamposObrigatorios = False
                        Exit Function
                    Else
                        'Altera a cor de fundo para branco
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.White
                    End If
                    'Caso seja MaskeCheckBox
                Case "MaskedTextBox"
                    'tira a formatação
                    argForm.Controls(campos(i)).TextMaskFormat = MaskFormat.ExcludePromptAndLiterals
                    'captura o valor sem a mascara
                    valor = argForm.Controls(campos(i)).Text
                    'retorna a formatação
                    argForm.Controls(campos(i)).TextMaskFormat = MaskFormat.IncludePromptAndLiterals
                    If String.IsNullOrEmpty(valor) Then
                        MsgBox("O Campo: " & argForm.Controls(campos(i)).Tag & ". Deve ser preenchido.", MsgBoxStyle.Information, TITULO_ALERTA)
                        argForm.Controls(campos(i)).Focus()
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                        validaCamposObrigatorios = False
                        Exit Function
                    Else
                        'Altera a cor de fundo para branco
                        argForm.Controls(campos(i)).BackColor = System.Drawing.Color.White
                    End If
                    'Caso seja CheckBox (Normalmente esta campo é opcional)
                Case "CheckBox"
                    '    'Caso seja OptionButton
                    'Case "OptionButton"
                    '    valor = argForm.Controls(campos(i)).Text
                    '    If String.IsNullOrEmpty(valor) Then
                    '        MsgBox("Uma opção: " & argForm.Controls(campos(i)).Tag & ". Deve ser selecionada.", MsgBoxStyle.Information, TITULO_ALERTA)
                    '        argForm.Controls(campos(i)).Focus()
                    '        'argForm.Controls(campos(i)).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                    '        validaCamposObrigatorios = False
                    '        Exit Function
                    '    End If

                    '    'Caso seja OptionGroup
                    'Case "OptionGroup"
                    '    valor = argForm.Controls(campos(i)).Text
                    '    If String.IsNullOrEmpty(valor) Then
                    '        MsgBox("Uma opção: " & argForm.Controls(campos(i)).Tag & ". Deve ser selecionada.", MsgBoxStyle.Information, TITULO_ALERTA)
                    '        argForm.Controls(campos(i)).Focus()
                    '        'argForm.Controls(campos(i)).BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                    '        validaCamposObrigatorios = False
                    '        Exit Function
                    '    End If

            End Select
        Next i
        validaCamposObrigatorios = True
    End Function
    ''Limpa objetos de um determinado formulário
    Public Sub LimparCampos(ByRef Tela As Control)
        'Caso ocorra erro, não mostrar o erro, ignorando e indo para á próxima linha
        On Error Resume Next
        'Declaramos uma variavel Campo do tipo Object
        '(Tipo Object porque iremos trabalhar com todos os campos do Form, podendo ser
        '       Label, Button, TextBox, ComboBox e outros)
        Dim Campo As Object
        'Usaremos For Each para passarmos por todos os controls do objeto atual
        For Each Campo In Tela.Controls
            'Verifica se o Campo é um GroupBox, TabPage ou Panel
            'Se for então precisa limpar os campos que estão dentro dele também...
            'Chamaremos novamente a função mas passando por referencia
            '      O GroupBox, TabPage ou Panel atual
            If TypeOf Campo Is System.Windows.Forms.GroupBox Or
                TypeOf Campo Is System.Windows.Forms.TabPage Or
                TypeOf Campo Is System.Windows.Forms.Panel Then
                LimparCampos(Campo)
            ElseIf TypeOf Campo Is System.Windows.Forms.TextBox Then
                Campo.Text = String.Empty 'Verificamos se o campo é uma TextBox se for então devemos limpar o campo
            ElseIf TypeOf Campo Is System.Windows.Forms.ComboBox Then
                'Verificamos se o campo é um ComboBox
                If Campo.DropDownStyle = ComboBoxStyle.DropDownList Then
                    Campo.SelectedIndex = -1 'Se o tipo da ComboBox for DropDownList então devemos deixar sem seleção
                    'ElseIf Campo.DropDownStyle = ComboBoxStyle.DropDown Then
                    'Campo.Text = ""
                Else
                    'Campo.Text = String.Empty
                    Campo.SelectedValue = 0
                End If
            ElseIf TypeOf Campo Is System.Windows.Forms.CheckBox Then
                Campo.Checked = False
            ElseIf TypeOf Campo Is System.Windows.Forms.DataGridView Then
                Campo.DataSource = Nothing
            ElseIf TypeOf Campo Is System.Windows.Forms.RadioButton Then
                Campo.Checked = False
            ElseIf TypeOf Campo Is System.Windows.Forms.MaskedTextBox Then
                Campo.Text = String.Empty
            End If
        Next

    End Sub


    Public Function validaCPF(ByVal argCpf As String) As Boolean
        'Função que verifica a validade de um CPF.
        Dim wSomaDosProdutos
        Dim wResto
        Dim wDigitChk1
        Dim wDigitChk2
        Dim wI
        'Inicia o valor da Soma
        wSomaDosProdutos = 0
        'Para posição I de 1 até 9
        For wI = 1 To 9
            'Soma = Soma + (valor da posição dentro do CPF x (11 - posição))
            wSomaDosProdutos = wSomaDosProdutos + Val(Mid(argCpf, wI, 1)) * (11 - wI)
        Next wI
        'Resto = Soma - ((parte inteira da divisão da Soma por 11) x 11)
        wResto = wSomaDosProdutos - Int(wSomaDosProdutos / 11) * 11
        'Dígito verificador 1 = 0 (se Resto=0 ou 1 ) ou 11 - Resto (nos casos restantes)
        wDigitChk1 = IIf(wResto = 0 Or wResto = 1, 0, 11 - wResto)
        'Reinicia o valor da Soma
        wSomaDosProdutos = 0
        'Para posição I de 1 até 9
        For wI = 1 To 9
            'Soma = Soma + (valor da posição dentro do CPF x (12 - posição))
            wSomaDosProdutos = wSomaDosProdutos + (Val(Mid(argCpf, wI, 1)) * (12 - wI))
        Next wI
        'Soma = Soma (2 x dígito verificador 1)
        wSomaDosProdutos = wSomaDosProdutos + (2 * wDigitChk1)
        'Resto = Soma - ((parte inteira da divisão da Soma por 11) x 11)
        wResto = wSomaDosProdutos - Int(wSomaDosProdutos / 11) * 11
        'Dígito verificador 2 = 0 (se Resto=0 ou 1 ) ou 11 - Resto (nos casos restantes)
        wDigitChk2 = IIf(wResto = 0 Or wResto = 1, 0, 11 - wResto)
        'Se o dígito da posição 10 = Dígito verificador 1 E
        'dígito da posição 11 = Dígito verificador 2 Então
        If Mid(argCpf, 10, 1) = Mid(Trim(Str(wDigitChk1)), 1, 1) And Mid(argCpf, 11, 1) = Mid(Trim(Str(wDigitChk2)), 1, 1) Then
            'CPF válido
            validaCPF = True
        Else
            'CPF inválido
            validaCPF = False
        End If
    End Function

    Public Function validaEmail(ByVal eMail As String) As Boolean
        'Função de validação do formato de um e-mail.

        Dim posicaoA As Integer
        Dim posicaoP As Integer

        'Busca posição do caracter @
        posicaoA = InStr(eMail, "@")
        'Busca a posição do ponto a partir da posição
        'do @ ou então da primeira posição
        posicaoP = InStr(posicaoA Or 1, eMail, ".")

        'Se a posição do @ for menor que 2 OU
        'a posição do ponto for menor que a posição
        'do caracter @
        If posicaoA < 2 Or posicaoP < posicaoA Then
            'Formato de e-mail inválido
            validaEmail = False
        Else
            'Formato de e-mail válido
            validaEmail = True
        End If

    End Function

    Public Function nomeProprio(ByVal argNome As String) As String
        'Função recursiva para converter a primeira letra
        'dos nomes próprios para maiúscula, mantendo os
        'aditivos em caixa baixa.
        Dim sNome As String
        Dim lEspaco As Long
        Dim lTamanho As Long
        'Pega o tamanho do nome
        lTamanho = Len(argNome)
        'Passa tudo para caixa baixa
        argNome = LCase(argNome)
        'Se o nome passado é vazio
        'acaba a função ou a recursão
        'retornando string vazia
        If lTamanho = 0 Then
            nomeProprio = ""
        Else
            'Procura a posição do primeiro espaço
            lEspaco = InStr(argNome, " ")
            'Se não tiver pega a posição da última letra
            If lEspaco = 0 Then lEspaco = lTamanho
            'Pega o primeiro nome da string
            sNome = Left(argNome, lEspaco)
            'Se não for aditivo converte a primeira letra
            If Not InStr("e da das de do dos ", sNome) > 0 Then
                sNome = UCase(Left(sNome, 1)) & LCase(Right(sNome, Len(sNome) - 1))
            End If
            'Monta o nome convertendo o restante através da recursão
            nomeProprio = sNome & nomeProprio(LCase(Trim(Right(argNome, lTamanho - lEspaco))))
        End If
    End Function

    Public Function abreviaNome(ByVal argNome As String) As String
        'Função que abrevia o penúltimo sobrenome, levando
        'em consideração os aditivos de, da, do, dos, das, e.

        'Define variáveis para controle de posição e para as
        'partes do nome que serão separadas e depois unidas
        'novamente.
        Dim ultimoEspaco As Integer, penultimoEspaco As Integer
        Dim primeiraParte As String, ultimaParte As String
        Dim parteNome As String
        Dim tamanho As Integer, i As Integer

        'Tamanho do nome passado
        'no argumento
        tamanho = Len(argNome)

        'Loop que verifica a posição do último e do penúltimo
        'espaços, utilizando apenas um loop.
        For i = tamanho To 1 Step -1
            If Mid(argNome, i, 1) = " " And ultimoEspaco <> 0 Then
                penultimoEspaco = i
                Exit For
            End If
            If Mid(argNome, i, 1) = " " And penultimoEspaco = 0 Then
                ultimoEspaco = i
            End If
        Next i

        'Caso i chegue a zero não podemos
        'abreviar o nome
        If i = 0 Then
            abreviaNome = argNome
            Exit Function
        End If

        'Separação das partes do nome em três: primeira, meio e última
        primeiraParte = Left(argNome, penultimoEspaco - 1)
        parteNome = Mid(argNome, penultimoEspaco + 1, ultimoEspaco - penultimoEspaco - 1)
        ultimaParte = Right(argNome, tamanho - ultimoEspaco)

        'Para a montagem do nome já abreviado verificamos se a parte retirada
        'não é um dos nomes de ligação: de, da ou do. Caso seja usamos o método
        'recursivo para refazer os passos.
        'Caso seja necessário basta acrescentar outros nomes de ligação para serem
        'verificados.
        If parteNome = "da" Or parteNome = "de" Or parteNome = "do" Or parteNome = "dos" Or parteNome = "das" Or parteNome = "e" Then
            abreviaNome = abreviaNome(primeiraParte & " " & parteNome) & " " & ultimaParte
        Else
            abreviaNome = primeiraParte & " " & Left(parteNome, 1) & ". " & ultimaParte
        End If
    End Function
    'Função para abrir uma caixa de seleção de arquivos
    Public Function EnderecoArqCapturar() As String
        Dim open As New OpenFileDialog()
        Try
            If open.ShowDialog = DialogResult.OK Then
                Return open.FileName.ToString
            End If
        Catch ex As Exception
            MsgBox("Não foi possível identificar o endereço do arquivo. Motivo:" & ex.Message, MsgBoxStyle.SystemModal, TITULO_ALERTA)
        End Try
        Return String.Empty
    End Function

    'para informar erros
    Public Sub InformaErro(Optional nomeFuncao As String = "")
        Dim ErrMsg As String
        ErrMsg = "Erro nº " & Err.Number & ": " & vbNewLine & Err.Description & vbNewLine & "** O aplicativo precisa ser encerrado! **"
        If Err.Number <> 0 Then
            Select Case Err.Number
                Case 3024, 3043, 3044, 3265 ' são os códigos de erro quando não há conexão com o banco de dados
                    'Exibe a mensagem do erro
                    'Não registra log, pois o acesso a rede foi interrompido
                    MsgBox(ErrMsg, MsgBoxStyle.Exclamation, TITULO_ALERTA)
                    Application.Exit()
                    End 'interrompe imediatamente
                Case Else
                    'registra um log do erro que aconteceu ao usuário
                    Call registrarLOG(Err.Number, Err.Description, nomeFuncao)
                    'Exibe a mensagem do erro
                    MsgBox(ErrMsg, MsgBoxStyle.Exclamation, TITULO_ALERTA)
                    Application.Exit()
                    End 'interrompe imediatamente
            End Select
        End If
    End Sub

    'Função para preencher um combobox
    Public Sub carregaComboBox(strSQL As String, frm As Form, cb As ComboBox)
        Dim con As New conexao
        Dim dt As New DataTable
        dt = con.RetornaDataTable(strSQL)
        With frm
            With cb
                .DataSource = dt
                .DisplayMember = dt.Columns(1).ToString
                .ValueMember = dt.Columns(0).ToString
                .Text = Nothing
                'Se não carregar nenhuma informação, entra com uma informação GERAL e de forma manual
                If .Items.Count = 0 Then
                    CarregaComboBoxManualmente("NÃO SE APLICA", frm, cb)
                End If
                'Se houver apenas um item no combobox este já fica selecionado
                If .Items.Count = 1 Then
                    .SelectedIndex = 0
                End If
            End With
        End With
    End Sub

    'Função para limpar as infos de um combobox
    Public Sub limpaCombobox(cb As ComboBox)
        With cb
            .DataSource = Nothing
            .DisplayMember = Nothing
            .Items.Clear()
        End With
    End Sub

    'função para abrir um formulario
    Public Sub abrirForm(frm As Form, Optional janelaRestrita As Boolean = False)
        'registrarLOG(, , GetCurrentMethodName, "Abriu: " & frm.Name.ToString)
        If janelaRestrita Then
            frm.ShowDialog()
        Else
            frm.Show()
        End If
    End Sub

    'função para fechar um formulario
    Public Sub fecharForm(frm As Form)
        frm.Close()
    End Sub

    'função para fechar aplicativo
    Public Sub fecharAplicativo(ByVal enviarMsg As Boolean)

        Dim fechar As Boolean = True

        If enviarMsg Then 'Temos opção de enviar ou não msg, visto que quando o usuário fecha por ALT+F4 ou BOTÃO DIREITO DO MOUSE não é possível questionar se deseja ou não fechar, pq o aplicativo irá fechar de toda forma.
            If MsgBox("Deseja realmente fechar o aplicativo?", vbQuestion + vbYesNo, TITULO_ALERTA) = vbNo Then
                fechar = False
            End If
        End If

        'Função para fechar o aplicativo
        If fechar Then
            'registrar na tbl de usuários a saída do sistema
            'Dim bll As New clsLoginBLL
            'bll.logOut()
            Application.Exit()
        End If

    End Sub

    'função para capturar o id de rede
    Public Function capturaIdRede() As String
        capturaIdRede = Environ("USERNAME").ToString.ToUpper
    End Function

    'função para limitar uma quantidade minima e maxima de caracteres.
    'Utilizar no LostFocus
    'txtCartao_Leave(sender As Object, e As EventArgs) Handles txtCartao.Leave
    'hlp.validaTamanhoMinMax(txtCartao, 15, 15)
    Public Function validaTamanhoMinMax(ByVal ctl As Control, iMinLen As Integer, iMaxLen As Integer) As Boolean
        If Not String.IsNullOrEmpty(ctl.Text) Then
            Dim texto As String = Trim(Replace(ctl.Text, " ", ""))

            'se diferente de vazio
            If Not String.IsNullOrEmpty(Replace(texto.Trim, "_", "")) Then
                'Limite Maximo
                If Len(Replace(texto.Trim, "_", "")) > iMaxLen Then
                    MsgBox("Limite máximo de " & iMaxLen & " caracteres foi excedido." & vbNewLine, vbInformation, TITULO_ALERTA)
                    ctl.Text = Left(texto.Trim, iMaxLen)
                    ctl.Focus()
                    ctl.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                    Return False
                    Exit Function
                End If
                'Limite Minimo
                If Len(Replace(texto.Trim, "_", "")) < iMinLen Then
                    MsgBox("Limite mínimo de " & iMinLen & " caracteres." & vbNewLine, vbInformation, TITULO_ALERTA)
                    ctl.Text = Left(texto.Trim, iMinLen)
                    ctl.Focus()
                    ctl.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                    Return False
                    Exit Function
                End If
                ctl.BackColor = System.Drawing.Color.White
                ctl.Text = texto
            Else
                ctl.BackColor = System.Drawing.Color.White
                Return False
                Exit Function
            End If
        End If
        Return True
    End Function

    'verificar se uma data é valida.
    'Utilizar no LostFocus
    Public Function validaData(ByVal Controle As Control) As Boolean
        Dim idiomaPC As String
        Dim formato As String = ""

        'se diferente de vazio
        On Error GoTo DataInvalida
        If Not String.IsNullOrEmpty(Replace(Replace(Controle.Text.Trim, "_", ""), "/", "").Trim) Then
            If Not IsDate(Controle.Text) Then
                'captura o idioma da maquina
                idiomaPC = CultureInfo.CurrentCulture.Name
                If idiomaPC = "pt-BR" Then
                    formato = "dia/mês/ano"
                Else
                    formato = "mês/dia/ano"
                End If
                'para o campo: " & Controle.Tag & "." & vbNewLine &
                MsgBox("Data inválida! " & vbNewLine &
                       "Possíveis motivos: " & vbNewLine &
                       " > Data inexistente." & vbNewLine &
                       " > Utilize o formato: " & idiomaPC.ToUpper & " (" & formato.ToUpper & ").", MsgBoxStyle.Information, TITULO_ALERTA)
                Controle.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
                Controle.Focus()
                Return False
            Else
                Controle.BackColor = System.Drawing.Color.White
                Return True
            End If
        Else
            Controle.BackColor = System.Drawing.Color.White
            Return False
        End If

DataInvalida:

        Controle.BackColor = System.Drawing.Color.White
        Return False

    End Function

    'Funções para formatação de data
    Public Function DataHoraAtual() As DateTime
        Return DateTime.Now
    End Function
    Public Function DataAbreviada() As Date
        Return CDate(DateTime.Now).ToString("yyyy-MM-dd")
    End Function
    Public Function FormataHoraAbreviada(hr As DateTime) As Date
        Return CDate(hr).ToString("HH:mm:ss")
    End Function
    Public Function FormataDataAbreviada(dt As Object) As DateTime
        If Replace(Replace(dt, "/", ""), "_", "").Trim = Nothing Then
            Return Nothing
        End If
        Return CDate(dt).ToString("yyyy-MM-dd")
    End Function
    Public Function FormataDataHoraCompleta(hr As DateTime) As Date
        Return CDate(hr).ToString("yyyy-MM-dd HH:mm:ss")
    End Function
    'Public Function formatarData(data As Date) As Date
    '    Return FormatDateTime(data, DateFormat.ShortDate)
    'End Function
    Public Function convertDatetime(data As Object) As DateTime
        If IsDBNull(data) Then
            Return Nothing
        Else
            Return Convert.ToDateTime(data).ToString
        End If
        'Convert.ToDateTime(argData).ToString("yyyy-MM-dd")
        'Convert.ToDateTime(argData).ToString("yyyy-MM-dd HH:mm:ss")
    End Function

    'função para ajustar um valor em decimal
    Public Function transformarMoeda(ctrl As String) As Double
        Dim valor As String = ctrl
        Dim n As String = String.Empty
        Dim v As Double = 0
        Try
            'Formatando para duas casas decimais antes das validações
            'este procedimento corrigi um bug para numeros com apenas 1 digito nas casas decimais
            Dim getDuasCasasDecimais As String = Microsoft.VisualBasic.Right(ctrl, 2)
            Dim getVirgulaouPontoCasasDecimais As String = Microsoft.VisualBasic.Left(getDuasCasasDecimais, 1)
            If getVirgulaouPontoCasasDecimais = "." Or getVirgulaouPontoCasasDecimais = "," Then
                valor = ctrl.PadRight(ctrl.Length + 1, "0")
            End If

            'Verificando se o valor contem ',' ou '.' ou ausencia de pontuação
            If InStr(valor, ".") Or InStr(valor, ",") Then
                n = valor.Replace(",", "").Replace(".", "")
                If n.Equals("") Then n = "000"
                If n.Length > 3 And n.Substring(0, 1) = "0" Then n = n.Substring(1, n.Length - 1)

            Else 'Caso não haja pontuação apenas acrescenta 2 zeros para gerar o valor moeda
                n = valor.PadRight(valor.Length + 2, "0")
            End If
            v = Convert.ToDouble(n) / 100
            Return CDbl(v)
            'valor = String.Format("{0:C}", v) 'acess {0:N}
        Catch ex As Exception
            Return 0
            Exit Function
        End Try
    End Function

    'função para ajustar um valor em decimal
    Public Function transformarMoedaValidandoCampo(ctrl As Control) As String
        Dim valor As String = ctrl.Text
        Dim n As String = String.Empty
        Dim v As Double = 0

        valor = Replace(valor, "$", "")
        valor = Replace(valor, "R", "")

        Try
            'Verificando se o valor contem ',' ou '.' ou ausencia de pontuação
            If InStr(valor, ".") Or InStr(valor, ",") Then
                n = valor.Replace(",", "").Replace(".", "")
                If n.Equals("") Then n = "000"
                If n.Length > 3 And n.Substring(0, 1) = "0" Then n = n.Substring(1, n.Length - 1)
            Else 'Caso não haja pontuação apenas acrescenta 2 zeros para gerar o valor moeda
                n = valor.PadRight(valor.Length + 2, "0")
            End If
            v = Convert.ToDouble(n) / 100
            valor = String.Format("{0:C}", v)
            Return valor

        Catch ex As Exception
            MsgBox("Valor digitado não é um valor de moeda. Tente novamente!", vbInformation, TITULO_ALERTA)
            ctrl.Focus()
            Return "Erro"
            Exit Function
        End Try
    End Function

    'altera o cursor para load
    Public Sub CursorPointer(bln As Boolean)
        If bln Then
            Cursor.Current = System.Windows.Forms.Cursors.WaitCursor
        Else
            Cursor.Current = System.Windows.Forms.Cursors.Default
        End If
    End Sub

    Public Sub killSistema()
        'Call mdlSysOffline.colocarOffline()
    End Sub

    'AutoCloseMsgBox "Msgbox1 - Clique em OK ou aguarde 2 segundos", "Fechar MsgBox1 automaticamente", 2 
    '2 segundos
    Sub AutoCloseMsgBox(Mensagem As String, Titulo As String, Segundos As Integer)
        Dim oSHL As Object
        oSHL = CreateObject("WScript.Shell")
        oSHL.PopUp(Mensagem, Segundos, Titulo, vbOKOnly + vbInformation)
    End Sub

    Public Function versaoSistema() As String
        Return Application.ProductVersion
    End Function

    Public Sub registrarLOG(Optional ByVal erroNumero As String = "", Optional ByVal erroDescricao As String = "", Optional ByVal funcaoExecutada As Object = "", Optional ByVal acao As String = "")
        Dim logDTO As New clsLogDTO
        Dim logDAL As New clsLogDAL
        With logDTO
            .data = Now()
            .idUsuario = capturaIdRede()
            .erroDescricao = erroDescricao.ToString
            .erroNumero = erroNumero.ToString
            .funcaoExecutada = funcaoExecutada.ToString
            .versaoSis = versaoSistema()
            .idiomaPC = retornaIdiomaPC()
            .hostname = Environ("COMPUTERNAME")
            .acao = acao.ToString
            .ferramenta = "CM IMPORTADOR"
        End With
        logDAL.Incluir(logDTO)
    End Sub

    'Carrega dataGrid
    Public Sub carregaDataGrid(frm As Form, dg As DataGridView, dt As DataTable)
        Try
            With frm
                With dg
                    .DataSource = dt
                End With
            End With
        Catch ex As Exception
            registrarLOG(Err.Number, Err.Description, GetNomeFuncao)
        End Try
    End Sub

    Public Sub colarDataGridView(ByVal frm As Form, ByVal dgv As DataGridView)
        Dim dt As New DataTable
        Dim dados() As String
        Dim linhas As Integer = 0
        Dim colunasDGV As Integer = 0
        Dim colunaNome As String = ""
        Dim colunaType As Object
        'Alimentando qtde de colunas existentes do DataGridView
        With frm
            With dgv
                colunasDGV = .Columns.Count
            End With
        End With

        Try
            'Adicionar colunas conforme as existentes no DataGridView
            For i As Integer = 0 To colunasDGV - 1
                With frm
                    With dgv
                        colunaNome = .Columns(i).HeaderText
                        colunaType = .Columns(i).HeaderCell.ValueType
                    End With
                End With
                dt.Columns.Add(colunaNome, colunaType)
            Next

            'Rodando a área de transferência e incluindo em uma nova linha do dataTable
            For Each line As String In Clipboard.GetText.Split(vbNewLine)
                dados = line.Trim.Split(vbTab)


                If dados.Length = colunasDGV Then 'Evitando colocar a última linha do clipboard que é em branco
                    dt.Rows.Add() 'Adicionando nova linha
                    For i As Integer = 0 To colunasDGV - 1 'For das Colunas
                        dt.Rows(linhas).Item(i) = dados(i)
                    Next
                End If
                linhas = linhas + 1 'Próxima linha do DataGridView

            Next

            With frm
                With dgv
                    .DataSource = dt
                End With
            End With

        Catch ex As Exception
            registrarLOG(Err.Number, Err.Description, GetNomeFuncao)
            MsgBox(Err.Number & " - " & Err.Description, MsgBoxStyle.Information, TITULO_ALERTA)
        End Try

    End Sub

    Public Sub carregaBarraProgresso(ByVal frm As Form, ByVal nomeBarraProgresso As ProgressBar, Optional maximo As Integer = 0, Optional limpeza As Boolean = True, Optional saltoProgresso As Boolean = False)
        With frm
            With nomeBarraProgresso
                If Not saltoProgresso Then
                    .Maximum = maximo
                    .Minimum = 0
                    .Step = 1
                    .Value = 0
                    .Visible = IIf(limpeza, False, True)
                Else
                    .PerformStep()
                    Application.DoEvents()
                End If
            End With
        End With
    End Sub



    'A classe StackFrame devolve a pilha de execuções, 
    'algo como a janela Call Stack no Visual Studio. O item 0 é esta própria função, 
    'então precisamos pegar o item 1, quem chamou essa função, e retornar o nome do método. 
    'Assim podemos obter em run-time o nome do método em execução e utilizar esse recurso em padrões de trace para nossos sistemas.
    Public Function GetNomeFuncao() As String
        Dim stack As New System.Diagnostics.StackFrame(1)
        Return stack.GetMethod().Name
    End Function

    'função para retornar caminho/nome do arquivo onde devemos "salvar Como"
    Public Function SavarComo(Optional ByVal nomeArquivo As String = "") As String
        Dim saveFileDialog1 As New SaveFileDialog()
        With saveFileDialog1
            .Filter = "txt files (*.txt)|*.txt|csv files (*.csv)|*.csv|All files (*.*)|*.*"
            .Title = "Salvar arquivo em..."
            '.InitialDirectory = nomeArquivo
            .FileName = nomeArquivo
            '.ShowDialog()
            If saveFileDialog1.ShowDialog() = DialogResult.OK Then
                If .FileName <> "" Then
                    Return .FileName
                Else
                    Return ""
                End If
            Else
                Return ""
            End If
        End With
    End Function

    'retorna um caminho pessoal no c:
    Public Function retornaDirPessoal() As String

        retornaDirPessoal = "c:\Users\" & Environ("USERNAME") & "\Documents\"
        'If DIR_PESSOAL = "ALGAR" Then
        '    retornaDirPessoal = "C:\Users\" & Environ("USERNAME") & "\Documents\"
        'Else

        '    'C:\Users\a058572\Documents
        '    'retornaDirPessoal = "\\" & Environ("COMPUTERNAME") & "\c$\Users\" & Environ("USERNAME") & "\Documents\"
        '    retornaDirPessoal = "c:\Users\" & Environ("USERNAME") & "\Documents\"

        '    'If capturaIdRede().ToUpper = "A053463" Or capturaIdRede().ToUpper = "A058572" Then
        '    '    retornaDirPessoal = "\\" & Environ("COMPUTERNAME") & "\c$\Users\" & Environ("USERNAME") & "\Documents\"
        '    'Else
        '    '    retornaDirPessoal = "\\d5174s006\Pessoal\" & Environ("USERNAME") & "\Documents\"
        '    'End If
        'End If
    End Function
    'abrir um determinado Arquivo
    Public Sub abrirArquivo(arquivo As String)
        On Error Resume Next
        System.Diagnostics.Process.Start(arquivo)
    End Sub
    'Função que verifica se um determinado arquivo esta aberto
    Public Function IsFileOpen(ByVal filename As String) As Boolean

        Dim filenum As Integer, errnum As Integer
        On Error Resume Next   ' Turn error checking off.
        filenum = FreeFile()   ' Get a free file number.
        FileOpen(filenum, filename, OpenMode.Random, OpenAccess.ReadWrite)
        FileClose(filenum)  'close the file.
        errnum = Err.Number 'Save the error number that occurred.
        On Error GoTo 0        'Turn error checking back on.
        ' Check to see which error occurred.
        Select Case errnum
            ' No error occurred.
            ' File is NOT MX_ALReady open by another user.
            Case 0
                Return False
                ' Error number for "Permission Denied."
                ' File is MX_ALReady opened by another user.
            Case 70, 55, 75
                Return True
                ' Another error occurred.
            Case Else
                Error errnum
        End Select

    End Function

    'para copiar para um determinado local
    Public Sub CopiaArquivo(ByVal origem As String, ByVal destino As String, ByVal arquivo As String, Optional ByVal id As String = "", Optional alterarNome As Boolean = False)
        Dim novoNome As String = ""
        Try
            origem = Replace(origem, arquivo, "") & arquivo
            'atribui um novo nome unico para o arquivo
            If alterarNome Then
                novoNome = id & " " & capturaIdRede() & " " & Format(Now, "ddMMyyyy HHmmss") & "." & PegarExtensao(arquivo)
                destino = destino & novoNome
            Else
                destino = destino & arquivo
            End If


            'novoNome = capturaIdRede() & " " & Format(Now, "ddMMyyyy HHmmss") & "." & PegarExtensao(arquivo)
            'se o arquivo não existir
            If Len(Dir(destino)) = 0 Then
                'se não existir nenhum arquivo, pode copiar
                Microsoft.VisualBasic.FileCopy(origem, destino)
                'caso existir apaga e depois copia
            Else
                'verifica se o arquivo ja esta em uso
                If IsFileOpen(destino) Then
                    MsgBox("Arquivo já em uso!", vbInformation, TITULO_ALERTA)
                    Exit Sub
                End If
                'apaga o arquivo antigo
                Microsoft.VisualBasic.Kill(destino)
                'copia o novo arquivo
                Microsoft.VisualBasic.FileCopy(origem, destino)


            End If
        Catch ex As Exception
            MsgBox(ex.Message & "Erro nº: " & Err.Number, vbCritical, TITULO_ALERTA)
        End Try
    End Sub

    'função para localizar um arquivo em uma determina pasta
    'retornar caminho+nome
    Public Function localizaArquivoPastaEspecifica(ByVal caminhoPasta As String, ByVal nomeParcialArquivo As String) As String
        Try
            Dim fso As New FileSystemObject

            For Each arq As Scripting.File In fso.GetFolder(caminhoPasta).Files
                If arq.Name.ToUpper Like nomeParcialArquivo.ToUpper & "*" Then
                    Return caminhoPasta & arq.Name
                End If
            Next

            Return Nothing
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    'para copiar para um determinado local
    Public Function CopiaArquivoRetornaNome(ByVal origem As String, ByVal destino As String, ByVal arquivo As String, Optional ByVal id As String = "", Optional ByVal renomear As Boolean = True) As String
        Dim novoNome As String = ""
        Try
            origem = Replace(origem, arquivo, "") & arquivo
            'atribui um novo nome unico para o arquivo
            novoNome = id & " " & capturaIdRede() & " " & Format(Now, "ddMMyyyy HHmmss") & "." & PegarExtensao(arquivo)

            If renomear Then
                destino = destino & novoNome
            Else
                destino = destino & arquivo
            End If


            'novoNome = capturaIdRede() & " " & Format(Now, "ddMMyyyy HHmmss") & "." & PegarExtensao(arquivo)
            'se o arquivo não existir
            If Len(Dir(destino)) = 0 Then
                'se não existir nenhum arquivo, pode copiar
                Microsoft.VisualBasic.FileCopy(origem, destino)
                'caso existir apaga e depois copia
            Else
                'verifica se o arquivo ja esta em uso
                If IsFileOpen(destino) Then
                    MsgBox("Este arquivo esta em uso.", vbCritical, TITULO_ALERTA)
                    Return novoNome
                    Exit Function
                End If
                'apaga o arquivo antigo
                Microsoft.VisualBasic.Kill(destino)
                'copia o novo arquivo
                Microsoft.VisualBasic.FileCopy(origem, destino)
            End If
            Return novoNome
        Catch ex As Exception
            MsgBox(ex.Message & "Erro nº: " & Err.Number, vbCritical, TITULO_ALERTA)
        End Try
        Return novoNome
    End Function

    Public Function desacentua(ByVal argTexto As String) As String
        'Função que retira acentos de qualquer texto.
        Dim strAcento As String
        Dim strNormal As String
        Dim strLetra As String
        Dim strNovoTexto As String = ""
        Dim intPosicao As Integer
        Dim i As Integer

        Try
            'Informa as duas sequências de caracteres, com e sem acento
            strAcento = "ÃÁÀÂÄÉÈÊËÍÌÎÏÕÓÒÔÖÚÙÛÜÝÇÑãáàâäéèêëíìîïõóòôöúùûüýçñ'*_"
            strNormal = "AAAAAEEEEIIIIOOOOOUUUUYCNaaaaaeeeeiiiiooooouuuuycn_"

            'Retira os espaços antes e após
            argTexto = Trim(argTexto)
            'Para i de 1 até o tamanho do texto
            For i = 1 To Len(argTexto)
                'Retira a letra da posição atual
                strLetra = Mid(argTexto, i, 1)
                'Busca a posição da letra na sequência com acento
                intPosicao = InStr(1, strAcento, strLetra)
                'Se a posição for maior que zero
                If intPosicao > 0 Then
                    'Retira a letra na mesma posição na
                    'sequência sem acentos.
                    strLetra = Mid(strNormal, intPosicao, 1)
                End If
                'Remonta o novo texto, sem acento
                strNovoTexto = strNovoTexto & strLetra
            Next
            'Devolve o resultado
            Return strNovoTexto
        Catch ex As Exception
            Return argTexto
        End Try

    End Function

    Public Function RemoverSimbolos(Valor As String) As String
        Dim Remover As String, i As Byte, Temp As String, Simbolos As String

        'Removendo símbolos
        Simbolos = "*-+'@Ø'-!$%&(),./:;?[\]^`{|}~¿¢£¤¥€+<>««»»∆√√□§©®°µ¼½¾ÁÀÂÄÃÅÆČÇʣÉÈÊËĔĞĢÍÌÎÏʪÑºÓÒÔÖŌØŒŜŞß™ʦÚÙÛÜŪŸЉЊЫѬ#"""""""
        Temp = Valor
        Try
            For s = 1 To Len(Simbolos)
                Remover = Mid(Simbolos, s, 1)
                For i = 1 To Len(Valor)
                    Temp = Replace(Temp, Remover, "")
                Next
            Next
            Return Trim(Temp)
        Catch ex As Exception
            Return Trim(Valor)
        End Try


    End Function


    'Limpa objetos de um determinado formulário
    Public Sub CapturaNomeCamposForm(strFrmName As Form)


        'Imports System.IO

        'Forma de uso:
        'Dim teste As New clsEscreveArquivoTxt
        '    With teste
        '        .CriaArquivo(caminhoNomeArquivo)
        '        .EscreveLn("A")
        '        .EscreveLn("B")
        '        .FechaStrm()
        '    End With

        'Public Class mdlEscreveArquivoTxt
        '    Private strm As StreamWriter

        '    'cria uma instância de StreamWriter para escrever no
        '    'arquivo desejado
        '    Public Function CriaArquivo(ByVal caminhoNomeArquivo As String) As StreamWriter
        '        strm = New StreamWriter(caminhoNomeArquivo)
        '        Return strm
        '    End Function

        '    Public Sub EscreveLn(ByVal linha As String)
        '        strm.WriteLine(linha)
        '    End Sub

        '    Public Sub SaltandoLinhas(ByVal quantidadeSaltos As Integer)
        '        For i As Integer = 1 To quantidadeSaltos
        '            strm.WriteLine("")
        '        Next i
        '    End Sub

        '    Public Sub FechaStrm()
        '        strm.Close() 'fecha o objeto strm
        '    End Sub

        'End Class

        '---------------------------------------------------------------------
        'Dim txt As New mdlEscreveArquivoTxt
        'Dim ctrl As Control
        'Dim a As String = ""
        'Dim arquivo As String
        'Dim Nome As String
        'Nome = "campos"
        'For Each ctrl In strFrmName.Controls
        '    If TypeOf ctrl Is ComboBox Then
        '        a = a & ctrl.Name & vbNewLine
        '    End If
        '    If TypeOf ctrl Is TextBox Then
        '        a = a & ctrl.Name & vbNewLine
        '    End If
        '    If TypeOf ctrl Is CheckBox Then
        '        a = a & ctrl.Name & vbNewLine
        '    End If
        '    If TypeOf ctrl Is MaskedTextBox Then
        '        a = a & ctrl.Name & vbNewLine
        '    End If
        '    If TypeOf ctrl Is RadioButton Then
        '        a = a & ctrl.Name & vbNewLine
        '    End If
        'Next
        'arquivo = retornaDirPessoal() & Trim(Nome) & ".txt"
        ''cria arquivo txt
        'With txt
        '    .CriaArquivo(arquivo)
        '    .EscreveLn(a)
        '    .FechaStrm()
        'End With
        ''abre o arquivo
        'abrirArquivo(arquivo)
    End Sub

    Public Function retornaIdiomaPC() As String
        ''Dim culture As CultureInfo = CultureInfo.CurrentCulture
        'Dim a = Thread.CurrentThread.CurrentCulture.Name
        'Dim b = culture.Name
        Return CultureInfo.CurrentCulture.Name.ToUpper.Trim
        Application.DoEvents()
    End Function

    'Metodo para desabilitar o botão "X" fechar
    'Disable the button on the current form:
    'RemoveXButton(Me.Handle())
    Public Declare Function GetSystemMenu Lib "user32" (ByVal hwnd As Integer, ByVal bRevert As Integer) As Integer
    Public Declare Function RemoveMenu Lib "user32" (ByVal hMenu As Integer, ByVal nPosition As Integer, ByVal wFlags As Integer) As Integer
    Public Const SC_CLOSE = &HF060&
    Public Const MF_BYCOMMAND = &H0&
    Public Function RemoveXButton(ByVal iHWND As Integer) As Integer
        Dim iSysMenu As Integer
        iSysMenu = GetSystemMenu(iHWND, False)
        Return RemoveMenu(iSysMenu, SC_CLOSE, MF_BYCOMMAND)
    End Function
    'Usar no KeyPress do componente
    Public Function somenteNumero(ctrl As Control) As Boolean
        If Not IsNumeric(ctrl.Text.Trim) And Not String.IsNullOrEmpty(ctrl.Text) Then
            MsgBox("Número do " & ctrl.Tag & " inválido.", MsgBoxStyle.Information, TITULO_ALERTA)
            ctrl.Focus()
            ctrl.BackColor = System.Drawing.Color.FromArgb(CType(255, Byte), CType(255, Byte), CType(128, Byte))
            Return False
            Exit Function
        Else
            ctrl.BackColor = System.Drawing.Color.White
            Return True
        End If
    End Function

    Public Function TotalLinhasArquivoTxt(caminhoArquivo As String) As Long
        'Regular Expression e contar as quebras de linhas:
        Dim re As New System.Text.RegularExpressions.Regex("\r\n")
        Dim sr As New System.IO.StreamReader(caminhoArquivo)
        Dim txt As String = sr.ReadToEnd()
        Dim qtdLinhas As Long = re.Matches(txt).Count
        sr.Close()
        'No final eu somo +1 para contar a última linha, que não tem a quebra de linha que é contada acima.
        Return qtdLinhas
    End Function

    'pegar extensao do arquivo
    Public Function PegarExtensao(arquivo As String) As String
        Dim i As Integer
        Dim j As Integer
        i = InStrRev(arquivo, ".")
        j = InStrRev(arquivo, "\")
        If j = 0 Then j = InStrRev(arquivo, ":")
        'End If
        If j < i Or i > 0 Then
            PegarExtensao = Right(arquivo, (Len(arquivo) - i))
        Else
            Return ""
        End If
    End Function

    'Procura uma determinada palavra em um texto e retorna verdadeiro caso encontre
    Public Function procurarPalavra(texto As String, texto_a_procurar As String) As Boolean
        Dim resultado As Long
        resultado = InStr(texto, texto_a_procurar)
        If resultado > 0 Then
            procurarPalavra = True
        Else : procurarPalavra = False
        End If
    End Function

    'Função para retornar vazio para campos textbox com data
    Public Function RetornaDataTextBox(argValor As Object) As String
        Dim idiomaPC As String
        Dim formato As String = ""
        Dim dataVazia As DateTime = Nothing
        If argValor = Nothing Then
            argValor = dataVazia
        End If
        If CDate(argValor).ToString("yyyy-MM-dd HH:mm:ss") = CDate(dataVazia).ToString("yyyy-MM-dd HH:mm:ss") Then
            Return ""
        Else

            'captura o idioma da maquina
            idiomaPC = CultureInfo.CurrentCulture.Name
            If idiomaPC = "pt-BR" Then
                formato = "dd/MM/yyyy HH:mm:ss" 'dia/mês/ano
            Else
                formato = "MM/dd/yyyy HH:mm:ss" 'mês/dia/ano"
            End If

            'Dim sFormat As System.Globalization.DateTimeFormatInfo = New System.Globalization.DateTimeFormatInfo()
            'sFormat.LongDatePattern = formato ' "yyyy-MM-DD HH:mm:ss" ' ShortDatePattern
            'Return Format(Convert.ToDateTime(argValor.ToString, sFormat), vbLongDate) 'MM/dd/yyyy HH:mm:ss
            Return CDate(argValor).ToString(formato)
            'Return Convert.ToDateTime(argValor.ToString, sFormat)

        End If
    End Function

    'Chamada
    'hlp.CarregaComboBoxManualmente("FAB;INQ;Tudo", Me, Me.cFilas)
    'função
    'Carregamento de Combobox de forma manual
    Public Sub CarregaComboBoxManualmente(ByVal strItens As String, ByVal frm As Form, ByVal cb As ComboBox)
        Dim itens As Object
        itens = Split(strItens, ";")
        'limpando o combobox para evitar duplo carregamento
        With frm
            With cb
                .DataSource = Nothing
                .Items.Clear()
            End With
        End With
        'Carregando itens
        For i = LBound(itens) To UBound(itens)
            With frm
                With cb
                    .Items.Add(itens(i))
                End With
            End With
        Next
    End Sub

    'matar processo do proprio aplicativo
    Public Sub killProcesso()
        'captura o processo do aplicativo
        Dim proc As Process = Process.GetCurrentProcess
        'captura o nome do processo deste aplicativo
        Dim processo As String = proc.ProcessName.ToString

        'percorrendo todos os processos abertos
        For Each prog As Process In Process.GetProcesses
            'fecha o processo deste aplicativo
            If prog.ProcessName = processo Then
                prog.Kill()
            End If
        Next
    End Sub

    'Função para limitar uma quantidade de caracteres por linha em um determinado Textbox
    Public Sub limiteCaracterPorLinha(ByVal limite As Long, ByVal ctrl As Control)
        Dim texto As String = ""
        Dim tamanho As Long = 0
        Dim nova_linha As String = ""
        Dim temp_linha As String = ""
        Dim delimitador As String = Replace(Space(limite), " ", "-")
        Dim nroBloco As Integer = 0

        texto = ctrl.Text.Trim
        'remove as quebras de linhas
        texto = texto.Replace(System.Environment.NewLine, String.Empty)
        tamanho = Len(texto)
        'se acima do limite
        If tamanho > limite Then
            'percorre toda a cadeia de caracteres uma a uma
            For i = 1 To tamanho
                temp_linha += Mid(texto, i, 1) 'recebe os caracteres
                If temp_linha.Length = limite Then 'verifica se alcançou o limite
                    nroBloco = nroBloco + 1
                    temp_linha += "\r\n" 'inserir quebra de linha na variavel temp
                    ''utilizar separador por blocos apenas se necessario
                    'If nroBloco = 4 Then
                    '    temp_linha += delimitador & "\r\n"
                    '    nroBloco = 0
                    'End If
                    nova_linha += temp_linha 'salva na variavel final
                    'formatando uma expressão regular para quebra de linha
                    nova_linha = System.Text.RegularExpressions.Regex.Unescape(String.Format(nova_linha))
                    temp_linha = "" 'limpa variavel temporaria para o proximo lote de caracteres
                ElseIf i = tamanho Then
                    nova_linha += temp_linha 'concatenar a ultima linha abaixo de 50 caracteres
                End If
            Next
            ctrl.Text = nova_linha 'retorna para o textbox
        End If
    End Sub


    'Exibe em um bloco de notas as informações dos usuários conectados ao banco de dados Acess
    'COMO USAR: Para ver o resultado,
    'Dim hlp As New mdlHelpers
    'hlp.UsuariosConectadosBD
    'Public Sub UsuariosConectadosBD()
    '    Dim path As String
    '    Dim arquivo As String
    '    Dim con As New clsConexao
    '    Dim txt As New mdlEscreveArquivoTxt
    '    path = retornaDirPessoal() '"\\" & Environ("COMPUTERNAME") & "\c$\Users\" & Environ("USERNAME") & "\"
    '    arquivo = path & CDate(DateTime.Now).ToString("yyyyMMddHHmmss") & ".txt"

    '    With txt
    '        .CriaArquivo(arquivo)
    '        .EscreveLn(con.ShowUserRosterMultipleUsers)
    '        .FechaStrm()
    '    End With
    '    abrirArquivo(arquivo)
    'End Sub
    Public Function verificarProcessoSeEstaAberto(ByVal nomeProcesso As String) As Boolean
        Try
            'percorrendo todos os processos abertos
            For Each prog As Process In Process.GetProcesses
                'fecha o processo deste aplicativo
                If prog.ProcessName.ToUpper = nomeProcesso.ToUpper Then
                    Return True
                End If
            Next

            Return False

        Catch ex As Exception
            Return False
        End Try

    End Function
    Public Function fecharProcesso(ByVal nomeProcesso As String) As Boolean
        Try
            'percorrendo todos os processos abertos
            For Each prog As Process In Process.GetProcesses
                'fecha o processo deste aplicativo
                If prog.ProcessName.ToUpper = nomeProcesso.ToUpper Then
                    prog.Kill()
                End If
            Next

            Return True

        Catch ex As Exception
            Return False
        End Try

    End Function

    'Formatar limite de caracteres com "zeros" a esquerda
    Public Function FormataLimiteCaracteres(nrCaracteres As Integer, valor As String) As String
        If String.IsNullOrEmpty(valor) Then
            Return Nothing
        Else
            Dim i As Long
            'Dim novovalor As String = valor.Trim
            Dim strRetorno As String = ""
            For i = 1 To nrCaracteres
                strRetorno = strRetorno & "0"
            Next
            Return Microsoft.VisualBasic.Right(strRetorno & valor.Trim, nrCaracteres)
        End If
    End Function

    'Funcao para contar a quantidade de caracteres de um determinado textbox
    Public Sub contCaracterer(ctrl As TextBox, lbExibicao As Label)
        Dim iLivre As Integer
        iLivre = (ctrl.MaxLength - ctrl.Text.Length)
        lbExibicao.Text = iLivre.ToString()
    End Sub

    ''Função para atualizar Relatórios
    ''parametros:Array list,ReportView,Nome do RDLC,Nome do DataSorce
    'Public Sub atualizaReport(ByRef lista As ICollection, reportview As Microsoft.Reporting.WinForms.ReportViewer, NomeRelatorioRdlc As String, reportDataSource As String)
    '    reportview.LocMX_ALReport.DataSources.Clear()
    '    reportview.LocMX_ALReport.ReportEmbeddedResource = "K2." & NomeRelatorioRdlc & ".rdlc"
    '    Dim ds As New Microsoft.Reporting.WinForms.ReportDataSource(reportDataSource, lista)
    '    reportview.LocMX_ALReport.DataSources.Add(ds)
    '    ds.Value = lista
    '    reportview.LocMX_ALReport.Refresh()
    '    reportview.RefreshReport()
    'End Sub

    'Função para atualizar Relatórios
    'parametros:DataTable,ReportView,Nome do RDLC,Nome do DataSorce
    'Public Sub atualizaReportDT(ByRef dt As DataTable, reportview As Microsoft.Reporting.WinForms.ReportViewer, NomeRelatorioRdlc As String, reportDataSource As String)
    '    'Set the processing mode for the ReportViewer to Remote
    '    reportview.ProcessingMode = Microsoft.Reporting.WinForms.ProcessingMode.Local
    '    reportview.LocalReport.DataSources.Clear()
    '    reportview.LocalReport.ReportEmbeddedResource = "MaTRiX." & NomeRelatorioRdlc & ".rdlc"
    '    Dim ds As New Microsoft.Reporting.WinForms.ReportDataSource(reportDataSource, dt)
    '    reportview.LocalReport.DataSources.Add(ds)
    '    ds.Value = dt
    '    'reportview.LocMX_ALReport.Refresh()
    '    reportview.RefreshReport()
    'End Sub

    '' VB.NET
    'Public Sub ExportarRelatorio(Report As Microsoft.Reporting.WinForms.ReportViewer, Formato As String, NomeArquivo As String)
    '    Dim Bytes = Report.LocalReport.Render(Formato)
    '    System.IO.File.WriteAllBytes(PATH_REPORT & NomeArquivo, Bytes)
    'End Sub

    'setando os parametro
    'Dim paramList As New Generic.List(Of ReportParameter)
    'paramList.Add(New ReportParameter("ReportParameter1", cbArea.Text))
    'rptv1.LocMX_ALReport.SetParameters(paramList)
    'rptv1.RefreshReport()
    '1.) design the table containing the columns at the .rdlc file
    '2.) added a parameter called ReportParameter1 
    '3.) right clicked the tablix and added a filter [AccountNumber] = [@ReportParameter1]
    '4.) added the code above.


    Public Function retornaValor(Optional ByVal vldecimal As String = "") As Decimal
        If Not String.IsNullOrEmpty(vldecimal) And Not IsNothing(vldecimal) Then
            Return vldecimal
        Else
            Return 0
        End If
    End Function

    'Função para executar um delay variando entre 2 e 11 segundos dependendo da qualidade da rede
    Public Sub Delay()

        Dim iCount As Long = 0
        Dim time1, time2 As DateTime
        'Inicializa o gerador de números aleatórios.
        Randomize()
        ' Gera um número aleatório entre 2 e 11 (inclusive)
        Dim numero As Integer = CInt(Int((9 * Rnd()) + 2))
        'Threading.Thread.Sleep(numero)
        'Debug.Print(Now & " > " & numero) 'Mensagem de hora inicial (teste)
        time1 = Now
        time2 = time1.AddSeconds(numero) 'TimeValue("0:00:0" & numero)
        Do Until time1 >= time2
            'Application.DoEvents
            time1 = Now()
        Loop
        'Debug.Print(Now & " > " & numero)  'Mensagem de hora final (teste)
        Exit Sub
    End Sub

    'A classe StackFrame devolve a pilha de execuções, 
    'algo como a janela Call Stack no Visual Studio. O item 0 é esta própria função, 
    'então precisamos pegar o item 1, quem chamou essa função, e retornar o nome do método. 
    'Assim podemos obter em run-time o nome do método em execução e utilizar esse recurso em padrões de trace para nossos sistemas.
    Public Function GetCurrentMethodName() As String
        Dim stack As New System.Diagnostics.StackFrame(1)
        Return stack.GetMethod().Name
    End Function


    'Função para converter segundos em hora / minuto / segundos
    Public Function converterSegundos(ByVal intSegundos As Long) As DateTime

        Dim emSegundos As Long, emMinutos As Long, emHoras As Long, emDias As Long
        Dim segundos As Long, miuntos As Long, horas As Long

        emSegundos = intSegundos
        segundos = emSegundos Mod (60)
        emMinutos = emSegundos \ (60)
        miuntos = emMinutos Mod (60)
        emHoras = emMinutos \ (60)
        horas = emHoras Mod (24)
        emDias = emHoras \ (24)


        Return Format(horas, "00") & ":" & Format(miuntos, "00") & ":" & Format(segundos, "00")


    End Function

    Public Function validarIdiomaPC(ByVal siglaIdioma As String) As Boolean

        'IDIOMAS MAIS USADOS:
        'PT-BR
        'EN-US

        'EXEMPLO:
        'If Not hlp.validarIdiomaPC("PT-BR") Then Exit Sub

        Dim idiomaPC As String = retornaIdiomaPC.ToLower

        If Not idiomaPC = siglaIdioma.ToLower Then
            MsgBox("O idioma para esta ação deve ser: " & siglaIdioma.ToUpper & ". " _
                    & vbNewLine & "Feche o aplicativo, troque o idioma e tente outra vez!" _
                    , MsgBoxStyle.Information, TITULO_ALERTA)
            abrirPainelRegiaoIdioma()
            Return False
        Else
            Return True
        End If

    End Function

    Public Sub abrirPainelRegiaoIdioma()
        Shell("rundll32.exe shell32.dll,Control_RunDLL intl.cpl,,0", vbMaximizedFocus)
    End Sub

    Public Sub Campos_Habilitar(ByRef Tela As Control, Optional bloquear As Boolean = False)
        'Caso ocorra erro, não mostrar o erro, ignorando e indo para á próxima linha
        On Error Resume Next
        'Declaramos uma variavel Campo do tipo Object
        '(Tipo Object porque iremos trabalhar com todos os campos do Form, podendo ser
        '       Label, Button, TextBox, ComboBox e outros)
        Dim Campo As Object
        'Usaremos For Each para passarmos por todos os controls do objeto atual
        For Each Campo In Tela.Controls

            If Not TypeOf Campo Is Label And
                Not TypeOf Campo Is MenuStrip And
                Not TypeOf Campo Is PictureBox And
                Not TypeOf Campo Is StatusStrip Then
                Campo.Enabled = bloquear
            End If

        Next

    End Sub

    Public Sub Campos_SomenteLeitura(ByRef Tela As Control, Optional Ativar As Boolean = False)
        'Caso ocorra erro, não mostrar o erro, ignorando e indo para á próxima linha
        On Error Resume Next
        'Declaramos uma variavel Campo do tipo Object
        '(Tipo Object porque iremos trabalhar com todos os campos do Form, podendo ser
        '       Label, Button, TextBox, ComboBox e outros)
        Dim Campo As Object
        'Usaremos For Each para passarmos por todos os controls do objeto atual
        For Each Campo In Tela.Controls

            If TypeOf Campo Is TextBox Or
                TypeOf Campo Is MaskedTextBox Then
                Campo.ReadOnly = Ativar
            End If

        Next

    End Sub

    Public Function CriarCopiarMoverDeletarAquivo(ByVal caminhoArquivo As String, ByVal acao As String, Optional NovoCaminho As String = "") As Boolean

        Dim arq As New FileInfo(caminhoArquivo)
        Try
            Select Case acao.ToUpper
                Case "CRIAR"
                    arq.Create()
                Case "COPIAR"
                    arq.CopyTo(NovoCaminho)
                Case "MOVER"
                    arq.MoveTo(NovoCaminho)
                Case "DELETAR"
                    arq.Delete()
            End Select
            Return True
        Catch ex As Exception
            Return False
            MsgBox("Ação: " & acao & " não realizada, tente novamente!", vbInformation, TITULO_ALERTA)
        End Try

    End Function

    'Public Sub exportarListViewParaExcel(ByVal lv As ListView)

    '    Try
    '        Dim objExcel As Microsoft.Office.Interop.Excel.Application
    '        Dim bkWorkBook As Microsoft.Office.Interop.Excel.Workbook
    '        Dim shWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
    '        Dim i As Integer
    '        Dim j As Integer

    '        objExcel = New Microsoft.Office.Interop.Excel.Application
    '        bkWorkBook = objExcel.Workbooks.Add
    '        shWorkSheet = CType(bkWorkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

    '        For i = 0 To lv.Columns.Count - 1
    '            shWorkSheet.Cells(1, i + 1) = lv.Columns(i).Text
    '        Next
    '        For i = 0 To lv.Items.Count - 1
    '            For j = 0 To lv.Items(i).SubItems.Count - 1
    '                shWorkSheet.Cells(i + 2, j + 1) = lv.Items(i).SubItems(j).Text
    '            Next
    '        Next

    '        objExcel.Visible = True

    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try

    'End Sub


    Public Function DatafimDeSemana(ByVal dataParaTeste As Date) As Date

        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim Objcon As New conexao
        Dim feriado As Boolean = True

        Do While feriado = True 'Caso tenha vários dias de feriado, serão considerados cada um deles
            sql = "SELECT * from SysFeriados Where (data = " & Objcon.valorSql(dataParaTeste) & ") "
            Objcon.banco_dados = "db_MaTRiX.accdb"
            rs = Objcon.RetornaRs(sql)

            If rs.RecordCount > 0 Then 'Trata-se de um dia de feriado
                dataParaTeste = DateAdd(DateInterval.Day, 1, dataParaTeste)
            Else
                feriado = False
            End If
        Loop

        'Fechando RecordSet
        rs = Nothing
        Objcon.Desconectar()

        'retirado por solicitação do Thiago em 26/07/2017.
        'retornado em 07/08/2017, conforme solicitação do Adriano, porém apenas os Domingos
        Select Case Weekday(dataParaTeste) 'Teste de fim de semana
            Case 1
                Return DateAdd(DateInterval.Day, 1, dataParaTeste) 'Se DOMINGO
                'Case 7
                '        Return DateAdd(DateInterval.Day, 2, dataParaTeste) 'Se SÁBADO
                '    Case Else
                '        Return dataParaTeste
        End Select
        Return dataParaTeste

    End Function


    Public Function feriadoHoje() As Boolean
        'Objetivo é devolver uma informações indicando se é ou não feriado hoje

        Dim rs As New ADODB.Recordset
        Dim sql As String
        Dim Objcon As New conexao
        Dim feriado As Boolean = False

        sql = "SELECT * from SysFeriados Where (data = " & Objcon.valorSql(DataAbreviada) & ") "
        Objcon.banco_dados = "db_MaTRiX.accdb"
        rs = Objcon.RetornaRs(sql)

        If rs.RecordCount > 0 Then
            feriado = True
        End If

        rs.Close()
        Return feriado

    End Function

    Public Function RetornaSoNumeroDeString(texto As String) As String
        Dim i As Integer, j As String = ""
        Dim parteNumerica As String = ""
        For i = 1 To Len(texto)
            If Asc(Mid(texto, i, 1)) < 48 Or
               Asc(Mid(texto, i, 1)) > 57 Then
            Else
                j = j & Mid(texto, i, 1)
            End If
            parteNumerica = j
        Next
        Return parteNumerica
    End Function

    Public Function Decrypt(str As String) As String
        Dim b As Byte()
        Dim decryp As String
        Try
            b = Convert.FromBase64String(str)
            decryp = System.Text.ASCIIEncoding.ASCII.GetString(b)
        Catch ex As Exception
            decryp = "Erro"
        End Try

        Return decryp
    End Function

    Public Function Encrypt(str As String) As String
        Dim b As Byte()
        Dim encryp As String
        Try
            b = System.Text.ASCIIEncoding.ASCII.GetBytes(str)
            encryp = Convert.ToBase64String(b)
        Catch ex As Exception
            encryp = "Erro"
        End Try
        Return encryp
    End Function

    Public Sub exportarListViewParaExcel(ByVal lv As ListView)

        Try
            Dim objExcel As Microsoft.Office.Interop.Excel.Application
            Dim bkWorkBook As Microsoft.Office.Interop.Excel.Workbook
            Dim shWorkSheet As Microsoft.Office.Interop.Excel.Worksheet
            Dim i As Integer
            Dim j As Integer

            objExcel = New Microsoft.Office.Interop.Excel.Application
            bkWorkBook = objExcel.Workbooks.Add
            shWorkSheet = CType(bkWorkBook.ActiveSheet, Microsoft.Office.Interop.Excel.Worksheet)

            For i = 0 To lv.Columns.Count - 1
                shWorkSheet.Cells(1, i + 1) = lv.Columns(i).Text
            Next
            For i = 0 To lv.Items.Count - 1
                For j = 0 To lv.Items(i).SubItems.Count - 1
                    shWorkSheet.Cells(i + 2, j + 1) = lv.Items(i).SubItems(j).Text
                Next
            Next

            objExcel.Visible = True

        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Public Sub desligarReiniciarWindows(ByVal executarFuncao_D_R As String)

        'No args                 Display this message (same as -?)
        '-i                      Display GUI interface, must be the first option
        '-l                      Log off (cannot be used with -m option)
        '-s                      Shutdown the computer
        '-r                      Shutdown And restart the computer
        '-a                      Abort a system shutdown
        '-m \\computername       Remote computer to shutdown/restart/abort
        '-t xx                   Set timeout for shutdown to xx seconds
        '-c "comment"            Shutdown comment (maximum of 127 characters)
        '-f                      Forces running applications to close without warning
        '-d [u][p]:xx : yy         The reason code for the shutdown
        '                        u Is the user code
        '                        p Is a planned shutdown code
        '                        xx Is the major reason code (positive integer less than 256)
        '                        yy Is the minor reason code (positive integer less than 65536)

        Try
            Select Case executarFuncao_D_R.ToUpper
                Case "D"
                    System.Diagnostics.Process.Start("shutdown", "-s -t 00 -f")
                Case "R"
                    System.Diagnostics.Process.Start("shutdown", "-r -t 00 -f")
            End Select
        Catch ex As Exception
            registrarLOG(Err.Number, Err.Description, GetNomeFuncao, executarFuncao_D_R)
            fecharAplicativo(False)
        End Try

    End Sub
    Public Function hostnameLocal() As String
        Return System.Windows.Forms.SystemInformation.ComputerName
    End Function

End Class


