# Bölüm 2.3 — Parametre Aktarımı ve `ref`

## Metotların değişkenler üzerindeki etkisi

- Normal parametre aktarımı
- Value type değerlerinin kopyalanması
- Reference type parametrelerinin davranışı
- `ref` ile orijinal değişkeni değiştirme
- `Swap` örneği
- Normal parametre ile `ref` arasındaki fark

**Hedef:** Parametre aktarımının metot dışındaki değişkenleri nasıl etkilediğini
anlamak.

## `ref` Parametresi

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
