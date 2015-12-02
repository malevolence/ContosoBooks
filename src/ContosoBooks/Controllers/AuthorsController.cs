 using System.Linq;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Mvc.Rendering;
using Microsoft.Data.Entity;
using ContosoBooks.Data;
using ContosoBooks.Models;

namespace ContosoBooks.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly IBookstoreRepository db;

		public AuthorsController(IBookstoreRepository repo)
		{
			this.db = repo;
		}

        // GET: Authors
        public IActionResult Index()
        {
			var authors = db.AuthorsIncluding(x => x.Books).OrderBy(x => x.LastName).ThenBy(x => x.FirstName).ToList();
            return View(authors);
        }

        // GET: Authors/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var author = db.GetAuthor(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        // GET: Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Authors/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Author author)
        {
            if (ModelState.IsValid)
            {
				db.AddOrUpdateAuthor(author);
				db.SaveChanges();
				TempData["success"] = $"New author added with Id = {author.Id}";
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var author = db.GetAuthor(id.Value);

            if (author == null)
            {
                return HttpNotFound();
            }
            return View(author);
        }

        // POST: Authors/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Author author)
        {
            if (ModelState.IsValid)
            {
				db.AddOrUpdateAuthor(author);
				db.SaveChanges();
				TempData["success"] = $"Changes to author with Id = {author.Id} saved successfully";
                return RedirectToAction("Index");
            }
            return View(author);
        }

        // GET: Authors/Delete/5
        [ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }

            var author = db.GetAuthor(id.Value);
            if (author == null)
            {
                return HttpNotFound();
            }

            return View(author);
        }

        // POST: Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
			db.DeleteAuthor(id);
			db.SaveChanges();
			TempData["success"] = $"Author with Id = {id} deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
