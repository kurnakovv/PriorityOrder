<div align="center">
 <img src="icon.png" weight="100px" height="100px" />
 <h2>PriorityOrder</h2>
 
 [![NuGet](https://img.shields.io/nuget/v/Kurnakov.PriorityOrder.svg)](https://www.nuget.org/packages/Kurnakov.PriorityOrder)
 [![NuGet download](https://img.shields.io/nuget/dt/Kurnakov.PriorityOrder.svg)](https://www.nuget.org/packages/Kurnakov.PriorityOrder) 
 ![Visitors](https://api.visitorbadge.io/api/visitors?path=https%3A%2F%2Fgithub.com%kurnakovv%PriorityOrder&countColor=%23263759&style=flat)
 [![Build/Test](https://github.com/kurnakovv/PriorityOrder/actions/workflows/build-test.yml/badge.svg)](https://github.com/kurnakovv/PriorityOrder/actions/workflows/build-test.yml)
 [![MIT License](https://img.shields.io/github/license/kurnakovv/PriorityOrder?color=%230b0&style=flat)](https://github.com/kurnakovv/PriorityOrder/blob/main/LICENSE)
 
</div>

# Description
PriorityOrder is an open source library that allows to order enumerable by priority.

# Install
```
dotnet add package Kurnakov.PriorityOrder
```

# Idea
``` cs
using PriorityOrder;

public class Employee
{
    public string Name { get; set; }
    public string Role { get; set; }
}

var source = new List<Employee>
{
    new Employee() { Name = "Willow", Role = "Manager" },
    new Employee() { Name = "Huxley", Role = "Programmer" },
    new Employee() { Name = "Tom", Role = "Programmer" },
    new Employee() { Name = "Sutton", Role = "Tester" },
    new Employee() { Name = "Lev", Role = "HR"  },
    new Employee() { Name = "Max", Role = "Programmer" },
    new Employee() { Name = "Bob", Role = "Manager" },
    new Employee() { Name = "Jack", Role = "Tester" },
    new Employee() { Name = "Ruby", Role = "Manager" },
    new Employee() { Name = "Willow", Role = "Accountant"  },
};

// For example, you want to order employees by these specific priorities (not alphabetically).
var priorities = new List<string>()
{
    "Programmer",
    "Tester",
    "Manager",
    // After "Manager" order doesn't matter anymore.
};
List<Employee> result = source
    .OrderByPriority(x => x.Role, priorities)
    .ThenBy(x => x.Name) // Optional.
    .ToList();

// result:
// Role: 'Programmer', Name: 'Huxley'
// Role: 'Programmer', Name: 'Max'
// Role: 'Programmer', Name: 'Tom'
// Role: 'Tester', Name: 'Jack'
// Role: 'Tester', Name: 'Sutton'
// Role: 'Manager', Name: 'Bob'
// Role: 'Manager', Name: 'Ruby'
// Role: 'Manager', Name: 'Willow'
// Role: 'HR', Name: 'Lev'
// Role: 'Accountant', Name: 'Willow'
```
> Besides strings you can use enum's, numbers etc (more details you can read on [Wiki](https://github.com/kurnakovv/PriorityOrder/wiki))

# Docs
You can look at the full docs on [Wiki](https://github.com/kurnakovv/PriorityOrder/wiki)
* [PriorityEnumerable.OrderByPriority(IEnumerable priorities)](https://github.com/kurnakovv/PriorityOrder/wiki/PriorityEnumerable.OrderByPriority(IEnumerable-priorities))
* [PriorityEnumerable.OrderByPriority(params[] priorities)](https://github.com/kurnakovv/PriorityOrder/wiki/PriorityEnumerable.OrderByPriority(params%5B%5D-priorities))
