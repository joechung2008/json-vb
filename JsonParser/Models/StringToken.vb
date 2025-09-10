Namespace JsonParser.Models

    Public Class StringToken
        Inherits Token

        Public Property Value As String

        Public Sub New(skip As Integer, value As String)
            MyBase.New(skip)
            Me.Value = value
        End Sub

        Public Overrides Function ToString() As String
            Return """" & Value & """"
        End Function
    End Class

End Namespace
