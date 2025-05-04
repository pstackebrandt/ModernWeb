# Project Tasks

## Current Steps

### Infrastructure Setup
- [X] Initial project setup
  - [X] Setup git
  - [X] Setup Central Package Management
  - [X] Create PowerShell script for project creation
- [X] Add project with Entity Models for Sqlite

### Database Setup
- [🟡] Setup Example Northwind DB, p. 633,634
- [🟡] Setup Entity Model
  - [ ] Verify Entity Models against DB schema
  - [ ] Add necessary model configurations
  - [ ] Test data access

### Web Application
- [ ] Create Blazor Web App (Server-Side)
  - [ ] Use New-DotNetProject.ps1 script
  - [ ] Configure app settings
  - [ ] Setup basic project structure
- [ ] Setup Basic Infrastructure
  - [ ] Configure dependency injection
  - [ ] Setup logging
  - [ ] Configure error handling
- [ ] Create Basic UI Components
  - [ ] Layout template
  - [ ] Navigation menu
  - [ ] Error pages

## Backlog
- [ ] Add Authentication/Authorization
- [ ] Implement CRUD operations
- [ ] Add Unit Tests
- [ ] Add Integration Tests
- [ ] Setup CI/CD Pipeline

## Completed
- [X] Create PowerShell script (scripts/New-DotNetProject.ps1)
  - Supports creating new .NET projects
  - Integrates with solution and CPM
  - Features:
    - WhatIf support for preview
    - Optional solution-level package restore
    - Configurable .NET version, HTTPS, Docker support
    - Documentation and error handling

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