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

        [HttpGet]
        public IActionResult PrintListeStage(string txtTitre,string txtDescription)
        {
            ViewBag.lesStages = repo.GetStage(txtTitre, txtDescription);
            return View();
        }

        [HttpPost]
        public IActionResult SearchListeStage(string txtTitre, string txtDescription)
        {
            ViewBag.lesStages = repo.GetStage(txtTitre, txtDescription);
            return View("ListeStage");
        }
    }
}