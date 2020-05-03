using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GestionStages.Properties;
using GestionStages.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace GestionStages.Controllers
{
    public class RestrictionController : Controller
    {
        IRestrictionRepository repo;

        public RestrictionController(IConfiguration configuration):base()
        {
            repo = new repoRestrictionMSSQL(configuration);
        }

        public IActionResult ListeRestriction(string txtTitre = "", string txtDescr = "")
        {
            ViewBag.lesRestrictions = repo.GetRestrictions(txtTitre, txtDescr);
            return View();
        }

        public IActionResult ViewRestriction(int id)
        {
            if (id == 0)
            {
                ViewBag.PageTitle = lang.AjouterRestriction;
                ViewBag.IconTitle = "add_circle";
                ViewBag.IconButton = "send";
                ViewBag.ColorButton = "green";
                ViewBag.TextButton = lang.Creer;
            }
            else
            {
                ViewBag.PageTitle = lang.AjouterUnMilieuDeStage;
                ViewBag.IconTitle = "create";
                ViewBag.Restriction = repo.GetRestrictionByID(id);
                ViewBag.IconButton = "create";
                ViewBag.ColorButton = "orange";
                ViewBag.TextButton = lang.Modifier;
            }
            ViewBag.LinkBack = "../ListeRestriction";
            return View();
        }

        public void SaveRestriction(int id,string txtTitre,string txtDescription, bool ChkEtat)
        {
            repo.SaveRestriction(id, txtTitre, txtDescription, Request.Form["ChkEtat"] == "on");
            Response.Redirect("../ListeRestriction");
        }
    }
}