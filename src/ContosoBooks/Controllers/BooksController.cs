using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ContosoBooks.Data;
using ContosoBooks.Models;

namespace ContosoBooks.Controllers
{
    public class BooksController : Controller
    {
        private SiteContext _context;

        public BooksController(SiteContext context)
        {
            _context = context;    
        }

        // GET: Books
        public IActionResult Index()
        {
            var siteContext = _context.Book.Include(b => b.Author);
            return View(siteContext.ToList());
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = _context.Book.Single(m => m.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Author");
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Book.Add(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Author", book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = _context.Book.Single(m => m.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Author", book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                _context.Update(book);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["AuthorId"] = new SelectList(_context.Set<Author>(), "Id", "Author", book.AuthorId);
            return View(book);
        }

        // GET: Books/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            Book book = _context.Book.Single(m => m.Id == id);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Book book = _context.Book.Single(m => m.Id == id);
            _context.Book.Remove(book);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
