# Bölüm 9 — Expression Tree ve IQueryable

## Kodun yapısını temsil etme ve sorgulama

- `Expression<TDelegate>` nedir?
- Delegate ile Expression arasındaki fark
- `Compile()` kullanımı
- Expression tree yapısını inceleme
- `IQueryable<T>` ve `IEnumerable<T>` farkı
- Entity Framework ve SQL'e dönüştürülebilen sorgular

**Hedef:** Bir lambda ifadesinin çalıştırılabilir davranış ile incelenebilir
kod yapısı arasındaki farkını anlamak.

## Expression nedir?

Normal bir lambda çalıştırılabilir:

```csharp
Func<int, bool> isPositive = number => number > 0;
```

Expression ise lambda ifadesini bir **ağaç yapısı** olarak temsil eder:

```csharp
using System.Linq.Expressions;

Expression<Func<int, bool>> expression =
    number => number > 0;
```

Bu ifade doğrudan çalıştırılabilir, ancak önce derlenmesi gerekir:

```csharp
Func<int, bool> compiledExpression = expression.Compile();

Console.WriteLine(compiledExpression(10)); // True
```

Expression'ın yapısını inceleyebiliriz:

```csharp
Console.WriteLine(expression);
// number => (number > 0)
```

Expression tree özellikle ORM araçlarında kullanılır:

```csharp
Expression<Func<Product, bool>> filter =
    product => product.Price > 100;
```

Entity Framework gibi araçlar bu ifadeyi SQL'e çevirebilir:

```sql
WHERE Price > 100
```

Bu nedenle `IQueryable<T>` ile kullanılan sorgular sadece C# kodu olarak
çalışmayabilir; veri kaynağı tarafından farklı bir dile çevrilebilir.

## Delegate ve Expression farkı

| Özellik | Delegate | Expression |
| --- | --- | --- |
| Lambda'yı çalıştırır | Evet | `Compile()` sonrası |
| Kod yapısını inceletir | Hayır | Evet |
| SQL'e çevrilebilir | Genellikle hayır | Evet, sağlayıcı destekliyorsa |
| Kullanım alanı | Uygulama içi davranış | Sorgu ve metaprogramlama |

Örnek:

```csharp
Func<Product, bool> funcFilter =
    product => product.Price > 100;

Expression<Func<Product, bool>> expressionFilter =
    product => product.Price > 100;
```

`Func` doğrudan .NET içinde çalışır. `Expression`, ifadenin ne yaptığını
temsil eder.

## Özet

- `delegate`, metotları değer gibi taşır.
- `Func`, değer döndüren delegate'tir.
- `Action`, değer döndürmeyen delegate'tir.
- `Predicate`, `bool` döndüren delegate'tir.
- `event`, olaylara abone olmayı sağlar.
- `Expression`, lambda ifadesinin yapısını temsil eder.
- Delegate davranışı çalıştırır; expression ise davranışı analiz edilebilir
  hale getirir.
