# C# Notları

C# öğrenirken tuttuğum konu anlatımlarını, örnek kodları ve alıştırmaları
düzenli bir klasör yapısında bir araya getiren kişisel çalışma reposu.

## Öğrenme sırası

Notlar, temel konulardan daha ileri konulara doğru bölüm bölüm ilerler.

### 1. C# Tip Sistemi

Value type, reference type, `struct`, `class`, `record`, `record struct`,
değer eşitliği ve doğru tür seçimi.

- [Bölüm 1 — C# Tip Sistemi](01-tip-sistemi/README.md)
- [Bölüm 1 alıştırmaları](01-tip-sistemi/note_1_test.md)

### 2. Değer Dönüşümleri ve Parametre Aktarımı

Boxing, unboxing, nullable türler, `ref`, `out` ve `in` parametreleri.

- [Bölüm 2 — Değer Dönüşümleri ve Parametre Aktarımı](02-deger-donusumleri/README.md)
- [Bölüm 2 alıştırmaları](02-deger-donusumleri/note_2_test.md)

### 3. Delegate, Event ve Expression

Delegate, `Func`, `Action`, `Predicate`, lambda ifadeleri, event'ler,
özel event argümanları, abonelik yönetimi, expression tree ve `IQueryable`.

- [Bölüm 3 — Delegate, Event ve Expression](03-delegate-event-expression/README.md)
- [Bölüm 3 alıştırmaları](03-delegate-event-expression/note_3_test.md)

### 4. İleri LINQ

LINQ temelleri, sıralama ve sayfalama, koleksiyonları gruplama ve birleştirme,
deferred execution, `IEnumerable`, `IQueryable`, extension metotları ve
performans.

- [Bölüm 4 — İleri LINQ](04-ileri-linq/README.md)

## İleride eklenecek bölümler

Aşağıdaki başlıklar öğrenme planında yer alıyor ve ilerleyen süreçte
klasörlere ayrılarak eklenecek:

5. Asenkron programlama
6. Threading ve concurrency
7. Bellek ve performans
8. Reflection, attributes ve source generator
9. Mimari ve tasarım
10. Test, hata yönetimi ve production kodu

## İçerik yapısı

Her bölüm kendi klasöründe tutulur. Bölüm klasörlerinde:

- `README.md`: Bölüm içindeki derslerin ve konuların listesi
- Numaralı `.md` dosyaları: Konu anlatımları ve örnek kodlar
- `note_*_test.md`: Konuyla ilgili tekrar problemleri ve alıştırmalar

Kod örnekleri C# kod blokları içinde, açıklamalar ise Markdown metni olarak
düzenlenmiştir.
