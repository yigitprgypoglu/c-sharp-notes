# Bölüm 3.2 — Event ve Olay Tabanlı Programlama

## Olaylara abone olma ve tepki verme

- `event` nedir?
- `EventHandler` ve `EventArgs`
- `+=` ile olaya abone olma
- `?.Invoke()` ile olay tetikleme
- Bir event'e birden fazla metot bağlama
- Gerçek hayattan sipariş oluşturma örneği

**Hedef:** Olaylara metot bağlayıp olay gerçekleştiğinde tepki verebilmek.

## Event nedir?

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

`EventHandler`, .NET ile hazır gelen standart bir delegedir (delegate).
Tanımı gereği iki parametre taşır: `(object sender, EventArgs e)`.

`?` (Nullable), bu olaya henüz kimse abone olmamış olabilir, yani event'in
değeri `null` olabilir uyarısını susturur ve olayı güvenli hale getirir.

Event'e metot bağlayalım:

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

`?.Invoke`, event'e kayıtlı metot varsa olayı çalıştırır. Kayıtlı metot yoksa
`NullReferenceException` oluşmasını engeller.
