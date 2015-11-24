using ContosoBooks.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Data
{
    public interface IBookstoreRepository
    {
		IQueryable<Book> Books { get; }
		IQueryable<Author> Authors { get; }

		Book GetBook(int id);
		Author GetAuthor(int id);

		void AddOrUpdateBook(Book book);
		void AddOrUpdateAuthor(Author author);

		void DeleteBook(int id);
		void DeleteAuthor(int id);

		void SaveChanges();
    }
}
