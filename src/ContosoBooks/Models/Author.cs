using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.Models
{
    public class Author
    {
		[ScaffoldColumn(false)]
		public int Id { get; set; }

		[StringLength(100)]
		[Display(Name = "First Name")]
		public string FirstName { get; set; }

		[Required, StringLength(100)]
		[Display(Name = "Last Name")]
		public string LastName { get; set; }

		public virtual ICollection<Book> Books { get; set; }
	}
}
