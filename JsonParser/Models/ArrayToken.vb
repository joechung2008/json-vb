Imports System.Linq

Namespace JsonParser.Models

    Public Class ArrayToken
        Inherits Token

        Public Property Elements As IEnumerable(Of Token)

        Public Sub New(skip As Integer, elements As IEnumerable(Of Token))
            MyBase.New(skip)
            Me.Elements = elements
        End Sub

        Public Overrides Function ToString() As String
            Return "[" & String.Join(",", Elements.Select(Function(element) element.ToString())) & "]"
        End Function
    End Class

End Namespace
