﻿Imports System.Text
Imports Microsoft.VisualStudio.TestTools.UnitTesting


<TestClass()> Public Class UnitTest1
    <TestMethod()> Public Sub TestMethod1()
        Dim meupro As New Automacao_CaseManager.Funcoes
        MandeiParar = False
        meupro.iniciar()
    End Sub

End Class