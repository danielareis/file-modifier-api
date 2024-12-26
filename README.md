# FileModifierApi

## Overview

FileModifierApi is a .NET 8 REST API that allows users to upload a text file, mutate its content by appending the current date and a random string, and then download the mutated file. The API is equipped with a Swagger UI for easy testing and interaction.

## Features

- **File Upload**: Users can upload a text file through the Swagger UI.
- **File Mutation**: The uploaded file's content is mutated by appending the current date and a random string.
- **File Download**: The mutated file is returned to the user for download.
- **Dependency Injection**: Utilizes dependency injection for service management.
- **Unit Testing**: Includes unit tests using NUnit.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio 2022](https://visualstudio.microsoft.com/vs/)

### Running the Application

1. Clone the repository: https://github.com/danielareis/file-modifier-api

2. Open the solution in Visual Studio 2022.

3. Build the solution to restore the dependencies.

4. Run the application:
   - You can run the application using IIS Express or by selecting the `http` or `https` profile in Visual Studio.
   - The application will launch and open the Swagger UI in your default browser.

### Using the API

1. Open the Swagger UI at `http://localhost:5166/swagger` (or the appropriate URL based on your launch profile).

2. Use the `/api/File/upload` endpoint to upload a text file.

3. The API will return the mutated file for download.

## Project Structure

- **FileModifierApi/Controllers/FileController.cs**: Contains the API endpoint for file upload and mutation.
- **FileModifierApi/Services/FileService.cs**: Contains the logic for mutating the file content.
- **FileModifier.Tests/FileServiceTests.cs**: Contains unit tests for the `FileService`.

## Unit Tests

The project includes unit tests using NUnit. To run the tests:

1. Open the Test Explorer in Visual Studio (`Test > Test Explorer`).

2. Run all tests to ensure they pass.