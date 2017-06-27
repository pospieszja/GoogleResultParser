using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GoogleResultParser.Web.Services;
using Microsoft.AspNetCore.Mvc;

namespace GoogleResultParser.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index(string q)
        {
            if (String.IsNullOrEmpty(q))
                return View();
            else
            {
                var googleResultService = new GoogleResultService();
                return View(googleResultService.GetResult(q));
            }
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
