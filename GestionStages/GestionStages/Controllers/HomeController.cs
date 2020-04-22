using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionStages.Models;
using System.Data.SqlClient;
using GestionStages.Properties;
using Microsoft.Extensions.Configuration;

namespace GestionStages.Controllers
{
    public class HomeController : Controller
    {
        Repositories.IEtudiantRepository repoEtu;

        public HomeController(IConfiguration configuration) :base()
        {
            repoEtu = new Repositories.repoEtudiantMSSQL(configuration);
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test(int id)
        {
            ViewBag.id = id;
            ViewBag.lesEtudiants = repoEtu.GetAllEtudiants();
            return View();
        }

        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
