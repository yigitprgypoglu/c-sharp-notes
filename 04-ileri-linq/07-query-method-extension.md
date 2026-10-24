# 4.7 — Query Syntax, Method Syntax ve Extension Metotları

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
