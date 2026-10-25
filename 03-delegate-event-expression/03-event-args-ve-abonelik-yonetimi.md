# Bölüm 3.3 — Event Args, Abonelik Yönetimi ve Pratik Kullanım

## Olayları gerçek uygulamalarda yönetme

- Özel event argümanları
- `EventHandler<TEventArgs>` kullanımı
- Event aboneliğini `-=` ile kaldırma
- Bellek yönetimi ve nesne yaşam süresi
- `OrderCreated` ve `OrderCancelled` olayları
- Event ile delegate arasındaki ilişki

**Hedef:** Event'leri veri taşıyacak ve yaşam döngüsü yönetilecek şekilde
kullanabilmek.

## Event'ten ayrılma

Bir event'e `+=` ile bağlanırız:

```csharp
orderService.OrderCreated += OnOrderCreated;
```

Bağlantıyı kaldırmak için `-=` kullanılır:

```csharp
orderService.OrderCreated -= OnOrderCreated;
```

Bu özellikle uzun ömürlü nesnelerde önemlidir. Event aboneliği kaldırılmazsa
nesneler gereğinden uzun süre bellekte tutulabilir.

## Özel event argümanları

Event ile daha fazla veri göndermek için `EventArgs` sınıfından türeyen bir
sınıf kullanabiliriz:

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

## Alıştırma

`OrderService` sınıfına `OrderCancelled` isminde ikinci bir event ekleyip,
iptal edilen siparişin numarasını event argümanı ile göndermeyi dene.
