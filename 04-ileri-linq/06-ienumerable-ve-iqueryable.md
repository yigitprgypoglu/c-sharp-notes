# 4.6 — IEnumerable ve IQueryable

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
