# C# Tekrar Problemleri — Problem 16–27


## Problem 16 — `Func`, `Action` ve `Predicate`


Aşağıdaki kodun çıktısını tahmin et:


```csharp
Func<int, int, int> add = (x, y) => x + y;

Action<string> print = message =>
{
    Console.WriteLine(message);
};

Predicate<int> isEven = number => number % 2 == 0;

int result = add(10, 5);

print($"Sonuç: {result}");
Console.WriteLine(isEven(result));
Console.WriteLine(isEven(8));

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?

Nedeni:
- Func:
- Action:
- Predicate:

```





Açıklamanda şunlara değin:



- `Func<int, int, int>` ne anlama gelir?

- `Action<string>` ne anlama gelir?

- `Predicate<int>` ne anlama gelir?

- `Func` ile `Action` arasındaki fark nedir?




## Problem 17 — Delegate ile Hesaplama


Aşağıdaki kodun çıktısını tahmin et:


```csharp
int result1 = Calculate(10, 5, (x, y) => x + y);
int result2 = Calculate(10, 5, (x, y) => x - y);
int result3 = Calculate(10, 5, (x, y) => x * y);

Console.WriteLine(result1);
Console.WriteLine(result2);
Console.WriteLine(result3);

static int Calculate(
    int first,
    int second,
    Func<int, int, int> operation)
{
    return operation(first, second);
}

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `operation` parametresi nedir?

- Lambda ifadeleri `Calculate` metoduna nasıl gönderiliyor?

- `Calculate` metodu işlemi nasıl çalıştırıyor?




## Problem 18 — Aynı Delegate’i Farklı Metotlara Bağlama


Aşağıdaki kodun çıktısını tahmin et:


```csharp
Func<int, int, int> operation = Add;

Console.WriteLine(operation(8, 2));

operation = Multiply;

Console.WriteLine(operation(8, 2));

static int Add(int first, int second)
{
    return first + second;
}

static int Multiply(int first, int second)
{
    return first * second;
}

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `operation` ilk olarak hangi metodu gösteriyor?

- `operation = Multiply` satırından sonra ne değişiyor?

- Aynı delegate değişkeni farklı metotları gösterebilir mi?




## Problem 19 — `Action`


Aşağıdaki kodun çıktısını tahmin et:


```csharp
Action<string> printMessage = message =>
{
    Console.WriteLine($"Mesaj: {message}");
};

printMessage("Merhaba");

Action<string, int> printUser = (name, age) =>
{
    Console.WriteLine($"{name} - {age}");
};

printUser("Ali", 30);

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?

Nedeni:
- Action nedir?
- `Action<string>` ne anlama gelir?
- `Action<string, int>` ne anlama gelir?
- Func ile arasındaki fark nedir?

```






## Problem 20 — `Predicate`


Aşağıdaki kodun çıktısını tahmin et:


```csharp
Predicate<int> isPositive = number => number > 0;
Predicate<int> isEven = number => number % 2 == 0;

Console.WriteLine(isPositive(5));
Console.WriteLine(isPositive(-2));

Console.WriteLine(isEven(8));
Console.WriteLine(isEven(7));

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?
4. çıktı: ?

Nedeni:
- Predicate nedir?
- isPositive neyi kontrol ediyor?
- isEven neyi kontrol ediyor?

```






## Problem 21 — Temel Event Kullanımı


Aşağıdaki kodun çıktısını tahmin et:


```csharp
var alarm = new Alarm();

alarm.Triggered += OnAlarmTriggered;

alarm.Trigger();

static void OnAlarmTriggered(object? sender, EventArgs e)
{
    Console.WriteLine("Alarm olayı yakalandı.");
}

public class Alarm
{
    public event EventHandler? Triggered;

    public void Trigger()
    {
        Console.WriteLine("Alarm çalıştı.");

        Triggered?.Invoke(this, EventArgs.Empty);
    }
}

```





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?

Nedeni:
- `+=` ne yapıyor?
- `Triggered?.Invoke(...)` ne zaman çalışıyor?
- `EventArgs.Empty` ne anlama geliyor?

```






## Problem 22 — Event ve Özel `EventArgs`


Aşağıdaki kodun çıktısını tahmin et:


```csharp
var orderService = new OrderService();

orderService.OrderCreated += OnOrderCreated;

orderService.CreateOrder(1001, 250.75m);

static void OnOrderCreated(
    object? sender,
    OrderCreatedEventArgs e)
{
    Console.WriteLine($"Sipariş numarası: {e.OrderId}");
    Console.WriteLine($"Toplam tutar: {e.Total}");
}

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





Cevap formatı:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?

Nedeni:
- `OrderCreated` event'i ne zaman tetikleniyor?
- `OrderCreatedEventArgs` ne amaçla kullanılıyor?
- `e.OrderId` ve `e.Total` nereden geliyor?

```






## Problem 23 — `EventHandler` ve `EventHandler<T>`


Aşağıdaki kodu incele:


```csharp
var alarm = new Alarm();

alarm.Triggered += (sender, e) =>
{
    Console.WriteLine("Alarm tetiklendi.");
};

alarm.Trigger();

var orderService = new OrderService();

orderService.OrderCreated += (sender, e) =>
{
    Console.WriteLine($"Sipariş: {e.OrderId}, Tutar: {e.Total}");
};

orderService.CreateOrder(1001, 250.75m);

public class Alarm
{
    public event EventHandler? Triggered;

    public void Trigger()
    {
        Triggered?.Invoke(this, EventArgs.Empty);
    }
}

public class OrderService
{
    public event EventHandler<OrderCreatedEventArgs>? OrderCreated;

    public void CreateOrder(int id, decimal total)
    {
        var args = new OrderCreatedEventArgs(id, total);

        OrderCreated?.Invoke(this, args);
    }
}

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





Cevapla:


```text
1. Çıktı nedir?
2. Alarm sınıfında neden `EventHandler` kullanıldı?
3. OrderService sınıfında neden `EventHandler<OrderCreatedEventArgs>` kullanıldı?
4. `EventArgs.Empty` ne demek?
5. `?` neden var?

```






## Problem 24 — Delegate ve Expression


Aşağıdaki kodu incele:


```csharp
using System;
using System.Linq.Expressions;

Func<int, bool> func = number => number > 10;

Expression<Func<int, bool>> expression =
    number => number > 10;

Console.WriteLine(func(15));

var compiledExpression = expression.Compile();

Console.WriteLine(compiledExpression(15));
Console.WriteLine(compiledExpression(5));

Console.WriteLine(expression);

```





Önce çıktıları tahmin et:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?
4. çıktı: ?

```





Sonra şu soruları cevapla:


```text
1. `func` nedir?
2. `expression` nedir?
3. `expression.Compile()` ne yapar?
4. Delegate ile Expression arasındaki temel fark nedir?

```





İpucu:



- `func(15)` doğrudan çalıştırılabilir.

- `expression`, lambda ifadesinin yapısını temsil eder.

- `expression.Compile()` expression ağacını çalıştırılabilir delegate’e dönüştürür.

- Son satırda expression’ın temsil ettiği lambda yazdırılır.




## Problem 25 — Delegate mi, Expression mı?


Aşağıdaki kodu incele:


```csharp
using System;
using System.Linq.Expressions;

Func<int, bool> func = number => number % 2 == 0;

Expression<Func<int, bool>> expression =
    number => number % 2 == 0;

Console.WriteLine(func(4));

var compiledExpression = expression.Compile();

Console.WriteLine(compiledExpression(4));
Console.WriteLine(compiledExpression(7));

Console.WriteLine(expression);

```





Önce çıktıları tahmin et:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?
4. çıktı: ?

```





Sonra şunları açıklamayı dene:


```text
- `func(4)` hangi işlemi yapıyor?
- `compiledExpression(7)` neden True veya False döndürüyor?
- `Console.WriteLine(expression)` neden expression'ı çalıştırmak yerine lambda yapısını yazdırıyor?
- `func` ile `expression` aynı lambda mantığını mı temsil ediyor?

```






## Problem 26 — Expression Ağacını İnceleme


Aşağıdaki kodu incele:


```csharp
using System;
using System.Linq.Expressions;

Expression<Func<int, bool>> expression =
    number => number > 10;

var body = expression.Body;
var parameter = expression.Parameters[0];

Console.WriteLine(parameter.Name);
Console.WriteLine(body.NodeType);
Console.WriteLine(body);

var compiled = expression.Compile();

Console.WriteLine(compiled(15));
Console.WriteLine(compiled(5));

```





Çıktıları tahmin et:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?
4. çıktı: ?
5. çıktı: ?

```





İpucu:



- `expression.Parameters[0]`, lambda’daki `number` parametresidir.

- `body`, lambda’nın gövdesidir: `number > 10`.

- `body.NodeType`, yapılan işlemin türünü belirtir.

- `GreaterThan`, `>` karşılaştırma işlemidir.




## Problem 27 — Delegate ve Expression Karşılaştırması


Aşağıdaki kodu incele:


```csharp
using System;
using System.Linq.Expressions;

Func<int, bool> isAdult = age => age >= 18;

Expression<Func<int, bool>> adultExpression =
    age => age >= 18;

Console.WriteLine(isAdult(20));
Console.WriteLine(isAdult(15));

var compiledAdultExpression = adultExpression.Compile();

Console.WriteLine(compiledAdultExpression(20));
Console.WriteLine(compiledAdultExpression(15));

Console.WriteLine(adultExpression);

```





Önce çıktıları tahmin et:


```text
1. çıktı: ?
2. çıktı: ?
3. çıktı: ?
4. çıktı: ?
5. çıktı: ?

```





Sonra açıklamaya çalış:


```text
1. `isAdult(20)` hangi işlemi yapıyor?
2. `adultExpression` neden doğrudan çağrılmıyor?
3. `compiledAdultExpression` nasıl oluşturuldu?
4. `compiledAdultExpression(15)` neden False döndürüyor?
5. Son satır neden True veya False değil, lambda yapısını yazdırıyor?

```






## Konu Özeti


### `Func`


Değer döndüren delegate türüdür:


```csharp
Func<int, int, int> add = (x, y) => x + y;

```





İki `int` alır ve bir `int` döndürür.


### `Action`


Değer döndürmeyen delegate türüdür:


```csharp
Action<string> print = message =>
{
    Console.WriteLine(message);
};

```





Bir `string` alır, işlem yapar fakat değer döndürmez.


### `Predicate`


`bool` döndüren delegate türüdür:


```csharp
Predicate<int> isEven = number => number % 2 == 0;

```





Bir `int` alır ve `True` veya `False` döndürür.


### Event


Bir olay gerçekleştiğinde abone olan metotları çalıştırır:


```csharp
public event EventHandler? Triggered;

```





Event’i tetiklemek için:


```csharp
Triggered?.Invoke(this, EventArgs.Empty);

```





### `EventHandler<T>`


Event ile özel veri taşımak için kullanılır:


```csharp
public event EventHandler<OrderCreatedEventArgs>? OrderCreated;

```





### Delegate ve Expression farkı


```text
Delegate    -> Davranışı çalıştırır.
Expression  -> Davranışın yapısını temsil eder.

```





Expression’ı çalıştırılabilir hale getirmek için:


```csharp
var compiled = expression.Compile();
```
