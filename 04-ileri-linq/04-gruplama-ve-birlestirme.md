# 4.4 — Koleksiyonları Gruplama ve Birleştirme

## 18. `Distinct`


Tekrarlanan değerleri kaldırır:


```csharp
var numbers = new[] { 1, 2, 2, 3, 3, 3 };

var distinctNumbers = numbers.Distinct();

foreach (var number in distinctNumbers)
{
    Console.WriteLine(number);
}

```





Çıktı:


```text
1
2
3

```





String değerlerde:


```csharp
var departments = new[]
{
    "IT",
    "HR",
    "IT",
    "Finance",
    "HR"
};

var uniqueDepartments = departments.Distinct();

```

## 19. `SelectMany`


İç içe koleksiyonları tek koleksiyona dönüştürür.


```csharp
var classes = new[]
{
    new[] { "Ali", "Ayşe" },
    new[] { "Mehmet", "Zeynep" }
};

var students = classes.SelectMany(classroom => classroom);

```





Sonuç:


```text
Ali
Ayşe
Mehmet
Zeynep

```





`Select` kullansaydık iç içe koleksiyon elde ederdik:


```csharp
var nested = classes.Select(classroom => classroom);

```





`SelectMany`, koleksiyonları düzleştirir.


Gerçek örnek:


```csharp
public record Order(int Id, List<string> Products);

var orders = new[]
{
    new Order(1, new List<string> { "Mouse", "Keyboard" }),
    new Order(2, new List<string> { "Monitor" })
};

var allProducts = orders
    .SelectMany(order => order.Products);

```





Sonuç:


```text
Mouse
Keyboard
Monitor

```

## 20. `GroupBy`


Elemanları belirli bir anahtara göre gruplar:


```csharp
var employees = new[]
{
    new Employee("Ali", "IT"),
    new Employee("Ayşe", "IT"),
    new Employee("Mehmet", "HR"),
    new Employee("Zeynep", "HR")
};

var groups = employees.GroupBy(employee => employee.Department);

foreach (var group in groups)
{
    Console.WriteLine(group.Key);

    foreach (var employee in group)
    {
        Console.WriteLine($"- {employee.Name}");
    }
}

public record Employee(string Name, string Department);

```





Çıktı:


```text
IT
- Ali
- Ayşe
HR
- Mehmet
- Zeynep

```





`group.Key`, grubun anahtarıdır:


```csharp
group.Key

```





Bu örnekte anahtar departmandır.


Grupları sayabiliriz:


```csharp
var employeeCounts = employees
    .GroupBy(employee => employee.Department)
    .Select(group => new
    {
        Department = group.Key,
        Count = group.Count()
    });

```

## 21. `Join`


İki koleksiyonu ortak property üzerinden birleştirir.


```csharp
var customers = new[]
{
    new Customer(1, "Ali"),
    new Customer(2, "Ayşe")
};

var orders = new[]
{
    new Order(100, 1, 500),
    new Order(101, 2, 750)
};

var result = customers.Join(
    orders,
    customer => customer.Id,
    order => order.CustomerId,
    (customer, order) => new
    {
        CustomerName = customer.Name,
        OrderId = order.Id,
        Total = order.Total
    });

```





Modeller:


```csharp
public record Customer(int Id, string Name);

public record Order(int Id, int CustomerId, decimal Total);

```





`Join` parametreleri:


```text
1. İlk koleksiyon
2. İkinci koleksiyon
3. İlk koleksiyonun anahtarı
4. İkinci koleksiyonun anahtarı
5. Birleştirilen sonucu oluşturacak fonksiyon

```
