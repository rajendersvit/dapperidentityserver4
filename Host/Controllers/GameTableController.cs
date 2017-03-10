using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Host.Controllers
{
    public class GameTableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}