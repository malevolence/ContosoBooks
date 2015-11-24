using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Models
{
    public class Book
    {
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[Required, StringLength(200)]
		public string Title { get; set; }

		[StringLength(100)]
		public string Genre { get; set; }

		[Display(Name = "Year Published")]
		public int YearPublished { get; set; }

		[Range(1, 500)]
		[DataType(DataType.Currency)]
		public decimal Price { get; set; }

		[ScaffoldColumn(false)]
		[Display(Name = "Author")]
		public int AuthorId { get; set; }

		public virtual Author Author { get; set; }
	}
}
