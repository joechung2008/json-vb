# json-vb

Port of the TypeScript JSON parser to Visual Basic in .NET 8.0.

## License

MIT

## Reference

[json.org](https://www.json.org/json-en.html)

## Building

To build the project, ensure you have .NET 8.0 SDK installed. Run the following command in the root directory:

```bash
dotnet build
```

This will build all projects in the solution.

## Formatting

To format the code, use the .NET CLI formatter:

```bash
dotnet format json-vb.sln
```

To verify that no formatting changes are required:

```bash
dotnet format json-vb.sln --verify-no-changes
```

This will automatically format all VB files according to the standard rules.

## Testing

To run the unit tests:

```bash
dotnet test
```

This will execute all tests in the JsonParser.Tests project and display the results.

## Running the CLI

The CLI application can be run to parse JSON input. Build the project first, then run:

```bash
dotnet run --project CLI
```

### Input Methods

You can provide JSON input in several ways:

1. **Manual Entry**: Run the CLI and type JSON directly, then press Ctrl+D (Linux/Mac) or Ctrl+Z (Windows) to end input.

```bash
dotnet run --project CLI
{"name": "example", "value": 42}
# Press Ctrl+D
```

2. **Using echo**: Pipe JSON from echo.

```bash
echo '{"name": "example", "value": 42}' | dotnet run --project CLI
```

3. **From a File**: Pipe JSON from a file.

```bash
cat sample.json | dotnet run --project CLI
```

Or using input redirection:

```bash
dotnet run --project CLI < sample.json
```

## Running the API

The solution includes a REST API that provides JSON parsing functionality via HTTP endpoints.

### Starting the API

To run the API server:

```bash
dotnet run --project API
```

The API will start on:

- **HTTP**: `http://localhost:8000`
- **HTTPS**: `https://localhost:8001`

### API Endpoints

#### POST `/api/v1/parse`

Parses JSON input and returns the parsed result.

**Request:**

- **Method**: POST
- **Content-Type**: `text/plain`
- **Body**: Raw JSON string

**Success Response (200):**

- **Content-Type**: `text/plain`
- **Body**: Pretty-printed JSON

**Error Responses:**

- **400 Bad Request**: Invalid JSON syntax

```json
{
  "code": 400,
  "message": "Error message describing the parsing issue"
}
```

- **415 Unsupported Media Type**: Wrong content type

```json
{
  "code": 415,
  "message": "Unsupported Media Type. Please use 'text/plain'."
}
```

## Testing with REST Client Extension

The API includes test data files in the `API/testdata/` directory that can be used with VS Code's REST Client extension for easy testing.

### Setup

1. Install the [REST Client extension](https://marketplace.visualstudio.com/items?itemName=humao.rest-client) for VS Code
2. Open any `.rest` file from the `API/testdata/` directory

### Using Test Files

1. Start the API server (see above)
2. Open any `.rest` file in VS Code
3. Click the "Send Request" link above each HTTP request in the file
4. View the response in the REST Client output panel

### Example Test Request

```http
POST http://localhost:8000/api/v1/parse
Content-Type: text/plain

{"name": "test", "value": 123}
```

This will send a POST request to the API with JSON content and display the parsed, pretty-printed result.
