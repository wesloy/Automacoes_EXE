Imports System.Configuration

Module constantes

    'Path de configurações do sistema
    Public PATH_ICONS As String = ConfigurationManager.AppSettings("PATH_ICONS")
    Public PATH_PASTA_MIS As String = ConfigurationManager.AppSettings("PATH_PASTA_MIS")
    Public PATH_PASTA_ANEXO As String = ConfigurationManager.AppSettings("PATH_PASTA_ANEXO")
    Public PATH_LOG_IMPORT As String = ConfigurationManager.AppSettings("PATH_LOG_IMPORT")
    Public PATH_MODELOS As String = ConfigurationManager.AppSettings("PATH_MODELOS")
    Public BD_PWD As String = GetConfig("BD_PWD") 'Captura a senha e decriptografa
    Public BD_NOME As String = ConfigurationManager.AppSettings("BD_NOME")
    Public BD_PATH As String = ConfigurationManager.AppSettings("BD_PATH")
    Public DIR_PESSOAL As String = ConfigurationManager.AppSettings("DIR_PESSOAL")
    Public HOSTNAME_ATM As String = ConfigurationManager.AppSettings("HOSTNAME_ATM")
    Public PATH_REPORT As String = ConfigurationManager.AppSettings("PATH_REPORT")
    Public PATH_ATM As String = ConfigurationManager.AppSettings("PATH_ATM")

    'Constantes >>> BD ALGAR
    Public ALGAR_BD As String = ConfigurationManager.AppSettings("ALGAR_BD")
    Public ALGAR_SERVIDOR As String = ConfigurationManager.AppSettings("ALGAR_SERVIDOR")
    Public ALGAR_USER As String = ConfigurationManager.AppSettings("ALGAR_USER")
    Public ALGAR_PWD As String = GetConfig("ALGAR_PWD")

    'Constantes >>> CASE MANAGER
    Public VERSAO_CM As String = ConfigurationManager.AppSettings("VERSAO_CM")

    'Constantes >>> DO SISTEMA
    Public Const TITULO_ALERTA = "Alerta do Sistema"
    Public Const FormatoDataUniversal = "yyyy-MM-dd"
    Public Const FormatoDataHoraUniversal = "yyyy-MM-dd HH:mm:ss"
    Public Const CREDITOS = "Wesley Eloy"
    Public Const Copyright = "Copyright © Microsoft"
    Public Const PATH_SISTEMA_EXTRA = "C:\Program Files\E!PC\Sessions\"

    'Constantes >>> VARIAVEIS DE CÁLCULO
    Public erroCriticoSistema As Integer = 0
    Public tratativaDeErros As Integer = 0
    Public msgDeErros As String = ""
    Public reiniciarAplicacao As Boolean = False
    Public stepAtual As String = ""
    Public encerrarAplicacao As Boolean = False
    Public pararAplicacao As Boolean = False

    'CONFIGURAÇÕES SMS
    Structure CategoriasSMS
        Const ENVIA_SMS = "ENVIA_SMS"
        Const ENVIA_TOKEN = "ENVIA_TOKEN"
    End Structure
    'const SMS_ALGAR
    Public SMS_USER As String = ConfigurationManager.AppSettings("SMS_USER")
    Public SMS_PWD As String = GetConfig("SMS_PWD")
    Public Const GRUPO_SMS As String = "SMS"
    Public Const AMBIENTE As String = "PRODUCAO"
    Public FilasSMSAlgar As List(Of Algar.SMS.objetos.SmsFilas) = listagemFilasSMS(GRUPO_SMS)
    Public FilasSMSAlgarSomenteEnvio As List(Of Algar.SMS.objetos.SmsFilas) = listagemFilasSMS(GRUPO_SMS, True)
    Public idCAMPOS_ENVIO_SMS As Algar.SMS.objetos.IDCamposSMS = retornaIDCamposSMS(AMBIENTE, CategoriasSMS.ENVIA_SMS)
    Public idCAMPOS_ENVIO_TOKEN As Algar.SMS.objetos.IDCamposSMS = retornaIDCamposSMS(AMBIENTE, CategoriasSMS.ENVIA_TOKEN)


    Public Function GetConfig(key As String) As String
        'Método que pega os valores do arquivo de configuração e decriptografa.
        Dim crypt As New helpers
        Return crypt.Decrypt(ConfigurationManager.AppSettings(key))
    End Function

    Public Function imglist() As ImageList
        'cria um imagelist se necessario
        Dim imageListSmall As New ImageList
        With imageListSmall
            '.ImageSize = New Size(16, 16) ' (the default is 16 x 16).
            .Images.Add(1, Image.FromFile(PATH_ICONS & "01.ico"))
            .Images.Add(2, Image.FromFile(PATH_ICONS & "02.ico"))
            .Images.Add(3, Image.FromFile(PATH_ICONS & "03.ico"))
            .Images.Add(4, Image.FromFile(PATH_ICONS & "04.ico"))
            .Images.Add(5, Image.FromFile(PATH_ICONS & "05.ico"))
            .Images.Add(6, Image.FromFile(PATH_ICONS & "06.ico"))
            .Images.Add(7, Image.FromFile(PATH_ICONS & "07.ico"))
            .Images.Add(8, Image.FromFile(PATH_ICONS & "08.ico"))
            .Images.Add(9, Image.FromFile(PATH_ICONS & "09.ico"))
            .Images.Add(10, Image.FromFile(PATH_ICONS & "10.ico"))
            .Images.Add(11, Image.FromFile(PATH_ICONS & "11.ico"))
            .Images.Add(12, Image.FromFile(PATH_ICONS & "12.ico"))
            .Images.Add(13, Image.FromFile(PATH_ICONS & "13.ico"))
            .Images.Add(14, Image.FromFile(PATH_ICONS & "14.ico"))
        End With
        'fim da criacao do imagelist
        Return imageListSmall
    End Function

    'HISTÓRICO DE MANUTENÇÃO
    '0.2 - Inclusão de registros de logs / alteração no formata moeda / e reinicialização automática da máquina em erros críticos
    '0.3 - Retirada várias tentativas de importação do excel
    '0.4 - implantada inteligencia de aguardar plan exportada do case, para depois fechar a mesma e continuar
    '0.5 - alterada lógica de DESCARTE na importacao (chaves de comparação) / e sistema de reinicialização do PC
    '0.6 - melhoria na chave de comparaçao - valor
    '0.7 - blindado para um único hostname
End Module


