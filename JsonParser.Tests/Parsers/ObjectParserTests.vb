Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class ObjectParserTests

        <Fact>
        Public Sub Parse_EmptyObject_ReturnsObjectTokenWithZeroMembers()
            Dim token = JsonParser.Parsers.ObjectParser.Parse("{}")
            Assert.NotNull(token)
            Assert.Equal(0, token.Members.Count())
            Assert.Equal(2, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ObjectWithOneMember_ReturnsObjectTokenWithOneMember()
            Dim token = JsonParser.Parsers.ObjectParser.Parse("{""a"":1}")
            Assert.NotNull(token)
            Assert.Equal(1, token.Members.Count())
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ObjectWithMultipleMembers_ReturnsObjectTokenWithCorrectMembers()
            Dim token = JsonParser.Parsers.ObjectParser.Parse("{""a"":1,""b"":2}")
            Assert.NotNull(token)
            Assert.Equal(2, token.Members.Count())
            Assert.Equal(13, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ObjectWithWhitespace_IgnoresWhitespace()
            Dim token = JsonParser.Parsers.ObjectParser.Parse("{  ""a""  :  1  }")
            Assert.NotNull(token)
            Assert.Equal(1, token.Members.Count())
            Assert.Equal(15, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidObject_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() JsonParser.Parsers.ObjectParser.Parse("{a:1}"))
        End Sub

    End Class

End Namespace
