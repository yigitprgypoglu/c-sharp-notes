# Bölüm 3.1 — Delegate, `Func`, `Action` ve Lambda İfadeleri

## Metotları değer gibi taşıma

- `delegate` nedir, nasıl tanımlanır?
- Metotları değişkenlere atama
- Delegate'i metoda parametre olarak gönderme
- `Func`, `Action` ve `Predicate`
- Lambda ifadeleri ve anonim fonksiyonlar
- LINQ içindeki `Where()` kullanımı

**Hedef:** Metotları değer gibi taşıyabilmek ve davranışı başka metotlara
parametre olarak gönderebilmek.

## Delegate nedir?

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

Delegate'i doğrudan metoda parametre olarak da gönderebiliriz:

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

## `Func`, `Action` ve `Predicate`

Çoğu durumda özel delegate tanımlamak yerine hazır delegate türlerini
kullanırız.

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

## Lambda ifadeleri

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

Lambda ifadeleri LINQ'te çok sık kullanılır:

```csharp
var numbers = new[] { 1, 2, 3, 4, 5, 6 };

var evenNumbers = numbers.Where(number => number % 2 == 0);

foreach (var number in evenNumbers)
{
    Console.WriteLine(number);
}
```

`.Where(...)`, C#'ın hazır filtreleme metodudur. Tanımına baktığında içine
parametre olarak tam olarak az önce öğrendiğin şeyi alır:

```csharp
Func<int, bool> predicate
```

Yani `.Where()`, "Bana her bir eleman için `true` ya da `false` dönecek bir
kural ver" der.

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
