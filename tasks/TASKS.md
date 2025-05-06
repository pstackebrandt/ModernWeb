# Project Tasks

This file is a task list for the project.
I try to update it automatically using cursor without specific instructions.
This doesn't work well, but for now I update the file seldom manually. I want to see where the automation fails or succeeds.

## Current Steps

### Infrastructure Setup
- [X] Initial project setup
  - [X] Setup git
  - [X] Setup Central Package Management
  - [X] Create PowerShell script for project creation
- [X] Add project with Entity Models for Sqlite
- [X] Create Web App
  - [X] Use New-DotNetProject.ps1 script
  - [X] Implement Blazor Static SSR

### Database Setup
- [🟡] Setup Example Northwind DB, p. 633,634
- [🟡] Setup Entity Model
  - [ ] Verify Entity Models against DB schema
  - [ ] Add necessary model configurations
  - [ ] Test data access
  - [ ] Move connection string to configuration (Recommended)

### Web Application Configuration
- [X] Basic Project Setup
  - [🟢] Configure app settings (appsettings.json)
  - [🟢] Setup environment-specific configurations
  - [X] Configure HTTPS
    - [X] Setup redirection from HTTP to HTTPS
    - [X] Configure development certificates
    - [X] Test with REST Client
- [🟡] Setup Infrastructure
  - [🟡] Configure dependency injection
  - [ ] Setup logging
  - [ ] Configure error handling (Recommended)
  - [ ] Add DB Context registration (Recommended)
- [🟡] Create Basic UI Components
  - [X] Initial Blazor SSR setup
  - [X] Layout template
  - [X] Navigation menu
  - [ ] Error pages (Not found)
  - [ ] Loading states (Not found)

## Backlog
- [ ] Add Authentication/Authorization
- [ ] Implement CRUD operations
  - [ ] List view components
  - [ ] Detail view components
  - [ ] Edit forms
  - [ ] Delete confirmations
- [ ] Add Unit Tests (Recommended to start)
  - [ ] Service layer tests
  - [ ] Component tests
- [ ] Add Integration Tests
  - [ ] API endpoint tests
  - [ ] DB integration tests
- [ ] Setup CI/CD Pipeline
  - [ ] Build automation
  - [ ] Test automation
  - [ ] Deployment scripts
- [ ] Enhance Blazor Implementation
  - [ ] Add interactive components
  - [ ] Implement client-side functionality
  - [ ] Optimize SSR performance
  - [ ] Add progressive enhancement

## Completed
- [X] Create PowerShell script (scripts/New-DotNetProject.ps1)
  - Supports creating new .NET projects
  - Integrates with solution and CPM
  - Features:
    - WhatIf support for preview
    - Optional solution-level package restore
    - Configurable .NET version, HTTPS, Docker support
    - Documentation and error handling
- [X] Create initial web application using script
- [X] Configure HTTPS and test with REST Client
  - Setup proper redirection from HTTP to HTTPS
  - Configure development certificates
  - Create test suite in test-api.http
- [X] Implement Blazor Static SSR
  - Setup initial Blazor configuration
  - Configure static server-side rendering
  - Test basic page rendering
- [X] Layout template (MainLayout.razor)
- [X] Navigation menu (NavMenu.razor)
- [X] Configure app settings with environment-specific configurations
  - Added appsettings.json and appsettings.Development.json
  - Created example configuration files for reference
  - Implemented environment-specific settings structure

## Task Status Legend
- 🔴 Not Started
- 🟡 In Progress
- 🟢 Completed
- ⚫ Blocked

## Notes
- Update this file as tasks progress
- Add new tasks as they are identified
- Move completed tasks to the Completed section
- Include relevant issue numbers or references
- Recommendations:
  - Move DB connection string to configuration files
  - Add error and loading state components/pages
  - Register DB context and add error handling in web app startup
  - Add at least a basic test project to start unit/integration testing