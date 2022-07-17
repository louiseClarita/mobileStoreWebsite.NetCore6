using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using mobilestore_website1._1.Data;
using mobilestore_website1._1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

using System.Security.Claims;


namespace mobilestore_website1._1.Controllers
{
    public class OrdersController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserService _userService;
        public OrdersController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
            
        }

       /* public async Task<IActionResult> YourMethodName()
        {
            
         }*/
        private readonly Data.ApplicationDbContext _context;

        public OrdersController(Data.ApplicationDbContext context,IUserService userService)
        {

            _context = context;
            _userService = userService;
        }

        // GET: Orders
        [Authorize(Roles = "Administrator, Clients")]
        public async Task<IActionResult> Index()
        {
             var applicationDbContext = _context.Order.Include(o => o.Product).Include(o => o.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Orders/Details/5
        [Authorize(Roles = "Administrator")]
                           
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        [Authorize(Roles = "Administrator, Clients")]
        public async Task<IActionResult> CreateAsync()
        {
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId");
            // ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId");
            //ViewData["UserId"] = User.Identity.FindFirstValue(ClaimTypes.NameIdentifier);
            //   ViewData["UserId"] = User.Identity;
            ViewBag.userid = _userManager.GetUserId(HttoContext.User);
            ViewData["UserName"] = User.Identity.Name;
            ViewData["UserId"] = _userService.GetUserId();

            // will give the user's userId
            //   System.Diagnostics(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var userName = User.FindFirstValue(ClaimTypes.Name); // will give the user's userName
            Console.WriteLine("Hello, Worldddddddddddddddd!");
            // For ASP.NET Core <= 3.1
            var applicationUser = await _userManager.GetUserAsync(User);
            string userEmail = applicationUser?.Email; // will give the user's Email

            // For ASP.NET Core >= 5.0
            //string userEmail = User.FindFirstValue(ClaimTypes.Email); // will give the user's Email
            // ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id");
            //   ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), User.Identity.Name, "Id");
            //  ViewData["UserName"] = User.Identity.Name;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "Administrator, Clients")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId,OrderDate,OrderBalance,OrderStreet,OrderCity,OrderBuilding,OrderStatus,OrderDeliveredData,ProductId,UserId")] Order order)
        {
            if (ModelState.IsValid)
            {
                _context.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", order.ProductId);
         
            ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", order.UserId);
            //  ViewData["UserId"] = User.FindFirstValue(ClaimTypes.NameIdentifier);
            ViewData["UserName"] = User.Identity;
            return View(order);
        }

        // GET: Orders/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", order.ProductId);
            ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", order.UserId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("OrderId,OrderDate,OrderBalance,OrderStreet,OrderCity,OrderBuilding,OrderStatus,OrderDeliveredData,ProductId,UserId")] Order order)
        {
            if (id != order.OrderId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderId))
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
            ViewData["ProductId"] = new SelectList(_context.Set<Product>(), "ProductId", "ProductId", order.ProductId);
            ViewData["UserId"] = new SelectList(_context.Set<IdentityUser>(), "Id", "Id", order.UserId);
            return View(order);
        }

        // GET: Orders/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Order == null)
            {
                return NotFound();
            }

            var order = await _context.Order
                .Include(o => o.Product)
                .Include(o => o.User)
                .FirstOrDefaultAsync(m => m.OrderId == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Order == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Order'  is null.");
            }
            var order = await _context.Order.FindAsync(id);
            if (order != null)
            {
                _context.Order.Remove(order);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Administrator")]
        private bool OrderExists(int id)
        {
          return (_context.Order?.Any(e => e.OrderId == id)).GetValueOrDefault();
        }
    }
}
/*
public class YourControllerNameController : Controller
{
    private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

    public YourControllerNameController(Microsoft.AspNetCore.Identity.UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<IActionResult> YourMethodName()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier) // will give the user's userId
        var userName = User.FindFirstValue(ClaimTypes.Name) // will give the user's userName

        // For ASP.NET Core <= 3.1
        ApplicationUser applicationUser = await _userManager.GetUserAsync(User);
        string userEmail = applicationUser?.Email; // will give the user's Email

        // For ASP.NET Core >= 5.0
        var userEmail = User.FindFirstValue(ClaimTypes.Email) // will give the user's Email
    }
}

/*
public class AccountController : Controller
{
    private readonly UserManager<IdentityUser> _userManager;

    public AccountController(UserManager<IdentityUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    public async Task<string> GetCurrentUserId()
    {
        IdentityUser usr = await GetCurrentUserAsync();
        return usr?.Id;
    }

    private Task<IdentityUser> GetCurrentUserAsync() => _userManager.GetUserAsync(HttpContext.User);
}
*/