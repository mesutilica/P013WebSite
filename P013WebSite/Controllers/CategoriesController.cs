using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using P013WebSite.Data;

namespace P013WebSite.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly DatabaseContext _context;

        public CategoriesController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index(int? id)
        {
            if (id is null) // eğer adres çubuğundan id gönderilmemişse
            {
                return BadRequest(); // ekrana geçersiz istek hatası ver
            }
            var category = _context.Categories.Include(p => p.Products).FirstOrDefault(c => c.Id == id); // id gönderilmişse db den o kategoriyi ara
            if (category == null) // eğer kategori db de yoksa
            {
                return NotFound(); // geriye bulunamadı hatası döndür
            }
            return View(category); // kategori bulunduysa ekrana yolla
        }
    }
}
