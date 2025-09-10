Imports JsonParser.Models
Imports System.Collections.Generic
Imports System.Text.RegularExpressions

Namespace JsonParser.Parsers

    Public Partial Class ObjectParser
        Private Shared Function GetWhitespaceRegex() As Regex
            Return New Regex("[ \n\r\t]")
        End Function

        Private Enum Mode
            Scanning
            Pair
            Delimiter
            EndMode
        End Enum

        Public Shared Function Parse(s As String) As ObjectToken
            Dim mode As Mode = Mode.Scanning
            Dim pos = 0
            Dim members = New List(Of PairToken)()

            While pos < s.Length AndAlso mode <> Mode.EndMode
                Dim ch = s.Substring(pos, 1)

                Select Case mode
                    Case Mode.Scanning
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "{" Then
                            pos += 1
                            mode = Mode.Pair
                        Else
                            Throw New Exception($"Expected '{{', actual '{ch}'")
                        End If

                    Case Mode.Pair
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "}" Then
                            If members.Count > 0 Then
                                Throw New Exception("Unexpected ','")
                            End If
                            pos += 1
                            mode = Mode.EndMode
                        Else
                            Dim slice = s.Substring(pos)
                            Dim pair = PairParser.Parse(slice)
                            members.Add(pair)
                            pos += pair.Skip
                            mode = Mode.Delimiter
                        End If

                    Case Mode.Delimiter
                        If GetWhitespaceRegex().IsMatch(ch) Then
                            pos += 1
                        ElseIf ch = "," Then
                            pos += 1
                            mode = Mode.Pair
                        ElseIf ch = "}" Then
                            pos += 1
                            mode = Mode.EndMode
                        Else
                            Throw New Exception($"Expected ',' or '}}', actual '{ch}'")
                        End If

                    Case Mode.EndMode
                        ' Do nothing

                    Case Else
                        Throw New Exception($"Unexpected mode {mode}")
                End Select
            End While

            Return New ObjectToken(pos, members)
        End Function
    End Class

End Namespace
