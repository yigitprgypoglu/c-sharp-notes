# 4.2 — Sıralama ve Sayfalama

## 5. `OrderBy`, `OrderByDescending`


Sıralama için kullanılır.


```csharp
var numbers = new[] { 5, 1, 8, 3, 2 };

var ascending = numbers.OrderBy(number => number);

```





Sonuç:


```text
1, 2, 3, 5, 8

```





Büyükten küçüğe:


```csharp
var descending = numbers.OrderByDescending(number => number);

```





Sonuç:


```text
8, 5, 3, 2, 1

```





Nesneleri property’ye göre sıralayabiliriz:


```csharp
var sortedProducts = products
    .OrderBy(product => product.Price);

```





Fiyatı büyükten küçüğe:


```csharp
var sortedProducts = products
    .OrderByDescending(product => product.Price);

```

## 6. `ThenBy` ve `ThenByDescending`


İlk sıralama kriteri eşit olduğunda ikinci kriteri kullanır.


```csharp
var employees = new[]
{
    new Employee("Ali", "IT", 30),
    new Employee("Ayşe", "IT", 25),
    new Employee("Mehmet", "HR", 40),
    new Employee("Zeynep", "IT", 25)
};

var result = employees
    .OrderBy(employee => employee.Department)
    .ThenBy(employee => employee.Age);

```





Önce departmana göre sıralar. Aynı departmandaki çalışanları da yaşa göre sıralar.


```csharp
public record Employee(string Name, string Department, int Age);

```





`ThenBy`, ilk sıralamayı değiştirmez; yalnızca eşitlik durumunda ek kriter uygular.

## 26. `Skip` ve `Take`


Sayfalama için kullanılır:


```csharp
var numbers = Enumerable.Range(1, 10);

var page = numbers
    .Skip(3)
    .Take(4);

```





Başlangıç:


```text
1, 2, 3, 4, 5, 6, 7, 8, 9, 10

```





`Skip(3)` sonrası:


```text
4, 5, 6, 7, 8, 9, 10

```





`Take(4)` sonrası:


```text
4, 5, 6, 7

```





Sayfalama örneği:


```csharp
int pageNumber = 2;
int pageSize = 10;

var page = products
    .Skip((pageNumber - 1) * pageSize)
    .Take(pageSize)
    .ToList();

```
