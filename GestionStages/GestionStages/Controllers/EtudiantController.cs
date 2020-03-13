using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionStages.Controllers
{
    public class EtudiantController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}