# Bölüm 2 — Boxing ve Unboxing

## Değer türlerinden referans türlerine dönüşüm

- Boxing ve unboxing nedir?
- `object` ile value type ilişkisi
- Tür dönüşüm hataları ve pattern matching
- Boxing'in performans maliyeti
- Generic türlerle gereksiz boxing'i önleme

**Hedef:** Değer türlerinin referans türlerine dönüştürülmesini ve güvenli
biçimde geri alınmasını anlamak.

## Boxing ve Unboxing

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
