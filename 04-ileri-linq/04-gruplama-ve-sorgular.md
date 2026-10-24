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






## 22. Query syntax ve method syntax


LINQ iki farklı biçimde yazılabilir.


### Method syntax


```csharp
var result = numbers
    .Where(number => number > 2)
    .Select(number => number * 2);

```





### Query syntax


```csharp
var result =
    from number in numbers
    where number > 2
    select number * 2;

```





İki kullanım da benzer sonucu üretir.


Modern C# kodlarında method syntax daha yaygın görülür:


```csharp
var result = numbers
    .Where(number => number > 2)
    .Select(number => number * 2);

```





Query syntax özellikle `join` ve karmaşık sorgularda okunabilir olabilir.
