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
    public class HomeController : Controller
    {
        Repositories.RepoMSSQL repo = new Repositories.RepoMSSQL();
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test(int id)
        {
            ViewBag.id = id;
            ViewBag.lesEtudiants = repo.getAllEtudiants();
            return View();
        }

        public IActionResult ListeMilieuStage()
        {
            ViewBag.lesMilieus = repo.getAllMilieuStage();
            return View();
        }

        public IActionResult VisionnerMilieuStage(int id = 0)
        {
            if (id != 0)
            {
                ViewBag.Milieu = repo.getMilieuStage(id);
                ViewBag.TitrePage = lang.VisionnerunMilieuDeStage;
                ViewBag.IconeTitre = "remove_red_eye";
                ViewBag.TexteBouton = lang.Creer;
            }
            return View();
        }

        public IActionResult AjouterModifierMilieuStage(int id=0,bool CreationCopie = false)
        {
            if (CreationCopie)
            {
                ViewBag.Milieu = repo.getMilieuStage(id);
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
                ViewBag.LienRetour = "../Home/ListeMilieuStage";
            }
            else
            {
                ViewBag.TitrePage = lang.ModifierUnMilieuDeStage;
                ViewBag.IconeTitre = "create";
                ViewBag.Milieu = repo.getMilieuStage(id);
                ViewBag.IconeBouton = "create";
                ViewBag.CouleurBouton = "orange";
                ViewBag.TexteBouton = lang.Modifier;
                ViewBag.LienRetour = "../VisionnerMilieuStage/"+id;
            }
            return View();
        }

        public void SaveMilieuStage(int id = 0)
        {
            repo.SaveMilieuStage( new MilieuStage(id, Request.Form["TxtTitre"], Request.Form["TxaDescription"], Request.Form["TxtNoCivique"], Request.Form["TxtRue"], Request.Form["TxtCodePostal"], Request.Form["TxtVille"], Request.Form["TxtProvince"], Request.Form["TxtPays"], Request.Form["TxtNumeroTelephone"], Request.Form["ChkEtat"] == "on"));
            Response.Redirect("../ListeMilieuStage");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
