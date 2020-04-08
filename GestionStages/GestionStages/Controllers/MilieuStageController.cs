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

        [HttpPost]
        public IActionResult SearchListeMilieuStage(string txtTitre,string txtDescr,string txtAdresse)
        {
            ViewBag.lesMilieus = repo.GetMilieuStage(txtTitre,txtDescr,txtAdresse);
            return View("ListeMilieuStage");
        }
    }
}