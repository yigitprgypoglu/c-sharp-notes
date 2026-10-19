
# C# Tekrar Problemleri — Bölüm 1


## Problem 1 — Value Type ve Reference Type


Aşağıdaki kodun çıktısını tahmin et:


C#






```
var firstNumber = 10;
var secondNumber = firstNumber;

secondNumber = 20;

Console.WriteLine(firstNumber);
Console.WriteLine(secondNumber);

var firstPerson = new Person
{
    Name = "Ali"
};

var secondPerson = firstPerson;

secondPerson.Name = "Veli";

Console.WriteLine(firstPerson.Name);
Console.WriteLine(secondPerson.Name);

public class Person
{
    public string Name { get; set; } = "";
}

```





Cevabını şu formatta yaz:


Plain text






```
1. satır: ?
2. satır: ?
3. satır: ?
4. satır: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `int` değişkenlerinde neden iki farklı sonuç oluşuyor?

- `Person` nesnesinde neden iki değişken de aynı ismi gösteriyor?

- Value type ve reference type arasındaki temel fark nedir?




## Problem 2 — `struct` ve `record`


Aşağıdaki kodun çıktısını tahmin et:


C#






```
var point1 = new Point { X = 10 };
var point2 = point1;

point2.X = 20;

Console.WriteLine(point1.X);
Console.WriteLine(point2.X);

var product1 = new Product("Laptop", 1000);
var product2 = new Product("Laptop", 1000);

Console.WriteLine(product1 == product2);

public struct Point
{
    public int X { get; set; }
}

public record Product(string Name, decimal Price);

```





Cevabını şu formatta yaz:


Plain text






```
1. satır: ?
2. satır: ?
3. satır: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `Point` neden `point2` değiştiğinde `point1` değişmiyor?

- `Point` hangi türdür?

- `var point1 = new Point { X = 10 };` satırındaki `{ X = 10 }` ne işe yarar?

- `Product` için `==` neden `true` veya `false` olur?

- `record` ile `struct` arasındaki fark nedir?




## Problem 3 — `class`, `record` ve `record struct`


Aşağıdaki kodun çıktısını tahmin et:


C#






```
var classProduct1 = new ClassProduct("Mouse", 500);
var classProduct2 = new ClassProduct("Mouse", 500);

var recordProduct1 = new RecordProduct("Mouse", 500);
var recordProduct2 = new RecordProduct("Mouse", 500);

var recordStruct1 = new RecordStructProduct("Mouse", 500);
var recordStruct2 = recordStruct1;

recordStruct2.Price = 700;

Console.WriteLine(classProduct1 == classProduct2);
Console.WriteLine(recordProduct1 == recordProduct2);
Console.WriteLine(recordStruct1.Price);
Console.WriteLine(recordStruct2.Price);

public class ClassProduct
{
    public ClassProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; }
    public decimal Price { get; }
}

public record RecordProduct(string Name, decimal Price);

public record struct RecordStructProduct(string Name, decimal Price);

```





Cevabını şu formatta yaz:


Plain text






```
1. satır: ?
2. satır: ?
3. satır: ?
4. satır: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `classProduct1 == classProduct2` neden `true` veya `false` olur?

- `recordProduct1 == recordProduct2` neden farklı sonuç verir?

- `recordStruct1` ve `recordStruct2` neden birbirinden bağımsızdır?

- `record`, `class` ve `record struct` arasındaki farklar nelerdir?




## Problem 4 — `get;` Property’leri


Aşağıdaki kodda hangi satırlar derleme hatası verir?


C#






```
var product = new Product("Keyboard", 750);

Console.WriteLine(product.Name);
Console.WriteLine(product.Price);

product.Name = "Mouse";
product.Price = 500;

public class Product
{
    public Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; }
    public decimal Price { get; }
}

```





Cevabını şu formatta yaz:


Plain text






```
1. Hata veren satır: ?
2. Hata veren satır: ?

Nedeni:
...

```





Açıklamanda şunlara değin:



- `{ get; }` ne anlama gelir?

- Property değerleri nerede atanabilir?

- Constructor neden kullanılmıştır?

- `product.Name = "Mouse";` ve `product.Price = 500;` satırları neden çalışmaz?




## Problem 5 — `get; set;`, `get; init;` ve `get;`


Aşağıdaki kodu incele:


C#






```
var editableProduct = new EditableProduct
{
    Name = "Keyboard",
    Price = 750
};

editableProduct.Name = "Mouse";
editableProduct.Price = 500;

Console.WriteLine(editableProduct.Name);
Console.WriteLine(editableProduct.Price);


var initProduct = new InitProduct
{
    Name = "Keyboard",
    Price = 750
};

// Aşağıdaki satırlar neden hata verir?
// initProduct.Name = "Mouse";
// initProduct.Price = 500;


var readonlyProduct = new ReadonlyProduct("Keyboard", 750);

// Aşağıdaki satırlar neden hata verir?
// readonlyProduct.Name = "Mouse";
// readonlyProduct.Price = 500;


public class EditableProduct
{
    public string Name { get; set; } = "";
    public decimal Price { get; set; }
}

public class InitProduct
{
    public string Name { get; init; } = "";
    public decimal Price { get; init; }
}

public class ReadonlyProduct
{
    public ReadonlyProduct(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public string Name { get; }
    public decimal Price { get; }
}

```





Aşağıdaki soruları cevapla:


Plain text






```
1. EditableProduct için çıktı nedir?
2. InitProduct oluşturulurken değerler nasıl atanıyor?
3. InitProduct oluşturulduktan sonra neden değiştirilemiyor?
4. ReadonlyProduct değerleri nerede atanıyor?
5. Üç sınıf arasındaki temel fark nedir?

```





Tabloyu doldur:




























| Property | Oluşturulurken atanabilir | Sonradan değiştirilebilir |
| --- | --- | --- |
| `get; set;` | ? | ? |
| `get; init;` | ? | ? |
| `get;` | ? | ? |



Ayrıca şu üç kullanım arasındaki farkı açıklamaya çalış:


C#






```
public string Name { get; set; }

```





C#






```
public string Name { get; init; }

```





C#






```
public string Name { get; }
```
