Namespace JsonParser.Models

    Public Class NullToken
        Inherits Token

        Public Sub New(skip As Integer)
            MyBase.New(skip)
        End Sub

        Public ReadOnly Property Value As Object
            Get
                Return Nothing
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return "null"
        End Function
    End Class

End Namespace
