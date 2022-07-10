using Newtonsoft.Json;
using PagedList;
using tez.Controllers;
using tez.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace tez.Controllers
{
    public class Keşfet1Controller : Controller
    {
        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();
        // GET: Keşfet
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Keşfet1(string ara, int sayfa = 1)
        {
            var aranacak = from d in db.TblİşArayan select d;


            if (!string.IsNullOrEmpty(ara))
            {

                aranacak = aranacak.Where(m => m.Ad.Contains(ara) || m.Soyad.Contains(ara));

            }

            return View(aranacak.ToList().ToPagedList(sayfa, 50));
            //return View(db.TblİşArayan.ToList());
        }

        public ActionResult Details2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TblİşArayan tblİşArayan = db.TblİşArayan.Find(id);
            if (tblİşArayan == null)
            {
                return HttpNotFound();
            }
            return View(tblİşArayan);
        }

        public ActionResult Firmalar(string ara, int sayfa=1)
        {
            var aranacak = from d in db.Tblİşveren select d;



            if (!string.IsNullOrEmpty(ara))
            {

                aranacak = aranacak.Where(m => m.KullanıcıAd.Contains(ara) || m.FirmaAd.Contains(ara) || m.Adres.Contains(ara) || m.Sektör.Contains(ara));

            }
            return View(aranacak.ToList().ToPagedList(sayfa, 50));
        }

        public ActionResult Details1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tblİşveren tblİşveren = db.Tblİşveren.Find(id);
            if (tblİşveren == null)
            {
                return HttpNotFound();
            }
            return View(tblİşveren);
        }

        public ActionResult İlanlar(string ara, int sayfa = 1)
        {
            var aranacak = from d in db.Tbl_İlan select d;



            if (!string.IsNullOrEmpty(ara))
            {

                aranacak = aranacak.Where(m => m.Baslik.Contains(ara) || m.Pozisyon.Contains(ara) || m.Adres.Contains(ara) || m.ilan_sahibi.Contains(ara));

            }
            return View(aranacak.ToList().ToPagedList(sayfa, 50));
        }

        public ActionResult İlanDet(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tbl_İlan tblİlan = db.Tbl_İlan.Find(id);
            if (tblİlan == null)
            {
                return HttpNotFound();
            }
            return View(tblİlan);
        }

        public ActionResult Yeniİlan()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle([Bind(Include ="Baslik,ilan_sahibi,Acıklama,Nitelikler,Adres,Pozisyon,Departman,CalismaSekli,Tecrube")]Tbl_İlan ilan)
        {


            if (ModelState.IsValid)
            {
                db.Tbl_İlan.Add(ilan);
                db.SaveChanges();
                return RedirectToAction("İlanlar", "Keşfet1");
            }
            return View(ilan);
            
        }

        
       
    }
}