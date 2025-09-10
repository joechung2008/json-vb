Namespace JsonParser.Models

    Public Class PairToken
        Inherits Token

        Public Property Key As StringToken

        Public Property Value As Token

        Public Sub New(skip As Integer, key As StringToken, value As Token)
            MyBase.New(skip)
            Me.Key = key
            Me.Value = value
        End Sub

        Public Overrides Function ToString() As String
            Return Key.ToString() & ":" & Value.ToString()
        End Function
    End Class

End Namespace
