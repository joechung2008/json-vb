Namespace JsonParser.Models

    Public MustInherit Class Token
        Public Property Skip As Integer

        Public Sub New(skip As Integer)
            Me.Skip = skip
        End Sub
    End Class

End Namespace
