# Bölüm 1.3 — `class`, `record` ve Değer Eşitliği

## Nesneleri karşılaştırma

- Normal sınıflarda varsayılan eşitlik
- `record` ile içerik eşitliği
- `==`, `Equals()` ve `GetHashCode()`
- `init` ve değişmezlik (immutability)
- DTO ve veri modellerinde `record` kullanımı

**Hedef:** Referans eşitliği ile içerik eşitliği arasındaki farkı anlayabilmek.

## `record` ile değer eşitliği

Normal sınıflarda varsayılan eşitlik çoğunlukla referans karşılaştırmasıdır:

```csharp
var person1 = new Person("Ali");
var person2 = new Person("Ali");

Console.WriteLine(person1 == person2); // False

public class Person
{
    public Person(string name) => Name = name;

    /*
    public Person(string name)
    {
        Name = name;
    }
    */

    public string Name { get; }
}
```

`record` ise içerik eşitliği sağlar:

```csharp
var personRecord1 = new PersonRecord("Ali");
var personRecord2 = new PersonRecord("Ali");

Console.WriteLine(personRecord1 == personRecord2); // True

public record PersonRecord(string Name);

/*
public record PersonRecord
{
    public string Name { get; init; }

    public PersonRecord(string name)
    {
        Name = name;
    }
}
*/
```

`record`, özellikle DTO, immutable model ve veri taşıma nesnelerinde
kullanışlıdır. `record`, varsayılan olarak nesnelerin referanslarını değil,
içerdikleri değerleri karşılaştırır.

Alıştırma: İki `Person` nesnesinin neden farklı, iki `PersonRecord` nesnesinin
neden eşit olduğunu açıklama.
