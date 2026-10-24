# Bölüm 3 — Nullable Value Types ve Nullable Reference Types

## Null değerlerle güvenli çalışma

- `int?` ve `Nullable<T>`
- `HasValue`, `Value` ve `??`
- `?.` null koşullu operatörü
- Nullable Reference Types ve `#nullable enable`
- `string` ile `string?` farkı
- Null kontrolü ve null-forgiving operator `!`

**Hedef:** Null gelebilen değer ve referans türleriyle güvenli çalışabilmek.

## Nullable Value Types

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

## Nullable Reference Types

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
