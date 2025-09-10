Imports JsonParser.Models
Imports JsonParser.Parsers
Imports Xunit

Namespace JsonParser.Tests.Parsers

    Public Class ArrayParserTests

        <Fact>
        Public Sub Parse_EmptyArray_ReturnsArrayTokenWithNoElements()
            Dim token = ArrayParser.Parse("[]")
            Assert.NotNull(token)
            Assert.Equal(0, token.Elements.Count())
            Assert.Equal(2, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ArrayOfNumbers_ReturnsArrayTokenWithCorrectElements()
            Dim token = ArrayParser.Parse("[1,2,3]")
            Assert.NotNull(token)
            Assert.Equal(3, token.Elements.Count())
            For Each e In token.Elements
                Assert.IsType(Of NumberToken)(e)
            Next
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_ArrayOfStrings_ReturnsArrayTokenWithCorrectElements()
            Dim token = ArrayParser.Parse("[""a"",""b""]")
            Assert.NotNull(token)
            Assert.Equal(2, token.Elements.Count())
            For Each e In token.Elements
                Assert.IsType(Of StringToken)(e)
            Next
            Assert.Equal(9, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_MixedArray_ReturnsArrayTokenWithMixedTypes()
            Dim token = ArrayParser.Parse("[1,""a"",true]")
            Assert.NotNull(token)
            Assert.Equal(3, token.Elements.Count())
            Assert.IsType(Of NumberToken)(token.Elements.ElementAt(0))
            Assert.IsType(Of StringToken)(token.Elements.ElementAt(1))
            Assert.IsType(Of TrueToken)(token.Elements.ElementAt(2))
            Assert.Equal(12, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_NestedArray_ReturnsArrayTokenWithArrayElement()
            Dim token = ArrayParser.Parse("[[1],2]")
            Assert.NotNull(token)
            Assert.Equal(2, token.Elements.Count())
            Assert.IsType(Of ArrayToken)(token.Elements.ElementAt(0))
            Assert.IsType(Of NumberToken)(token.Elements.ElementAt(1))
            Assert.Equal(7, token.Skip)
        End Sub

        <Fact>
        Public Sub Parse_InvalidArray_ThrowsException()
            Assert.Throws(Of System.Exception)(Sub() ArrayParser.Parse("[1,]"))
        End Sub

    End Class

End Namespace
