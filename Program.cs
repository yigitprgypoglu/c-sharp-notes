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
