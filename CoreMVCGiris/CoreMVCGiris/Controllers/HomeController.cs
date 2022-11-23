using CoreMVCGiris.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using System.Diagnostics;

namespace CoreMVCGiris.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        #region Controllerlar
        /* Controllerlar view ile modellerimiz arasındaki katmandır, client tarafından gelen isteklere yanıt verip
         * gerekli işlemleri yaparak cevap döndüren yapılardır. Controller sınıflarını oluştururken belirli kurallara
         * dikkat ederiz, controller sınıfını önce sınıfın ismini sonuna controller yazarak oluştururuz örneğin
         * ArabaController gibi ve bu oluşturduğumuz sınıfı Controller base classından kalıtım alırız. Controller içerisine
         * tanımladığımız metodlar action metodları olarak geçer bu metodlar kullanıcılara bir view sayfası veya direkt olarak
         * veri döndürebilir. Action sayfaları varsayılan olarak HttpPost verb'ünü kullanır bu kullanıcıdan gelen istege yanıt
         * veren sayfa ve verileri gönderen protokoldür, kullanıcılardan veri alacağımız zaman HttpPost protokolü ile verilerimizi alırız.
         * 
         */
        #endregion


        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Kullanici() {
            
            return View(new Kullanici { AdSoyad = "Burkay Akgül", ID = 1, Yas = 26});
        }

        [HttpPost]
        public IActionResult Kullanici(Kullanici kullanici)
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}