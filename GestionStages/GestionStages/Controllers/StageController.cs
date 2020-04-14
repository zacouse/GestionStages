using System;
using System.Collections.Generic;
using System.Globalization;
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
        public IActionResult PrintListeStage(string txtTitre, string txtDescription, string txtMilieu, int txtMinH, int txtMaxH, string txtMinDate, string txtMaxDate)
        {
            //DateTime minDate = txtMinDate == null ? DateTime.MinValue : DateTime.Parse(txtMinDate);
            //DateTime maxDate = txtMaxDate == null ? DateTime.MaxValue : DateTime.Parse(txtMaxDate);
            ViewBag.lesStages = repo.GetStage(txtTitre, txtDescription, txtMilieu, txtMinH, txtMaxH, txtMinDate, txtMaxDate);
            return View();
        }

        [HttpPost]
        public IActionResult SearchListeStage(string txtTitre, string txtDescription, string txtMilieu, int txtMinH, int txtMaxH, string txtMinDate, string txtMaxDate)
        {
            //DateTime minDate = (txtMinDate == null) ? DateTime.MinValue : DateTime.Parse(txtMinDate);
            //DateTime maxDate = (txtMaxDate == null) ? DateTime.Now.AddYears(10) : DateTime.Parse(txtMaxDate);
            ViewBag.lesStages = repo.GetStage(txtTitre?.ToString() ?? "", txtDescription?.ToString() ?? "", txtMilieu?.ToString() ?? "", txtMinH, txtMaxH, txtMinDate, txtMaxDate);
            return View("ListeStage");
        }
    }
}