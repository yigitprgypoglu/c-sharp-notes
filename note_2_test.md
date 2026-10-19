# C# Tekrar Problemleri — Bölüm 2

## Problem 6 — Boxing ve Unboxing

```csharp
int number = 42;

object boxedNumber = number;

number = 100;

int unboxedNumber = (int)boxedNumber;

Console.WriteLine(number);
Console.WriteLine(boxedNumber);
Console.WriteLine(unboxedNumber);
```

### Cevap formatı

1. satır: ?
2. satır: ?
3. satır: ?

**Nedeni:**

...

Açıklamanda şunlara değin:

- Boxing hangi satırda gerçekleşiyor?
- `number = 100` neden `boxedNumber` değerini değiştirmiyor?
- Unboxing hangi satırda gerçekleşiyor?

## Problem 7 — Hatalı Unboxing

```csharp
object value = 42;

int number = (int)value;

Console.WriteLine(number);

string text = (string)value;

Console.WriteLine(text);
```

### Cevap formatı

**Hata veren satır:** ?

**Hata nedeni:**

...

**İpucu:** `value` içindeki gerçek değer `int` türündedir. Bir `int` değeri
doğrudan `string` olarak unbox edilebilir mi?

## Problem 8 — Nullable Value Type

```csharp
int? age = null;

Console.WriteLine(age.HasValue);
Console.WriteLine(age ?? 18);

age = 25;

Console.WriteLine(age.HasValue);
Console.WriteLine(age ?? 18);
```

### Cevap formatı

1. satır: ?
2. satır: ?
3. satır: ?
4. satır: ?

**Nedeni:**

...

**İpuçları:**

- `int?`, `Nullable<int>` anlamına gelir.
- `HasValue`, değer olup olmadığını kontrol eder.
- `??`, sol taraf `null` ise sağ taraftaki değeri kullanır.

## Problem 9 — Nullable Reference Type

```csharp
string? name = null;

if (name is null)
{
    Console.WriteLine("İsim boş.");
}

Console.WriteLine(name ?? "Bilinmeyen");

name = "Ali";

if (name is not null)
{
    Console.WriteLine(name.Length);
}
```

### Cevap formatı

1. satır: ?
2. satır: ?
3. satır: ?

Ayrıca açıkla:

- `string?` ne anlama gelir?
- `??` burada ne yapıyor?
- `name is not null` kontrolünden sonra neden `name.Length` kullanılabiliyor?

## Problem 10 — `ref` ve Değer Kopyası

```csharp
int first = 10;
int second = 10;

Increase(first);
Increase(ref second);

Console.WriteLine(first);
Console.WriteLine(second);

static void Increase(int value)
{
    value++;
}

static void Increase(ref int value)
{
    value++;
}
```

### Cevap formatı

1. satır: ?
2. satır: ?

**Nedeni:**

...

**İpucu:**

- İlk `Increase` çağrısında `value`, `first` değerinin kopyasını alır.
- İkinci çağrıda `ref` kullanıldığı için metot doğrudan `second` değişkenini
  değiştirebilir.

## Problem 11 — `TryDivide` ve `out`

```csharp
Console.WriteLine(TryDivide(10, 2, out double result));
Console.WriteLine(result);

Console.WriteLine(TryDivide(10, 0, out result));
Console.WriteLine(result);

static bool TryDivide(int first, int second, out double result)
{
    if (second == 0)
    {
        result = 0;
        return false;
    }

    result = (double)first / second;
    return true;
}
```

### Cevap formatı

1. satır: ?
2. satır: ?
3. satır: ?
4. satır: ?

**Nedeni:**

...

Özellikle şunları açıklamaya çalış:

- İlk çağrıda neden `true` ve `5` döner?
- İkinci çağrıda neden `false` ve `0` döner?
- `out` parametresine neden her durumda değer atanmalıdır?

## Problem 12 — `in` Parametresi

```csharp
var point = new Point(10, 20);

PrintPoint(point);

Console.WriteLine($"X: {point.X}, Y: {point.Y}");

static void PrintPoint(in Point point)
{
    Console.WriteLine($"X: {point.X}, Y: {point.Y}");

    // point.X = 100;
}

public readonly record struct Point(int X, int Y);
```

Şu soruları cevapla:

1. Programın çıktısı nedir?
2. `in` parametresi `Point` değerinin değiştirilmesine izin verir mi?
3. `in` hangi durumda faydalı olabilir?

**İpucu:** `in`, değeri metoda salt okunur şekilde aktarır. Yorum satırının
başındaki `//` kaldırılırsa derleme hatası oluşur.

## Problem 13 — `ref`, `out` ve `in` Karşılaştırması

```csharp
int number = 10;

AddOne(number);
Console.WriteLine(number);

AddOneByRef(ref number);
Console.WriteLine(number);

CreateNumber(out number);
Console.WriteLine(number);

static void AddOne(int value)
{
    value++;
}

static void AddOneByRef(ref int value)
{
    value++;
}

static void CreateNumber(out int value)
{
    value = 100;
}
```

### Cevap formatı

1. çıktı: ?
2. çıktı: ?
3. çıktı: ?

**Nedeni:**

- `AddOne`:
- `AddOneByRef`:
- `CreateNumber`:

Açıklamanda şunlara değin:

- İlk metot neden dışarıdaki `number` değerini değiştirmiyor?
- İkinci metot neden değiştiriyor?
- Üçüncü metot neden yeni değer oluşturuyor?

## Problem 14 — `ref` ile `out` Farkı

```csharp
int first = 10;
int second;

Update(ref first);
Create(out second);

Console.WriteLine(first);
Console.WriteLine(second);

static void Update(ref int value)
{
    value += 5;
}

static void Create(out int value)
{
    value = 50;
}
```

Şunları cevapla:

1. `first` neden metoda gönderilmeden önce değer almak zorunda?
2. `second` neden önceden değer almadan `out` ile gönderilebiliyor?
3. `Update` metodu neden `value` parametresini okuyabilir?
4. `Create` metodu neden `value` parametresine mutlaka değer atamalıdır?
5. Programın çıktısı nedir?

## Problem 15 — `TryParse` Deseni

```csharp
string input = "123";

if (int.TryParse(input, out int number))
{
    Console.WriteLine($"Başarılı: {number}");
}
else
{
    Console.WriteLine("Başarısız");
}

input = "abc";

if (int.TryParse(input, out number))
{
    Console.WriteLine($"Başarılı: {number}");
}
else
{
    Console.WriteLine($"Başarısız: {number}");
}
```

### Cevap formatı

1. çıktı: ?
2. çıktı: ?

**Nedeni:**

...

Özellikle şunları açıklamaya çalış:

- `TryParse` hangi durumda `true` döndürür?
- `out number` ne işe yarar?
- İkinci `TryParse` başarısız olduğunda `number` neden yine kullanılabilir?
- `out` parametresi `true` veya `false` değerini mi, yoksa sayıyı mı taşır?
