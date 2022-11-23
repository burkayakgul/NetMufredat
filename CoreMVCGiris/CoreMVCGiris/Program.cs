
#region .NET 6 Yenilikleri
/*     **** Startup ****
 * .Net 6 ile birlikte Startup.cs dosyası kaldırıldı. Normalde startup.cs dosyasında yaptığımız servis ve middleware
 *    işlemlerini artık program.cs dosyası üzerinden yapıyoruz
 *    
 *     **** Top Level Statements **** 
 * .Net 6 ile birlikte top level statements özelliği kazandırılmıştır, normal şartlarda bir sınıfta kod 
 *      yazmak istediğimiz zaman o kodda kullanacağımız kütüphanelerin using yapısı ile belirtilip bir namespace içerisinde sınıfımızın
 *      tanımlandığı o sınıf içersindeki main metodu içerisine kodumuzu yazıyorduk,  bu özellik ile birlikte direkt olarak program.cs 
 *      içerisinde yazacağımız kodlar o sınıfın main metodunun içerisine yazdığımızı varsayarak çalışma zamanında kodlarımızı bu yapı içerisine alır
 *      böylece karmaşık bir ortamdan kurtulup kodumuza odaklanabiliriz.
 *      
 *     **** Port ****
 *  .Net 6 öncesinde uygulamalar default 5001 ve 5000 portlarından ayağa kalkarken .Net 6'da bu port değişiklik göstermektedir.
 */
#endregion


#region .Net 7 Yenilikleri
/*     **** Rate Limit ****
 *  Rate limit api'larımıza gelen istek limitini sınırlamamıza yarayan güvenlik yapılarıdır, kötü niyetli kullanıcıların api'ımızı
 *      isteğe boğup çalışamaz hale getirmesini önleyen bir sistemdir. Rate limit işlemlerini program.cs üzerinden builder nesnemiz
 *      aracılığı ile yaparız.
 *      
 *     **** Output Caching ****
 *  Output caching özelliği ile apilarımıza gelen istekleri önce cache belleğe alarak belirli süre içerisinde tekrarlanan
 *      istekleri güncelleme yapmadan cache'deki sonucu gösterir. Cacheleme işlemini anlık güncellenmesi gerekmeyen işlemlerde
 *      kullanabiliriz böylece yapılacak gereksiz isteklere karşı programımızın performansını iyileştiririz.
 *      
 *     
*/
#endregion

// .Net 6 ile birlikte builder nesnesini oluşturabilmek için gelmiştir, .net 5'teki Host sınıfına karşılık gelmektedir.
//  Uygulamayı inşa edeceğimiz nesneyi oluşturmaktadır.
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddOutputCache();
// Elde ettiğimiz builder nesnesini build yani inşa eder.
var app = builder.Build();

app.UseOutputCache();

// Eğer programımız geliştirme sürecinde değilse kullanıcılara hatalı isteklerini göstermek için error sayfalarına yönlendiren yapı
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// Uygulamamız içerisindeki wwwroot klasörü altında bulunan statik yani css javascript gibi kodlarımızın ve kütüphanelerin bulunduğu
// yapıları projemize dahil edildiği kısım.
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

#region Route Yapısı
// Sayfamıza gelen istekleri route yani yönlendirme işleminin yapıldığı kısım, burada varsayılan olarak sayfamıza istek gönderildiği
//      zaman Home ismindeki controller sınıfının içersindeki Index action yönlendirme yapacaktır
// Varsayılan route sadece site adresine yapılan istekler geldiği zaman çalışacaktır bu varsayılan adresi bir alışveriş sitesinin
//      sayfasına ilk girdiğimiz zaman bizi karşılayan anasayfası olarak düşünülebilir
//      İstediğimiz bir sayfaya yönlenmek istediğimiz zaman öncelikle o sayfanın bulunduğu controller ismini sonrasında bulunduğu
//      controller ismini ve son olarak göndereceğimiz bir veri var ise o veriyi de yazarak yönlendirme işlemini yaparız
//      Örnek olarak alisverissayfam.com/kiyafetler/detay/5  kiyafetler controller'ındaki detay action'ın içerisine 5 numaralı veriyi
//      görüntülemek için istek göndermiş olduk.
//  
#endregion
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Uygulamamızı çalıştırdığımız kısım.
app.Run();
