using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P013WebSite.Data;
using P013WebSite.Entities;
using P013WebSite.Tools;

namespace P013WebSite.Areas.Admin.Controllers
{
    [Area("Admin"), Authorize]
    public class ProductsController : Controller
    {
        private readonly DatabaseContext _databaseContext; // _databaseContext i boş olarak ekledik, sağ klik yapıp ampule tıklayıp açılan menüden generate consructor diyerek DI(DependencyInjection) işlemini yapıyoruz

        public ProductsController(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext; // context i kurucu metotta doldurduk
        }

        // GET: ProductsController
        public ActionResult Index()
        {
            //return View(_databaseContext.Products.ToList()); // var model oluşturmadan direk sayfaya modeli bu şekilde de yollayabiliyoruz
            return View(_databaseContext.Products.Include(c => c.Category).ToList()); // Products tablosundaki kayıtlara EntityFrameworkCore un Include metoduyla kategorilerini de dahil ettik, böylece sql deki join işlemi yapılmış oldu..
        }

        // GET: ProductsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ProductsController/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories.ToList(), "Id", "Name");
            return View();
        }

        // POST: ProductsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(Product collection, IFormFile? Image)
        {
            try
            {
                collection.Image = await FileHelper.FileLoaderAsync(Image); // asenkron metotlar çağrılırken mutlaka başına await anahtar kelimesini yazıyoruz!
                await _databaseContext.Products.AddAsync(collection);
                await _databaseContext.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(_databaseContext.Categories.ToList(), "Id", "Name");
                return View();
            }
        }

        // GET: ProductsController/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.CategoryId = new SelectList(_databaseContext.Categories.ToList(), "Id", "Name");
            return View(_databaseContext.Products.Find(id));
        }

        // POST: ProductsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(int id, Product collection, IFormFile? Image)
        {
            try
            {
                if (Image is not null)
                {
                    collection.Image = await FileHelper.FileLoaderAsync(Image); // Bir senkron metodun içerisinde asenkron bir metot çağrılırsa ilgili senkron metot da asenkrona çevrilmelidir! Bu işlem için içerdeki asenkron metodun üzerine gelip ampulün çıkmasını bekliyoruz ve gelen menüden make method async seçeneğine tıklayıp hata nın giderilmesini sağlıyoruz.
                }
                _databaseContext.Products.Update(collection);
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                ViewBag.CategoryId = new SelectList(_databaseContext.Categories.ToList(), "Id", "Name");
                return View();
            }
        }

        // GET: ProductsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_databaseContext.Products.Include(c => c.Category).FirstOrDefault(p => p.Id == id));
        }

        // POST: ProductsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Product collection)
        {
            try
            {
                _databaseContext.Products.Remove(collection);
                _databaseContext.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
