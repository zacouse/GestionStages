using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionStages.Models;
using System.Data.SqlClient;
using GestionStages.Properties;

namespace GestionStages.Controllers
{
    public class HomeController : Controller
    {
        Repositories.RepoMSSQL repo = new Repositories.RepoMSSQL();
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

        public IActionResult AjouterModifierMilieuStage(int id=0)
        {
             
            if (id != 0)
            {
                ViewBag.TitrePage = lang.ModifierUnMilieuDeStage;
                ViewBag.Milieu = repo.getMilieuStage(id);
            }
            else
            {
                ViewBag.TitrePage = lang.AjouterUnMilieuDeStage;
            }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
