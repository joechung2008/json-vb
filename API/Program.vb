Imports System.IO
Imports System.Threading.Tasks
Imports System.Text.Json
Imports System.Text.Encodings.Web
Imports JsonParser
Imports Microsoft.AspNetCore.Builder
Imports Microsoft.AspNetCore.Hosting
Imports Microsoft.AspNetCore.Http
Imports Microsoft.Extensions.Hosting

Module Program
    Private ReadOnly JsonOptions As New JsonSerializerOptions() With {
        .Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
    }

    Sub Main(args As String())
        Dim builder = WebApplication.CreateBuilder(args)
        builder.WebHost.UseUrls("http://localhost:8000", "https://localhost:8001")
        Dim app = builder.Build()

        app.MapPost("/api/v1/parse", AddressOf HandleParseAsync)

        app.Run()
    End Sub

    Private Async Function HandleParseAsync(context As HttpContext) As Task
        If context.Request.ContentType <> "text/plain" Then
            Await SendJsonErrorAsync(context, 415, "Unsupported Media Type. Please use 'text/plain'.")
            Return
        End If

        Dim body As String = Nothing
        Dim parsed As Object = Nothing
        Dim errorMessage As String = Nothing

        Try
            body = Await New StreamReader(context.Request.Body).ReadToEndAsync()
            parsed = JSON.Parse(body)
        Catch ex As Exception
            errorMessage = ex.Message
        End Try

        If errorMessage IsNot Nothing Then
            Await SendJsonErrorAsync(context, 400, errorMessage)
        Else
            context.Response.ContentType = "application/json"
            Await context.Response.WriteAsync(parsed.ToString())
        End If
    End Function

    Private Async Function SendJsonErrorAsync(context As HttpContext, ByVal statusCode As Integer, ByVal message As String) As Task
        Dim errorObj = New With {.code = statusCode, .message = message}
        Dim errorJson = JsonSerializer.Serialize(errorObj, JsonOptions)
        context.Response.StatusCode = statusCode
        context.Response.ContentType = "application/json"
        Await context.Response.WriteAsync(errorJson)
    End Function
End Module
