using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tez.Models;
using Newtonsoft.Json;
using tez.Controllers;
using System.Web.Helpers;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ionic.Zip;
using System.IO;
using System.Text;
using Xceed.Words.NET;
using System.Web.Security;

namespace tez.Controllers
{
    public class İşverenProfilController : Controller
    {
        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();

        // GET: İşverenProfil
        [Authorize]
        public ActionResult Index(Tblİşveren tblİşveren)
        {
            var bilgiler = db.Tblİşveren.FirstOrDefault(
              s => s.FirmaAd == tblİşveren.FirmaAd);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAd, false);
                ViewBag.firmaAd = bilgiler.FirmaAd;
                ViewBag.hakkında = bilgiler.Hakkında;
                ViewBag.misyon = bilgiler.Misyon;
                ViewBag.adres = bilgiler.Adres;
                ViewBag.iletisim = bilgiler.İletişim;


                return View(bilgiler);


            }
            return View();

        }
        [Authorize]
        public ActionResult Profil1(Tblİşveren tblİşveren)
        {
            var bilgiler = db.Tblİşveren.FirstOrDefault(
               s => s.KullanıcıAd == tblİşveren.KullanıcıAd &&
               s.Parola == tblİşveren.Parola);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAd, false);
                ViewBag.firmaAd = bilgiler.FirmaAd;
                ViewBag.hakkında = bilgiler.Hakkında;
                ViewBag.sektor = bilgiler.Sektör;
                ViewBag.misyon = bilgiler.Misyon;
                ViewBag.adres = bilgiler.Adres;
                ViewBag.iletisim = bilgiler.İletişim;
                ViewBag.id = bilgiler.ID;
                

                return View();
               

            }
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Güncelle([Bind(Include = "FirmaAd,Hakkında,Misyon,Kuruluş,İletişim,Adres,Sektör")] Tblİşveren tblİşveren)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            var bilgiler = db.Tblİşveren.FirstOrDefault(s => s.FirmaAd == tblİşveren.FirmaAd);
            if(bilgiler != null)
            {
                bilgiler.FirmaAd = tblİşveren.FirmaAd;
                bilgiler.Hakkında = tblİşveren.Hakkında;
                bilgiler.Misyon = tblİşveren.Misyon;
                bilgiler.Kuruluş = tblİşveren.Kuruluş;
                bilgiler.İletişim = tblİşveren.İletişim;
                bilgiler.Adres = tblİşveren.Adres;
                bilgiler.Sektör = tblİşveren.Sektör;
                db.SaveChanges();
                return RedirectToAction("Profil1", "İşverenProfil", bilgiler);
            }
            return RedirectToAction("Index");
         
     
        }

        public ActionResult Keşfet()
        {
            return RedirectToAction("Keşfet1", "Keşfet");
        }

    }
}
