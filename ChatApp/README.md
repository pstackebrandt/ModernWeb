# ChatApp

A .NET 9 console application demonstrating AI-powered conversational
experiences using Microsoft's Semantic Kernel.

Based on the original project from [tools-skills-net8/Chapter09](https://github.com/markjprice/tools-skills-net8/tree/main/code/Chapter09/ChatApp).

## Components

- **Semantic Kernel** - AI capabilities and chat functions orchestration
- **Microsoft Extensions Configuration** - Application settings management  
- **Console Logging** - Diagnostics and output
- **HTTP Resilience** - Network communication

## Getting Started

### Prerequisites

- .NET 9 SDK (9.0.300 or later)
- Visual Studio 2022 or VS Code

### Setup

1. Configure your API settings in `appsettings.json` (see `appsettings.example.json`)
2. Or use environment variables: `SemanticKernel__ApiKey=your-api-key`

### Usage

**Run with Launch Profile:**
- **Visual Studio**: Select "ChatApp" profile and press F5 or Ctrl+F5
- **VS Code**: Use the "ChatApp" launch configuration from `Properties/launchSettings.json`
- **Command Line with Profile**:
In project root directory

```powershell
dotnet run --launch-profile ChatApp
```
The launch profile automatically sets environment variables defined in `launchSettings.json`,
making it easier to run with pre-configured settings.

## Purpose

Demonstrates building intelligent chat interfaces that combine traditional
application data with AI-powered conversational abilities using .NET 9
and Microsoft Semantic Kernel.