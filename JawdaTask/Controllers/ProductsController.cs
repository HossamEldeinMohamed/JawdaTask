using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using JawdaTask.DTO;
using JawdaTask.Models;
using AutoMapper;
using JawdaTask.Common_Utility.Helpers;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace JawdaTask.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly E_CommerceContext _context;
        private readonly IMapper _mapper;
        private readonly IWebHostEnvironment environment;

        public ProductsController(E_CommerceContext context, IMapper mapper, IWebHostEnvironment environment)
        {
            _context = context;
            _mapper = mapper;
            this.environment = environment;
        }
        // GET: Products
        public async Task<IActionResult> Index()
        
        {
            var e_CommerceContext = _context.Products.Include(p => p.Category);
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            return View(await e_CommerceContext.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Index(string CategoryName) 
        {
            var Category =await _context.Categories.FirstOrDefaultAsync(x => x.Name == CategoryName);
            if (Category == null)
                return View();
            var e_CommerceContext =await _context.Products.Include(p => p.Category).Where(x=> x.CategoryId== Category.CategoryId).ToListAsync();

            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");

            return View(e_CommerceContext);
        }
        // GET: Products/Details/5

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["CategoryName"] = new SelectList(_context.Categories, "Name", "Name");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( ProductDTO productDTO)
        {
            if (ModelState.IsValid)
            {
                string uploadsFolder = Path.Combine(environment.WebRootPath, "images");
                var product = _mapper.Map<Product>(productDTO);
                product.Picture = Upload.UploadedFile(productDTO.Picture, uploadsFolder);
                product.CategoryId = await _context.Categories.Where(x=> x.Name== productDTO.CategoryName).Select(s => s.CategoryId).FirstOrDefaultAsync();
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryName"] = new SelectList(_context.Categories, "CategoryName", "CategoryName", productDTO.CategoryName);
            return View(productDTO);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,Name,Description,CategoryId,Quanity,Note,Price,Picture")] Product product)
        {
            if (id != product.ProductId)
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
                    if (!ProductExists(product.ProductId))
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
            ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryId", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
    }
}
