# Project Tasks

This file is a task list for the project.
I try to update it automatically using cursor without specific instructions.
This doesn't work well, but for now I update the file seldom manually. I want to see where the automation fails or succeeds.

## Current Steps

### Database Setup
- [🟡] Setup Example Northwind DB, p. 633,634
- [🟡] Setup Entity Model
  - [ ] Verify Entity Models against DB schema
  - [ ] Add necessary model configurations
  - [ ] Test data access
  - [ ] Move connection string to configuration

### Web Application Configuration
- [🟡] Setup Infrastructure
  - [🟡] Configure dependency injection
  - [ ] Setup logging
  - [ ] Configure error handling
  - [ ] Add DB Context registration
- [🟡] Create Basic UI Components
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
- [ ] Enhance Blazor Implementation
  - [ ] Add interactive components
  - [ ] Implement client-side functionality
  - [ ] Optimize SSR performance
  - [ ] Add progressive enhancement

## Completed
- [X] Infrastructure Setup
  - [X] Initial project setup (git, CPM)
  - [X] Create PowerShell scripts for project creation
    - WhatIf support, package restore, configurable options
  - [X] Add project with Entity Models for Sqlite
- [X] Web Application Setup
  - [X] Create initial web application using script
  - [X] Configure app settings
    - Environment-specific configurations
    - Example configuration files
  - [X] Configure HTTPS with test suite
  - [X] Implement Blazor Static SSR
  - [X] Basic UI Components
    - Layout template (MainLayout.razor)
    - Navigation menu (NavMenu.razor)

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