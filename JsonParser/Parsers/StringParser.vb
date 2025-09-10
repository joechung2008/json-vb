Imports JsonParser.Models
Imports System.Globalization
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class StringParser
        Private Shared Function GetWhitespaceRegex() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            LeftQuote
            Character
            EscapedChar
            Unicode
            EndMode
        End Enum

        Public Shared Function Parse(s As String) As StringToken
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0
            Dim value = ""

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = """" Then
                            mode = Mode.LeftQuote
                        Else
                            Throw New Exception($"Expected '""', actual '{ch}'")
                        End If

                    Case Mode.LeftQuote
                        If ch = """" Then
                            value = ""
                            pos += 1
                            mode = Mode.Character
                        Else
                            Throw New Exception($"Expected '""', actual '{ch}'")
                        End If

                    Case Mode.Character
                        If ch = "\" Then
                            pos += 1
                            mode = Mode.EscapedChar
                        ElseIf ch = """" Then
                            pos += 1
                            mode = Mode.EndMode
                        ElseIf ch <> vbLf And ch <> vbCr Then
                            value &= ch
                            pos += 1
                        Else
                            Throw New Exception($"Unexpected character '{ch}'")
                        End If

                    Case Mode.EscapedChar
                        If ch = "\" Or ch = """" Or ch = "/" Then
                            value &= ch
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "b" Then
                            value &= vbBack
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "f" Then
                            value &= vbFormFeed
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "n" Then
                            value &= vbLf
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "r" Then
                            value &= vbCr
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "t" Then
                            value &= vbTab
                            pos += 1
                            mode = Mode.Character
                        ElseIf ch = "u" Then
                            pos += 1
                            mode = Mode.Unicode
                        End If

                    Case Mode.Unicode
                        Dim slice = s.Substring(pos, 4)
                        Dim hex = Integer.Parse(slice, NumberStyles.HexNumber)
                        value &= Convert.ToChar(hex)
                        pos += 4
                        mode = Mode.Character

                    Case Mode.EndMode
                        ' Do nothing

                    Case Else
                        Throw New Exception($"Unexpected mode {mode}")
                End Select
            End While

            Return New StringToken(pos, value)
        End Function
    End Class

End Namespace
