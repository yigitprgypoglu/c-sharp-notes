## Kaynak ve Atıf

Bu not hazırlanırken aşağıdaki kaynaktan yararlanılmıştır:

- Kaynak: [C# Windows Forms: Olaylar (Events) Nedir?](https://furkanmorova.com/csharp-windows-form-events/)
- Erişim tarihi: 22 Ekim 2026

Notlar, öğrenme amacıyla düzenlenmiş ve Markdown formatına dönüştürülmüştür.
Tüm haklar orijinal yazara aittir.

# C# Windows Forms: Olaylar (Events)

## Olaylar (Events) Nedir?

C# ve Windows Forms dünyasına adım attığınızda, programlarınız durağan siyah ekranlardan sıyrılıp kullanıcının eylemlerine tepki veren canlı yapılara dönüşür.

Uygulamalarda:

- Bir düğmeye tıklandığında işlem yapılması
- Farenin bir nesnenin üzerine geldiğinde rengin değişmesi
- Klavyeden bir tuşa basıldığında karakterlerin kontrol edilmesi

gibi işlemleri sağlayan temel mekanizmaya **Olaylar (Events)** denir.

## Olay Yönelimli Programlama (Event-Driven Programming) Nedir?

Olay yönelimli programlamada bir form uygulaması, arka planda karmaşık bir işlem yapmıyorsa zamanının büyük bir kısmını kullanıcının bir eylem gerçekleştirmesini bekleyerek geçirir.

Kullanıcının veya sistemin gerçekleştirdiği bu etkileşimlere **olay (event)** denir.

Gerçek hayattan bir örnek vermek gerekirse, bir dükkân sahibinin programınızı temsil ettiğini düşünelim. Dükkân sahibi içeride bekler ve bir müşteri içeri girip zile bastığında müşteriye “Hoş geldiniz” der. Burada:

- Zile basılması: **Olay (Event)**
- “Hoş geldiniz” denmesi: **Olay metodu (Event Handler)**

olur.

## Visual Studio'da Olaylar Nasıl Oluşturulur?

Her nesnenin form üzerinde belirli bir görevi vardır. Kullanıcı ile nesne arasındaki etkileşimler olaylar sayesinde sağlanır.

Visual Studio'da bir nesneye, örneğin bir butona, olay eklemek için:

1. Form üzerindeki nesneye tıklayın.
2. Sağ alt köşedeki **Properties** penceresine gelin.
3. Pencerenin üst kısmında bulunan sarı şimşek ikonuna tıklayın.
4. Nesneye ait kullanılabilir olaylar listesinden istediğiniz olayı bulun.
5. Olayın yanındaki boş alana çift tıklayın.

Visual Studio sizin için olayı yakalayacak bir C# metodu oluşturur ve sizi kod ekranına yönlendirir.

### Pratik İpucu

En çok kullanılan olay olan `Click` için şimşek ikonuna gitmek zorunda değilsiniz. Tasarım ekranındayken bir nesneye çift tıklarsanız, nesnenin varsayılan olayı otomatik olarak oluşturulur.

Bu olay genellikle butonlarda `Click`, formlarda ise `Load` olur.

## En Çok Kullanılan C# Form Olayları

### 1. Tıklama ve Fare Olayları

- **Click:** Nesneye fare ile tıklanması veya nesne seçiliyken klavyeden `Enter` tuşuna basılmasıyla çalışır.
- **MouseClick:** `Click` olayına benzer; ancak klavyedeki `Enter` tuşundan etkilenmez. Ayrıca farenin hangi tuşuna basıldığı ve farenin `X`, `Y` koordinatları gibi bilgileri sağlar.
- **MouseDown:** Farenin tuşuna basıldığı anda çalışır.
- **MouseUp:** Basılı tutulan fare tuşu bırakıldığında çalışır.
- **MouseEnter:** Fare imleci nesnenin sınırları içine girdiğinde çalışır.
- **MouseLeave:** Fare imleci nesnenin üzerinden ayrıldığında çalışır.
- **MouseHover:** Fare imleci bir nesnenin üzerinde belirli bir süre hareketsiz kaldığında çalışır. Genellikle ipucu veya tooltip göstermek için kullanılır.

### 2. Klavye Olayları

Klavye olayları, kullanıcı klavyeden bir tuşa bastığında çalışır. Özellikle `TextBox` kontrollerinde sık kullanılır.

- **KeyDown:** Klavyeden bir tuşa basıldığı anda çalışır.
- **KeyPress:** Basılan tuş bir karakter üretiyorsa çalışır.
- **KeyUp:** Basılan tuş serbest bırakıldığında çalışır.

### 3. Form Yaşam Döngüsü ve Odak Olayları

Uygulamanın açılması, kapanması veya kontroller arasında gezinilmesi de olayları tetikler.

- **Load:** Form ilk açılırken çalışır. Veritabanı bağlantıları ve açılış ayarları genellikle burada yapılır.
- **FormClosing:** Form kapanmadan hemen önce çalışır. Kullanıcıya “Çıkmak istediğinize emin misiniz?” sorusunu sormak için uygundur.
- **Enter:** Kullanıcı bir kontrole odaklandığında çalışır.
- **Leave:** Kullanıcı bir kontrolden ayrıldığında çalışır.
- **Validating:** Kullanıcı bir kontrolden ayrılırken verinin doğrulanması gerektiğinde çalışır.

## Olay Parametreleri: `sender` ve `EventArgs e`

Visual Studio bir olay metodu oluşturduğunda genellikle şu iki parametreyi ekler:

```csharp
(object sender, EventArgs e)
```

### `object sender`

Olayı hangi nesnenin tetiklediğini belirtir.

Örneğin formda 10 tane buton varsa ve hepsi aynı `Click` olay metodunu kullanıyorsa, hangi butona tıklandığını anlamak için `sender` nesnesinden yararlanabilirsiniz.

### `EventArgs e`

Gerçekleşen olayla ilgili ek bilgileri taşır.

Örneğin:

- Fare olaylarında `MouseEventArgs`
- Klavye olaylarında `KeyEventArgs`

kullanılır.

Fare olaylarında farenin `X` ve `Y` koordinatlarına, klavye olaylarında ise basılan tuşun bilgisine erişilebilir.

## Örneklerle C# Olay Kullanımı

### Örnek 1: Klasik `Click` Olayı

Formunuza bir `Button` eklediğinizi ve üzerine çift tıkladığınızı varsayalım. Butona tıklandığında kullanıcıyı selamlayan kod şöyledir:

```csharp
private void btnSelamla_Click(object sender, EventArgs e)
{
    // Ekranda açılır bir bilgi mesajı kutusu gösterir
    MessageBox.Show("Sistemimize Hoş Geldiniz!");
}
```

### Örnek 2: `MouseEnter` ve `MouseLeave` Olayları

Formunuza bir buton ekleyin. Fare ile bu butonun üzerine geldiğinizde butonun rengi yeşil olsun. Fareyi butonun üzerinden çektiğinizde ise buton eski rengine, örneğin griye, geri dönsün.

```csharp
// Fare butonun üzerine geldiğinde çalışır.
private void button1_MouseEnter(object sender, EventArgs e)
{
    button1.BackColor = Color.Green;
    button1.Text = "Üzerimdesin!";
}

// Fare butonun üzerinden ayrıldığında çalışır.
private void button1_MouseLeave(object sender, EventArgs e)
{
    button1.BackColor = Color.LightGray;
    button1.Text = "Beni Bırakma!";
}
```

Bu olayları oluşturmak için tasarım ekranında butonu seçin. Daha sonra:

1. **Properties** penceresini açın.
2. Sarı şimşek ikonuna tıklayın.
3. `MouseEnter` ve `MouseLeave` olaylarını bulun.
4. Bu olaylara çift tıklayın.

### Örnek 3: Form Kapatılırken Kullanıcıyı Uyarmak

Kullanıcı uygulamayı yanlışlıkla kapatmasın diye, sağ üstteki kırmızı `X` tuşuna bastığında bir onay mesajı gösterebiliriz.

Formun `FormClosing` olayına çift tıklayıp şu kodu yazabilirsiniz:

```csharp
private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    // Kullanıcıya Evet/Hayır seçenekli bir uyarı gösterir.
    DialogResult cevap = MessageBox.Show(
        "Uygulamadan çıkmak istediğinize emin misiniz?",
        "Çıkış",
        MessageBoxButtons.YesNo,
        MessageBoxIcon.Question);

    // Kullanıcı Hayır derse kapanma işlemini iptal eder.
    if (cevap == DialogResult.No)
    {
        e.Cancel = true;
    }
}
```

## Arka Planda Ne Olur?

Olay metotlarını yazdıktan sonra programın, hangi metodun hangi nesnenin hangi olayına ait olduğunu bilmesi gerekir.

Visual Studio, şimşek ikonundaki bir olaya çift tıkladığınızda bu bağlantıyı arka planda otomatik olarak kurar. Bu bağlantı genellikle `Form1.Designer.cs` dosyasında veya `InitializeComponent` metodu içinde oluşturulur.

Bir buton için bağlantı şu şekilde olabilir:

```csharp
this.button1.Click += new System.EventHandler(this.button1_Click);
```

Buradaki `+=` operatörü, `button1` nesnesinin `Click` olayına yeni bir dinleyici ekler.

C# mimarisinde bir olaya birden fazla metot bağlanabildiği için burada eşittir (`=`) yerine artı-eşittir (`+=`) kullanılır.

## Sonuç

C# Windows Forms projelerinde olaylar, uygulamanın kullanıcı etkileşimlerine tepki vermesini sağlayan temel mekanizmadır.

Fare tıklamalarından klavye girişlerine, pencerelerin açılış ve kapanış işlemlerinden kontroller arasındaki geçişlere kadar birçok işlem olaylar sayesinde gerçekleştirilir.

Yeni bir nesneyi forma sürükleyip bıraktığınızda **Properties** penceresindeki sarı şimşek ikonunu inceleyerek kullanılabilir olayları görebilirsiniz. Bu olayları kodlarla bağlayarak uygulamalarınızı daha akıllı, dinamik ve kullanıcı dostu hale getirebilirsiniz.
