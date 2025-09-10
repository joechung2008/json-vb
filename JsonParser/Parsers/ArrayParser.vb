Imports JsonParser.Models
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class ArrayParser
        Private Shared Function DelimitersRegex() As Regex
            Return New Regex("[,\]]")
        End Function

        Private Shared Function WhitespaceRegex() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            Element
            Comma
            EndMode
        End Enum

        Public Shared Function Parse(s As String) As ArrayToken
            Dim elements = New List(Of Token)()
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If WhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "[" Then
                            pos += 1
                            mode = Mode.Element
                        Else
                            Throw New Exception($"Expected '[', actual '{ch}'")
                        End If

                    Case Mode.Element
                        If WhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "]" Then
                            If elements.Count > 0 Then
                                Throw New Exception("Unexpected ','")
                            End If
                            pos += 1
                            mode = Mode.EndMode
                        Else
                            Dim slice = s.Substring(pos)
                            Dim element = ValueParser.Parse(slice, DelimitersRegex())
                            elements.Add(element)
                            pos += element.Skip
                            mode = Mode.Comma
                        End If

                    Case Mode.Comma
                        If WhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "," Then
                            pos += 1
                            mode = Mode.Element
                        ElseIf ch = "]" Then
                            pos += 1
                            mode = Mode.EndMode
                        End If

                    Case Mode.EndMode
                        ' Do nothing

                    Case Else
                        Throw New Exception($"Unexpected mode {mode}")
                End Select
            End While

            Return New ArrayToken(pos, elements)
        End Function
    End Class

End Namespace
