# Exercise 14.2: Times Tables Blazor Component

## Task
- [x] Create a Blazor component that displays a times table
  - [x] Component is routable
  - [x] The component name: TimesTable
- [x] Parameter "Number" for the number to display
  - [x] Number is required
- [x] Parameter "Size" for the size of the table
  - [x] Default size: 12

- [x] Test component by adding to Home.razor
- [x] Call component via url: /timestable/6, /timestable/7/10

- [x] The component should display the times table for the given number

## Steps
- [X] Describe the task
- [X] Plan the design
- [x] Plan the steps
- [x] Create the component
- [x] Add to Home.razor
- [x] Test with Home.razor
- [x] Test with url
- [ ] Add alignment of numbers
- [ ] Plan optimized design
- [ ] Implement optimized design
- [ ] Test optimized design

## Design

``` text
Timestable
--------------------
number: 3, size: 12

 3 x  1 =  3
 3 x  2 =  6
 3 x  3 =  9
 3 x  4 = 12
 3 x  5 = 15
 3 x  6 = 18
 3 x  7 = 21
 3 x  8 = 24
 3 x  9 = 27
 3 x 10 = 30
 3 x 11 = 33
 3 x 12 = 36
```

### Alignment of numbers
The numbers should be right-aligned.

Size indicates how many digits can be displayed maximum.
For smaller numbers, corresponding spaces must be inserted.

The result of Size times number gives the maximum number of digits in the result.
For smaller results, corresponding spaces must be inserted.

## Design Optimization Proposals

- [x] **Present selected number and size visually (card, badge, etc.)**  
  Display the selected number and size in a more visually distinct way, such as using a card, badge,  
  or highlighted text, rather than plain text.

- [x] **Use an HTML <table> for the times table instead of <p> tags**  
  Improves readability, accessibility, and alignment by using a semantic table structure.

- [ ] **Allow interactive input for number and size (optional)**  
  Consider allowing users to change the number and size interactively with input fields or sliders,  
  especially if the component is used outside of strict routing.

- [ ] **Display clear validation messages for invalid input**  
  If a user enters an invalid value (e.g., via query string or input), display a clear, styled validation message.

- [ ] **Ensure responsive design for all screen sizes**  
  Make sure the layout adapts well to different screen sizes, especially for mobile users.

- [ ] **Add navigation links/buttons for quick access to other tables**  
  Add navigation links or buttons to quickly try other numbers or reset to defaults.

- [ ] **Use semantic HTML and ARIA attributes for accessibility**  
  Use semantic HTML and ARIA attributes where appropriate to improve accessibility for screen readers.

- [ ] **Apply consistent styling with the app's design system**  
  Apply consistent styling (using Bootstrap or your design system) for headings, tables, and parameter displays  
  to match the rest of your app.

- [ ] **Consider virtualization or limit max size for performance**  
  For larger tables, consider virtualization or limiting the maximum size to prevent performance issues.

## Solution
Prototyping:
- Create a new component TimesTable.razor
- Get number and size from parameters
- Loop from 1 to size

Optimization:
- Calculate the result of number times size
- Use

## Bugs
- [X] Title should be visible once only if the component is used on a page like Home.razor
- [x] Alignment of numbers is not correct
- [x] Result is not aligned