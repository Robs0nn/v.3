using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;
using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using SnacesOficina_v2.Models;

namespace SnacesOficina_v2.Controllers
{
    public class UsuariosController : Controller
    {
        private SnacesOficina_v2Context db = new SnacesOficina_v2Context();

        // GET: Usuarios
        public ActionResult Create()
        {
            return View();
        }

        // POST: Login 
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                ConexaoSql bd = new ConexaoSql();
                MySqlConnection conexao = bd.conexaobd();
                Criptografia crip = new Criptografia();

                MySqlCommand comando = new MySqlCommand("SELECT * from usuario where Login = @Login and Senha = @Senha", conexao);
                comando.Parameters.AddWithValue("@Login", usuario.Login);
                comando.Parameters.AddWithValue("@Senha", usuario.Senha);

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();


                if (leitor.HasRows) //siginifica que login e senha digitados iguais aos do BD {
                {
                    leitor.Close();
                    return RedirectToAction("Home", "Home");
                }


            }

            return View(usuario);
        }

        // GET: Usuarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // GET: Usuarios/Create
        [AllowAnonymous]
        public ActionResult Create1()
        {
            return View();
        }

        // POST: Usuarios/Create
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create1([Bind(Include = "Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Usuarios.Add(usuario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(usuario);
        }

        // GET: Usuarios/Create
        public ActionResult CadastroLogin()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroLogin([Bind(Include = "Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {

                ConexaoSql bd = new ConexaoSql();
                MySqlConnection conexao = bd.conexaobd();
                Criptografia crip = new Criptografia();
                MySqlCommand comando = new MySqlCommand(" INSERT INTO usuario( Login, Senha) values ( @Login, @Senha)", conexao);
                comando.Parameters.AddWithValue("@Login", usuario.Login);
                comando.Parameters.AddWithValue("@Senha", crip.GerarMD5(usuario.Senha));

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (leitor.HasRows) //siginifica que login e senha digitados iguais aos do BD {
                {
                    leitor.Close();
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(usuario);
        }


        // GET: Usuarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Edit/5
        // Para se proteger de mais ataques, ative as propriedades específicas a que você quer se conectar. Para 
        // obter mais detalhes, consulte https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Login,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(usuario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Usuario usuario = db.Usuarios.Find(id);
            if (usuario == null)
            {
                return HttpNotFound();
            }
            return View(usuario);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Usuario usuario = db.Usuarios.Find(id);
            db.Usuarios.Remove(usuario);
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
