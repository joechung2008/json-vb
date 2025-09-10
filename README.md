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
