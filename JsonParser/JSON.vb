Imports JsonParser.Models
Imports JsonParser.Parsers

Namespace JsonParser

    Public Class JSON
        Public Shared Function Parse(json As String) As Token
            Return ValueParser.Parse(json)
        End Function
    End Class

End Namespace
