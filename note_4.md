# Bölüm 4 — İleri LINQ


LINQ, koleksiyonlar üzerinde filtreleme, dönüştürme, sıralama, gruplama ve sorgulama yapmamızı sağlayan bir yapıdır.


LINQ’in temel amacı şudur:


```text
Koleksiyondaki veriyi nasıl işleyeceğimizi daha okunabilir ve zincirlenebilir şekilde yazmak.

```





Örnek veri:


```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

```






## 1. `IEnumerable<T>` nedir?


`IEnumerable<T>`, bir koleksiyon üzerinde sırayla dolaşabilmeyi ifade eder.


```csharp
IEnumerable<int> numbers = new[] { 1, 2, 3, 4, 5 };

```





Bu yapı `foreach` ile kullanılabilir:


```csharp
foreach (var number in numbers)
{
    Console.WriteLine(number);
}

```





Birçok koleksiyon `IEnumerable<T>` olarak kullanılabilir:


```csharp
var list = new List<int> { 1, 2, 3 };
var array = new[] { 4, 5, 6 };

IEnumerable<int> listNumbers = list;
IEnumerable<int> arrayNumbers = array;

```





LINQ metotlarının çoğu `IEnumerable<T>` üzerinde çalışır:


```csharp
var evenNumbers = numbers.Where(number => number % 2 == 0);

```





Burada `Where` sonucu genellikle yine `IEnumerable<int>` olur.



## 2. `Where` — Filtreleme


`Where`, belirli koşulu sağlayan elemanları seçer.


```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var evenNumbers = numbers.Where(number => number % 2 == 0);

```





Sonuç:


```text
2, 4, 6

```





`Where` bir `Func<T, bool>` alır:


```csharp
Func<int, bool> condition = number => number % 2 == 0;

var evenNumbers = numbers.Where(condition);

```





Şu iki kullanım aynıdır:


```csharp
var result = numbers.Where(number => number > 3);

```





```csharp
var result = numbers.Where(IsGreaterThanThree);

static bool IsGreaterThanThree(int number)
{
    return number > 3;
}

```





Birden fazla koşul yazabiliriz:


```csharp
var result = numbers.Where(number =>
    number > 2 && number % 2 == 0);

```





Sonuç:


```text
4, 6

```






## 3. `Select` — Dönüştürme


`Select`, her elemanı başka bir değere dönüştürür.


```csharp
var numbers = new[] { 1, 2, 3, 4 };

var doubledNumbers = numbers.Select(number => number * 2);

```





Sonuç:


```text
2, 4, 6, 8

```





Burada koleksiyonun eleman sayısı değişmez; sadece elemanların biçimi veya değeri değişir.


Örneğin sayıları metne dönüştürebiliriz:


```csharp
var texts = numbers.Select(number => $"Sayı: {number}");

```





Sonuç:


```text
Sayı: 1
Sayı: 2
Sayı: 3
Sayı: 4

```





Bir nesnenin belirli property’sini seçmek de mümkündür:


```csharp
var products = new[]
{
    new Product("Laptop", 30000),
    new Product("Mouse", 500),
    new Product("Keyboard", 1500)
};

var productNames = products.Select(product => product.Name);

foreach (var name in productNames)
{
    Console.WriteLine(name);
}

public record Product(string Name, decimal Price);

```






## 4. `Where` ve `Select` birlikte


LINQ metotları zincirlenebilir:


```csharp
var result = numbers
    .Where(number => number > 2)
    .Select(number => number * 10);

```





Adım adım:


Başlangıç:


```text
1, 2, 3, 4, 5, 6

```





`Where` sonrası:


```text
3, 4, 5, 6

```





`Select` sonrası:


```text
30, 40, 50, 60

```





Genellikle önce filtrelemek, sonra dönüştürmek daha verimlidir:


```csharp
var result = products
    .Where(product => product.Price > 1000)
    .Select(product => product.Name);

```





Böylece yalnızca gerekli ürünler dönüştürülür.



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



## 7. Deferred Execution — Ertelenmiş çalıştırma


LINQ’in en önemli özelliklerinden biri sorgunun hemen çalıştırılmamasıdır.


```csharp
var numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(number => number > 1);

numbers.Add(4);

foreach (var number in query)
{
    Console.WriteLine(number);
}

```





Çıktı:


```text
2
3
4

```





Neden? Çünkü `Where` sorgusu oluşturulduğu anda değil, `foreach` başladığında çalışır.


Akış:


```text
1. query oluşturulur.
2. Henüz filtreleme yapılmaz.
3. Listeye 4 eklenir.
4. foreach sorguyu çalıştırır.
5. Güncel liste filtrelenir.

```





Bu davranışa **deferred execution**, yani ertelenmiş çalıştırma denir.



## 8. Deferred execution örneği


```csharp
var numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(number =>
{
    Console.WriteLine($"Kontrol ediliyor: {number}");
    return number > 1;
});

Console.WriteLine("Sorgu oluşturuldu.");

foreach (var number in query)
{
    Console.WriteLine($"Sonuç: {number}");
}

```





Muhtemel çıktı:


```text
Sorgu oluşturuldu.
Kontrol ediliyor: 1
Kontrol ediliyor: 2
Sonuç: 2
Kontrol ediliyor: 3
Sonuç: 3

```





`Where` içindeki kod, `query` tanımlandığında çalışmaz. `foreach` sırasında çalışır.



## 9. Sorguyu hemen çalıştırmak: `ToList`, `ToArray`


Sorguyu o anda çalıştırıp sonucu sabitlemek için `ToList()` veya `ToArray()` kullanılır:


```csharp
var numbers = new List<int> { 1, 2, 3 };

var query = numbers
    .Where(number => number > 1)
    .ToList();

numbers.Add(4);

foreach (var number in query)
{
    Console.WriteLine(number);
}

```





Çıktı:


```text
2
3

```





Çünkü `ToList()` çağrıldığı anda sorgu çalıştırılmış ve sonuç yeni bir listeye kopyalanmıştır. Daha sonra kaynak listeye `4` eklenmesi `query` listesini değiştirmez.


Karşılaştırma:


```csharp
var deferredQuery = numbers.Where(number => number > 1);

```





Sorgu ertelenir.


```csharp
var immediateQuery = numbers
    .Where(number => number > 1)
    .ToList();

```





Sorgu hemen çalışır.



## 10. `yield return`


Kendi ertelenmiş çalışan iterator metodumuzu yazabiliriz:


```csharp
static IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
{
    foreach (var number in numbers)
    {
        if (number % 2 == 0)
        {
            yield return number;
        }
    }
}

```





Kullanımı:


```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var evenNumbers = GetEvenNumbers(numbers);

foreach (var number in evenNumbers)
{
    Console.WriteLine(number);
}

```





Çıktı:


```text
2
4
6

```





`yield return`:



- Metodu tek seferde bütün sonuçları üretmeye zorlamaz.

- Elemanları ihtiyaç oldukça üretir.

- Iterator oluşturur.

- Bellek kullanımını azaltabilir.



Bu kod:


```csharp
yield return number;

```





şu anlama gelir:


Bu elemanı şimdi döndür, daha sonra kaldığın yerden devam et.


## 11. `yield return` ile normal liste arasındaki fark


Liste oluşturan metot:


```csharp
static List<int> GetEvenNumbers(List<int> numbers)
{
    var result = new List<int>();

    foreach (var number in numbers)
    {
        if (number % 2 == 0)
        {
            result.Add(number);
        }
    }

    return result;
}

```





Bu metot bütün sonuçları bellekte tutar.


`yield return` kullanan metot:


```csharp
static IEnumerable<int> GetEvenNumbers(IEnumerable<int> numbers)
{
    foreach (var number in numbers)
    {
        if (number % 2 == 0)
        {
            yield return number;
        }
    }
}

```





Sonuçları tek tek üretir.


Büyük koleksiyonlarda `yield return` faydalı olabilir.



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



## 23. `IEnumerable<T>` ve `IQueryable<T>` farkı


### `IEnumerable<T>`


Veri genellikle bellektedir:


```csharp
IEnumerable<int> numbers = new[] { 1, 2, 3, 4 };

```





LINQ işlemleri .NET uygulaması içinde çalışır.


```csharp
var result = numbers.Where(number => number > 2);

```





### `IQueryable<T>`


Veri genellikle dış bir kaynaktadır:



- Veritabanı

- Uzak servis

- Özel sorgu sağlayıcısı



```csharp
IQueryable<Product> products = dbContext.Products;

var result = products
    .Where(product => product.Price > 1000);

```





Entity Framework gibi araçlar expression ağacını SQL’e çevirebilir:


```sql
WHERE Price > 1000

```





Temel fark:


```text
IEnumerable  -> Sorgu uygulama içinde çalışır.
IQueryable   -> Sorgu veri kaynağına çevrilebilir.

```





`IQueryable<T>` için kullanılan lambda genellikle şudur:


```csharp
Expression<Func<Product, bool>>

```





`IEnumerable<T>` için ise:


```csharp
Func<Product, bool>

```





kullanılır.



## 24. `IEnumerable` ile `IQueryable` dikkat noktası


Şu kod:


```csharp
var products = dbContext.Products
    .Where(product => product.Price > 1000)
    .ToList();

```





Önce filtreyi veritabanına göndermeye çalışır, sonra sonucu listeye çevirir.


Fakat:


```csharp
var products = dbContext.Products
    .ToList()
    .Where(product => product.Price > 1000);

```





önce bütün ürünleri veritabanından çeker, sonra filtrelemeyi bellekte yapar.


Genellikle daha iyi olan:


```csharp
var products = dbContext.Products
    .Where(product => product.Price > 1000)
    .ToList();

```





Çünkü filtre veritabanında uygulanır ve gereksiz veriler taşınmaz.



## 25. LINQ’in değerlendirme noktaları


Şu metotlar genellikle sorguyu hemen çalıştırır:


```csharp
ToList()
ToArray()
First()
FirstOrDefault()
Single()
SingleOrDefault()
Count()
Any()
All()
Sum()
Min()
Max()
Average()

```





Şu metotlar ise çoğunlukla ertelenmiş sorgu üretir:


```csharp
Where()
Select()
OrderBy()
OrderByDescending()
ThenBy()
Skip()
Take()
GroupBy()
SelectMany()

```





Önemli not: `GroupBy` veya `OrderBy` ertelenmiş görünse de çalıştırıldığında kendi işlemi için veriyi büyük ölçüde tarayabilir.



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






## 27. LINQ sorgu zinciri nasıl okunur?


```csharp
var result = products
    .Where(product => product.Price > 1000)
    .OrderByDescending(product => product.Price)
    .Select(product => new
    {
        product.Name,
        product.Price
    })
    .Take(3)
    .ToList();

```





Adım adım:


```text
1. Fiyatı 1000’den büyük ürünleri seç.
2. Pahalıdan ucuza sırala.
3. Sadece Name ve Price bilgilerini seç.
4. İlk 3 ürünü al.
5. Sorguyu hemen çalıştır ve listeye çevir.

```





Bu, LINQ’in en yaygın kullanım biçimlerinden biridir.



## 28. LINQ’te sık yapılan hatalar


### Gereksiz `ToList`


```csharp
var result = numbers
    .Where(number => number > 2)
    .ToList()
    .Select(number => number * 2)
    .ToList();

```





Çoğu durumda ara `ToList()` gereksizdir:


```csharp
var result = numbers
    .Where(number => number > 2)
    .Select(number => number * 2)
    .ToList();

```





### `Count() > 0` yerine `Any()`


```csharp
if (numbers.Count() > 0)
{
}

```





Daha uygun:


```csharp
if (numbers.Any())
{
}

```





Koşullu kontrol için:


```csharp
if (numbers.Any(number => number > 100))
{
}

```





### `First()` ile boş koleksiyon


```csharp
var first = numbers.First();

```





Koleksiyon boş olabilir:


```csharp
var first = numbers.FirstOrDefault();

```





Ancak `FirstOrDefault()` için de `null` veya varsayılan değer ihtimali dikkate alınmalıdır.


### Sorguyu birden fazla kez çalıştırmak


```csharp
var query = numbers.Where(number => number > 2);

foreach (var number in query)
{
}

foreach (var number in query)
{
}

```





Deferred execution nedeniyle filtreleme iki kez yapılabilir. Sonuç tekrar kullanılacaksa:


```csharp
var result = numbers
    .Where(number => number > 2)
    .ToList();

```






## 29. Kendi LINQ extension metodumuzu yazmak


LINQ metotları extension method olarak tanımlanır.


```csharp
public static class EnumerableExtensions
{
    public static IEnumerable<int> WherePositive(
        this IEnumerable<int> numbers)
    {
        foreach (var number in numbers)
        {
            if (number > 0)
            {
                yield return number;
            }
        }
    }
}

```





Kullanımı:


```csharp
var numbers = new[] { -2, 0, 1, 3, -5 };

var positiveNumbers = numbers.WherePositive();

foreach (var number in positiveNumbers)
{
    Console.WriteLine(number);
}

```





Çıktı:


```text
1
3

```





Buradaki:


```csharp
this IEnumerable<int> numbers

```





ilk parametrenin extension method üzerinde çağrılacağını gösterir.



## 30. Generic LINQ extension metodu


Daha genel bir filtre yazabiliriz:


```csharp
public static class EnumerableExtensions
{
    public static IEnumerable<T> WhereNotNull<T>(
        this IEnumerable<T?> source)
        where T : class
    {
        foreach (var item in source)
        {
            if (item is not null)
            {
                yield return item;
            }
        }
    }
}

```





Bu tarz generic extension metotlar, LINQ’in nasıl çalıştığını anlamak ve tekrar kullanılabilir yardımcılar yazmak için kullanılır.



# Bölüm 4 Özeti


## Filtreleme


```csharp
Where()

```





Koşulu sağlayan elemanları seçer.


## Dönüştürme


```csharp
Select()

```





Elemanları başka bir biçime dönüştürür.


## Sıralama


```csharp
OrderBy()
OrderByDescending()
ThenBy()
ThenByDescending()

```





## Gruplama ve düzleştirme


```csharp
GroupBy()
SelectMany()

```





## Eleman seçme


```csharp
First()
FirstOrDefault()
Single()
SingleOrDefault()

```





## Kontrol


```csharp
Any()
All()

```





## Hesaplama


```csharp
Count()
Sum()
Min()
Max()
Average()

```





## Sayfalama


```csharp
Skip()
Take()

```





## Çalıştırma davranışı


```text
Where, Select gibi metotlar genellikle ertelenmiş çalışır.
ToList, ToArray, Count, Any gibi metotlar sorguyu çalıştırır.

```





## Arayüz farkı


```text
IEnumerable<T> -> Bellekteki veriler üzerinde LINQ
IQueryable<T>  -> Sorgu sağlayıcısına çevrilebilen LINQ

```





## Bölüm 4’ün ana fikri


```csharp
var result = products
    .Where(product => product.Price > 1000)
    .OrderByDescending(product => product.Price)
    .Select(product => product.Name)
    .Take(5)
    .ToList();

```





Bu kod:



1. Fiyatı 1000’den büyük ürünleri seçer.

2. Pahalıdan ucuza sıralar.

3. Ürün isimlerini alır.

4. İlk 5 sonucu seçer.

5. Sorguyu çalıştırıp listeye dönüştürür.



Bölüm 4’ün teorik kısmı artık tamamlandı. Bundan sonra soruları **`IEnumerable` ve temel `Where`/`Select` işlemlerinden başlayarak** sırayla çözeceğiz.

