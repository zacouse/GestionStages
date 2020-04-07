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
    public class StageController : Controller
    {
        Repositories.IStageRepository repo = new Repositories.repoStageMSSQL();
        public IActionResult ListeStage()
        {
            ViewBag.lesStages = repo.GetAllStage();
            return View();
        }
        public IActionResult AddSetStage()
        {
            return View();
        }
        public void SaveStage(int id)
        {
            //DateTime parsedDateDebut = DateTime.Parse(debut);
            repo.AddSetStage(new Stage(id, Convert.ToInt32(Request.Form["TxtMilieuStage"]), Request.Form["TxtTitre"], Request.Form["TxtaDescription"],Convert.ToInt32(Request.Form["TxtNbPostes"]), Convert.ToInt32(Request.Form["TxtStatut"]), Convert.ToInt32(Request.Form["TxtPeriodeTravail"]), Convert.ToInt32(Request.Form["TxtNombreDHeureParSemaine"]), DateTime.Parse(Request.Form["TxtDateDeDebut"]), DateTime.Parse(Request.Form["TxtDateDeFin"]), Request.Form["ChkEtat"] == "on")); 
            Response.Redirect("../ListeStage");
        }
    }
}