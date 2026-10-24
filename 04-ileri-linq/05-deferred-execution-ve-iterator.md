# 4.5 — Deferred Execution ve Iterator Mantığı

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
