Imports System.Globalization

Namespace JsonParser.Models

    Public Class NumberToken
        Inherits Token

        Public Property Value As Double

        Public Sub New(skip As Integer, value As Double)
            MyBase.New(skip)
            Me.Value = value
        End Sub

        Public Overrides Function ToString() As String
            Return Value.ToString(CultureInfo.InvariantCulture)
        End Function
    End Class

End Namespace
