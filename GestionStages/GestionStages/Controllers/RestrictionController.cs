using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IActionResult ViewRestriction()
        {
            return View();
        }
    }
}