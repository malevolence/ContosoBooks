using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using ContosoBooks.Models;
using Microsoft.Data.Entity;

namespace ContosoBooks.Data
{
	public class EFBookstoreRepository : IBookstoreRepository
	{
		private readonly SiteContext context;

		public EFBookstoreRepository(SiteContext context)
		{
			this.context = context;
		}

		public IQueryable<Author> Authors
		{
			get
			{
				return context.Authors;
			}
		}

		public IQueryable<Book> Books
		{
			get
			{
				return context.Books;
			}
		}

		public IQueryable<Book> BooksIncluding(params Expression<Func<Book, object>>[] includeProperties)
		{
			IQueryable<Book> query = context.Books;

			foreach (var includeProp in includeProperties)
			{
				query = query.Include(includeProp);
			}

			return query;
		}

		public IQueryable<Author> AuthorsIncluding(params Expression<Func<Author, object>>[] includeProperties)
		{
			IQueryable<Author> query = context.Authors;

			foreach (var includeProp in includeProperties)
			{
				query = query.Include(includeProp);
			}

			return query;
		}

		public Author GetAuthor(int id)
		{
			return context.Authors.SingleOrDefault(x => x.Id == id);
		}

		public Book GetBook(int id)
		{
			return context.Books.Include(x => x.Author).SingleOrDefault(x => x.Id == id);
		}

		public void AddOrUpdateAuthor(Author author)
		{
			if (author.Id == default(int))
				context.Authors.Add(author);
			else
				context.Entry(author).State = EntityState.Modified;
		}

		public void AddOrUpdateBook(Book book)
		{
			if (book.Id == default(int))
				context.Books.Add(book);
			else
				context.Entry(book).State = EntityState.Modified;
		}

		public void DeleteAuthor(int id)
		{
			var author = GetAuthor(id);
			context.Authors.Remove(author);
		}

		public void DeleteBook(int id)
		{
			var book = GetBook(id);
			context.Books.Remove(book);
		}

		public void SaveChanges()
		{
			context.SaveChanges();
		}
	}
}
