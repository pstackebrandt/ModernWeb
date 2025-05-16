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

## Solution
Prototyping:
- Create a new component TimesTable.razor
- Get number and size from parameters
- Loop from 1 to size

Optimization:
- Calculate the result of number times size
- Use

## Bugs
- [ ] Title should be visible once only if the component is used on a page like Home.razor
