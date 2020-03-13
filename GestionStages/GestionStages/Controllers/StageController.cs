using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionStages.Controllers
{
    public class StageController : Controller
    {
        Repositories.IStageRepository repo = new Repositories.repoStageMSSQL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ListeStage()
        {
            ViewBag.lesStages = repo.GetAllStage();
            return View();
        }
    }
}