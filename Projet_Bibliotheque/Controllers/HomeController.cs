using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Projet_Bibliotheque.Models;

namespace Projet_Bibliotheque.Controllers
{
    public class HomeController : Controller
    {
        private SkyBiblioEntities db = new SkyBiblioEntities();


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Recherche(String searchBy, String searching)
        {


            if (searchBy == "Titre")
            {
                return View(db.Livres.Where(x => x.Titre.StartsWith(searching)).ToList());
            }
            else

            {
                return View(db.Livres.Where(x => x.Categorie == searching).ToList());

            }

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}