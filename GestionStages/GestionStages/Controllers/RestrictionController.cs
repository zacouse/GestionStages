using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionStages.Controllers
{
    public class RestrictionController : Controller
    {
        public IActionResult ListeRestriction()
        {
            return View();
        }

        public IActionResult ViewRestriction()
        {
            return View();
        }
    }
}