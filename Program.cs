// Bölüm 1: C# Tip Sistemi
//
// C#'ta değişkenler temel olarak iki kategoriye ayrılır:
//
// - Value type: Değerin kendisini taşır.
// - Reference type: Nesnenin bellekteki adresine referans taşır.

// Value type örneği
static void ValueTypeExample()
{
    int first = 10;
    int second = first;

    second = 20;

    Console.WriteLine(first);  // 10
    Console.WriteLine(second); // 20
}

// second, first değişkeninin değerinin kopyasını alır.
// Daha sonra second değişince first etkilenmez.

// Reference type örneği
static void ReferenceTypeExample()
{
    var first = new ReferencePerson { Name = "Ali" };
    var second = first;

    second.Name = "Veli";

    Console.WriteLine(first.Name);  // Veli
    Console.WriteLine(second.Name); // Veli
}

// Burada iki değişken de aynı Person nesnesini gösterir.
// Bu yüzden birinden yapılan değişiklik diğerinden de görünür.
public class ReferencePerson
{
    public string Name { get; set; } = "";
}

// struct ve class farkı
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

// struct genellikle küçük ve değişmez değerleri temsil etmek için kullanılır.
// class kimliği olan, daha karmaşık veya yaşam süresi yönetilen nesneler için kullanılır.

// struct kopyalama örneği
static void StructCopyExample()
{
    var point1 = new Point { X = 1, Y = 2 };
    var point2 = point1;

    point2.X = 99;

    Console.WriteLine(point1.X); // 1
    Console.WriteLine(point2.X); // 99
}

// struct kopyalandığı için point1 değişmez.

// Bir struct:
// - Başka bir class'tan kalıtım alamaz.
// - Başka bir struct'tan kalıtım alamaz.
// - Base class olarak kullanılamaz.
// - Ancak interface uygulayabilir.

// class:
// - object'ten gelir.
// - Başka class'lardan kalıtım alabilir.
// - Interface uygulayabilir.
//
// struct:
// - ValueType üzerinden object'ten gelir.
// - Başka class veya struct'tan kalıtım alamaz.
// - Interface uygulayabilir.

// record ile değer eşitliği
//
// Normal sınıflarda varsayılan eşitlik çoğunlukla referans karşılaştırmasıdır:
var person1 = new Person("Ali");
var person2 = new Person("Ali");

Console.WriteLine(person1 == person2); // False

public class Person
{
    public Person(string name) => Name = name; //constructor
    /*
    public Person(string name)
    {
        Name = name;  -> constructorun kisaltilmamis hali.  
    }
    */
    public string Name { get; }
}

// record ise içerik eşitliği sağlar:
var personRecord1 = new PersonRecord("Ali");
var personRecord2 = new PersonRecord("Ali");

Console.WriteLine(personRecord1 == personRecord2); // True

public record PersonRecord(string Name);

// record, özellikle DTO, immutable model ve veri taşıma nesnelerinde kullanışlıdır.

// record struct
// Değer tipinin kopyalama davranışını ve record'un içerik eşitliğini birleştirir:
public readonly record struct Money(decimal Amount, string Currency); 
//Bu satır, `Money` adında bir **readonly record struct** tanımlar. İçinde iki property otomatik olarak oluşturulur:
var money1 = new Money(100, "TRY");
var money2 = new Money(100, "TRY");

Console.WriteLine(money1 == money2); // True

// Önemli kural:
// Bir türün class, struct veya record olması; performans, eşitlik,
// mutability ve API tasarımı üzerinde doğrudan etkilidir.
//
// Genel yaklaşım:
// - Küçük, basit ve mümkünse immutable değerler: readonly struct
// - Veri modelleri ve immutable DTO'lar: record
// - Davranış ve kimlik taşıyan nesneler: class
// - Değer eşitliği gereken küçük nesneler: record struct

// İlk alıştırma
// Aşağıdaki kodun çıktısını tahmin et:
var a = new Counter { Value = 5 };
var b = a;

b.Value++;

Console.WriteLine(a.Value);
Console.WriteLine(b.Value);

public class Counter
{
    public int Value { get; set; }
}

// Product modelini record kullanarak yeniden yazma alıştırması:
public class Product
{
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
}

// Record olarak yeniden yazılmış hali:
public record ProductRecord(string Name, decimal Price);
