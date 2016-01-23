using ContosoBooks.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoBooks.ViewModels
{
    public class HomeVM
    {
		public BraintreeSettings BraintreeSettings { get; set; }
		public ConstantContactSettings ConstantContactSettings { get; set; }
	}
}
