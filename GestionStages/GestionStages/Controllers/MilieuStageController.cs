using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Models;
using GestionStages.Properties;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestionStages.Controllers
{
    public class MilieuStageController : Controller
    {
        Repositories.IMilieuStageRepository repo;
        Repositories.IStageRepository repoStage;
        Repositories.IRestrictionRepository repoRestriction;

        public MilieuStageController(IConfiguration configuration) : base()
        {
            repo = new Repositories.repoMilieuStageMSSQL(configuration);
            repoStage = new Repositories.repoStageMSSQL(configuration);
            repoRestriction = new Repositories.repoRestrictionMSSQL(configuration);
        }

        public IActionResult ListeMilieuStage()
        {
            ViewBag.lesMilieus = repo.GetAllMilieuStage();
            return View();
        }

        public IActionResult VisionnerMilieuStage(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.Milieu = repo.GetMilieuStageById(id);
                ViewBag.lesStages = repoStage.GetStagesByIdMilieu(id);
                ViewBag.LesRestrictions = repoRestriction.GetAllMilieuStageRestrictionByIdMilieuStage(id);
                ViewBag.TitrePage = lang.VisionnerUnMilieuDeStage;
                ViewBag.IconeTitre = "remove_red_eye";
                ViewBag.TexteBouton = lang.Creer;
            }
            return View();
        }

        public IActionResult AjouterModifierMilieuStage(int id = 0, bool CreationCopie = false)
        {
            if (CreationCopie)
            {
                ViewBag.Milieu = repo.GetMilieuStageById(id);
                ViewBag.LesRestrictionUtilise = repoRestriction.GetRestrictionIDFromMilieuStageID(id);
                ViewBag.Milieu.IDMilieuStage = 0;
                id = 0;
            }
            if (id == 0)
            {
                ViewBag.TitrePage = lang.AjouterUnMilieuDeStage;
                ViewBag.IconeTitre = "add_circle";
                ViewBag.IconeBouton = "send";
                ViewBag.CouleurBouton = "green";
                ViewBag.TexteBouton = lang.Creer;
                ViewBag.LienRetour = "../MilieuStage/ListeMilieuStage";
                ViewBag.LesRestrictionUtilise = new List<int>();
            }
            else
            {
                ViewBag.TitrePage = lang.ModifierUnMilieuDeStage;
                ViewBag.IconeTitre = "create";
                ViewBag.Milieu = repo.GetMilieuStageById(id);
                ViewBag.IconeBouton = "create";
                ViewBag.CouleurBouton = "orange";
                ViewBag.TexteBouton = lang.Modifier;
                ViewBag.LienRetour = "../VisionnerMilieuStage/" + id;
                ViewBag.LesRestrictionUtilise = repoRestriction.GetRestrictionIDFromMilieuStageID(id);
            }
            ViewBag.LesRestriction = repoRestriction.GetAllRestriction();
            return View();
        }

        public void SaveMilieuStage(int id = 0)
        {
            repo.SaveMilieuStage(new MilieuStage(id, Request.Form["TxtTitre"], Request.Form["TxaDescription"], Request.Form["TxtNoCivique"], Request.Form["TxtRue"], Request.Form["TxtCodePostal"], Request.Form["TxtVille"], Request.Form["TxtProvince"], Request.Form["TxtPays"], Request.Form["TxtNumeroTelephone"], Request.Form["ChkEtat"] == "on"),Request.Form["Restriction"]);
            Response.Redirect("../ListeMilieuStage");
        }

        [HttpPost]
        public IActionResult SearchListeMilieuStage(string txtTitre,string txtAdresse)
        {
            ViewBag.lesMilieus = repo.GetMilieuStage(txtTitre,txtAdresse);
            return View("ListeMilieuStage");
        }
    }
}