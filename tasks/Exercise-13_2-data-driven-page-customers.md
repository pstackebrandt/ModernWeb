# Exercise 13.2: Data-Driven Page  with Customer data

## Task
- [ ] Add a new razor page that shows customers grouped by country
- [ ] When the user clicks on a customer record, they should see a page showing the full contact details of the customer
  - [ ] and a list of orders for that customer

## Solution

What do we need?
- Customers page
- Customer Orders page (Customer details, order list)
- Link to Customers page in Main Navigation

### Customers page
- [X] A new razor page "Customers.razor"
***Content of the page***
- [X] List of customers grouped by country
- Click on a customer leads to the Customer Orders page

***Data of a single customer***
- [X] CompanyName, Country, City, Customer ID
- [X] Titles:" Company Name", "Country", "City", "Customer ID"

### Customer Orders page
- A new razor page "CustomerOrders.razor"
- Page shows the "full contact details" of the customer
- and list of orders of that customer

***Content of the page***
- Customer details:
  - CompanyName, Customer ID, Country, City, ContactName, ContactTitle
  - Optional: Region, Address, PostalCode, Phone, Fax
- List of orders:
  - OrderID, OrderDate, EmployeeId
  - Optional: decide later

## Steps
- [X] Plan content of the pages
- [X] Plan first steps
- [ ] Plan more steps

### Customers page prototype
- [ ] Create Customers.razor with dummy data
- [ ] Link to Customers page in Main Layout
- [ ] Get data from database for Customers.razor

### Customer Orders page prototype
- [ ] Create CustomerOrders.razor with dummy data
  - [ ] Customer details
  - [ ] List of orders
- [ ] Link to CustomerOrders page from Customer in Customers.razor

### Customer Orders data
- [ ] Get data from database for CustomerOrders.razor
- [ ] Send data (customer ID??, Customer object??) from Customers page to CustomerOrders.razor

### More steps
to be added
