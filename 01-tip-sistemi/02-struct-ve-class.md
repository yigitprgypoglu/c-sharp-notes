# Bölüm 1.2 — `struct` ve `class` Arasındaki Farklar

## Bellek davranışı ve tür tasarımı

- `struct` ve `class` karşılaştırması
- Struct kopyalama davranışı
- Kalıtım ve interface kuralları
- `object` ve `ValueType` ilişkisi
- `readonly struct` kullanım amacı

**Hedef:** Değer türü ve referans türü seçiminin davranış üzerindeki etkisini
anlayabilmek.

## `struct` ve `class` farkı

```csharp
public struct Point
{
    public int X { get; set; }
    public int Y { get; set; }
}

public class Coordinate
{
    public int X { get; set; }
    public int Y { get; set; }
}
```

`struct` genellikle küçük ve değişmez değerleri temsil etmek için kullanılır.
`class` kimliği olan, daha karmaşık veya yaşam süresi yönetilen nesneler için
kullanılır.

### `struct` kopyalama örneği

```csharp
var point1 = new Point { X = 1, Y = 2 };
var point2 = point1;

point2.X = 99; //Bu işlem yalnızca point2 değerinin X özelliğini değiştirir:

Console.WriteLine(point1.X); // 1
Console.WriteLine(point2.X); // 99
```

`struct` kopyalandığı için `point1` değişmez.

Bir `struct`:

- Başka bir `class`'tan kalıtım alamaz.
- Başka bir `struct`'tan kalıtım alamaz.
- Base class olarak kullanılamaz.
- Ancak interface uygulayabilir.

### `class` ve `struct` kalıtım özellikleri

Alıştırma: `Point` ve `Coordinate` örneklerini karşılaştırma.

**class:**

- `object`'ten gelir.
- Başka class'lardan kalıtım alabilir.
- Interface uygulayabilir.

**struct:**

- `ValueType` üzerinden `object`'ten gelir.
- Başka class veya struct'tan kalıtım alamaz.
- Interface uygulayabilir.
