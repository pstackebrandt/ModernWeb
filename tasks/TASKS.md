# Project Tasks

## Current Steps

### Infrastructure Setup
- [X] Initial project setup
  - [X] Setup git
  - [X] Setup Central Package Management
  - [X] Create PowerShell script for project creation
- [X] Add project with Entity Models for Sqlite
- [X] Create Web App
  - [X] Use New-DotNetProject.ps1 script

### Database Setup
- [🟡] Setup Example Northwind DB, p. 633,634
- [🟡] Setup Entity Model
  - [ ] Verify Entity Models against DB schema
  - [ ] Add necessary model configurations
  - [ ] Test data access

### Web Application Configuration
- [X] Basic Project Setup
  - [ ] Configure app settings (appsettings.json)
  - [ ] Setup environment-specific configurations
  - [ ] Configure HTTPS
- [ ] Setup Infrastructure
  - [ ] Configure dependency injection
  - [ ] Setup logging
  - [ ] Configure error handling
  - [ ] Add DB Context registration
- [ ] Create Basic UI Components
  - [ ] Layout template
  - [ ] Navigation menu
  - [ ] Error pages
  - [ ] Loading states

## Backlog
- [ ] Add Authentication/Authorization
- [ ] Implement CRUD operations
  - [ ] List view components
  - [ ] Detail view components
  - [ ] Edit forms
  - [ ] Delete confirmations
- [ ] Add Unit Tests
  - [ ] Service layer tests
  - [ ] Component tests
- [ ] Add Integration Tests
  - [ ] API endpoint tests
  - [ ] DB integration tests
- [ ] Setup CI/CD Pipeline
  - [ ] Build automation
  - [ ] Test automation
  - [ ] Deployment scripts

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