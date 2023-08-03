using KitApp.Models;
using KitApp.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KitApp.Controllers
{
    public class BookTypesController : Controller
    {
        private readonly AppDbContext _context;

        public BookTypesController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            List<BookType> bookTypes = _context.BookTypes.ToList();
            return View(bookTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(BookType model)
        {
            if (ModelState.IsValid)
            {
                // ModelState doğrulandıysa, gelen veriler geçerli demektir.
                // Veritabanına yeni kitap türünü eklemek için aşağıdaki adımları takip edebilirsiniz.

                try
                {
                    // Gelen modeli doğrudan veritabanına ekleyin.
                    _context.BookTypes.Add(model);

                    // Değişiklikleri kaydedin.
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Kitap türü başarıyla eklendi.";
                    // Başarılı bir şekilde kitap türü eklediğinizde başka bir sayfaya yönlendirebilirsiniz.
                    // Örneğin, Index action'ı ile kitap türlerinin listelendiği sayfaya yönlendirebilirsiniz.
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Veritabanına kaydedilirken bir hata oluşursa burada işleyebilirsiniz.
                    // Hatayı loglamak, kullanıcıya uygun bir hata mesajı göstermek gibi adımlar yapılabilir.
                    // Bu örnek için, hatayı ModelState üzerinden kullanıcıya gösterelim:
                    ModelState.AddModelError("", "Kitap türü eklenirken bir hata oluştu.");
                }
            }

            // Eğer ModelState doğrulanmadıysa (yani gelen veriler geçerli değilse) veya
            // veritabanına eklemek için bir hata oluştuysa, kullanıcıyı aynı sayfada gösterin.
            return View(model);
        }

        [HttpGet]
        public IActionResult Update(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            //BookType? values = _context.BookTypes.Find(id);
            //if (values == null)
            //{
            //    return NotFound();
            //}
            //return View(values);
            // İlk olarak, güncellenecek kitap türünü veritabanından bulalım.
            BookType bookType = _context.BookTypes.Find(id);

            if (bookType == null)
            {
                // Eğer verilen id'ye sahip kitap türü bulunamazsa, hata sayfasına yönlendirin veya
                // farklı bir işlem yapmak istiyorsanız onu yapabilirsiniz.
                return NotFound();
            }

            // Kitap türünü güncelleme için bir model olarak view'a gönderelim.
            return View(bookType);
        }

        [HttpPost]
        public IActionResult Update(BookType model)
        {
            if (ModelState.IsValid)
            {
                // ModelState doğrulandıysa, gelen veriler geçerli demektir.

                try
                {
                    // Gelen modeldeki değişiklikleri veritabanında güncelleyin.
                    _context.Entry(model).State = EntityState.Modified;

                    // Değişiklikleri kaydedin.
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Kitap türü başarıyla güncellendi.";
                    // Başarılı bir şekilde kitap türünü güncellediğinizde başka bir sayfaya yönlendirebilirsiniz.
                    // Örneğin, Index action'ı ile kitap türlerinin listelendiği sayfaya yönlendirebilirsiniz.
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    // Veritabanına kaydedilirken bir hata oluşursa burada işleyebilirsiniz.
                    // Hatayı loglamak, kullanıcıya uygun bir hata mesajı göstermek gibi adımlar yapılabilir.
                    // Bu örnek için, hatayı ModelState üzerinden kullanıcıya gösterelim:
                    ModelState.AddModelError("", "Kitap türü güncellenirken bir hata oluştu.");
                }
            }

            // Eğer ModelState doğrulanmadıysa (yani gelen veriler geçerli değilse) veya
            // veritabanında güncelleme için bir hata oluştuysa, kullanıcıyı aynı sayfada gösterin.
            return View(model);
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // İlk olarak, silinecek kitap türünü veritabanından bulalım.
            BookType bookType = _context.BookTypes.Find(id);

            if (bookType == null)
            {
                // Eğer verilen id'ye sahip kitap türü bulunamazsa, hata sayfasına yönlendirin veya
                // farklı bir işlem yapmak istiyorsanız onu yapabilirsiniz.
                return NotFound();
            }

            // Kitap türünü silme için bir model olarak view'a gönderelim.
            return View(bookType);
        }

        [HttpPost]
        [ValidateAntiForgeryToken] // Güvenlik için bu attribut'i ekleyin.
        public IActionResult Delete(int id)
        {
            // Veritabanından kitap türünü bulalım.
            BookType bookType = _context.BookTypes.Find(id);

            if (bookType == null)
            {
                // Eğer verilen id'ye sahip kitap türü bulunamazsa, hata sayfasına yönlendirin veya
                // farklı bir işlem yapmak istiyorsanız onu yapabilirsiniz.
                return NotFound();
            }

            try
            {
                // Veritabanından kitap türünü silin.
                _context.BookTypes.Remove(bookType);

                // Değişiklikleri kaydedin.
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Kitap türü başarıyla silindi.";
                // Başarılı bir şekilde kitap türünü sildiğinizde başka bir sayfaya yönlendirebilirsiniz.
                // Örneğin, Index action'ı ile kitap türlerinin listelendiği sayfaya yönlendirebilirsiniz.
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                // Veritabanından silerken bir hata oluşursa burada işleyebilirsiniz.
                // Hatayı loglamak, kullanıcıya uygun bir hata mesajı göstermek gibi adımlar yapılabilir.
                // Bu örnek için, hatayı ModelState üzerinden kullanıcıya gösterelim:
                ModelState.AddModelError("", "Kitap türü silinirken bir hata oluştu.");
            }

            // Eğer silme işlemi başarısız olursa veya bir hata oluşursa, kullanıcıyı aynı sayfada gösterin.
            return View(bookType);
        }



    }
}
