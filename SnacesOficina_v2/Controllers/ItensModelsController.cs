using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using SnacesOficina_v2.Models;

namespace SnacesOficina_v2.Controllers
{
    public class ItensModelsController : Controller
    {
        private SnacesOficina_v2Context db = new SnacesOficina_v2Context();

        // GET: ItensModels
        public ActionResult Index()
        {
            return View(db.ItensModels.ToList());
        }

        // GET: ItensModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensModels itensModels = db.ItensModels.Find(id);
            if (itensModels == null)
            {
                return HttpNotFound();
            }
            return View(itensModels);
        }

        // GET: ItensModels/Create
        public ActionResult Create()
        {
                ViewBag.ListaItens = new SelectList
              (
                 new ItensModels().ListaItens(),
                  "Id",
                  "Descricao",
                  "Preco"
               );
            return View();
        }

        // POST: ItensModels/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Descricao,Preco")] ItensModels itensModels)
        {
            if (ModelState.IsValid)
            {
                db.ItensModels.Add(itensModels);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListaItens = new SelectList
           (
              new ItensModels().ListaItens(),
               "Id",
               "Descricao",
               "Preco"
            );
            return View(itensModels);
        }

        // GET: ItensModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensModels itensModels = db.ItensModels.Find(id);
            if (itensModels == null)
            {
                return HttpNotFound();
            }
            return View(itensModels);
        }

        // POST: ItensModels/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Descricao,Preco")] ItensModels itensModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itensModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itensModels);
        }

        // GET: ItensModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensModels itensModels = db.ItensModels.Find(id);
            if (itensModels == null)
            {
                return HttpNotFound();
            }
            return View(itensModels);
        }

        // POST: ItensModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItensModels itensModels = db.ItensModels.Find(id);
            db.ItensModels.Remove(itensModels);
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
