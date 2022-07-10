using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
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
    public class LoginController : Controller
    {
        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();

        // GET: Login
        public ActionResult Login1()
        {
            return View();
        }

        public ActionResult Login2()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Giriş1(Tblİşveren tblİşveren)
        {
            
            var bilgiler = db.Tblİşveren.FirstOrDefault(
                s => s.KullanıcıAd == tblİşveren.KullanıcıAd &&
                s.Parola == tblİşveren.Parola);
            if(bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAd, false);
                Session["KullanıcıAd"] = bilgiler.KullanıcıAd.ToString();
                Session["FirmaAd"] = bilgiler.FirmaAd.ToString();
                return RedirectToAction("Profil1","İşverenProfil",bilgiler);
                
            }
         
            return RedirectToAction("Login1");
            
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public ActionResult Giriş2(TblİşArayan tblİşArayan)
        {
            var bilgiler = db.TblİşArayan.FirstOrDefault(
                s => s.KullanıcıAdı == tblİşArayan.KullanıcıAdı &&
                s.Parola == tblİşArayan.Parola);
            if (bilgiler != null)
            {
                FormsAuthentication.SetAuthCookie(bilgiler.KullanıcıAdı, false);

                return RedirectToAction("Profil", "İşArayanProfil",bilgiler);
            }

            return RedirectToAction("Login1");

        }
        public ActionResult Sil(TblİşArayan tblİşArayan)
        {
            var bilgiler = db.TblİşArayan.FirstOrDefault(s => s.KullanıcıAdı == tblİşArayan.KullanıcıAdı);
            if (bilgiler != null)
            {
                db.TblİşArayan.Remove(bilgiler);
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

    }
}