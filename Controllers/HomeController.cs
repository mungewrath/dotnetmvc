using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dotnetmvctest.Controllers
{
    public class HomeController : Controller
    {
        private readonly int TimesBetterThanWindows;
        private readonly string MyString;
        private IBusinessMan businessBro;
        public HomeController(IOptions<TestConfig> config, IBusinessMan businessMan)
        {
            MyString = config.Value.SampleString;
            TimesBetterThanWindows = config.Value.SampleInt;
            businessBro = businessMan;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page." + MyString;

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = string.Format("New MVC is {0} times better than the old one. You can get it for only ${1}",
               TimesBetterThanWindows,
               businessBro.GetSwag());

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
