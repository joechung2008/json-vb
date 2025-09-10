Imports System.Linq

Namespace JsonParser.Models

    Public Class ObjectToken
        Inherits Token

        Public Property Members As IEnumerable(Of PairToken)

        Public Sub New(skip As Integer, members As IEnumerable(Of PairToken))
            MyBase.New(skip)
            Me.Members = members
        End Sub

        Public Overrides Function ToString() As String
            Return "{" & String.Join(",", Members.Select(Function(member) member.ToString())) & "}"
        End Function
    End Class

End Namespace
