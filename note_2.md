# Bölüm 2: Boxing, Nullable Reference Types ve `ref` / `in` / `out`

## 1. Boxing ve Unboxing

**Boxing**, bir value type değerinin `object` veya interface türüne
dönüştürülmesidir. Değer heap üzerinde bir nesneye sarılır.

```csharp
int number = 42;

object boxed = number; // Boxing

Console.WriteLine(boxed);
```

Burada `number` stack üzerinde bir `int` değeridir. `object` bir reference type
olduğu için C# değeri heap üzerinde nesneye dönüştürür.

**Unboxing**, tekrar value type'a dönüştürme işlemidir:

```csharp
object boxed = 42;

int number = (int)boxed; // Unboxing

Console.WriteLine(number);
```

Tür yanlışsa hata oluşur:

```csharp
object value = 42;

// InvalidCastException oluşur
string text = (string)value;
```

Güvenli dönüşüm için pattern matching kullanılabilir:

```csharp
object value = 42;

if (value is int number)
{
    Console.WriteLine($"Sayı: {number}");
}
```

Boxing performans maliyeti oluşturabilir:

```csharp
int total = 0;

for (int i = 0; i < 1_000_000; i++)
{
    object boxed = i;
    total += (int)boxed;
}
```

Performans kritik kodlarda gereksiz boxing'den kaçınmak için generic türler
tercih edilir:

```csharp
static T ReturnValue<T>(T value)
{
    return value;
}

int result = ReturnValue(42);
```

## 2. Nullable Value Types

Value type türler normalde `null` alamaz:

```csharp
int age = null; // Derleme hatası
```

Nullable value type ile `int` değerinin boş olmasına izin verebiliriz:

```csharp
int? age = null;

if (age.HasValue)
{
    Console.WriteLine(age.Value);
}
```

Daha güvenli ve kısa kullanım:

```csharp
int? age = null;

Console.WriteLine(age ?? 0);
```

`??` operatörü, değer `null` ise varsayılan değeri kullanır.

```csharp
int? score = null;

int finalScore = score ?? -1;
Console.WriteLine(finalScore); // -1
```

Null koşullu erişim için `?.` kullanılabilir:

```csharp
int? age = 25;

Console.WriteLine(age?.ToString());
```

## 3. Nullable Reference Types

C#'ta reference type'lar varsayılan olarak `null` olabilir:

```csharp
string name = null;
```

Nullable reference types aktifse derleyici uyarı verir:

```csharp
#nullable enable

string name = null;   // Uyarı
string? surname = null;
```

`string?`, değişkenin `null` olabileceğini açıkça belirtir:

```csharp
string? name = null;

if (name is not null)
{
    Console.WriteLine(name.Length);
}
```

Null kontrolünden sonra derleyici `name` değerinin boş olmadığını anlayabilir.

```csharp
void PrintLength(string? text)
{
    if (text is null)
    {
        return;
    }

    Console.WriteLine(text.Length);
}
```

Null-forgiving operator `!`, derleyiciye değerin null olmadığını söyler:

```csharp
string? name = GetName();

Console.WriteLine(name!.Length);
```

Ancak `!` runtime kontrolü yapmaz. Değer gerçekten `null` ise hata oluşur. Bu
nedenle yalnızca değerin kesinlikle null olmadığını biliyorsan kullanmalısın.

## 4. `ref` Parametresi

Normal parametrelerde value type değerinin kopyası gönderilir:

```csharp
static void Increase(int number)
{
    number++;
}

int value = 10;

Increase(value);

Console.WriteLine(value); // 10
```

`ref` kullanılırsa metodun orijinal değişkeni değiştirmesine izin verilir:

```csharp
static void Increase(ref int number)
{
    number++;
}

int value = 10;

Increase(ref value);

Console.WriteLine(value); // 11
```

Hem metot tanımında hem çağrıda `ref` yazılmalıdır.

```csharp
static void Swap(ref int first, ref int second)
{
    (first, second) = (second, first);
}

int a = 10;
int b = 20;

Swap(ref a, ref b);

Console.WriteLine(a); // 20
Console.WriteLine(b); // 10
```

## 5. `out` Parametresi

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

## 6. `in` Parametresi

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

## Üç yöntemin karşılaştırması


### Normal parametre


C#






```
static void Method(Point point)

```





Plain text






```
Point kopyalanır.
Okunabilir.
Kopya üzerinde değişiklik yapılabilir.
Dışarıdaki Point değişmez.

```





### `ref`


C#






```
static void Method(ref Point point)

```





Plain text






```
Kopya oluşturulmaz.
Orijinal değer kullanılır.
Okunabilir ve değiştirilebilir.

```





Örnek:


C#






```
static void ChangePoint(ref Point point)
{
    point.X = 99;
}

var point = new Point { X = 10, Y = 20 };

ChangePoint(ref point);

Console.WriteLine(point.X); // 99

```





`ref` kullanırken çağrıda da `ref` yazılır:


C#






```
ChangePoint(ref point);

```





### `in`


C#






```
static void Method(in Point point)

```





Plain text






```
Kopya oluşturulmadan orijinal değer okunur.
Okunabilir.
Değiştirilemez.

```





Çağırırken genellikle şöyle yazılır:


C#






```
Method(point);

```





İstersen açıkça da yazabilirsin:


C#






```
Method(in point);
```

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
