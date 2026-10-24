## 12. `First`, `FirstOrDefault`


İlk elemanı almak için kullanılır:


```csharp
var numbers = new[] { 10, 20, 30 };

int first = numbers.First();

Console.WriteLine(first); // 10

```





Koşullu kullanım:


```csharp
int firstEven = numbers.First(number => number % 2 == 0);

```





Eğer hiç eleman yoksa `First()` exception fırlatır:


```csharp
var numbers = Array.Empty<int>();

int first = numbers.First(); // InvalidOperationException

```





`FirstOrDefault()` eleman yoksa varsayılan değer döndürür:


```csharp
int first = numbers.FirstOrDefault();

Console.WriteLine(first); // 0

```





Reference type için varsayılan değer `null` olabilir:


```csharp
Product? product = products.FirstOrDefault(
    product => product.Price > 100000);

```






## 13. `Single`, `SingleOrDefault`


Tam olarak bir eleman beklediğimiz zaman kullanılır:


```csharp
var users = new[]
{
    new User(1, "Ali"),
    new User(2, "Ayşe")
};

var user = users.Single(user => user.Id == 1);

```





`Single` şu durumlarda hata verir:



- Hiç eleman yoksa

- Birden fazla eleman varsa



```csharp
var result = users.Single(user => user.Id == 99);
// Hiç eleman yok: hata

```





```csharp
var duplicateUsers = new[]
{
    new User(1, "Ali"),
    new User(1, "Veli")
};

var result = duplicateUsers.Single(user => user.Id == 1);
// Birden fazla eleman var: hata

```





`SingleOrDefault()` hiç eleman yoksa varsayılan değer döndürür; ancak birden fazla eleman varsa yine hata verir.


Kullanım farkı:


```text
First      -> İlk elemanı alır, birden fazla olabilir.
Single     -> Tam olarak bir eleman bekler.

```






## 14. `Any`


Koleksiyonda en az bir eleman olup olmadığını kontrol eder:


```csharp
var numbers = new[] { 1, 2, 3 };

bool hasNumbers = numbers.Any();

Console.WriteLine(hasNumbers); // True

```





Koşullu kullanım:


```csharp
bool hasEvenNumber = numbers.Any(number => number % 2 == 0);

```





`Any`, sonuç bulduğu anda durabilir:


```csharp
var hasExpensiveProduct =
    products.Any(product => product.Price > 10000);

```





Bu, genellikle şu kullanımdan daha uygundur:


```csharp
products.Count(product => product.Price > 10000) > 0

```





Çünkü `Count` bütün elemanları kontrol etmek zorunda kalabilir; `Any` ilk uygun elemanda durabilir.



## 15. `All`


Bütün elemanların koşulu sağlayıp sağlamadığını kontrol eder:


```csharp
var numbers = new[] { 2, 4, 6, 8 };

bool allEven = numbers.All(number => number % 2 == 0);

Console.WriteLine(allEven); // True

```





Bir eleman bile koşulu sağlamazsa sonuç `False` olur:


```csharp
var numbers = new[] { 2, 4, 5, 8 };

bool allEven = numbers.All(number => number % 2 == 0);

Console.WriteLine(allEven); // False

```






## 16. `Count` ve `LongCount`


Eleman sayısını bulur:


```csharp
var numbers = new[] { 1, 2, 3, 4, 5 };

int count = numbers.Count();

```





Koşullu sayım:


```csharp
int evenCount = numbers.Count(number => number % 2 == 0);

```





Sonuç:


```text
2

```





Çok büyük koleksiyonlarda `LongCount()` kullanılabilir:


```csharp
long count = numbers.LongCount();

```






## 17. `Sum`, `Min`, `Max`, `Average`


Sayısal işlemler:


```csharp
var numbers = new[] { 10, 20, 30 };

int sum = numbers.Sum();       // 60
int min = numbers.Min();       // 10
int max = numbers.Max();       // 30
double average = numbers.Average(); // 20

```





Nesnelerin property’leriyle de kullanılabilir:


```csharp
decimal totalPrice = products.Sum(product => product.Price);
decimal highestPrice = products.Max(product => product.Price);
decimal lowestPrice = products.Min(product => product.Price);

```





`Average()` dönüş tipi kaynağa göre değişebilir; `decimal` değerler için genellikle `decimal` döndürür.



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
