Imports JsonParser.Models
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class NumberParser
        Private Shared Function Digits() As Regex
            Return New Regex("\d")
        End Function

        Private Shared Function NonZeroDigits() As Regex
            Return New Regex("[1-9]")
        End Function

        Private Shared Function Whitespace() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            Characteristic
            CharacteristicDigit
            DecimalPoint
            Mantissa
            Exponent
            ExponentSign
            ExponentFirstDigit
            ExponentDigits
            EndMode
        End Enum

        Public Shared Function Parse(s As String, Optional delimiters As Regex = Nothing) As NumberToken
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0
            Dim value = ""

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If Whitespace().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "-" Then
                            pos += 1
                            value &= "-"
                        ElseIf Digits().IsMatch(ch) Then
                            mode = Mode.Characteristic
                        Else
                            Throw New Exception($"Expected '-' or digit, actual '{ch}'")
                        End If

                    Case Mode.Characteristic
                        If ch = "0" Then
                            value &= "0"
                            pos += 1
                            mode = Mode.DecimalPoint
                        ElseIf NonZeroDigits().IsMatch(ch) Then
                            value &= ch
                            pos += 1
                            mode = Mode.CharacteristicDigit
                        Else
                            Throw New Exception($"Expected digit, actual '{ch}'")
                        End If

                    Case Mode.CharacteristicDigit
                        If Digits().IsMatch(ch) Then
                            value &= ch
                            pos += 1
                        ElseIf delimiters IsNot Nothing AndAlso delimiters.IsMatch(ch) Then
                            mode = Mode.EndMode
                        Else
                            mode = Mode.DecimalPoint
                        End If

                    Case Mode.DecimalPoint
                        If ch = "." Then
                            value &= "."
                            pos += 1
                            mode = Mode.Mantissa
                        Else
                            mode = Mode.Exponent
                        End If

                    Case Mode.Mantissa
                        If Digits().IsMatch(ch) Then
                            value &= ch
                            pos += 1
                        Else
                            mode = Mode.Exponent
                        End If

                    Case Mode.Exponent
                        If ch = "e" Or ch = "E" Then
                            value &= ch
                            pos += 1
                            mode = Mode.ExponentSign
                        ElseIf delimiters IsNot Nothing AndAlso delimiters.IsMatch(ch) Then
                            mode = Mode.EndMode
                        ElseIf pos >= s.Length - 1 Then
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Unexpected character '{ch}'")
                        End If

                    Case Mode.ExponentSign
                        If ch = "-" Or ch = "+" Then
                            value &= ch
                            pos += 1
                        End If
                        mode = Mode.ExponentFirstDigit

                    Case Mode.ExponentFirstDigit
                        If Digits().IsMatch(ch) Then
                            value &= ch
                            pos += 1
                            mode = Mode.ExponentDigits
                        Else
                            Throw New Exception($"Expected digit, actual '{ch}'")
                        End If

                    Case Mode.ExponentDigits
                        If Digits().IsMatch(ch) Then
                            value &= ch
                            pos += 1
                        ElseIf delimiters IsNot Nothing AndAlso delimiters.IsMatch(ch) Then
                            mode = Mode.EndMode
                        ElseIf Whitespace().IsMatch(ch) Then
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Expected digit, actual '{ch}'")
                        End If

                    Case Mode.EndMode
                        ' Do nothing
                End Select
            End While

            If mode = Mode.Characteristic Or mode = Mode.ExponentFirstDigit Then
                Throw New Exception($"Incomplete expression, mode = {mode}")
            End If

            ' Skip trailing whitespace after parsing the number
            While pos < s.Length AndAlso Whitespace().IsMatch(s.Substring(pos, 1))
                pos += 1
            End While

            Return New NumberToken(pos, Double.Parse(value))
        End Function
    End Class

End Namespace
