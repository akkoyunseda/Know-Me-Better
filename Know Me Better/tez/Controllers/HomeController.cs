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
using wordeaktar = Microsoft.Office.Interop.Word;

namespace tez.Controllers
{
    public class HomeController : Controller

    {

        private DbKnowMeBetterEntities db = new DbKnowMeBetterEntities();

        public ActionResult Index()
        {

            return View();
        }



        //[HttpPost]
        //public ActionResult SignIn()
        //{

        //    return PartialView();
        //}


        public ActionResult Signup1()
        {

            return View();
        }

        public ActionResult Signup2()
        {
            return View();
        }

        public ActionResult Login()
        {

            return View();
        }


        public ActionResult Create()
        {
            return View();

        }

        // POST: NewCv/Create
        // Aşırı gönderim saldırılarından korunmak için bağlamak istediğiniz belirli özellikleri etkinleştirin. 
        // Daha fazla bilgi için bkz. https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Ad,Soyad,Mail,Telefon,Adres,D_Tarihi,Egitim,Tecrube,Sertifika,Pc_yetenek,Dil_yetenek,Hobiler,Uyelik,Sürücü_bel")] Tbl_Ziyaretci tblZiyaretci)
        {
            if (ModelState.IsValid)
            {
                db.Tbl_Ziyaretci.Add(tblZiyaretci);
                db.SaveChanges();
                var bilgiler = tblZiyaretci; // Veritabanından başvuru bilgisi seçiyoruz. Veri çekmenin bir sürü farklı yöntemi var. İstediğinizi kullanabilirsiniz.
                if (bilgiler != null)
                {
                    StringWriter writer = new StringWriter();
                    HtmlTextWriter htmlWriter = new HtmlTextWriter(writer);
                    string doc1 = Server.MapPath("~/Sablon/Sablon1.docx"); //Şablonumuzun olduğu dosya yolu
                    string doc2 = Server.MapPath("~/Sablon/temp.docx"); //Bu da temp dosyası, sürekli değişip silinecek olan dosya
                    System.IO.File.Delete(doc2);//Öncelikle devamlı değişen dosyayı siliyoruz.
                    System.IO.File.Copy(doc1, doc2);//daha sonra istediğimiz taslağın içerğini bu dosyaya kopyalıyoruz.
                    using (DocX doc = DocX.Load(doc2))
                    {
                        //Değişkenleri gerçek bilgiler ile replace ediyoruz

                        doc.ReplaceText("@AD@", bilgiler.Ad, false);

                        doc.ReplaceText("@SOYAD@", bilgiler.Soyad, false);
                        doc.ReplaceText("@EGITIM@", bilgiler.Egitim, false);
                        doc.ReplaceText("@Adres@", bilgiler.Adres, false);
                        doc.ReplaceText("@TECRUBE@", bilgiler.Tecrube, false);
                        doc.ReplaceText("@PC_YETENEK@", bilgiler.Pc_yetenek, false);
                        doc.ReplaceText("@Telefon@", bilgiler.Telefon, false);
                        doc.ReplaceText("@DIL_YETENEK@", bilgiler.Dil_Yetenek, false);
                        doc.ReplaceText("@HOBI@", bilgiler.Hobiler, false);
                        doc.ReplaceText("@D_TARIH@", bilgiler.D_Tarihi, false);
                        doc.ReplaceText("@SURUCU_BEL@", bilgiler.Sürücü_bel, false);
                        doc.ReplaceText("@UYELİK@", bilgiler.Uyelik, false);
                        doc.ReplaceText("@SERIFIKA@", bilgiler.Sertifika, false);
                        doc.ReplaceText("@eposta@", bilgiler.Mail, false);
                        string ad = (bilgiler.Ad) + ".docx"; // yeni oluşturulacak dosyanın adı
                        doc.SaveAs(doc2);
                        WebClient req = new WebClient();
                        HttpResponse response = System.Web.HttpContext.Current.Response;
                        string filePath = "~/Sablon/temp.docx";
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        Response.AppendHeader("content-disposition", "attachment; filename=" + ad);
                        byte[] data = req.DownloadData(Server.MapPath(filePath));
                        response.BinaryWrite(data);
                        response.End();


                        //string zipDosya = Server.MapPath("/Sablon/Sablon1.docx");
                        //string tempKlasor = string.Format(Server.MapPath("/Sablon/{0}"), Guid.NewGuid().ToString("N"));

                        //using (ZipFile zip = ZipFile.Read(zipDosya))
                        //{
                        //    zip.ExtractAll(tempKlasor);
                        //}

                        //string documentDosya = tempKlasor + @"\word\document.xml";
                        //string icerik = System.IO.File.ReadAllText(documentDosya);

                        //icerik = icerik.Replace("@AD@", bilgiler.Ad);
                        //icerik = icerik.Replace("@SOYAD@", bilgiler.Soyad);
                        //icerik = icerik.Replace("@EGITIM@", bilgiler.Egitim);
                        //icerik = icerik.Replace("@TECRUBE@", bilgiler.Tecrube);
                        //icerik = icerik.Replace("@PC_YETENEK@", bilgiler.Pc_yetenek);
                        //icerik = icerik.Replace("@DIL_YETENEK@", bilgiler.Dil_Yetenek);
                        //icerik = icerik.Replace("@SERTIFIKA@", bilgiler.Sertifika);
                        //icerik = icerik.Replace("@UYELIK@", bilgiler.Uyelik);
                        //icerik = icerik.Replace("@HOBI@", bilgiler.Hobiler);
                        //icerik = icerik.Replace("@Adres@", bilgiler.Adres);
                        //icerik = icerik.Replace("@Telefon@", bilgiler.Telefon);
                        //icerik = icerik.Replace("@eposta@", bilgiler.Mail);
                        //icerik = icerik.Replace("@SURUCU_BEL@", bilgiler.Sürücü_bel);
                        //StreamWriter writer = new StreamWriter(documentDosya, false);
                        //writer.Write(icerik);
                        //writer.Close();

                        //using (ZipFile zip = new ZipFile())
                        //{
                        //    string zipAdi = "Cv.docx";
                        //    Response.Clear();
                        //    Response.BufferOutput = false;
                        //    Response.ContentType = "application/zip";
                        //    Response.AddHeader("content-disposition", "filename=" + zipAdi);

                        //    zip.AddDirectory(tempKlasor);
                        //    zip.Save(Response.OutputStream);
                        //}
                        //Directory.Delete(tempKlasor, true);
                        //Response.Close();
                        
                    }
                    return RedirectToAction("Index");
                }
            }
            return View(tblZiyaretci);
        }

        [HttpPost]

        public ActionResult Signup1([Bind(Include = "KullanıcıAd, FirmaAd, Mail, Parola")] Tblİşveren tblİşveren)
        {
            if (ModelState.IsValid)
            {
                db.Tblİşveren.Add(tblİşveren);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblİşveren);
        }

        [HttpPost]
        public ActionResult Signup2([Bind(Include = "KullanıcıAdı, Ad, Soyad,Mail,Parola")] TblİşArayan tblİşArayan)
        {
            if (ModelState.IsValid)
            {
                db.TblİşArayan.Add(tblİşArayan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tblİşArayan);
        }

        //public ActionResult CV()
        // {
        //     string zipDosya = Server.MapPath("Sablon/Sablon1.docx");
        //     string tempKlasor = string.Format(Server.MapPath("Sablon/{0}"), Guid.NewGuid().ToString("N"));

        //     using (ZipFile zip = ZipFile.Read(zipDosya))
        //     {
        //         zip.ExtractAll(tempKlasor);
        //     }

        //     string documentDosya = tempKlasor + @"\word\document.xml";
        //     string icerik = File.ReadAllText(documentDosya);

        //     icerik = icerik.Replace("@Ad@")

        // }


    }
}