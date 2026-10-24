## 23. `IEnumerable<T>` ve `IQueryable<T>` farkı



Query syntax özellikle `join` ve karmaşık sorgularda okunabilir olabilir.





```

    .Select(number => number * 2);
    .Where(number => number > 2)
var result = numbers
```csharp


Modern C# kodlarında method syntax daha yaygın görülür:


İki kullanım da benzer sonucu üretir.





```

    select number * 2;
    where number > 2
    from number in numbers
var result =
```csharp


### Query syntax





```

    .Select(number => number * 2);
    .Where(number => number > 2)
var result = numbers
```csharp


### Method syntax


LINQ iki farklı biçimde yazılabilir.


## 22. Query syntax ve method syntax






```

5. Birleştirilen sonucu oluşturacak fonksiyon
4. İkinci koleksiyonun anahtarı
3. İlk koleksiyonun anahtarı
2. İkinci koleksiyon
1. İlk koleksiyon
```text


`Join` parametreleri:





```

public record Order(int Id, int CustomerId, decimal Total);

public record Customer(int Id, string Name);
```csharp


Modeller:





```

    });
        Total = order.Total
        OrderId = order.Id,
        CustomerName = customer.Name,
    {
    (customer, order) => new
    order => order.CustomerId,
    customer => customer.Id,
    orders,
var result = customers.Join(

};
    new Order(101, 2, 750)
    new Order(100, 1, 500),
{
var orders = new[]

};
    new Customer(2, "Ayşe")
    new Customer(1, "Ali"),
{
var customers = new[]
```csharp


İki koleksiyonu ortak property üzerinden birleştirir.


## 21. `Join`






```

    });
        Count = group.Count()
        Department = group.Key,
    {
    .Select(group => new
    .GroupBy(employee => employee.Department)
var employeeCounts = employees
```csharp


Grupları sayabiliriz:


Bu örnekte anahtar departmandır.





```

group.Key
```csharp


`group.Key`, grubun anahtarıdır:





```

- Zeynep
- Mehmet
HR
- Ayşe
- Ali
IT
```text


Çıktı:





```

public record Employee(string Name, string Department);

}
    }
        Console.WriteLine($"- {employee.Name}");
    {
    foreach (var employee in group)

    Console.WriteLine(group.Key);
{
foreach (var group in groups)

var groups = employees.GroupBy(employee => employee.Department);

};
    new Employee("Zeynep", "HR")
    new Employee("Mehmet", "HR"),
    new Employee("Ayşe", "IT"),
    new Employee("Ali", "IT"),
{
var employees = new[]
```csharp


Elemanları belirli bir anahtara göre gruplar:


## 20. `GroupBy`






```

Monitor
Keyboard
Mouse
```text


Sonuç:





```

    .SelectMany(order => order.Products);
var allProducts = orders

};
    new Order(2, new List<string> { "Monitor" })
    new Order(1, new List<string> { "Mouse", "Keyboard" }),
{
var orders = new[]

public record Order(int Id, List<string> Products);
```csharp


Gerçek örnek:


`SelectMany`, koleksiyonları düzleştirir.





```

var nested = classes.Select(classroom => classroom);
```csharp


`Select` kullansaydık iç içe koleksiyon elde ederdik:





```

Zeynep
Mehmet
Ayşe
Ali
```text


Sonuç:





```

var students = classes.SelectMany(classroom => classroom);

};
    new[] { "Mehmet", "Zeynep" }
    new[] { "Ali", "Ayşe" },
{
var classes = new[]
```csharp


İç içe koleksiyonları tek koleksiyona dönüştürür.


## 19. `SelectMany`






```

var uniqueDepartments = departments.Distinct();

};
    "HR"
    "Finance",
    "IT",
    "HR",
    "IT",
{
var departments = new[]
```csharp


String değerlerde:





```

3
2
1
```text


Çıktı:





```

}
    Console.WriteLine(number);
{
foreach (var number in distinctNumbers)

var distinctNumbers = numbers.Distinct();

var numbers = new[] { 1, 2, 2, 3, 3, 3 };
```csharp


Tekrarlanan değerleri kaldırır:


## 18. `Distinct`



`Average()` dönüş tipi kaynağa göre değişebilir; `decimal` değerler için genellikle `decimal` döndürür.





```

decimal lowestPrice = products.Min(product => product.Price);
decimal highestPrice = products.Max(product => product.Price);
decimal totalPrice = products.Sum(product => product.Price);
```csharp


Nesnelerin property’leriyle de kullanılabilir:





```

double average = numbers.Average(); // 20
int max = numbers.Max();       // 30
int min = numbers.Min();       // 10
int sum = numbers.Sum();       // 60

var numbers = new[] { 10, 20, 30 };
```csharp


Sayısal işlemler:


## 17. `Sum`, `Min`, `Max`, `Average`






```

long count = numbers.LongCount();
```csharp


Çok büyük koleksiyonlarda `LongCount()` kullanılabilir:





```

2
```text


Sonuç:





```

int evenCount = numbers.Count(number => number % 2 == 0);
```csharp


Koşullu sayım:





```

int count = numbers.Count();

var numbers = new[] { 1, 2, 3, 4, 5 };
```csharp


Eleman sayısını bulur:


## 16. `Count` ve `LongCount`






```

Console.WriteLine(allEven); // False

bool allEven = numbers.All(number => number % 2 == 0);

var numbers = new[] { 2, 4, 5, 8 };
```csharp


Bir eleman bile koşulu sağlamazsa sonuç `False` olur:





```

Console.WriteLine(allEven); // True

bool allEven = numbers.All(number => number % 2 == 0);

var numbers = new[] { 2, 4, 6, 8 };
```csharp


Bütün elemanların koşulu sağlayıp sağlamadığını kontrol eder:


## 15. `All`



Çünkü `Count` bütün elemanları kontrol etmek zorunda kalabilir; `Any` ilk uygun elemanda durabilir.





```

products.Count(product => product.Price > 10000) > 0
```csharp


Bu, genellikle şu kullanımdan daha uygundur:





```

    products.Any(product => product.Price > 10000);
var hasExpensiveProduct =
```csharp


`Any`, sonuç bulduğu anda durabilir:





```

bool hasEvenNumber = numbers.Any(number => number % 2 == 0);
```csharp


Koşullu kullanım:





```

Console.WriteLine(hasNumbers); // True

bool hasNumbers = numbers.Any();

var numbers = new[] { 1, 2, 3 };
```csharp


Koleksiyonda en az bir eleman olup olmadığını kontrol eder:


## 14. `Any`






```

Single     -> Tam olarak bir eleman bekler.
First      -> İlk elemanı alır, birden fazla olabilir.
```text


Kullanım farkı:


`SingleOrDefault()` hiç eleman yoksa varsayılan değer döndürür; ancak birden fazla eleman varsa yine hata verir.





```

// Birden fazla eleman var: hata
var result = duplicateUsers.Single(user => user.Id == 1);

};
    new User(1, "Veli")
    new User(1, "Ali"),
{
var duplicateUsers = new[]
```csharp





```

// Hiç eleman yok: hata
var result = users.Single(user => user.Id == 99);
```csharp



- Birden fazla eleman varsa

- Hiç eleman yoksa



`Single` şu durumlarda hata verir:





```

var user = users.Single(user => user.Id == 1);

};
    new User(2, "Ayşe")
    new User(1, "Ali"),
{
var users = new[]
```csharp


Tam olarak bir eleman beklediğimiz zaman kullanılır:


## 13. `Single`, `SingleOrDefault`






```

    product => product.Price > 100000);
Product? product = products.FirstOrDefault(
```csharp


Reference type için varsayılan değer `null` olabilir:





```

Console.WriteLine(first); // 0

int first = numbers.FirstOrDefault();
```csharp


`FirstOrDefault()` eleman yoksa varsayılan değer döndürür:





```

int first = numbers.First(); // InvalidOperationException

var numbers = Array.Empty<int>();
```csharp


Eğer hiç eleman yoksa `First()` exception fırlatır:





```

int firstEven = numbers.First(number => number % 2 == 0);
```csharp


Koşullu kullanım:





```

Console.WriteLine(first); // 10

int first = numbers.First();

var numbers = new[] { 10, 20, 30 };
```csharp


İlk elemanı almak için kullanılır:


## 12. `First`, `FirstOrDefault`



Büyük koleksiyonlarda `yield return` faydalı olabilir.


Sonuçları tek tek üretir.





```

}
    }
        }
            yield return number;
        {
        if (number % 2 == 0)
    {
    foreach (var number in numbers)
{
static IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
```csharp


`yield return` kullanan metot:


Bu metot bütün sonuçları bellekte tutar.





```

}
    return result;

    }
        }
            result.Add(number);
        {
        if (number % 2 == 0)
    {
    foreach (var number in numbers)

    var result = new List<int>();
{
static List<int> GetEvenNumbers(List<int> numbers)
```csharp


Liste oluşturan metot:


## 11. `yield return` ile normal liste arasındaki fark


Bu elemanı şimdi döndür, daha sonra kaldığın yerden devam et.


şu anlama gelir:





```

yield return number;
```csharp


Bu kod:



- Bellek kullanımını azaltabilir.

- Iterator oluşturur.

- Elemanları ihtiyaç oldukça üretir.

- Metodu tek seferde bütün sonuçları üretmeye zorlamaz.



`yield return`:





```

6
4
2
```text


Çıktı:





```

}
    Console.WriteLine(number);
{
foreach (var number in evenNumbers)

var evenNumbers = GetEvenNumbers(numbers);

var numbers = new[] { 1, 2, 3, 4, 5, 6 };
```csharp


Kullanımı:





```

}
    }
        }
            yield return number;
        {
        if (number % 2 == 0)
    {
    foreach (var number in numbers)
{
static IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
```csharp


Kendi ertelenmiş çalışan iterator metodumuzu yazabiliriz:


## 10. `yield return`



Sorgu hemen çalışır.





```

    .ToList();
    .Where(number => number > 1)
var immediateQuery = numbers
```csharp


Sorgu ertelenir.





```

var deferredQuery = numbers.Where(number => number > 1);
```csharp


Karşılaştırma:


Çünkü `ToList()` çağrıldığı anda sorgu çalıştırılmış ve sonuç yeni bir listeye kopyalanmıştır. Daha sonra kaynak listeye `4` eklenmesi `query` listesini değiştirmez.





```

3
2
```text


Çıktı:





```

}
    Console.WriteLine(number);
{
foreach (var number in query)

numbers.Add(4);

    .ToList();
    .Where(number => number > 1)
var query = numbers

var numbers = new List<int> { 1, 2, 3 };
```csharp


Sorguyu o anda çalıştırıp sonucu sabitlemek için `ToList()` veya `ToArray()` kullanılır:


## 9. Sorguyu hemen çalıştırmak: `ToList`, `ToArray`



`Where` içindeki kod, `query` tanımlandığında çalışmaz. `foreach` sırasında çalışır.





```

Sonuç: 3
Kontrol ediliyor: 3
Sonuç: 2
Kontrol ediliyor: 2
Kontrol ediliyor: 1
Sorgu oluşturuldu.
```text


Muhtemel çıktı:





```

}
    Console.WriteLine($"Sonuç: {number}");
{
foreach (var number in query)

Console.WriteLine("Sorgu oluşturuldu.");

});
    return number > 1;
    Console.WriteLine($"Kontrol ediliyor: {number}");
{
var query = numbers.Where(number =>

var numbers = new List<int> { 1, 2, 3 };
```csharp


## 8. Deferred execution örneği



Bu davranışa **deferred execution**, yani ertelenmiş çalıştırma denir.





```

5. Güncel liste filtrelenir.
4. foreach sorguyu çalıştırır.
3. Listeye 4 eklenir.
2. Henüz filtreleme yapılmaz.
1. query oluşturulur.
```text


Akış:


Neden? Çünkü `Where` sorgusu oluşturulduğu anda değil, `foreach` başladığında çalışır.





```

4
3
2
```text


Çıktı:





```

}
    Console.WriteLine(number);
{
foreach (var number in query)

numbers.Add(4);

var query = numbers.Where(number => number > 1);

var numbers = new List<int> { 1, 2, 3 };
```csharp


LINQ’in en önemli özelliklerinden biri sorgunun hemen çalıştırılmamasıdır.


## 7. Deferred Execution — Ertelenmiş çalıştırma



`ThenBy`, ilk sıralamayı değiştirmez; yalnızca eşitlik durumunda ek kriter uygular.





```

public record Employee(string Name, string Department, int Age);
```csharp


Önce departmana göre sıralar. Aynı departmandaki çalışanları da yaşa göre sıralar.





```

    .ThenBy(employee => employee.Age);
    .OrderBy(employee => employee.Department)
var result = employees

};
    new Employee("Zeynep", "IT", 25)
    new Employee("Mehmet", "HR", 40),
    new Employee("Ayşe", "IT", 25),
    new Employee("Ali", "IT", 30),
{
var employees = new[]
```csharp


İlk sıralama kriteri eşit olduğunda ikinci kriteri kullanır.


## 6. `ThenBy` ve `ThenByDescending`






```

    .OrderByDescending(product => product.Price);
var sortedProducts = products
```csharp


Fiyatı büyükten küçüğe:





```

    .OrderBy(product => product.Price);
var sortedProducts = products
```csharp


Nesneleri property’ye göre sıralayabiliriz:





```

8, 5, 3, 2, 1
```text


Sonuç:





```

var descending = numbers.OrderByDescending(number => number);
```csharp


Büyükten küçüğe:





```

1, 2, 3, 5, 8
```text


Sonuç:





```

var ascending = numbers.OrderBy(number => number);

var numbers = new[] { 5, 1, 8, 3, 2 };
```csharp


Sıralama için kullanılır.


## 5. `OrderBy`, `OrderByDescending`



Böylece yalnızca gerekli ürünler dönüştürülür.





```

    .Select(product => product.Name);
    .Where(product => product.Price > 1000)
var result = products
```csharp


Genellikle önce filtrelemek, sonra dönüştürmek daha verimlidir:





```

30, 40, 50, 60
```text


`Select` sonrası:





```

3, 4, 5, 6
```text


`Where` sonrası:





```

1, 2, 3, 4, 5, 6
```text


Başlangıç:


Adım adım:





```

    .Select(number => number * 10);
    .Where(number => number > 2)
var result = numbers
```csharp


LINQ metotları zincirlenebilir:


## 4. `Where` ve `Select` birlikte






```

public record Product(string Name, decimal Price);

}
    Console.WriteLine(name);
{
foreach (var name in productNames)

var productNames = products.Select(product => product.Name);

};
    new Product("Keyboard", 1500)
    new Product("Mouse", 500),
    new Product("Laptop", 30000),
{
var products = new[]
```csharp


Bir nesnenin belirli property’sini seçmek de mümkündür:





```

Sayı: 4
Sayı: 3
Sayı: 2
Sayı: 1
```text


Sonuç:





```

var texts = numbers.Select(number => $"Sayı: {number}");
```csharp


Örneğin sayıları metne dönüştürebiliriz:


Burada koleksiyonun eleman sayısı değişmez; sadece elemanların biçimi veya değeri değişir.





```

2, 4, 6, 8
```text


Sonuç:





```

var doubledNumbers = numbers.Select(number => number * 2);

var numbers = new[] { 1, 2, 3, 4 };
```csharp


`Select`, her elemanı başka bir değere dönüştürür.


## 3. `Select` — Dönüştürme






```

4, 6
```text


Sonuç:





```

    number > 2 && number % 2 == 0);
var result = numbers.Where(number =>
```csharp


Birden fazla koşul yazabiliriz:





```

}
    return number > 3;
{
static bool IsGreaterThanThree(int number)

var result = numbers.Where(IsGreaterThanThree);
```csharp





```

var result = numbers.Where(number => number > 3);
```csharp


Şu iki kullanım aynıdır:





```

var evenNumbers = numbers.Where(condition);

Func<int, bool> condition = number => number % 2 == 0;
```csharp


`Where` bir `Func<T, bool>` alır:





```

2, 4, 6
```text


Sonuç:





```

var evenNumbers = numbers.Where(number => number % 2 == 0);

var numbers = new[] { 1, 2, 3, 4, 5, 6 };
```csharp


`Where`, belirli koşulu sağlayan elemanları seçer.


## 2. `Where` — Filtreleme



Burada `Where` sonucu genellikle yine `IEnumerable<int>` olur.





```

var evenNumbers = numbers.Where(number => number % 2 == 0);
```csharp


LINQ metotlarının çoğu `IEnumerable<T>` üzerinde çalışır:





```

IEnumerable<int> arrayNumbers = array;
IEnumerable<int> listNumbers = list;

var array = new[] { 4, 5, 6 };
var list = new List<int> { 1, 2, 3 };
```csharp


Birçok koleksiyon `IEnumerable<T>` olarak kullanılabilir:





```

}
    Console.WriteLine(number);
{
foreach (var number in numbers)
```csharp


Bu yapı `foreach` ile kullanılabilir:





```

IEnumerable<int> numbers = new[] { 1, 2, 3, 4, 5 };
```csharp


`IEnumerable<T>`, bir koleksiyon üzerinde sırayla dolaşabilmeyi ifade eder.


## 1. `IEnumerable<T>` nedir?






```

var numbers = new[] { 1, 2, 3, 4, 5, 6 };
```csharp


Örnek veri:





```

Koleksiyondaki veriyi nasıl işleyeceğimizi daha okunabilir ve zincirlenebilir şekilde yazmak.
```text


LINQ’in temel amacı şudur:


LINQ, koleksiyonlar üzerinde filtreleme, dönüştürme, sıralama, gruplama ve sorgulama yapmamızı sağlayan bir yapıdır.


# Bölüm 4 — İleri LINQ
