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
        Repositories.IMilieuStageRepository repoMilieu = new Repositories.repoMilieuStageMSSQL();
        public IActionResult ListeStage()
        {
            ViewBag.lesStages = repo.GetAllStage();
            ViewBag.lesMilieus = repoMilieu.GetAllMilieuStage();
            return View();
        }
        public IActionResult AddSetStage(int idStage = 0, bool Duplicate = false)
        {
            if (Duplicate)
            {
                ViewBag.lesStages = repo.GetStageByID(idStage);
                ViewBag.lesStages.IDStage = 0;
                idStage = 0;
            }
            if (idStage == 0)
            {
                ViewBag.lesMilieus = repoMilieu.GetAllMilieuStage();
                ViewBag.PageTitle = lang.AjouterUnStage;
                ViewBag.IconTitle = "add_circle";
                ViewBag.IconButton = "send";
                ViewBag.ColorButton = "green";
                ViewBag.TextButton = lang.Creer;
                ViewBag.LinkBack = "../ListeStage";
            }
            else
            {
                ViewBag.PageTitle = lang.ModifierUnStage;
                ViewBag.IconTitle = "create";
                ViewBag.LeStage = repo.GetStageByID(idStage);
                //ViewBag.LeMilieu = repoMilieu.GetMilieuStageById(idMilieu);
                ViewBag.IconButton = "create";
                ViewBag.ColorButton = "orange";
                ViewBag.TextButton = lang.Modifier;
                ViewBag.LinkBack = "../ListeStage/" + idStage;
            }
            return View();
        }

        public IActionResult VisionnerStage(int id = 0)
        {
            ViewBag.PageTitle = lang.VisionnerUnStage;
            ViewBag.IconTitle = "remove_red_eye";
            ViewBag.leStage = repo.GetStageByID(id);
            ViewBag.IconButton = "create";
            ViewBag.ColorButtonModify = "orange";
            ViewBag.TextButton = lang.Modifier;
            ViewBag.IconButtonCopy = "content_copy";
            ViewBag.ColorButtonCopy = "Blue";
            ViewBag.ColorButton = "grey";
            return View();
        }
        public void SaveStage(int id)
        {
            repo.SaveStage(new Stage(id, Convert.ToInt32(Request.Form["TxtMilieuStage"]), Request.Form["TxtTitre"], Request.Form["TxtaDescription"],Convert.ToInt32(Request.Form["TxtNbPostes"]), Convert.ToInt32(Request.Form["TxtStatut"]), Convert.ToInt32(Request.Form["TxtPeriodeTravail"]), Convert.ToInt32(Request.Form["TxtNombreDHeureParSemaine"]), DateTime.Parse(Request.Form["TxtDateDeDebut"]), DateTime.Parse(Request.Form["TxtDateDeFin"]), Request.Form["ChkEtat"] == "on")); 
            Response.Redirect("../ListeStage");
        }
    }
}