using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;

namespace SnacesOficina_v2.Controllers
{
    public class ItensOsModelsController : Controller
    {
        private SnacesOficina_v2Context db = new SnacesOficina_v2Context();

        // GET: ItensOsModels
        public ActionResult Index()
        {
            return View(db.ItensOsModels.ToList());
        }
        [HttpPost]
        public void InserirItens(InserirItem it)
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");

                // Abre a conexão
                conexao.Open();

                var idOs = it.ListaOS[0];
                for (int i = 0; i < it.Itens.Count; i++)
                {
                    MySqlCommand comando2 = new MySqlCommand(" INSERT INTO itensos( idOS, IdItens, qtdItens, ValorItensOs ) " +
                        " values ( @idOs, @Id, @qtdItens, @ValorItensOs) ", conexao);
                    comando2.Parameters.AddWithValue("@idOs", idOs );
                    comando2.Parameters.AddWithValue("@Id",it.Itens[i]);
                    comando2.Parameters.AddWithValue("@qtdItens", it.qtdItem[i]);
                    comando2.Parameters.AddWithValue("@ValorItensOs", it.Preco[i]);

                    comando2.ExecuteNonQuery();

                }


                conexao.Close();


            }
            catch (Exception)
            {

                throw;
            }
        }

        // GET: ItensOsModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensOsModels itensOsModels = db.ItensOsModels.Find(id);
            if (itensOsModels == null)
            {
                return HttpNotFound();
            }
            return View(itensOsModels);
        }

        // GET: ItensOsModels/Create
        public ActionResult Create()
        {
            ViewBag.ListaOS = new SelectList
             (
             new ModeloOrdemServico().ListaOS(),
             "Id",
             "Numero"
             );
            ViewBag.ListaItens = new SelectList
          (
             new ItensModels().ListaItens(),
              "Id",
              "Descricao",
              "Preco"
              
           );
            return View();
        }

        // POST: ItensOsModels/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,IdOs,IdItem")] ItensOsModels itensOsModels)
        {
           
            ViewBag.ListaOS = new SelectList
             (
             new ModeloOrdemServico().ListaOS(),
             "Id",
             "Numero"
             );
            ViewBag.ListaItens = new SelectList
          (
             new ItensModels().ListaItens(),
              "Id",
              "Descricao",
              "Preco"
              
           );
            return View(itensOsModels);
        }

        // GET: ItensOsModels/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensOsModels itensOsModels = db.ItensOsModels.Find(id);
            if (itensOsModels == null)
            {
                return HttpNotFound();
            }
            return View(itensOsModels);
        }

        // POST: ItensOsModels/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,IdOs,IdItem")] ItensOsModels itensOsModels)
        {
            if (ModelState.IsValid)
            {
                db.Entry(itensOsModels).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(itensOsModels);
        }

        // GET: ItensOsModels/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ItensOsModels itensOsModels = db.ItensOsModels.Find(id);
            if (itensOsModels == null)
            {
                return HttpNotFound();
            }
            return View(itensOsModels);
        }

        // POST: ItensOsModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ItensOsModels itensOsModels = db.ItensOsModels.Find(id);
            db.ItensOsModels.Remove(itensOsModels);
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
        public ActionResult ListarItens()
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new ItensModels().ListaItens());
            return Content(resultado);
        }
    }
}
