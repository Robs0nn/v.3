using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class ReclamacaoController : Controller
    {
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroRecalmacao()
        {
            ViewBag.reclamacao = new SelectList
                (
                 new Reclamacao().ListaReclamacao(),
                 "Id",
                 "Descricao"
                );
           

            return View();
        }
        [HttpPost]
        public ActionResult CadastroVeiculo(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO reclamcao(Id, Descricao) values (@Id, @Descricao) ", conexao);
            comando2.Parameters.AddWithValue("@Id", inputs["Id"]);
            comando2.Parameters.AddWithValue("@Descricao", inputs["Descricao"]);
            comando2.ExecuteNonQuery();

            ViewBag.reclamacao = new SelectList
              (
               new Reclamacao().ListaReclamacao(),
               "Id",
               "Descricao"
              );
            // Fecha a conexão
            conexao.Close();
            return View();
        }
    }
}