# Notes

## Use of a regex to add string length attributes to the Northwind model
see p. 644

```search pattern
\[Column\(TypeName = "(nchar|nvarchar) \((.*)\)"\)\]
```
```replace pattern
$0\n    [StringLength($2)]
```

***Result:***
```csharp
[Column(TypeName = "nchar (15)")]
[StringLength(15)] // Added string length attribute
public string CategoryName { get; set; } = null!;
```
