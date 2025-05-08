[CmdletBinding(SupportsShouldProcess)]
param (
    [Parameter(Mandatory = $false)]
    [string]$ProjectName = "Northwind.Blazor",
    
    [Parameter(Mandatory = $false)]
    [string]$SolutionName = "ModernWeb",
    
    [Parameter(Mandatory = $false)]
    [string]$DotNetVersion = "9.0",
    
    [Parameter(Mandatory = $false)]
    [bool]$UseHttps = $true,
    
    [Parameter(Mandatory = $false)]
    [ValidateSet("Auto", "Server", "WebAssembly")]
    [string]$Interactivity = "Auto",
    
    [Parameter(Mandatory = $false)]
    [bool]$Authentication = $false,
    
    [Parameter(Mandatory = $false)]
    [ValidateSet("PerPage", "Global")]
    [string]$InteractivityLocation = "PerPage",
    
    [Parameter(Mandatory = $false)]
    [bool]$IncludeSamplePages = $true,
    
    [Parameter(Mandatory = $false)]
    [bool]$UseTopLevelStatements = $true,
    
    [Parameter(Mandatory = $false)]
    [bool]$AddToSolution = $true,

    [Parameter(Mandatory = $false)]
    [bool]$RestoreSolutionAfterUpdate = $false
)

<#
.SYNOPSIS
    Script is buggy. It creates a solution even if one already exists.
    Possibly it does not expect 2 projects to be created, so it will have problems adding the (second) project/s to the solution.
    I used this prompt manually in the solution folder and got also an additional solution:
    dotnet new blazor --interactivity Auto -o Northwind.Blazor --framework net9.0

    Creates a new Blazor Web App project and optionally adds it to an existing solution.

.DESCRIPTION
    This script creates a new Blazor Web App project with specified parameters and can add it
    to an existing solution. It's designed to work with Central Package Management
    (CPM) solutions that use Directory.Packages.props.

.PARAMETER ProjectName
    The name of the project to create (default: Northwind.Blazor)

.PARAMETER SolutionName
    The name of the solution file without extension (default: ModernWeb)

.PARAMETER DotNetVersion
    The target .NET version (default: 9.0)

.PARAMETER UseHttps
    Whether to enable HTTPS (default: true)

.PARAMETER Interactivity
    The interactivity mode (Auto, Server, or WebAssembly) (default: Auto)

.PARAMETER Authentication
    Whether to include authentication (default: false)

.PARAMETER InteractivityLocation
    Where to enable interactivity (PerPage or Global) (default: PerPage)

.PARAMETER IncludeSamplePages
    Whether to include sample pages (default: true)

.PARAMETER UseTopLevelStatements
    Whether to use top-level statements (default: true)

.PARAMETER AddToSolution
    Whether to add the project to the solution (default: true)

.PARAMETER RestoreSolutionAfterUpdate
    Whether to restore solution packages after project creation (default: false)

.EXAMPLE
    .\New-BlazorProject.ps1 -ProjectName "MyProject.Blazor"
    # Run the script with what-if from the solution root
    pwsh -Command ".\scripts\New-BlazorProject.ps1 -WhatIf"

.EXAMPLE
    .\New-BlazorProject.ps1 -ProjectName "MyProject.Blazor" -Interactivity "Server" -Authentication $true

    # Run the script with what-if from the solution root
    pwsh -Command ".\scripts\New-BlazorProject.ps1 -ProjectName 'Test.Project' -WhatIf"

.NOTES
    Author: Peter Stackebrandt
    Last Modified: 2024-03-21
    Requires: .NET SDK, PowerShell 7+
#>

# Set strict mode and error action
Set-StrictMode -Version Latest
$ErrorActionPreference = "Stop"

# Function to check prerequisites
function Test-Prerequisites {
    [CmdletBinding(SupportsShouldProcess)]
    param()

    # Check if solution file exists
    $solutionPath = "$(Get-Location)\$SolutionName.sln"
    if (-not (Test-Path $solutionPath)) {
        Write-Error "Solution file not found at: $solutionPath"
        exit 1
    }

    # Check if Directory.Packages.props exists
    if (-not (Test-Path "Directory.Packages.props")) {
        Write-Error "Directory.Packages.props not found in solution root"
        exit 1
    }

    # Check if .NET SDK is installed
    try {
        if ($PSCmdlet.ShouldProcess("dotnet --version", "Check .NET SDK version")) {
            $null = dotnet --version
        }
    }
    catch {
        Write-Error ".NET SDK is not installed or not in PATH"
        exit 1
    }
}

# Function to create the project
function New-Project {
    [CmdletBinding(SupportsShouldProcess)]
    param()

    $projectArgs = @(
        "new", "blazor",
        "--name", $ProjectName,
        "--framework", "net$DotNetVersion",
        "--interactivity", $Interactivity,
        # Skip automatic package restore since we're using Central Package Management
        # and want to restore at solution level to ensure consistent versions
        "--no-restore"
    )

    if (-not $UseHttps) {
        $projectArgs += "--no-https"
    }

    if ($Authentication) {
        $projectArgs += "--auth", "Individual"
    }

    if ($InteractivityLocation -ne "PerPage") {
        $projectArgs += "--interactivity-location", $InteractivityLocation
    }

    if (-not $IncludeSamplePages) {
        $projectArgs += "--no-include-sample-pages"
    }

    if (-not $UseTopLevelStatements) {
        $projectArgs += "--no-top-level-statements"
    }

    $cmdString = "dotnet $($projectArgs -join ' ')"
    if ($PSCmdlet.ShouldProcess($cmdString, "Create new project")) {
        Write-Host "Creating new Blazor Web App project: $ProjectName"
        & dotnet $projectArgs

        if ($LASTEXITCODE -ne 0) {
            Write-Error "Failed to create project"
            exit 1
        }

        Write-Host "`nIMPORTANT: To use Central Package Management (CPM):" -ForegroundColor Yellow
        Write-Host "1. Remove the <PackageReference> items from $ProjectName/$ProjectName.csproj"
        Write-Host "2. Run 'dotnet restore' at solution level"
        Write-Host "This ensures consistent package versions across your solution.`n"
    }
}

# Function to add project to solution
function Add-ProjectToSolution {
    [CmdletBinding(SupportsShouldProcess)]
    param()

    if ($AddToSolution) {
        $projectPath = "$(Get-Location)\$ProjectName\$ProjectName.csproj"
        $cmdString = "dotnet sln `"$SolutionName.sln`" add `"$projectPath`""
        
        if ($PSCmdlet.ShouldProcess($cmdString, "Add project to solution")) {
            Write-Host "Adding project to solution: $SolutionName"
            & dotnet sln "$SolutionName.sln" add $projectPath

            if ($LASTEXITCODE -ne 0) {
                Write-Error "Failed to add project to solution"
                exit 1
            }
        }
    }
}

# Main execution
try {
    Test-Prerequisites
    New-Project
    Add-ProjectToSolution
    
    if ($RestoreSolutionAfterUpdate) {
        Write-Host "Restoring solution packages..." -ForegroundColor Blue
        if ($PSCmdlet.ShouldProcess("dotnet restore", "Restore solution packages")) {
            & dotnet restore
            if ($LASTEXITCODE -ne 0) {
                Write-Error "Failed to restore solution packages"
                exit 1
            }
        }
    }
    
    if (-not $WhatIfPreference) {
        Write-Host "Project creation completed successfully" -ForegroundColor Green
    }
}
catch {
    Write-Error "An error occurred: $_"
    exit 1
} 