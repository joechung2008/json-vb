Imports System

Namespace JsonParser.Models

    Public Class FalseToken
        Inherits Token

        Public Sub New(skip As Integer)
            MyBase.New(skip)
        End Sub

        Public ReadOnly Property Value As Boolean
            Get
                Return False
            End Get
        End Property

        Public Overrides Function ToString() As String
            Return Boolean.FalseString.ToLower()
        End Function
    End Class

End Namespace
