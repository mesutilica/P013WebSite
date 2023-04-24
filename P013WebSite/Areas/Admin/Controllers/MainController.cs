using Microsoft.AspNetCore.Authorization; // oturum açmayı gerekli kılan kütüphane
using Microsoft.AspNetCore.Mvc;

namespace P013WebSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize] // Bir controller a Authorize attribute ü uygularsak controller içerisindeki bütün action lara erişimi engellemiş oluruz. Sadece oturum açan kullanıcılar ekranları görebilir.
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
