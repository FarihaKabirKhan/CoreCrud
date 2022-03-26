using Crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crud.Controllers
{
    public class BookController : Controller
    {
        private readonly BookContext _context;

        public BookController(BookContext context)
        {
            _context = context; ;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Books.ToListAsync());
        }



        public IActionResult CreateEdit(int id = 0)
        {
            if (id == 0)
                return View(new Book());
            else
                return View(_context.Books.Find(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEdit(Book book)
        {
            if (ModelState.IsValid)
            {
                if (book.BookID == 1)
                    _context.Add(book);
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public async Task<IActionResult> delete(int? id)
        {
            var emp = await _context.Books.FindAsync(id);
            _context.Books.Remove(emp);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

