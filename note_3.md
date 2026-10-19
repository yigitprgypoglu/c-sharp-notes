# Bölüm 3: Delegate, Event ve Expression

Bu bölümde C#’ta metotları **değer gibi taşımayı**, olaylara tepki vermeyi ve kodu çalışma zamanında temsil etmeyi öğreneceğiz.

## 1. Delegate nedir?

`delegate`, belirli bir metot imzasını temsil eden özel bir türdür.

```csharp
public delegate int Operation(int first, int second);

```

Bu delegate şu özelliklere sahip metotları kabul eder:

- İki adet `int` parametre

- `int` dönüş değeri

```csharp
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

```csharp
static int Calculate(int first, int second, Operation operation)
{
    return operation(first, second);
}

int result = Calculate(10, 5, Add);

Console.WriteLine(result); // 15

```

Farklı bir işlem gönderebiliriz:

```csharp
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

```csharp
Func<int, int, int> add = (first, second) => first + second;

Console.WriteLine(add(10, 5)); // 15

```

Bu tanım şu anlama gelir:

```text
Func<int, int, int>
       |    |   |
       |    |   dönüş tipi
       |    parametre
       ilk parametre

```

Başka bir örnek:

```csharp
Func<string, int> getLength = text => text.Length;

Console.WriteLine(getLength("CSharp")); // 6

```

### `Action`

`Action`, değer döndürmeyen metotları temsil eder:

```csharp
Action<string> print = message =>
{
    Console.WriteLine(message);
};

print("Merhaba");

```

Birden fazla parametre alabilir:

```csharp
Action<string, int> printUser = (name, age) =>
{
    Console.WriteLine($"{name} - {age}");
};

printUser("Ali", 30);

```

### `Predicate`

`Predicate<T>`, `bool` döndüren metotları temsil eder:

```csharp
Predicate<int> isEven = number => number % 2 == 0;

Console.WriteLine(isEven(10)); // True
Console.WriteLine(isEven(7));  // False

```

`Predicate<T>` aslında şu yapıya benzer:

```csharp
Func<int, bool>

```

## 3. Lambda ifadeleri

Lambda, kısa ve isimsiz bir metot yazma biçimidir:

```csharp
(int x, int y) => x + y

```

Kullanımı:

```csharp
Func<int, int, int> add = (x, y) => x + y;

```

Tek parametrede parantez isteğe bağlıdır:

```csharp
Func<int, bool> isPositive = number => number > 0;

```

Birden fazla satır gerektiğinde gövde kullanılır:

```csharp
Func<int, int> calculateSquare = number =>
{
    int result = number * number;
    return result;
};

```

Lambda ifadeleri LINQ’te çok sık kullanılır:

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var evenNumbers = numbers.Where(number => number % 2 == 0);

foreach (var number in evenNumbers)
{
    Console.WriteLine(number);
}

```

.Where(...): C#'ın hazır filtreleme metodudur. Tanımına baktığında içine parametre olarak tam olarak az önce öğrendiğin şeyi alır:

```csharp
Func<int, bool> predicate

```
Yani .Where(), "Bana her bir eleman için true ya da false dönecek bir kural ver" der.

## 4. Event nedir?

`event`, bir olay gerçekleştiğinde kayıtlı metotların çalıştırılmasını sağlar.

Örneğin bir sipariş oluşturulduğunda:

- E-posta gönderilebilir.

- Log yazılabilir.

- Stok azaltılabilir.

Önce bir sınıf oluşturalım:

```csharp
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
EventHandler: .NET ile hazır gelen standart bir delegedir (delegate). Tanımı gereği iki parametre taşır: (object sender, EventArgs e).

? (Nullable): Bu olaya henüz kimse abone olmamış olabilir (yani içi null olabilir) uyarısını susturur ve güvenli hale getirir.

Event’e metot bağlayalım:

```csharp
var orderService = new OrderService();

orderService.OrderCreated += OnOrderCreated;

orderService.CreateOrder();

static void OnOrderCreated(object? sender, EventArgs e)
{
    Console.WriteLine("Sipariş oluşturma olayı yakalandı.");
}

```

Çıktı:

```text
Sipariş oluşturuldu.
Sipariş oluşturma olayı yakalandı.

```

`?.Invoke`, event’e kayıtlı metot varsa olayı çalıştırır. Kayıtlı metot yoksa `NullReferenceException` oluşmasını engeller.

## 5. Event’ten ayrılma

Bir event’e `+=` ile bağlanırız:

```csharp
orderService.OrderCreated += OnOrderCreated;

```

Bağlantıyı kaldırmak için `-=` kullanılır:

```csharp
orderService.OrderCreated -= OnOrderCreated;

```

Bu özellikle uzun ömürlü nesnelerde önemlidir. Event aboneliği kaldırılmazsa nesneler gereğinden uzun süre bellekte tutulabilir.

## 6. Özel event argümanları

Event ile daha fazla veri göndermek için `EventArgs` sınıfından türeyen bir sınıf kullanabiliriz:

```csharp
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

```csharp
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

```csharp
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

```csharp
Func<int, bool> isPositive = number => number > 0;

```

Expression ise lambda ifadesini bir **ağaç yapısı** olarak temsil eder:

```csharp
using System.Linq.Expressions;

Expression<Func<int, bool>> expression =
    number => number > 0;

```

Bu ifade doğrudan çalıştırılabilir, ancak önce derlenmesi gerekir:

```csharp
Func<int, bool> compiledExpression = expression.Compile();

Console.WriteLine(compiledExpression(10)); // True

```

Expression’ın yapısını inceleyebiliriz:

```csharp
Console.WriteLine(expression);
// number => (number > 0)

```

Expression tree özellikle ORM araçlarında kullanılır:

```csharp
Expression<Func<Product, bool>> filter =
    product => product.Price > 100;

```

Entity Framework gibi araçlar bu ifadeyi SQL’e çevirebilir:

```sql
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

```csharp
Func<Product, bool> funcFilter =
    product => product.Price > 100;

Expression<Func<Product, bool>> expressionFilter =
    product => product.Price > 100;

```

`Func` doğrudan .NET içinde çalışır. `Expression`, ifadenin ne yaptığını temsil eder.

## Tam örnek: Delegate ve event

```csharp
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

```csharp
static int Calculate(
    int first,
    int second,
    Func<int, int, int> operation)

```

Şu işlemleri destekle:

```csharp
Calculate(10, 5, (x, y) => x + y); // 15
Calculate(10, 5, (x, y) => x - y); // 5
Calculate(10, 5, (x, y) => x * y); // 50

```

Ayrıca `OrderService` sınıfına `OrderCancelled` isminde ikinci bir event ekleyip, iptal edilen siparişin numarasını event argümanı ile göndermeyi dene.
