## `interface` nedir?


`interface`, bir türün hangi davranışları sağlaması gerektiğini belirleyen bir **sözleşmedir**.


C#






```
public interface IShape
{
    double Area();
}

```





Bu interface'i uygulayan her class veya struct, `Area()` metodunu tanımlamak zorundadır:


C#






```
public struct Circle : IShape
{
    public double Radius { get; set; }

    public double Area()
    {
        return Math.PI * Radius * Radius;
    }
}

```





Burada `Circle` bir struct'tır ve `IShape` interface'ini uygular.


C#






```
var circle = new Circle { Radius = 5 };

Console.WriteLine(circle.Area());
```
