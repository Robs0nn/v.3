using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class ConsultorController : Controller
    {
        // GET: Consultor
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroConsultor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadastroVeiculo(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO consultor(Id,Login, Senha, Id_pessoa) values (@Id, @Login, @Senha @Id_pessoa) ", conexao);
            comando2.Parameters.AddWithValue("@Id", inputs["Id"]);
            comando2.Parameters.AddWithValue("@Login", inputs["Login"]);
            comando2.Parameters.AddWithValue("@Senha", inputs["Senha"]);
            comando2.Parameters.AddWithValue("@Id_pessoa", inputs["Id_pessoa"]);
            comando2.ExecuteNonQuery();
            // Fecha a conexão
            conexao.Close();
            return View();
        }
    }
}