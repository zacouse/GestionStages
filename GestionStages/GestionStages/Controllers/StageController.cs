using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GestionStages.Models;
using GestionStages.Properties;
using Microsoft.AspNetCore.Mvc.Rendering;using static GestionStages.Models.Stage;using Microsoft.Extensions.Configuration;

namespace GestionStages.Controllers
{
    public class StageController : Controller
    {
        Repositories.IStageRepository repo;
        Repositories.IMilieuStageRepository repoMilieu;
        Repositories.IRestrictionRepository repoRestriction;
        public StageController(IConfiguration configuration) : base()
        {
            repo = new Repositories.repoStageMSSQL(configuration);
            repoMilieu = new Repositories.repoMilieuStageMSSQL(configuration);
            repoRestriction = new Repositories.repoRestrictionMSSQL(configuration);
        }

        public IActionResult AddSetStage(int idStage = 0, bool Duplicate = false)
        {

            if (Duplicate)
            {
                ViewBag.LeStage = repo.GetStageByID(idStage);
                ViewBag.LesRestrictionUtilise = repoRestriction.GetRestrictionIDFromStageID(idStage);
                ViewBag.LeStage.IDStage = 0;
                idStage = 0;
            }
            if (idStage == 0)
            {
                ViewBag.PageTitle = lang.AjouterUnStage;
                ViewBag.IconTitle = "add_circle";
                ViewBag.IconButton = "send";
                ViewBag.ColorButton = "green";
                ViewBag.TextButton = lang.Creer;
                ViewBag.LinkBack = "../Stage/ListeStage";
                ViewBag.LesRestrictionUtilise = new List<int>();
            }
            else
            {
                ViewBag.PageTitle = lang.ModifierUnStage;
                ViewBag.IconTitle = "create";
                ViewBag.LeStage = repo.GetStageByID(idStage);
                ViewBag.IconButton = "create";
                ViewBag.ColorButton = "orange";
                ViewBag.TextButton = lang.Modifier;
                ViewBag.LinkBack = "../Stage/VisionnerStage/" + idStage;
                ViewBag.LesRestrictionUtilise = repoRestriction.GetRestrictionIDFromStageID(idStage);
            }
            ViewBag.lesMilieus = repoMilieu.GetAllMilieuStage();
            ViewBag.LesRestriction = repoRestriction.GetAllRestriction();
            return View();
        }

        public IActionResult VisionnerStage(int id = 0, bool isStudent = false)
        {
            ViewBag.PageTitle = lang.VisionnerUnStage;
            ViewBag.IconTitle = "remove_red_eye";
            ViewBag.LeStage = repo.GetStageByID(id);
            ViewBag.LesRestrictions = repoRestriction.GetAllStageRestrictionByIdStage(id);
            ViewBag.ColorButtonBack = "grey";
            ViewBag.TextButtonBack = lang.Retour;
            ViewBag.ColorButtonCopy = "blue";
            ViewBag.IconButtonCopy = "content_copy";            ViewBag.TextButtonCopy = lang.CreerUneCopie;            ViewBag.ColorButtonModify = "orange";            ViewBag.IconButtonModify = "create";            ViewBag.TextButtonModify = lang.Modifier;
            ViewBag.isStudent = isStudent;
            return View();
        }

        public void SaveStage(int id)
        {            repo.SaveStage(new Stage(id, Convert.ToInt32(Request.Form["TxtMilieuStage"]), Request.Form["TxtTitre"], Request.Form["TxtaDescription"], Convert.ToInt32(Request.Form["TxtNbPostes"]), Convert.ToInt32(Request.Form["TxtStatut"]), Convert.ToInt32(Request.Form["TxtPeriodeTravail"]), Convert.ToInt32(Request.Form["TxtNombreDHeureParSemaine"]), DateTime.Parse(Request.Form["TxtDateDeDebut"]), DateTime.Parse(Request.Form["TxtDateDeFin"]), Request.Form["ChkEtat"] == "on"), Request.Form["Restriction"]);
            Response.Redirect("../ListeStage");
        }

        public IActionResult ListeStage(bool isStudent = false, string IdEtudiant = "")
        {
            if (isStudent)            {                ViewBag.lesStages = repo.GetAllStageActif();            }            else            {                ViewBag.lesStages = repo.GetAllStage();            }

            ViewBag.isStudent = isStudent;
            ViewBag.IdEtudiant = IdEtudiant;
            if (isStudent && IdEtudiant != null)            {                AfficherChoixEtudiant(IdEtudiant);            }            return View();
        }
        private void AfficherChoixEtudiant(string IdEtudiant)        {            if (int.TryParse(IdEtudiant, out int intIdEtudiant))            {
                List<ChoixStageEtudiant> choixStages = repo.getChoixStage(IdEtudiant);
                if (choixStages.Count > 0)                {
                    ViewBag.Choix1 = choixStages.Where(c => c.NumeroChoix == 1).Select(c => c.getIDStage()).FirstOrDefault();                    ViewBag.Choix2 = choixStages.Where(c => c.NumeroChoix == 2).Select(c => c.getIDStage()).FirstOrDefault();                    ViewBag.Choix3 = choixStages.Where(c => c.NumeroChoix == 3).Select(c => c.getIDStage()).FirstOrDefault();                }            }        }
        [HttpGet]
        public IActionResult PrintListeStage(string txtTitre, string txtDescription, string txtMilieu, int txtMinH, int txtMaxH, string txtMinDate, string txtMaxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive)        {            ViewBag.lesStages = repo.GetStage(txtTitre, txtDescription, txtMilieu, txtMinH, txtMaxH, txtMinDate, txtMaxDate, chkIsJour, chkIsSoir, chkIsNuit, chkIsActive, chkIsInactive);            return View();        }

        [HttpPost]
        public IActionResult SearchListeStage(string txtTitre, string txtDescription, string txtMilieu, int txtMinH, int txtMaxH, string txtMinDate, string txtMaxDate, bool chkIsJour, bool chkIsSoir, bool chkIsNuit, bool chkIsActive, bool chkIsInactive, bool isStudent = false, string IdEtudiant = "")        {            if (isStudent)            {                chkIsActive = true;                chkIsInactive = false;            }
            ViewBag.lesStages = repo.GetStage(txtTitre?.ToString() ?? "", txtDescription?.ToString() ?? "", txtMilieu?.ToString() ?? "", txtMinH, txtMaxH, txtMinDate, txtMaxDate, chkIsJour, chkIsSoir, chkIsNuit, chkIsActive, chkIsInactive);            ViewBag.isStudent = isStudent;            if (isStudent && IdEtudiant != null)            {                AfficherChoixEtudiant(IdEtudiant);            }            return View("ListeStage");        }

        [HttpPost]
        public void AddSetChoixStage(string Choix1, string Choix2, string Choix3, string IdEtudiant, bool isStudent = false)        {            if (int.TryParse(Choix1, out int Choix1Int))            {                repo.SaveChoixStage(0, Choix1Int, Convert.ToInt32(IdEtudiant), 1, false, true);            }            else            {                repo.RemoveChoixStage(Convert.ToInt32(IdEtudiant), 1);            }
            if (int.TryParse(Choix2, out int Choix2Int))            {                repo.SaveChoixStage(0, Choix2Int, Convert.ToInt32(IdEtudiant), 2, false, true);            }            else            {                repo.RemoveChoixStage(Convert.ToInt32(IdEtudiant), 2);            }
            if (int.TryParse(Choix3, out int Choix3Int))            {                repo.SaveChoixStage(0, Choix3Int, Convert.ToInt32(IdEtudiant), 3, false, true);            }            else            {                repo.RemoveChoixStage(Convert.ToInt32(IdEtudiant), 3);            }
            ViewBag.lesStages = repo.GetAllStage();            ViewBag.isStudent = isStudent;
            if (isStudent && IdEtudiant != null)            {                AfficherChoixEtudiant(IdEtudiant);            }
            Response.Redirect("../Stage/ListeStage");        }
    }
}