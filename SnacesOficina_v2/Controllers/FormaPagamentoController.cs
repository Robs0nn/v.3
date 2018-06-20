using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class FormaPagamentoController : Controller
    {
        // GET: FormaPagamento
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult CadastroFormaPagamento()
        {
            ViewBag.ModeloFormaPagamento = new SelectList
           (
           new ModeloFormaPagamento().ListaFormaPagamento(),
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
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO formapagamento(id, Descricao) values (@id, @Descricao) ", conexao);
            comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@descricao", inputs["Descricao"]);
            //Executa a Query SQL
            comando2.ExecuteNonQuery();
            ViewBag.ModeloFormaPagamento = new SelectList
            (
            new ModeloFormaPagamento().ListaFormaPagamento(),
            "Id",
            "Descricao"
            );
            // Fecha a conexão
            conexao.Close();
            return View();
        }
    }
}