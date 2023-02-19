using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data.Entities;
using OnlineShop.DbContexts;

namespace OnlineShop.Controllers
{
    public class ProductsController : Controller
    {
        private readonly OnlineShopDbContext _context;
        private UserManager<UserAccount> UserManager;

        public ProductsController(OnlineShopDbContext context, UserManager<UserAccount> userManager)
        {
            _context = context;
            UserManager = userManager;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return _context.Products != null ?
                        View(await _context.Products.ToListAsync()) :
                        Problem("Entity set 'OnlineShopDbContext.Products' is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        //[Authorize(Policy = "OnlyEmployees")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        //[Authorize(Policy = "OnlyEmployees")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Price,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Price,Stock,Available")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'OnlineShopDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cart()
        {
            var customerProducts = from cp in _context.CustomerProductCarts
                                   join p in _context.Products
                                   on cp.ProductId equals p.Id
                                   where cp.CustomerId == User.FindFirstValue(ClaimTypes.NameIdentifier)
                                   select p;
            return View(customerProducts);
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddToCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null && id != null)
            {
                var cp = new CustomerProductCart()
                {
                    CustomerId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                    ProductId = (int)id
                };
                if (!_context.CustomerProductCarts.Any(f => f.CustomerId == cp.CustomerId && f.ProductId == cp.ProductId))
                {
                    _context.CustomerProductCarts.Add(cp);
                    _context.SaveChanges();
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ActionName("RemoveFromCart")]
        [ValidateAntiForgeryToken]
        public void RemoveFromCart(int id)
        {
            var product = _context.Products.Find(id);
            if (product != null)
            {
                var cp = _context.CustomerProductCarts.FirstOrDefault(cp => cp.CustomerId == User.FindFirstValue(ClaimTypes.NameIdentifier) && cp.ProductId == id);
                if (cp != null)
                {
                    _context.CustomerProductCarts.Remove(cp);
                    _context.SaveChanges();
                }
            }
        }

        private bool ProductExists(int id)
        {
            return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
