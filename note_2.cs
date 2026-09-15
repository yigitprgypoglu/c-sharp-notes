// Bölüm 2

// Bölüm 2: Boxing, Nullable Reference Types ve ref / in / out

// 1. Boxing ve Unboxing
//
// Boxing, bir value type değerinin object veya interface türüne
// dönüştürülmesidir. Değer heap üzerinde bir nesneye sarılır.
int number = 42;

object boxed = number; // Boxing

Console.WriteLine(boxed);

// Burada number stack üzerinde bir int değeridir.
// object bir reference type olduğu için C# değeri heap üzerinde nesneye dönüştürür.

// Unboxing, tekrar value type'a dönüştürme işlemidir:
object boxedNumber = 42;

int unboxedNumber = (int)boxedNumber; // Unboxing

Console.WriteLine(unboxedNumber);

// Tür yanlışsa hata oluşur:
object value = 42;

// InvalidCastException oluşur
string text = (string)value;

// Güvenli dönüşüm için pattern matching kullanılabilir:
object matchedValue = 42;

if (matchedValue is int matchedNumber)
{
    Console.WriteLine($"Sayı: {matchedNumber}");
}

// Boxing performans maliyeti oluşturabilir:
int total = 0;

for (int i = 0; i < 1_000_000; i++)
{
    object boxedValue = i;
    total += (int)boxedValue;
}

// Performans kritik kodlarda gereksiz boxing'den kaçınmak için
// generic türler tercih edilir:
static T ReturnValue<T>(T value)
{
    return value;
}

int result = ReturnValue(42);

// 2. Nullable Value Types
//
// Value type türler normalde null alamaz:
// int age = null; // Derleme hatası

// Nullable value type ile int değerinin boş olmasına izin verebiliriz:
int? age = null;

if (age.HasValue)
{
    Console.WriteLine(age.Value);
}

// Daha güvenli ve kısa kullanım:
int? nullableAge = null;

Console.WriteLine(nullableAge ?? 0);

// ?? operatörü, değer null ise varsayılan değeri kullanır.
int? score = null;

int finalScore = score ?? -1;
Console.WriteLine(finalScore); // -1

// Null koşullu erişim için ?. kullanılabilir:
int? conditionalAge = 25;

Console.WriteLine(conditionalAge?.ToString());

// 3. Nullable Reference Types
//
// C#'ta reference type'lar varsayılan olarak null olabilir:
// string name = null;

// Nullable reference types aktifse derleyici uyarı verir:
#nullable enable

// string name = null;   // Uyarı
string? surname = null;

// string?, değişkenin null olabileceğini açıkça belirtir:
string? nullableName = null;

if (nullableName is not null)
{
    Console.WriteLine(nullableName.Length);
}

// Null kontrolünden sonra derleyici name değerinin boş olmadığını anlayabilir.
static void PrintLength(string? text)
{
    if (text is null)
    {
        return;
    }

    Console.WriteLine(text.Length);
}

// Null-forgiving operator !, derleyiciye değerin null olmadığını söyler:
string? nameFromMethod = GetName();

Console.WriteLine(nameFromMethod!.Length);

// Ancak ! runtime kontrolü yapmaz.
// Değer gerçekten null ise hata oluşur.
// Bu nedenle yalnızca değerin kesinlikle null olmadığını biliyorsan kullanmalısın.

// 4. ref Parametresi
//
// Normal parametrelerde value type değerinin kopyası gönderilir:
static void Increase(int number)
{
    number++;
}

int valueCopy = 10;

Increase(valueCopy);

Console.WriteLine(valueCopy); // 10

// ref kullanılırsa metodun orijinal değişkeni değiştirmesine izin verilir:
static void IncreaseByReference(ref int number)
{
    number++;
}

int referenceValue = 10;

IncreaseByReference(ref referenceValue);

Console.WriteLine(referenceValue); // 11

// Hem metot tanımında hem çağrıda ref yazılmalıdır.
static void Swap(ref int first, ref int second)
{
    (first, second) = (second, first);
}

int a = 10;
int b = 20;

Swap(ref a, ref b);

Console.WriteLine(a); // 20
Console.WriteLine(b); // 10

// 5. out Parametresi
//
// out, metodun bir değer üretip çağıran tarafa göndermesini sağlar.
static bool TryParseNumber(string text, out int number)
{
    return int.TryParse(text, out number);
}

if (TryParseNumber("123", out int parsedResult))
{
    Console.WriteLine(parsedResult);
}

// out parametresinin metot içinde mutlaka atanması gerekir:
static void CreateValue(out int value)
{
    value = 42;
}

// Modern C#'ta TryParse deseni çok yaygındır:
if (int.TryParse("250", out int parsedNumber))
{
    Console.WriteLine($"Başarılı: {parsedNumber}");
}
else
{
    Console.WriteLine("Geçersiz sayı");
}

// out kullanırken değişkeni önceden tanımlamak zorunda değilsin:
if (DateTime.TryParse("2026-09-11", out DateTime date))
{
    Console.WriteLine(date);
}

// 6. in Parametresi
//
// in, bir value type değerini kopyalamadan, yalnızca okunabilir şekilde
// metoda gönderir.
static void PrintPoint(in Point point)
{
    Console.WriteLine($"X: {point.X}, Y: {point.Y}");
}

// Çağırırken genellikle in yazmak isteğe bağlıdır:
var point = new Point(10, 20);

PrintPoint(point);

// in parametresi metot içinde değiştirilemez:
static void ChangePoint(in Point point)
{
    // point.X = 50; // Derleme hatası
}

// ref, out ve in karşılaştırması:
//
// | Anahtar kelime | Amaç | Metot içinde değiştirilebilir mi? | Çağrıdan önce değer gerekir mi? |
// | --- | --- | --- | --- |
// | ref | Orijinal değeri değiştirmek | Evet | Evet |
// | out | Metottan değer döndürmek | Evet | Hayır |
// | in | Kopyalamadan salt okunur göndermek | Hayır | Evet |

// Mini alıştırma
// Aşağıdaki kodun çıktısı ne olur?
static void Update(ref int value)
{
    value *= 2;
}

static void Create(out int value)
{
    value = 10;
}

int exerciseNumber = 5;

Update(ref exerciseNumber);
Create(out exerciseNumber);

Console.WriteLine(exerciseNumber);

// Ardından şu metodu yaz:
static bool TryDivide(
    int dividend,
    int divisor,
    out double result)
{
    if (divisor == 0)
    {
        result = 0;
        return false;
    }

    result = (double)dividend / divisor;
    return true;
}

static string? GetName()
{
    return "Ali";
}

// Büyük struct değerlerinde gereksiz kopyalamayı önlemek için kullanılabilir.
public readonly record struct Point(int X, int Y);
