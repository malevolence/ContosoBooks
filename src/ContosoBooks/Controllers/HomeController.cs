using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using ContosoBooks.Config;
using Microsoft.Extensions.OptionsModel;
using ContosoBooks.ViewModels;

namespace ContosoBooks.Controllers
{
    public class HomeController : Controller
    {
		private BraintreeSettings braintreeSettings;
		private ConstantContactSettings constantContactSettings;

		public HomeController(IOptions<BraintreeSettings> btSettingsAccessor, IOptions<ConstantContactSettings> ccSettingsAccessor)
		{
			braintreeSettings = btSettingsAccessor.Value;
			constantContactSettings = ccSettingsAccessor.Value;
		}

        public IActionResult Index()
        {
			var model = new HomeVM();
			model.BraintreeSettings = braintreeSettings;
			model.ConstantContactSettings = constantContactSettings;
            return View(model);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
