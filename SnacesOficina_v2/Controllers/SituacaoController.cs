using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class SituacaoController : Controller
    {
        // GET: Situacao
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroSituacao()
        {
            ViewBag.ModeloSituacao = new SelectList
                (
                new ModeloSituacao().ListaSituacao(),
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
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO situacao(Id, Descricao) values (@Id, @Descricao) ", conexao);
            comando2.Parameters.AddWithValue("@Id", inputs["Id"]);
            comando2.Parameters.AddWithValue("@Descricao", inputs["Descricao"]);

            comando2.ExecuteNonQuery();
            ViewBag.ModeloSituacao = new SelectList
               (
               new ModeloSituacao().ListaSituacao(),
               "Id",
               "Descricao"
               );
            // Fecha a conexão
            conexao.Close();
            return View();
        }
    }
}