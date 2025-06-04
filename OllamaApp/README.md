# OllamaApp

A .NET 9.0 console application for interacting with Ollama AI models.

## Description

OllamaApp is a console application built with .NET 9.0 that provides functionality to interact with Ollama AI models.
This application enables users to leverage local AI capabilities through a simple command-line interface.

Example used from Tools and Skills for .NET 8 by MJ Price, p. 341 ff.
This project was created and tested on Windows 11 only.

## Dependencies

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or later
- [Ollama](https://ollama.ai/) installed and running locally
- [OllamaSharp](https://github.com/awaescher/OllamaSharp) - .NET bindings for the Ollama API
- [Spectre.Console](https://spectreconsole.net/) - Modern console UI library for .NET

OllamaSharp provides .NET bindings for the Ollama API, simplifying interactions with Ollama both locally and remotely.

Spectre.Console is a .NET library designed to help developers create beautiful,
cross-platform console applications with rich formatting, tables, progress bars, and more.

## Installation

1. Clone the repository or navigate to the OllamaApp directory
2. Restore the project dependencies:

   ```bash
   dotnet restore
   ```

3. Build the application:

   ```bash
   dotnet build
   ```

## Usage

Run the application using the .NET CLI:

```bash
dotnet run
```

Or build and run the executable:

```bash
dotnet build --configuration Release
cd bin/Release/net9.0
./OllamaApp
```

## Features

- Console-based interface for AI model interactions
- Built with .NET 9.0 for optimal performance
- Nullable reference types enabled for better code safety
- Implicit usings for cleaner code

## Development

### Building

To build the project:

```bash
dotnet build
```

### Running in Development

To run the project in development mode:

```bash
dotnet run
```