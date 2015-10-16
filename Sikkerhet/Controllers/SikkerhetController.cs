using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sikkerhet.Models;

namespace Sikkerhet.Controllers
{
    public class SikkerhetController : Controller
    {
        // GET: Sikkerhet
        public ActionResult Index()
        {
            // vis innlogging
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }
        public ActionResult Login()
        {
            // vis innlogging
            if (Session["LoggetInn"] == null)
            {
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggetInn"];
            }
            return View();
        }
        [HttpPost]
        public ActionResult Login(bruker innLogget)
        {
            // sjekk om innlogging OK
            if (bruker_i_db(innLogget))
            {
                // ja brukernavn og passord er OK!
                Session["LoggetInn"] = true;
                ViewBag.Innlogget = true;
                return View();
            }
            else
            {
                // ja brukernavn og passord er OK!
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }
        [HttpPost]
        public ActionResult Index(bruker innLogget)
        {
            // sjekk om innlogging OK
            if (bruker_i_db(innLogget))
            {
                // ja brukernavn og passord er OK!
                Session["LoggetInn"] = true;
                ViewBag.Innlogget = true;
                return View();
            }
            else
            {
                // ja brukernavn og passord er OK!
                Session["LoggetInn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }
        public ActionResult Registrer()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Registrer(bruker innBruker)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            using (var db = new BrukerContext())
            {
                try
                {
                    var nyBruker = new dbBruker();
                    byte[] passordDB = lagHash(innBruker.Passord);
                    nyBruker.Navn = innBruker.Navn;
                    nyBruker.Passord = passordDB;
                    db.Brukere.Add(nyBruker);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch
                {
                    return View();
                }
            }
        }
        private static byte[] lagHash(string innPassord)
        {
            byte[] innData, utData;
            var algoritme = System.Security.Cryptography.SHA256.Create();
            innData = System.Text.Encoding.ASCII.GetBytes(innPassord);
            utData = algoritme.ComputeHash(innData);
            return utData;
        }

        private static bool bruker_i_db(bruker innBruker)
        {
            using (var db = new BrukerContext())
            {
                byte[] passordDB = lagHash(innBruker.Passord);
                dbBruker funnetBruker = db.Brukere.FirstOrDefault(
                    b => b.Passord == passordDB && b.Navn == innBruker.Navn);
                if (funnetBruker == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public ActionResult InnloggetSide()
        {
            if (Session["LoggetInn"] != null)
            {
                bool loggetInn = (bool)Session["LoggetInn"];
                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoggUt()
        {
            Session["LoggetInn"] = false;
            return RedirectToAction("index");
        }
    }
}