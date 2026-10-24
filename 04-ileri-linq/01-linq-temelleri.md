# 4.1 — LINQ Temelleri ve Veri Dönüştürme

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
