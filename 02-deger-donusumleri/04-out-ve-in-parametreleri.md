# Bölüm 5 — `out` ve `in` Parametreleri

## Çıktı üretme ve salt okunur parametreler

- `out` ile metottan değer döndürme
- `TryParse` deseni
- `in` ile salt okunur parametre aktarımı
- Büyük struct değerlerinde kopyalama maliyeti
- `ref`, `out` ve `in` karşılaştırması
- `TryDivide` uygulaması

**Hedef:** `out` ve `in` parametrelerini doğru senaryolarda kullanabilmek.

## `out` Parametresi

`out`, metodun bir değer üretip çağıran tarafa göndermesini sağlar.

```csharp
static bool TryParseNumber(string text, out int number)
{
    return int.TryParse(text, out number);
}

if (TryParseNumber("123", out int result))
{
    Console.WriteLine(result);
}
```

`out` parametresinin metot içinde mutlaka atanması gerekir:

```csharp
static void CreateValue(out int value)
{
    value = 42;
}
```

Modern C#'ta `TryParse` deseni çok yaygındır:

```csharp
if (int.TryParse("250", out int number))
{
    Console.WriteLine($"Başarılı: {number}");
}
else
{
    Console.WriteLine("Geçersiz sayı");
}
```

`out` kullanırken değişkeni önceden tanımlamak zorunda değilsin:

```csharp
if (DateTime.TryParse("2026-09-11", out DateTime date))
{
    Console.WriteLine(date);
}
```

## `in` Parametresi

`in`, bir value type değerini kopyalamadan, yalnızca okunabilir şekilde metoda
gönderir.

```csharp
static void PrintPoint(in Point point)
{
    Console.WriteLine($"X: {point.X}, Y: {point.Y}");
}
```

Çağırırken genellikle `in` yazmak isteğe bağlıdır:

```csharp
var point = new Point(10, 20);

PrintPoint(point);
```

`in` parametresi metot içinde değiştirilemez:

```csharp
static void ChangePoint(in Point point)
{
    // point.X = 50; // Derleme hatası
}
```

Büyük `struct` değerlerinde gereksiz kopyalamayı önlemek için kullanılabilir.

```csharp
public readonly record struct Point(int X, int Y);
```

### `ref`, `out` ve `in` karşılaştırması

| Anahtar kelime | Amaç | Metot içinde değiştirilebilir mi? | Çağrıdan önce değer gerekir mi? |
| --- | --- | --- | --- |
| `ref` | Orijinal değeri değiştirmek | Evet | Evet |
| `out` | Metottan değer döndürmek | Evet | Hayır |
| `in` | Kopyalamadan salt okunur göndermek | Hayır | Evet |

## Mini alıştırma

Aşağıdaki kodun çıktısı ne olur?

```csharp
static void Update(ref int value)
{
    value *= 2;
}

static void Create(out int value)
{
    value = 10;
}

int number = 5;

Update(ref number);
Create(out number);

Console.WriteLine(number);
```

Ardından şu metodu yaz:

```csharp
static bool TryDivide(
    int dividend,
    int divisor,
    out double result)
{
    if (divisor == 0)
    {
        result = 0;
        return false;
    }

    result = (double)dividend / divisor;
    return true;
}
```

## Üç yöntemin karşılaştırması

### Normal parametre

```csharp
static void Method(Point point)
```

- `Point` kopyalanır.
- Okunabilir.
- Kopya üzerinde değişiklik yapılabilir.
- Dışarıdaki `Point` değişmez.

### `ref`

```csharp
static void Method(ref Point point)
```

- Kopya oluşturulmaz.
- Orijinal değer kullanılır.
- Okunabilir ve değiştirilebilir.

Örnek:

```csharp
static void ChangePoint(ref Point point)
{
    point.X = 99;
}

var point = new Point { X = 10, Y = 20 };

ChangePoint(ref point);

Console.WriteLine(point.X); // 99
```

`ref` kullanırken çağrıda da `ref` yazılır:

```csharp
ChangePoint(ref point);
```

### `in`

```csharp
static void Method(in Point point)
```

- Kopya oluşturulmadan orijinal değer okunur.
- Okunabilir.
- Değiştirilemez.

Çağırırken genellikle şöyle yazılır:

```csharp
Method(point);
```

İstersen açıkça da yazabilirsin:

```csharp
Method(in point);
```
