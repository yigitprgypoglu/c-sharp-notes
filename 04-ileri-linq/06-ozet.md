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
