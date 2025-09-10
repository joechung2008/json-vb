Imports JsonParser
Imports System.IO

Module Program
    Sub Main(args As String())
        Try
            ' Read all input from standard input
            Dim input As String
            Using reader As New StreamReader(Console.OpenStandardInput())
                input = reader.ReadToEnd()
            End Using

            ' Parse input using JsonParser JSON parser
            Dim parsed = JSON.Parse(input)

            ' Output pretty-printed result
            Console.WriteLine(parsed.ToString())
        Catch ex As Exception
            Console.Error.WriteLine("Error parsing JSON: " & ex.Message)
            Environment.Exit(1)
        End Try
    End Sub
End Module
