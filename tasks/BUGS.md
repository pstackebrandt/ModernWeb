# Project Bugs

This file is a bug list for the project.

## Bug in WebAPI
- [ ] Call to customers of a country returns empty list if the country is not found.
  - Example: https://localhost:5151/customers/in/Germany
  - Expected: A list of customers from Germany or an error message that the country is not found.
  - Actual: An empty list., Url: https://localhost:5151/customers/in/germany
    - Auffaellig: Country name was changed to lower case
  - Call to UK works: https://localhost:5151/customers/in/UK , casing not changed
  
## Task Status Legend
- [ ] Not Started
- [🟡] In Progress
- [X] Completed
- [🔴] Blocked