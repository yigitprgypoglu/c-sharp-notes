# 4.8 — Sorgu Akışı, Performans ve Yaygın Hatalar

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
