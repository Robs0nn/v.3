using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SnacesOficina_v2.Controllers
{
    public class LogarController : Controller
    {
        // GET: Logar
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult Logar()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logar ([Bind(Include = "Id,Login,Senha")] Logar usuario)
        {
            if (ModelState.IsValid)
            {

                ConexaoSql bd = new ConexaoSql();
                MySqlConnection conexao = bd.conexaobd();
                Criptografia crip = new Criptografia();

                MySqlCommand comando = new MySqlCommand("SELECT * from usuario where Login = @Login and Senha = @Senha", conexao);
                comando.Parameters.AddWithValue("@Login", usuario.Login);
                comando.Parameters.AddWithValue("@Senha", crip.GerarMD5(usuario.Senha));

                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                if (leitor.HasRows) //siginifica que login e senha digitados iguais aos do BD {
                {
                    FormsAuthentication.SetAuthCookie(usuario.Login, false);
                    leitor.Close();
                    return RedirectToAction("Home", "Home");
                }
            }
                return View(usuario);
        }
        [AllowAnonymous]
        public ActionResult CadastroLogar()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CadastroLogar([Bind(Include = "Id,Login,Senha")] Logar usuario)
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
                    return RedirectToAction("Home", "Home");
                }
            }
            return View(usuario);
        }

    }
}