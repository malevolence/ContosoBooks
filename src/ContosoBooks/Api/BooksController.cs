using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity;
using ContosoBooks.Data;
using ContosoBooks.Models;

namespace ContosoBooks.Api
{
    [Produces("application/json")]
    [Route("api/books")]
    public class BooksController : Controller
    {
		private readonly IBookstoreRepository db;

        public BooksController(IBookstoreRepository repo)
        {
            db = repo;
        }

        // GET: api/Books
        [HttpGet]
        public IEnumerable<Book> GetBooks()
        {
            return db.Books.OrderBy(x => x.Author.LastName).ThenBy(x => x.Author.FirstName).ThenBy(x => x.YearPublished).ThenBy(x => x.Title);
        }

        // GET: api/Books/5
        [HttpGet("{id}", Name = "GetBook")]
        public IActionResult GetBook([FromRoute] int id)
        {
            Book book = db.GetBook(id);

            if (book == null)
            {
                return HttpNotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        [HttpPut("{id}")]
        public IActionResult PutBook(int id, [FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return HttpBadRequest();
            }

            db.AddOrUpdateBook(book);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return new HttpStatusCodeResult(StatusCodes.Status204NoContent);
        }

        // POST: api/Books
        [HttpPost]
        public IActionResult PostBook([FromBody] Book book)
        {
            if (!ModelState.IsValid)
            {
                return HttpBadRequest(ModelState);
            }

            db.AddOrUpdateBook(book);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (BookExists(book.Id))
                {
                    return new HttpStatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetBook", new { id = book.Id }, book);
        }

        // DELETE: api/Books/5
        [HttpDelete("{id}")]
        public IActionResult DeleteBook(int id)
        {
            db.DeleteBook(id);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return HttpNotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok();
        }

        private bool BookExists(int id)
        {
			var book = db.GetBook(id);
            return book != null;
        }
    }
}