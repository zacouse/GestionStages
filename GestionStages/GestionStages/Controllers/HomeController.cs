using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionStages.Models;
using System.Data.SqlClient;

namespace GestionStages.Controllers
{
    public class HomeController : Controller
    {
        Repositories.repoMSSQL repo = new Repositories.repoMSSQL();
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
            ViewBag.lesEtudiants = repo.getAllEtudiants();
            return View();
        }

        public IActionResult ListeMilieuStage()
        {
            ViewBag.lesMilieus = repo.getAllMilieuStage();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
