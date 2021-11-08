using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using JawdaTask.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace JawdaTask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly E_CommerceContext _context;

        public HomeController(ILogger<HomeController> logger, E_CommerceContext context)
        {
            _logger = logger;
            _context = context;
            _context = context;
        }

        public async Task<IActionResult> Index()

        {
            var e_CommerceContext = _context.Products.Include(p => p.Category);
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            return View(await e_CommerceContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string CategoryName)
        {
            var Category = await _context.Categories.FirstOrDefaultAsync(x => x.Name == CategoryName);
            if (Category == null)
                return View();
            var e_CommerceContext = await _context.Products.Include(p => p.Category).Where(x => x.CategoryId == Category.CategoryId).ToListAsync();

            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");

            return View(e_CommerceContext);
        }
               
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }        
    }
}
