Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class ValueParserTests

        <Fact>
        Public Sub Parse_Number_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("42")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(2, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_String_ReturnsStringToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("""hello""")
            Assert.NotNull(token)
            Assert.IsType(Of StringToken)(token)
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_True_ReturnsTrueToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("true")
            Assert.NotNull(token)
            Assert.IsType(Of TrueToken)(token)
            Assert.Equal(4, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_False_ReturnsFalseToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("false")
            Assert.NotNull(token)
            Assert.IsType(Of FalseToken)(token)
            Assert.Equal(5, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_Null_ReturnsNullToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("null")
            Assert.NotNull(token)
            Assert.IsType(Of NullToken)(token)
            Assert.Equal(4, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_Object_ReturnsObjectToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("{""a"":1}")
            Assert.NotNull(token)
            Assert.IsType(Of ObjectToken)(token)
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_Array_ReturnsArrayToken()
            Dim token = JsonParser.Parsers.ValueParser.Parse("[1,2]")
            Assert.NotNull(token)
            Assert.IsType(Of ArrayToken)(token)
            Assert.Equal(5, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidValue_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() JsonParser.Parsers.ValueParser.Parse("???"))
        End Sub

    End Class

End Namespace
