using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace SnacesOficina_v2.Controllers
{
    public class MarcaController : Controller
    {
        // GET: Marca
        public ActionResult Index()
        {
            ViewBag.ModeloMarca = new SelectList
                (
                new ModeloMarca().ListaMarca(),
                "Id",
                "Desceicao"
                );
            return View();
        }

        public ActionResult ListarMarcas(int Pagina = 1)
        {
            var Marca = new ModeloMarca().ListaMarca();
            var listamarca = Marca.OrderBy(p => p.Id)
                             .ToPagedList(Pagina, 10);
            return View(listamarca);


        }

        public ActionResult CadastroMarca()
        {
            return View();
        }
        [HttpPost]
        public ActionResult CadastroVeiculo(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO marcas(id, Descricao) values (@id, @Descricao) ", conexao);
            comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@descricao", inputs["Descricao"]);
             //Executa a Query SQL
            comando2.ExecuteNonQuery();
            // Fecha a conexão
            conexao.Close();
            ViewBag.ModeloMarca = new SelectList
                (
                new ModeloMarca().ListaMarca(),
                "Id",
                "Desceicao"
                );
            return View();
        }
    }
}