# Modern Web Apps

This repository contains web apps built with Blazor and ASP.NET Core.

## Getting Started

### Database Setup

The solution uses a SQLite version of the Northwind database. The database file
(`Northwind.db`) is not version controlled for the following reasons:

- It's a binary file that doesn't work well with version control
- It can be easily recreated from the SQL scripts
- Prevents potential database corruption from concurrent version control operations

To set up the database, please follow the instructions in the
`Northwind.EntityModels.Sqlite/README.md` file.
