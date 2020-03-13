using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GestionStages.Controllers
{
    public class MilieuStageController : Controller
    {
        Repositories.IMilieuStageRepository repo = new Repositories.repoMilieuStageMSSQL();
        public IActionResult ListeMilieuStage()
        {
            ViewBag.lesMilieus = repo.GetAllMilieuStage();
            return View();
        }
    }
}