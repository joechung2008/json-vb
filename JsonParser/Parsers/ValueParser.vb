Imports JsonParser.Models
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class ValueParser
        Private Shared Function GetNumberRegex() As Regex
            Return New Regex("[\-\d]")
        End Function

        Private Shared Function GetWhitespaceRegex() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            Array
            FalseMode
            Null
            Number
            ObjectMode
            StringMode
            TrueMode
            EndMode
        End Enum

        Public Shared Function Parse(s As String, Optional delimiters As Regex = Nothing) As Token
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0
            Dim slice As String
            Dim token As Token = Nothing

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "[" Then
                            mode = Mode.Array
                        ElseIf ch = "f" Then
                            mode = Mode.FalseMode
                        ElseIf ch = "n" Then
                            mode = Mode.Null
                        ElseIf GetNumberRegex().IsMatch(ch) Then
                            mode = Mode.Number
                        ElseIf ch = "{" Then
                            mode = Mode.ObjectMode
                        ElseIf ch = """" Then
                            mode = Mode.StringMode
                        ElseIf ch = "t" Then
                            mode = Mode.TrueMode
                        Else
                            Throw New Exception($"Unexpected character '{ch}'")
                        End If

                    Case Mode.Array
                        slice = s.Substring(pos)
                        token = ArrayParser.Parse(slice)
                        pos += token.Skip
                        mode = Mode.EndMode

                    Case Mode.FalseMode
                        slice = s.Substring(pos, 5)
                        If slice = "false" Then
                            token = New FalseToken(5)
                            pos += token.Skip
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Expected 'false', actual '{slice}'")
                        End If

                    Case Mode.Null
                        slice = s.Substring(pos, 4)
                        If slice = "null" Then
                            token = New NullToken(4)
                            pos += token.Skip
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Expected 'null', actual '{slice}'")
                        End If

                    Case Mode.Number
                        slice = s.Substring(pos)
                        token = NumberParser.Parse(slice, delimiters)
                        pos += token.Skip
                        mode = Mode.EndMode

                    Case Mode.ObjectMode
                        slice = s.Substring(pos)
                        token = ObjectParser.Parse(slice)
                        pos += token.Skip
                        mode = Mode.EndMode

                    Case Mode.StringMode
                        slice = s.Substring(pos)
                        token = StringParser.Parse(slice)
                        pos += token.Skip
                        mode = Mode.EndMode

                    Case Mode.TrueMode
                        slice = s.Substring(pos, 4)
                        If slice = "true" Then
                            token = New TrueToken(4)
                            pos += token.Skip
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Expected 'true', actual '{slice}'")
                        End If

                    Case Mode.EndMode
                        ' Do nothing
                End Select
            End While

            If token Is Nothing Then
                Throw New Exception("value cannot be empty")
            End If

            token.Skip = pos
            Return token
        End Function
    End Class

End Namespace
