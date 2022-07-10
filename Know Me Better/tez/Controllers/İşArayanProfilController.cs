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
    public class İşArayanProfilController : Controller
    {
        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();
        // GET: İşArayanProfil
        [Authorize]
        public ActionResult Index(TblİşArayan tblİşArayan)
        {
            var bilgiler = db.TblİşArayan.FirstOrDefault(s => s.KullanıcıAdı == tblİşArayan.KullanıcıAdı);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAdı, false);
                ViewBag.kullanıcı = bilgiler.KullanıcıAdı;
                ViewBag.Ad = bilgiler.Ad;
                ViewBag.Soyad = bilgiler.Soyad;
                ViewBag.dtarihi = bilgiler.D_Tarihi;
                ViewBag.hakkında = bilgiler.Hakkında;
                ViewBag.egitim = bilgiler.Egitim;
                ViewBag.hedef = bilgiler.Hedefler;
                ViewBag.tecrube = bilgiler.Tecrube;
                ViewBag.yetenek1 = bilgiler.Pc_yetenek;
                ViewBag.yetenek2 = bilgiler.Dil_yetenek;
                ViewBag.sertifika = bilgiler.Sertifika;
                ViewBag.uyelik = bilgiler.Uyelik;
                ViewBag.hobi = bilgiler.Hobiler;
                ViewBag.adres = bilgiler.Adres;
                ViewBag.iletisim = bilgiler.İletisim;

                return View(bilgiler);
            }
            return View();
        }
        [Authorize]
        public ActionResult Profil(TblİşArayan tblİşArayan)
        {
            var bilgiler = db.TblİşArayan.FirstOrDefault(s => s.KullanıcıAdı == tblİşArayan.KullanıcıAdı &&
                                                           s.Parola == tblİşArayan.Parola);

            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAdı, false);
                ViewBag.kullanıcı = bilgiler.KullanıcıAdı;
                ViewBag.Ad = bilgiler.Ad;
                ViewBag.Soyad = bilgiler.Soyad;
                ViewBag.dtarihi = bilgiler.D_Tarihi;
                ViewBag.hakkında = bilgiler.Hakkında;
                ViewBag.egitim = bilgiler.Egitim;
                ViewBag.hedef = bilgiler.Hedefler;
                ViewBag.tecrube = bilgiler.Tecrube;
                ViewBag.yetenek1 = bilgiler.Pc_yetenek;
                ViewBag.yetenek2 = bilgiler.Dil_yetenek;
                ViewBag.sertifika = bilgiler.Sertifika;
                ViewBag.uyelik = bilgiler.Uyelik;
                ViewBag.hobi = bilgiler.Hobiler;
                ViewBag.adres = bilgiler.Adres;
                ViewBag.iletisim = bilgiler.İletisim;

                return View();
            }
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Güncelle([Bind(Include = "Ad,Soyad,KullanıcıAdı,D_Tarihi,Hakkında,Egitim,Hedefler,Tecrube,Pc_yetenek,Dil_yetenek,Sertifika,Uyelik,Hobiler,Adres,İletisim")] TblİşArayan tblİşArayan)
        {
            if (!ModelState.IsValid) { return RedirectToAction("Index"); }
            var bilgiler = db.TblİşArayan.FirstOrDefault(s => s.KullanıcıAdı == tblİşArayan.KullanıcıAdı);
            if (bilgiler != null)
            {
                bilgiler.KullanıcıAdı = tblİşArayan.KullanıcıAdı;
                bilgiler.Ad = tblİşArayan.Ad;
                bilgiler.Soyad = tblİşArayan.Soyad;
                bilgiler.D_Tarihi = tblİşArayan.D_Tarihi;
                bilgiler.Hakkında = tblİşArayan.Hakkında;
                bilgiler.Egitim = tblİşArayan.Egitim;
                bilgiler.Hedefler = tblİşArayan.Hedefler;
                bilgiler.Tecrube = tblİşArayan.Tecrube;
                bilgiler.Pc_yetenek = tblİşArayan.Pc_yetenek;
                bilgiler.Dil_yetenek = tblİşArayan.Dil_yetenek;
                bilgiler.Sertifika = tblİşArayan.Sertifika;
                bilgiler.Uyelik = tblİşArayan.Uyelik;
                bilgiler.Hobiler = tblİşArayan.Hobiler;
                bilgiler.Adres = tblİşArayan.Adres;
                bilgiler.İletisim = tblİşArayan.İletisim;
                db.SaveChanges();
                return RedirectToAction("Profil", "İşArayanProfil", bilgiler);
            }
            return RedirectToAction("Index");
        }  
        
        public ActionResult Keşfet(TblİşArayan tblİşArayan)
        {
            return RedirectToAction("Keşfet2", "Keşfet2",tblİşArayan);
        }
    }


}