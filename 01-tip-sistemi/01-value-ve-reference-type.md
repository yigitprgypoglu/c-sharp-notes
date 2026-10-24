# Bölüm 1 — C# Tip Sistemi: Value Type ve Reference Type

## Temel kavramlar

- Value type ve reference type farkı
- Değer kopyalama ve referans kopyalama
- `int` ve `class` örnekleri
- Değişikliklerin diğer değişkenlere etkisi

**Hedef:** Bir değerin kopyalanması ile bir nesneye referans kopyalanması
arasındaki farkı anlayabilmek.

# Bölüm 1: C# Tip Sistemi

C#'ta değişkenler temel olarak iki kategoriye ayrılır:

- **Value type:** Değerin kendisini taşır.
- **Reference type:** Nesnenin bellekteki adresine referans taşır.

## Value type örneği

```csharp
int first = 10;
int second = first;

second = 20;

Console.WriteLine(first);  // 10
Console.WriteLine(second); // 20
```

`second`, `first` değişkeninin değerinin kopyasını alır. Daha sonra `second`
değişince `first` etkilenmez.

## Reference type örneği

```csharp
var first = new ReferencePerson { Name = "Ali" };
var second = first;

second.Name = "Veli";

Console.WriteLine(first.Name);  // Veli
Console.WriteLine(second.Name); // Veli

public class ReferencePerson
{
    public string Name { get; set; } = "";
}
```

Burada iki değişken de aynı `Person` nesnesini gösterir. Bu yüzden birinden
yapılan değişiklik diğerinden de görünür.

## İlk alıştırma

`Counter` örneğinin çıktısını tahmin etme.

Aşağıdaki kodun çıktısını tahmin et:

```csharp
var a = new Counter { Value = 5 };
var b = a;

b.Value++;

Console.WriteLine(a.Value);
Console.WriteLine(b.Value);

public class Counter
{
    public int Value { get; set; }
}
```
