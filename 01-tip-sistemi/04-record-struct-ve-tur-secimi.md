# Bölüm 4 — `record struct` ve Doğru Tür Seçimi

## Pratik tasarım kararları

- `record struct` nedir?
- `readonly record struct`
- Otomatik üretilen eşitlik üyeleri
- `struct`, `class`, `record` ve `record struct` karşılaştırması
- Hangi senaryoda hangi tür seçilmeli?

**Hedef:** İhtiyaca uygun türü seçebilmek.

## `record struct`

Değer tipinin kopyalama davranışını ve `record`'un içerik eşitliğini birleştirir:

```csharp
public readonly record struct Money(decimal Amount, string Currency);

var money1 = new Money(100, "TRY");
var money2 = new Money(100, "TRY");

Console.WriteLine(money1 == money2); // True
```

Bu satır, `Money` adında bir **readonly record struct** tanımlar. İçinde
`Amount` ve `Currency` property'leri otomatik olarak oluşturulur.

Normal bir `struct` için de değer karşılaştırması vardır; ancak `record struct`,
bu davranışları daha düzenli şekilde otomatik üretir:

- `Equals`
- `GetHashCode`
- `==`
- `!=`
- `ToString`

`readonly` ise oluşturulan `Money` değerinin sonradan değiştirilmesini engeller.

## Önemli kural

Bir türün `class`, `struct` veya `record` olması; performans, eşitlik,
mutability ve API tasarımı üzerinde doğrudan etkilidir.

Genel yaklaşım:

- Küçük, basit ve mümkünse immutable değerler: `readonly struct`
- Veri modelleri ve immutable DTO'lar: `record`
- Davranış ve kimlik taşıyan nesneler: `class`
- Değer eşitliği gereken küçük nesneler: `record struct`

Ardından şu modeli `record` kullanarak yeniden yaz:

```csharp
public class Product
{
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
}
```

Örnek bir `record` karşılığı:

```csharp
public record ProductRecord(string Name, decimal Price);
```

Alıştırma: `Money` ve `Product` modellerini uygun türlerle tasarlama.
