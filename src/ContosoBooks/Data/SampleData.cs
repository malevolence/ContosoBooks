using ContosoBooks.Models;
using Microsoft.Data.Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Data
{
    public static class SampleData
    {
		public static void Initialize(IServiceProvider serviceProvider)
		{
			var context = serviceProvider.GetService<SiteContext>();
			context.Database.Migrate();
			if (!context.Book.Any())
			{
				var tolkien = context.Author.Add(new Author { LastName = "Tolkien", FirstName = "J.R.R." }).Entity;
				var asimov = context.Author.Add(new Author { LastName = "Asimov", FirstName = "Isaac" }).Entity;
				var grrm = context.Author.Add(new Author { LastName = "Martin", FirstName = "George R.R." }).Entity;

				context.Book.AddRange(
					new Book { Author = tolkien, Title = "Fellowship of the Ring", Genre = "Fantasy", YearPublished = 1954, Price = 7.99M },
					new Book { Author = tolkien, Title = "The Two Towers", Genre = "Fantasy", YearPublished = 1954, Price = 8.99M },
					new Book { Author = tolkien, Title = "Return of the King", Genre = "Fantasy", YearPublished = 1955, Price = 9.99M },
					new Book { Author = asimov, Title = "Foundation", Genre = "Science Fiction", YearPublished = 1951, Price = 5.99M },
					new Book { Author = asimov, Title = "Foundation and Empire", Genre = "Science Fiction", YearPublished = 1952, Price = 6.99M },
					new Book { Author = asimov, Title = "Second Foundation", Genre = "Science Fiction", YearPublished = 1953, Price = 7.99M },
					new Book { Author = asimov, Title = "Foundation's Edge", Genre = "Science Fiction", YearPublished = 1982, Price = 9.99M },
					new Book { Author = asimov, Title = "Foundation and Earth", Genre = "Science Fiction", YearPublished = 1986, Price = 8.99M },
					new Book { Author = grrm, Title = "A Game of Thrones", Genre = "Fantasy", YearPublished = 1996, Price = 9.99M },
					new Book { Author = grrm, Title = "A Clash of Kings", Genre = "Fantasy", YearPublished = 1999, Price = 9.99M },
					new Book { Author = grrm, Title = "A Storm of Swords", Genre = "Fantasy", YearPublished = 2000, Price = 9.99M },
					new Book { Author = grrm, Title = "A Feast for Crows", Genre = "Fantasy", YearPublished = 2005, Price = 9.99M },
					new Book { Author = grrm, Title = "A Dance with Dragons", Genre = "Fantasy", YearPublished = 2011, Price = 9.99M }
				);

				context.SaveChanges();
			}
		}
    }
}
