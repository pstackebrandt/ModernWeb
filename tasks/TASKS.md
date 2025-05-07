# Project Tasks

This file is a task list for the project.
I try to update it automatically using cursor often without specific instructions.
This doesn't work well, but for now I update the file from time to time manually.
I want to see where the automation fails or succeeds.

## Current Steps
- [🟡] Create Basic UI Components
  - [ ] Error pages
  - [ ] Loading states
  - [ ] Data display components
  - [ ] Form components

## Next Steps

### Web Application Configuration
- [ ] Implement Authentication & Authorization
  - [ ] User management
  - [ ] Role-based access control
  - [ ] Secure API endpoints

- [ ] Add API Documentation
  - [ ] Swagger/OpenAPI integration
  - [ ] API versioning
  - [ ] Documentation generation

## Backlog
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
  - [X] Add Northwind Database Context
  - [X] Setup Database Context logging

### Web Application Setup Northwind.Web
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

### Northwind Database Usage
- [X] Add and setup project with Northwind Entity Models for Sqlite
- [X] Add and Setup project with Northwind DataContext for Sqlite
- [X] Setup Example Northwind DB, p. 633,634
- [X] Setup Entity Model
- [X] Customize Northwind Model, OnModelCreating(), remove ValueGeneratedNever() calls, fix data types p. 639
- [X] Add NorthwindContextExtensions, adds db context to service collection, p. 640

## Task Status Legend
- [ ] Not Started
- [🟡] In Progress
- [X] Completed
- [🔴] Blocked

## Notes
- Update this file as tasks progress
- Move completed tasks to the Completed section, sort them by subject/area
- Include relevant issue numbers or references
- Consider adding estimated effort/complexity for each task