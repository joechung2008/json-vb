Imports JsonParser.Models
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class Pair
        Private Shared Function GetDelimitersRegex() As Regex
            Return New Regex("[ \n\r\t\},]")
        End Function

        Private Shared Function GetWhitespaceRegex() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            Key
            Colon
            Value
            EndMode
        End Enum

        Public Shared Function Parse(s As String) As PairToken
            Dim key As StringToken = Nothing
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0
            Dim slice As String
            Dim value As Token = Nothing

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        Else
                            mode = Mode.Key
                        End If

                    Case Mode.Key
                        slice = s.Substring(pos)
                        key = StringParser.Parse(slice)
                        pos += key.Skip
                        mode = Mode.Colon

                    Case Mode.Colon
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = ":" Then
                            pos += 1
                            mode = Mode.Value
                        Else
                            Throw New Exception($"Expected ':', actual '{ch}'")
                        End If

                    Case Mode.Value
                        slice = s.Substring(pos)
                        value = ValueParser.Parse(slice, GetDelimitersRegex())
                        pos += value.Skip
                        mode = Mode.EndMode

                    Case Mode.EndMode
                        ' Do nothing

                    Case Else
                        Throw New Exception($"Unexpected mode {mode}")
                End Select
            End While

            If key Is Nothing Then
                Throw New Exception("Invalid pair: missing key")
            ElseIf value Is Nothing Then
                Throw New Exception("Invalid pair: missing value")
            Else
                Return New PairToken(pos, key, value)
            End If
        End Function
    End Class

End Namespace
