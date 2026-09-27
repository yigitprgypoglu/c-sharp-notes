## object kullanım alanları
## 1. Her türü kabul eden metotlar


Bir metodun farklı türlerde veri almasını istiyorsan parametreyi `object` yapabilirsin:


C#






```
static void Print(object value)
{
    Console.WriteLine(value);
}

Print(10);
Print("Merhaba");
Print(true);

```





Bu metot `int`, `string` veya `bool` arasında ayrım yapmadan çalışır.


Fakat değerin türüne göre işlem yapmak istersen kontrol etmen gerekir:


C#






```
static void ShowType(object value)
{
    if (value is int number)
    {
        Console.WriteLine($"Sayı: {number}");
    }
    else if (value is string text)
    {
        Console.WriteLine($"Metin: {text}");
    }
}

```





## 2. Farklı türleri aynı koleksiyonda tutmak


C#






```
List<object> values = new List<object>
{
    10,
    "Ali",
    true
};

```





Burada farklı türlerdeki değerler aynı listede tutulabilir.


Ancak bu kullanımda tür güvenliği azalır:


C#






```
int number = (int)values[0];
string text = (string)values[1];

```





Yanlış tür dönüşümü yaparsan hata alırsın:


C#






```
int wrong = (int)values[1]; // InvalidCastException

```





Bu nedenle bütün elemanlar aynı türdeyse genellikle şu daha doğrudur:


C#






```
List<int> numbers = new List<int>();

```





## 3. `object` ile temel davranışlara erişmek


Her tür `object`'ten geldiği için `object` değişkeni üzerinden temel metotları kullanabilirsin:


C#






```
object value = 42;

Console.WriteLine(value.ToString());
Console.WriteLine(value.GetType());
Console.WriteLine(value.Equals(42));

```





Bu metotlar bütün nesnelerde ortak olarak bulunur.


## Boxing tam olarak neden kullanılır?


Boxing, bir **değer tipini** `object` veya interface gibi referans türü bekleyen bir yere verebilmek için kullanılır:


C#






```
int number = 10;

object value = number; // Boxing

```





`int` bir değer tipidir, `object` ise referans tipidir. C# bu durumda `10` değerinin bir kopyasını heap üzerinde nesne gibi saklar:


Plain text






```
number = 10

value ──> object içindeki int değeri: 10

```





Boxing sayesinde şu tür metotlara değer tipi gönderebilirsin:


C#






```
static void Print(object value)
{
    Console.WriteLine(value);
}

int number = 10;

Print(number); // int -> object, boxing

```





Metot `object` bekliyor; `int` ise değer tipi. Aradaki uyumu boxing sağlar.


## Interface'e atarken de boxing olabilir


C#






```
public interface IPrintable
{
    void Print();
}

public struct Report : IPrintable
{
    public void Print()
    {
        Console.WriteLine("Rapor");
    }
}

Report report = new Report();

IPrintable printable = report; // Boxing olabilir
printable.Print();

```





`Report` bir struct olduğu için interface değişkenine aktarılırken değer nesne olarak kutulanabilir.


## Boxing'i neden manuel olarak kullanırız?


Günümüzde çoğu zaman boxing'i özellikle istemeyiz. Genellikle şu sebeplerle **zorunlu olarak** oluşur:



- Eski `ArrayList` gibi `object` tabanlı koleksiyonlar

- `object` parametresi alan metotlar

- Değer tipini bir interface değişkenine atama

- Generic olmayan eski API'lerle çalışma

- Türün çalışma zamanında bilinmediği genel yapılar



Örneğin eski kullanım:


C#






```
ArrayList list = new ArrayList();

list.Add(10);       // Boxing
int number = (int)list[0]; // Unboxing

```





Modern ve daha iyi kullanım:


C#






```
List<int> list = new List<int>();

list.Add(10);       // Boxing yok
int number = list[0]; // Unboxing yok

```





`List<int>` doğrudan `int` ile çalıştığı için boxing maliyeti oluşmaz.


## Boxing neden her zaman tercih edilmez?


Çünkü:



1. Değerin kopyası oluşturulur.

2. Heap üzerinde ek nesne oluşabilir.

3. Sonradan unboxing için açık dönüşüm gerekir.

4. Yanlış tür dönüşümü çalışma zamanında hata verebilir.

5. Çok sık yapılırsa performans ve bellek maliyeti oluşturabilir.



Örneğin:


C#






```
object value = 10; // Boxing

int number = (int)value; // Unboxing

```





Burada `value` içindeki değeri `int` olarak kullanmak için unboxing gerekir:


C#






```
Console.WriteLine(number + 5); // 15

```





## Kısa özet


Plain text






```
object:
    Her türü tutabilen genel bir türdür.
    Farklı türleri kabul eden metotlarda ve yapılarda kullanılabilir.

Boxing:
    Değer tipini object/interface olarak kullanabilmek için yapılır.

Unboxing:
    Kutulanmış değeri tekrar kendi türünde kullanabilmek için yapılır.

```





Modern C# kodunda tercih genellikle şöyledir:


Plain text






```
Tür belliyse:
    int, string, Person veya List<int> kullan.

Tür farklı olabilir ve gerçekten genel bir yapı gerekiyorsa:
    object kullan.

Değer tipini object'e atamak zorunda kalırsan:
    boxing otomatik gerçekleşir.

```





Yani boxing çoğunlukla başlı başına kullanmak istediğimiz bir özellik değil, değer tipleri `object` veya interface gibi genel türlerle birlikte kullanılınca ortaya çıkan bir uyumluluk mekanizmasıdır.
