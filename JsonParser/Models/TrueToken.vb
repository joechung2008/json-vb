Namespace JsonParser.Models

    Public Class TrueToken
        Inherits Token

        Public Sub New(skip As Integer)
            MyBase.New(skip)
        End Sub

        Public ReadOnly Property Value As Boolean
            Get
                Return True
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Boolean.TrueString.ToLower()
        End Function
    End Class

End Namespace
