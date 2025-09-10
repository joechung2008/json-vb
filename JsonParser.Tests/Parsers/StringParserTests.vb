Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class StringParserTests

        <Fact>
        Public Sub Parse_SimpleString_ReturnsStringToken()
            Dim token = JsonParser.Parsers.StringParser.Parse("""hello""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal("hello", token.Value)
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_EmptyString_ReturnsStringToken()
            Dim token = JsonParser.Parsers.StringParser.Parse("""""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal("", token.Value)
            Assert.Equal(2, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_StringWithEscapedQuote_ReturnsStringToken()
            Dim token = JsonParser.Parsers.StringParser.Parse("""he\""llo""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal("he""llo", token.Value)
            Assert.Equal(9, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_StringWithEscapedBackslash_ReturnsStringToken()
            Dim token = JsonParser.Parsers.StringParser.Parse("""he\\llo""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal("he\llo", token.Value)
            Assert.Equal(9, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_StringWithUnicode_ReturnsStringToken()
            Dim token = JsonParser.Parsers.StringParser.Parse("""hi\u0041""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal("hiA", token.Value)
            Assert.Equal(10, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidString_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() JsonParser.Parsers.StringParser.Parse("hello"))
        End Sub

    End Class

End Namespace
