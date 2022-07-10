using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using tez.Models;

namespace tez.Controllers
{
    public class TblİşArayanController : Controller
    {
        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();

        // GET: TblİşArayan
        public ActionResult Index()
        {
            return View(db.TblİşArayan.ToList());
        }

        // GET: TblİşArayan/Details/5
        public ActionResult Details(int? id)
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

        // GET: TblİşArayan/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TblİşArayan/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KullanıcıAdı,Ad,Soyad,Mail,Parola,İletisim,Adres,D_Tarihi,Egitim,Tecrube,Sertifika,Uyelik,Pc_yetenek,Dil_yetenek,Hobiler,Surucu_bel,Hakkında,Hedefler,ProfilFoto")] TblİşArayan tblİşArayan)
        {
            if (ModelState.IsValid)
            {
                db.TblİşArayan.Add(tblİşArayan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tblİşArayan);
        }

        // GET: TblİşArayan/Edit/5
        public ActionResult Edit(int? id)
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

        // POST: TblİşArayan/Edit/5
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KullanıcıAdı,Ad,Soyad,Mail,Parola,İletisim,Adres,D_Tarihi,Egitim,Tecrube,Sertifika,Uyelik,Pc_yetenek,Dil_yetenek,Hobiler,Surucu_bel,Hakkında,Hedefler,ProfilFoto")] TblİşArayan tblİşArayan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tblİşArayan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblİşArayan);
        }

        // GET: TblİşArayan/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: TblİşArayan/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TblİşArayan tblİşArayan = db.TblİşArayan.Find(id);
            db.TblİşArayan.Remove(tblİşArayan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
