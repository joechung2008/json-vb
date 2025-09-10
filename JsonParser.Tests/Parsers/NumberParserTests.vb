Imports System.Text.RegularExpressions
Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class NumberParserTests

        <Fact>
        Public Sub Parse_Integer_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("123")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(123D, token.Value)
            Assert.Equal(3, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_NegativeInteger_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("-456")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(-456D, token.Value)
            Assert.Equal(4, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_Decimal_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("3.14")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(3.14D, token.Value)
            Assert.Equal(4, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_Zero_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("0")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(0D, token.Value)
            Assert.Equal(1, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_LargeNumber_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("1234567890")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(1234567890D, token.Value)
            Assert.Equal(10, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidNumber_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() JsonParser.Parsers.NumberParser.Parse("abc"))
        End Sub

        <Fact>
        Public Sub Parse_NumberWithExponent_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("1e3")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(1000D, token.Value)
            Assert.Equal(3, token.Skip)

            token = JsonParser.Parsers.NumberParser.Parse("-2.5E-2")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(-0.025D, token.Value)
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_NumberWithTrailingSpace_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("42 ")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(42D, token.Value)
            Assert.Equal(3, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_NumberWithTrailingTab_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("123" & vbTab)
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(123D, token.Value)
            Assert.Equal(4, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_DecimalWithTrailingNewline_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("3.14" & vbLf)
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(3.14D, token.Value)
            Assert.Equal(5, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ExponentWithTrailingCarriageReturn_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("8.9e2" & vbCr)
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(890D, token.Value)
            Assert.Equal(6, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ExponentWithTrailingSpaces_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("-0.5E-1  ")
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(-0.05D, token.Value)
            Assert.Equal(9, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_NumberWithDelimiter_ReturnsNumberToken()
            Dim token = JsonParser.Parsers.NumberParser.Parse("77,", New Regex(","))
            Assert.NotNull(token)
            Assert.IsType(Of NumberToken)(token)
            Assert.Equal(77D, token.Value)
            Assert.Equal(2, token.Skip)
        End Sub

    End Class

End Namespace
