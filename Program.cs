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
    var first = new Person { Name = "Ali" };
    var second = first;

    second.Name = "Veli";

    Console.WriteLine(first.Name);  // Veli
    Console.WriteLine(second.Name); // Veli
}

// Burada iki değişken de aynı Person nesnesini gösterir.
// Bu yüzden birinden yapılan değişiklik diğerinden de görünür.
public class Person
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
