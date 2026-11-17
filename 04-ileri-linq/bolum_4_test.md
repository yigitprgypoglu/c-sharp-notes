# 04 — İleri LINQ Tekrar Çalışması

## 01 — LINQ Temelleri: `Where` ve `Select`

Aşağıdaki kodun çıktısını tahmin et:

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var result = numbers
    .Where(number => number > 2)
    .Select(number => number * 2);

foreach (var number in result)
{
    Console.WriteLine(number);
}
```

Cevabında `Where` ve `Select` sonrasındaki koleksiyonları ayrı ayrı yaz.

## 02 — Sıralama ve Sayfalama

Aşağıdaki kodun çıktısını tahmin et:

```csharp
var numbers = new[] { 8, 3, 10, 1, 6, 4, 9 };

var result = numbers
    .Where(number => number % 2 == 0)
    .OrderByDescending(number => number)
    .Skip(1)
    .Take(2);

foreach (var number in result)
{
    Console.WriteLine(number);
}
```

İşlemleri sırayla yaz:

```text
Where sonrası:
OrderByDescending sonrası:
Skip sonrası:
Take sonrası:
```

## 03 — Eleman Seçme, Kontrol ve Hesaplama

Aşağıdaki kodu incele:

```csharp
var numbers = new[] { 4, 8, 12, 16 };

Console.WriteLine(numbers.Any(number => number > 10));
Console.WriteLine(numbers.All(number => number % 2 == 0));
Console.WriteLine(numbers.First(number => number > 5));
Console.WriteLine(numbers.Count());
Console.WriteLine(numbers.Sum());
Console.WriteLine(numbers.Average());
```

Her satırın çıktısını tahmin et. Ardından şunları açıkla:

- `First` yerine `FirstOrDefault` hangi durumda tercih edilebilir?
- `Any` ile `Count() > 0` arasındaki fark nedir?
- `All` neyi kontrol eder?

## 04 — Gruplama ve Birleştirme

Aşağıdaki kodun oluşturduğu grupları ve çıktı sırasını tahmin et:

```csharp
var employees = new[]
{
    new Employee("Ali", "IT"),
    new Employee("Ayşe", "IT"),
    new Employee("Mehmet", "HR")
};

var groups = employees.GroupBy(employee => employee.Department);

foreach (var group in groups)
{
    Console.WriteLine(group.Key);

    foreach (var employee in group)
    {
        Console.WriteLine(employee.Name);
    }
}

public record Employee(string Name, string Department);
```

Sonra `GroupBy` kullanarak her departmanda kaç çalışan olduğunu bulan bir sorgu yaz.

## 05 — Deferred ve Immediate Execution

Aşağıdaki kodun çıktısını tahmin et:

```csharp
var numbers = new List<int> { 1, 2, 3 };

var query = numbers.Where(number => number > 1);

numbers.Add(4);

foreach (var number in query)
{
    Console.WriteLine(number);
}
```

Sonra kodu `ToList()` kullanacak şekilde değiştir ve sonucun neden farklı
olabileceğini açıkla.

## 06 — `IEnumerable<T>` ve `IQueryable<T>`

Şu iki türü kendi cümlelerinle karşılaştır:

```text
IEnumerable<Product>
IQueryable<Product>
```

Aşağıdaki iki sorguda filtrelemenin nerede gerçekleştiğini açıkla:

```csharp
var result1 = dbContext.Products
    .Where(product => product.Price > 1000)
    .ToList();
```

```csharp
var result2 = dbContext.Products
    .ToList()
    .Where(product => product.Price > 1000);
```

Hangisi genellikle veritabanından daha az veri çeker? Neden?

## 07 — Iterator ve LINQ Extension Method

`yield return` kullanarak pozitif sayıları döndüren bir metot yaz:

```csharp
static IEnumerable<int> GetPositiveNumbers(
    IEnumerable<int> numbers)
{
    // Kodunu buraya yaz
}
```

Şu veri için metodun döndürdüğü değerleri belirt:

```csharp
var numbers = new[] { -3, 0, 2, 5, -1 };
```

Ardından `yield return` ile sonuçları bir `List<int>` içinde biriktirip
döndürmek arasındaki farkı açıkla.

## 08 — Sorgu Akışı ve Performans

Aşağıdaki kodu incele:

```csharp
var result = products
    .Where(product => product.Price > 1000)
    .ToList()
    .Select(product => product.Name)
    .ToList();
```

Şunları cevapla:

- Burada kaç kez `ToList()` çağrılıyor?
- İlk `ToList()` gerekli mi?
- Sorguyu daha sade nasıl yazarsın?
- Sorgu birden fazla kez kullanılacaksa `ToList()` ne zaman yararlı olabilir?

## 09 — Karma LINQ Problemi

Şu ürünleri kullan:

```csharp
var products = new[]
{
    new Product("Mouse", "Aksesuar", 500),
    new Product("Keyboard", "Aksesuar", 1500),
    new Product("Monitor", "Ekran", 8000),
    new Product("Webcam", "Aksesuar", 2500),
    new Product("Laptop", "Bilgisayar", 40000)
};

public record Product(string Name, string Category, decimal Price);
```

LINQ kullanarak:

1. Fiyatı 1000’den fazla ürünleri seç.
2. Fiyata göre büyükten küçüğe sırala.
3. Her üründen yalnızca `Name` ve `Price` bilgilerini al.
4. İlk iki ürünü seç.
5. Sonucu `List` olarak elde et.

Kullandığın zinciri yaz ve her adımın ne yaptığını açıkla.
