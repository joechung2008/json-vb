Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class PairParserTests

        <Fact>
        Public Sub Parse_SimplePair_ReturnsPairToken()
            Dim token = JsonParser.Parsers.PairParser.Parse("""a"":1")
            Assert.NotNull(token)
            Assert.Equal("a", token.Key.Value)
            Assert.IsType(Of NumberToken)(token.Value)
            Assert.Equal(5, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_PairWithWhitespace_ReturnsPairToken()
            Dim token = JsonParser.Parsers.PairParser.Parse("  ""b""  :  ""val""  ")
            Assert.NotNull(token)
            Assert.Equal("b", token.Key.Value)
            Assert.IsType(Of StringToken)(token.Value)
            Assert.Equal(15, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_PairWithBooleanValue_ReturnsPairToken()
            Dim token = JsonParser.Parsers.PairParser.Parse("""flag"":true")
            Assert.NotNull(token)
            Assert.Equal("flag", token.Key.Value)
            Assert.IsType(Of TrueToken)(token.Value)
            Assert.Equal(11, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_PairWithNullValue_ReturnsPairToken()
            Dim token = JsonParser.Parsers.PairParser.Parse("""x"":null")
            Assert.NotNull(token)
            Assert.Equal("x", token.Key.Value)
            Assert.IsType(Of NullToken)(token.Value)
            Assert.Equal(8, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_PairWithEscapedKey_ReturnsPairToken()
            Dim token = JsonParser.Parsers.PairParser.Parse("""a\""b"":2")
            Assert.NotNull(token)
            Assert.Equal("a""b", token.Key.Value)
            Assert.IsType(Of NumberToken)(token.Value)
            Assert.Equal(8, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidPair_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() JsonParser.Parsers.PairParser.Parse("a:1"))
        End Sub

    End Class

End Namespace
