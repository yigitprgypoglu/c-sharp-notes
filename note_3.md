# Bölüm 3: Delegate, Event ve Expression


Bu bölümde C#’ta metotları **değer gibi taşımayı**, olaylara tepki vermeyi ve kodu çalışma zamanında temsil etmeyi öğreneceğiz.


## 1. Delegate nedir?


`delegate`, belirli bir metot imzasını temsil eden özel bir türdür.


C#






```
public delegate int Operation(int first, int second);

```





Bu delegate şu özelliklere sahip metotları kabul eder:



- İki adet `int` parametre

- `int` dönüş değeri



C#






```
static int Add(int first, int second)
{
    return first + second;
}

Operation operation = Add;

int result = operation(10, 5);

Console.WriteLine(result); // 15

```





Burada `operation`, `Add` metodunu referans olarak taşır.


Delegate’i doğrudan metoda parametre olarak da gönderebiliriz:


C#






```
static int Calculate(int first, int second, Operation operation)
{
    return operation(first, second);
}

int result = Calculate(10, 5, Add);

Console.WriteLine(result); // 15

```





Farklı bir işlem gönderebiliriz:


C#






```
static int Multiply(int first, int second)
{
    return first * second;
}

int result = Calculate(10, 5, Multiply);

Console.WriteLine(result); // 50

```





Bu yapı, davranışı metoda parametre olarak göndermemizi sağlar.



## 2. `Func`, `Action` ve `Predicate`


Çoğu durumda özel delegate tanımlamak yerine hazır delegate türlerini kullanırız.


### `Func`


`Func`, değer döndüren metotları temsil eder.


Son generic tür dönüş değeridir:


C#






```
Func<int, int, int> add = (first, second) => first + second;

Console.WriteLine(add(10, 5)); // 15

```





Bu tanım şu anlama gelir:


Plain text






```
Func<int, int, int>
       |    |   |
       |    |   dönüş tipi
       |    parametre
       ilk parametre

```





Başka bir örnek:


C#






```
Func<string, int> getLength = text => text.Length;

Console.WriteLine(getLength("CSharp")); // 6

```





### `Action`


`Action`, değer döndürmeyen metotları temsil eder:


C#






```
Action<string> print = message =>
{
    Console.WriteLine(message);
};

print("Merhaba");

```





Birden fazla parametre alabilir:


C#






```
Action<string, int> printUser = (name, age) =>
{
    Console.WriteLine($"{name} - {age}");
};

printUser("Ali", 30);

```





### `Predicate`


`Predicate<T>`, `bool` döndüren metotları temsil eder:


C#






```
Predicate<int> isEven = number => number % 2 == 0;

Console.WriteLine(isEven(10)); // True
Console.WriteLine(isEven(7));  // False

```





`Predicate<T>` aslında şu yapıya benzer:


C#






```
Func<int, bool>

```






## 3. Lambda ifadeleri


Lambda, kısa ve isimsiz bir metot yazma biçimidir:


C#






```
(int x, int y) => x + y

```





Kullanımı:


C#






```
Func<int, int, int> add = (x, y) => x + y;

```





Tek parametrede parantez isteğe bağlıdır:


C#






```
Func<int, bool> isPositive = number => number > 0;

```





Birden fazla satır gerektiğinde gövde kullanılır:


C#






```
Func<int, int> calculateSquare = number =>
{
    int result = number * number;
    return result;
};

```





Lambda ifadeleri LINQ’te çok sık kullanılır:


C#






```
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var evenNumbers = numbers.Where(number => number % 2 == 0);

foreach (var number in evenNumbers)
{
    Console.WriteLine(number);
}

```






## 4. Event nedir?


`event`, bir olay gerçekleştiğinde kayıtlı metotların çalıştırılmasını sağlar.


Örneğin bir sipariş oluşturulduğunda:



- E-posta gönderilebilir.

- Log yazılabilir.

- Stok azaltılabilir.



Önce bir sınıf oluşturalım:


C#






```
public class OrderService
{
    public event EventHandler? OrderCreated;

    public void CreateOrder()
    {
        Console.WriteLine("Sipariş oluşturuldu.");

        OrderCreated?.Invoke(this, EventArgs.Empty);
    }
}

```





Event’e metot bağlayalım:


C#






```
var orderService = new OrderService();

orderService.OrderCreated += OnOrderCreated;

orderService.CreateOrder();

static void OnOrderCreated(object? sender, EventArgs e)
{
    Console.WriteLine("Sipariş oluşturma olayı yakalandı.");
}

```





Çıktı:


Plain text






```
Sipariş oluşturuldu.
Sipariş oluşturma olayı yakalandı.

```





`?.Invoke`, event’e kayıtlı metot varsa olayı çalıştırır. Kayıtlı metot yoksa `NullReferenceException` oluşmasını engeller.



## 5. Event’ten ayrılma


Bir event’e `+=` ile bağlanırız:


C#






```
orderService.OrderCreated += OnOrderCreated;

```





Bağlantıyı kaldırmak için `-=` kullanılır:


C#






```
orderService.OrderCreated -= OnOrderCreated;

```





Bu özellikle uzun ömürlü nesnelerde önemlidir. Event aboneliği kaldırılmazsa nesneler gereğinden uzun süre bellekte tutulabilir.



## 6. Özel event argümanları


Event ile daha fazla veri göndermek için `EventArgs` sınıfından türeyen bir sınıf kullanabiliriz:


C#






```
public class OrderCreatedEventArgs : EventArgs
{
    public int OrderId { get; }
    public decimal Total { get; }

    public OrderCreatedEventArgs(int orderId, decimal total)
    {
        OrderId = orderId;
        Total = total;
    }
}

```





Servis:


C#






```
public class OrderService
{
    public event EventHandler<OrderCreatedEventArgs>? OrderCreated;

    public void CreateOrder(int orderId, decimal total)
    {
        Console.WriteLine("Sipariş oluşturuldu.");

        var eventArgs = new OrderCreatedEventArgs(orderId, total);

        OrderCreated?.Invoke(this, eventArgs);
    }
}

```





Kullanımı:


C#






```
var orderService = new OrderService();

orderService.OrderCreated += (sender, eventArgs) =>
{
    Console.WriteLine($"Sipariş no: {eventArgs.OrderId}");
    Console.WriteLine($"Toplam: {eventArgs.Total}");
};

orderService.CreateOrder(1001, 250.75m);

```






## 7. Expression nedir?


Normal bir lambda çalıştırılabilir:


C#






```
Func<int, bool> isPositive = number => number > 0;

```





Expression ise lambda ifadesini bir **ağaç yapısı** olarak temsil eder:


C#






```
using System.Linq.Expressions;

Expression<Func<int, bool>> expression =
    number => number > 0;

```





Bu ifade doğrudan çalıştırılabilir, ancak önce derlenmesi gerekir:


C#






```
Func<int, bool> compiledExpression = expression.Compile();

Console.WriteLine(compiledExpression(10)); // True

```





Expression’ın yapısını inceleyebiliriz:


C#






```
Console.WriteLine(expression);
// number => (number > 0)

```





Expression tree özellikle ORM araçlarında kullanılır:


C#






```
Expression<Func<Product, bool>> filter =
    product => product.Price > 100;

```





Entity Framework gibi araçlar bu ifadeyi SQL’e çevirebilir:


SQL






```
WHERE Price > 100

```





Bu nedenle `IQueryable<T>` ile kullanılan sorgular sadece C# kodu olarak çalışmayabilir; veri kaynağı tarafından farklı bir dile çevrilebilir.



## Delegate ve Expression farkı

































| Özellik | Delegate | Expression |
| --- | --- | --- |
| Lambda’yı çalıştırır | Evet | `Compile()` sonrası |
| Kod yapısını inceletir | Hayır | Evet |
| SQL’e çevrilebilir | Genellikle hayır | Evet, sağlayıcı destekliyorsa |
| Kullanım alanı | Uygulama içi davranış | Sorgu ve metaprogramlama |



Örnek:


C#






```
Func<Product, bool> funcFilter =
    product => product.Price > 100;

Expression<Func<Product, bool>> expressionFilter =
    product => product.Price > 100;

```





`Func` doğrudan .NET içinde çalışır. `Expression`, ifadenin ne yaptığını temsil eder.



## Tam örnek: Delegate ve event


C#






```
using System;

public class NotificationService
{
    public event EventHandler<string>? MessageReceived;

    public void Send(string message)
    {
        Console.WriteLine($"Mesaj gönderiliyor: {message}");

        MessageReceived?.Invoke(this, message);
    }
}

var notificationService = new NotificationService();

notificationService.MessageReceived += (sender, message) =>
{
    Console.WriteLine($"Bildirim alındı: {message}");
};

notificationService.Send("Yeni mesajınız var.");

```





## Özet



- `delegate`, metotları değer gibi taşır.

- `Func`, değer döndüren delegate’tir.

- `Action`, değer döndürmeyen delegate’tir.

- `Predicate`, `bool` döndüren delegate’tir.

- `event`, olaylara abone olmayı sağlar.

- `Expression`, lambda ifadesinin yapısını temsil eder.

- Delegate davranışı çalıştırır; expression ise davranışı analiz edilebilir hale getirir.



## Alıştırma


Aşağıdaki metodu yaz:


C#






```
static int Calculate(
    int first,
    int second,
    Func<int, int, int> operation)

```





Şu işlemleri destekle:


C#






```
Calculate(10, 5, (x, y) => x + y); // 15
Calculate(10, 5, (x, y) => x - y); // 5
Calculate(10, 5, (x, y) => x * y); // 50

```





Ayrıca `OrderService` sınıfına `OrderCancelled` isminde ikinci bir event ekleyip, iptal edilen siparişin numarasını event argümanı ile göndermeyi dene.
