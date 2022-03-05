using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Data;
using Ecommerce.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Ecommerce.Areas.Admin.Controllers
{

    //    [Area("Admin")]
    //    public class Products : Controller
    //    {
    //        private ApplicationDbContext _db;
    //        private IHostingEnvironment _he;

    //        public Products(ApplicationDbContext db, IHostingEnvironment he)
    //        {
    //            _db = db;
    //            _he = he;
    //        }
    //        public IActionResult Index()
    //        {
    //            return View(_db.Product.Include(c => c.Category).ToList());
    //        }

    //        //POST Index action method
    //        [HttpPost]
    //        public IActionResult Index(decimal? lowAmount, decimal? largeAmount)
    //        {
    //            var product = _db.Product.Include(c => c.Category)
    //                .Where(c => c.Price >= lowAmount && c.Price <= largeAmount).ToList();
    //            if (lowAmount == null || largeAmount == null)
    //            {
    //                product = _db.Product.Include(c => c.Category).ToList();
    //            }
    //            return View(product);
    //        }

    //        //Get Create method
    //        public IActionResult Create()
    //        {
    //            ViewData["Id"] = new SelectList(_db.Category.ToList(), "Id", "ProductType");
    //           // ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
    //            return View();
    //        }


    //        //Post Create method
    //        [HttpPost]
    //        public async Task<IActionResult> Create(Product product, IFormFile image)
    //        {
    //            if (ModelState.IsValid)
    //            {
    //               // var searchProduct = _db.Product.FirstOrDefault(c => c.Name == product.Name);
    //                //if (searchProduct != null)
    //                //{
    //                //    ViewBag.message = "This product is already exist";
    //                //    ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
    //                //  //  ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
    //                //    return View(product);
    //                //}

    //                if (image != null)
    //                {
    //                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
    //                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
    //                    product.Image = "Images/" + image.FileName;
    //                }

    //                //if (image == null)
    //                //{
    //                //    product.Image = "Images/noimage.PNG";
    //                //}
    //                _db.Product.Add(product);
    //                await _db.SaveChangesAsync();
    //                return RedirectToAction(nameof(Index));
    //            }

    //            return View(product);
    //        }

    //        //GET Edit Action Method

    //        public ActionResult Edit(int? id)
    //        {
    //            ViewData["productTypeId"] = new SelectList(_db.ProductTypes.ToList(), "Id", "ProductType");
    //           // ViewData["TagId"] = new SelectList(_db.SpecialTags.ToList(), "Id", "Name");
    //            if (id == null)
    //            {
    //                return NotFound();
    //            }

    //            var product = _db.Product.Include(c => c.Category)
    //                .FirstOrDefault(c => c.Id == id);
    //            if (product == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(product);
    //        }

    //        //POST Edit Action Method
    //        [HttpPost]
    //        public async Task<IActionResult> Edit(Product product, IFormFile image)
    //        {
    //            if (ModelState.IsValid)
    //            {
    //                if (image != null)
    //                {
    //                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
    //                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
    //                    product.Image = "Images/" + image.FileName;
    //                }

    //                //if (image == null)
    //                //{
    //                //    product.Image = "Images/noimage.PNG";
    //                //}
    //                _db.Product.Update(product);
    //                await _db.SaveChangesAsync();
    //                return RedirectToAction(nameof(Index));
    //            }

    //            return View(product);
    //        }

    //        //GET Details Action Method
    //        public ActionResult Details(int? id)
    //        {

    //            if (id == null)
    //            {
    //                return NotFound();
    //            }

    //            var product = _db.Product.Include(c => c.Category)
    //                .FirstOrDefault(c => c.Id == id);
    //            if (product == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(product);
    //        }

    //        //GET Delete Action Method

    //        public ActionResult Delete(int? id)
    //        {
    //            if (id == null)
    //            {
    //                return NotFound();
    //            }

    //            var product = _db.Product.Include(c => c.Category).Where(c => c.Id == id).FirstOrDefault();
    //            if (product == null)
    //            {
    //                return NotFound();
    //            }
    //            return View(product);
    //        }

    //        //POST Delete Action Method

    //        [HttpPost]
    //        [ActionName("Delete")]
    //        public async Task<IActionResult> DeleteConfirm(int? id)
    //        {
    //            if (id == null)
    //            {
    //                return NotFound();
    //            }

    //            var product = _db.Product.FirstOrDefault(c => c.Id == id);
    //            if (product == null)
    //            {
    //                return NotFound();
    //            }

    //            _db.Product.Remove(product);
    //            await _db.SaveChangesAsync();
    //            return RedirectToAction(nameof(Index));
    //        }

    //    }
    //}




    [Area("Admin")]
    public class Products : Controller
    {
        private readonly ApplicationDbContext _context;
        private IWebHostEnvironment _he;

        public Products(ApplicationDbContext context, IWebHostEnvironment he)
        {
            _context = context;
            _he = he;
        }

        // GET: Admin/Products
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Product.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Admin/Products/Create
        public IActionResult Create()
        {
            ViewData["Id"] = new SelectList(_context.Category, "Id", "ProductType");
            return View();
        }

        // POST: Admin/Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PId,Name,Price,Image,IsAvailable,Id")] Product product, IFormFile image)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;

                }
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Category, "Id", "ProductType", product.Id);
            return View(product);
        }

        // GET: Admin/Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["Id"] = new SelectList(_context.Category, "Id", "ProductType", product.Id);
            return View(product);
        }

        // POST: Admin/Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PId,Name,Price,Image,IsAvailable,Id")] Product product, IFormFile image)
        {
            if (id != product.PId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    var name = Path.Combine(_he.WebRootPath + "/Images", Path.GetFileName(image.FileName));
                    await image.CopyToAsync(new FileStream(name, FileMode.Create));
                    product.Image = "Images/" + image.FileName;
                }

                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.PId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Id"] = new SelectList(_context.Category, "Id", "ProductType", product.Id);
            return View(product);
        }

        // GET: Admin/Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.PId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Admin/Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            _context.Product.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.PId == id);
        }
    }
}
