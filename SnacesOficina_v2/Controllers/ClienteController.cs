using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Cliente
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroCliente()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadastroVeiculo(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO cliente(Id, Id_pessoa) values (@Id, @Id_pessoa) ", conexao);
            comando2.Parameters.AddWithValue("@Id", inputs["Id"]);
            comando2.Parameters.AddWithValue("@Id_pessoa", inputs["Id_pessoa"]);
            comando2.ExecuteNonQuery();
            // Fecha a conexão
            conexao.Close();
            return View();
        }
    }
}