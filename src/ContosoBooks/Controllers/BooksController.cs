using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ContosoBooks.Data;
using ContosoBooks.Models;
using System.Collections.Generic;

namespace ContosoBooks.Controllers
{
    public class BooksController : Controller
    {
		private readonly IBookstoreRepository db;

        public BooksController(IBookstoreRepository repo)
        {
			this.db = repo;
        }

        // GET: Books
        public IActionResult Index()
        {
			var books = db.Books.Include(x => x.Author).OrderBy(x => x.Author.LastName).ThenBy(x => x.Author.FirstName).ThenBy(x => x.YearPublished).ThenBy(x => x.Title).ToList();
            return View(books);
        }

        // GET: Books/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var book = db.GetBook(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }

            return View(book);
        }

        // GET: Books/Create
        public IActionResult Create()
        {
            AddAuthorDropdown();
            return View();
        }

        // POST: Books/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Book book)
        {
            if (ModelState.IsValid)
            {
				db.AddOrUpdateBook(book);
				db.SaveChanges();
				TempData["success"] = $"New book added successfully with Id = {book.Id}";
                return RedirectToAction("Index");
            }
            AddAuthorDropdown(book.AuthorId);
            return View(book);
        }

        // GET: Books/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var book = db.GetBook(id.Value);
            if (book == null)
            {
                return HttpNotFound();
            }
            AddAuthorDropdown(book.AuthorId);
            return View(book);
        }

        // POST: Books/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Book book)
        {
            if (ModelState.IsValid)
            {
                db.AddOrUpdateBook(book);
                db.SaveChanges();
				TempData["success"] = $"Changes saved successfully for book with Id = {book.Id}";
                return RedirectToAction("Index");
            }
            AddAuthorDropdown(book.AuthorId);
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

            var book = db.GetBook(id.Value);
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
			db.DeleteBook(id);
			db.SaveChanges();
			TempData["success"] = $"Book with Id = {id} deleted successfully";
            return RedirectToAction("Index");
        }

		private void AddAuthorDropdown(int? authorId = null)
		{
			ViewBag.PossibleAuthors = db.Authors.OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList().Select(x => new SelectListItem
			{
				Text = x.FirstName + " " + x.LastName,
				Value = x.Id.ToString()
			}).ToList();
		}
    }
}
