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
        public IActionResult ListeStage(int id = 0)
        {
            ViewBag.lesStages = repo.GetAllStage();
            ViewBag.leMilieu = repo.GetMilieuStageForStage(id);
            if (id != 0)
            {
                
            }
            return View();
        }
        public IActionResult AddSetStage(int id = 0, bool Duplicate = false)
        {
            if (Duplicate)
            {
                ViewBag.lesStages = repo.GetStageByID(id);
                ViewBag.lesStages.IDStage = 0;
                id = 0;
            }
            if (id == 0)
            {
                ViewBag.PageTitle = lang.AddStage;
                ViewBag.IconTitle = "add_circle";
                ViewBag.IconButton = "send";
                ViewBag.ColorButton = "green";
                ViewBag.TextButton = lang.Create;
                ViewBag.LinkBack = "../ListeStage";
            }
            else
            {
                ViewBag.PageTitle = lang.ModifyStage;
                ViewBag.IconTitle = "create";
                ViewBag.LeStage = repo.GetStageByID(id);
                ViewBag.IconButton = "create";
                ViewBag.ColorButton = "orange";
                ViewBag.TextButton = lang.Modifier;
                ViewBag.LinkBack = "../ListeStage/" + id;
            }
            return View();
        }

        public IActionResult VisionnerStage(int id = 0)
        {
            ViewBag.PageTitle = lang.ModifyStage;
            ViewBag.IconTitle = "create";
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