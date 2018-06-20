using MySql.Data.MySqlClient;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using X.PagedList;

namespace SnacesOficina_v2.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarVeiculos( int Pagina = 1)
        {
            var veiculos = new ModeloVeiculo().ListaVeiculo() ;
            var listaveiculo = veiculos.OrderBy(p => p.Id)
                             .ToPagedList(Pagina, 10);
            return View(listaveiculo);

            
        }
        public ActionResult CadastroVeiculo()
        {
            ViewBag.ModeloMarca = new SelectList
                (
                new ModeloMarca().ListaMarca(),
                "Id",
                "Descricao"
                );
            ViewBag.ModeloModelo = new SelectList
               (
               new ModeloModelo().ListaModelo(),
               "Id",
               "Descricao",
               "Id_marca"
               );
            return View();
        }

        [HttpPost]
        public ActionResult CadastroVeiculo(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO veiculos(id, Placa, Chassi, Id_marca, Id_modelo, AnoFabricacao, AnoModelo) values (@id, @Placa, @Chassi, @Id_marca, @Id_modelo, @AnoFabricacao, @AnoModelo) ", conexao);
            comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@Placa", inputs["Placa"]);
            comando2.Parameters.AddWithValue("@Chassi", inputs["Chassi"]);
            comando2.Parameters.AddWithValue("@Id_marca", inputs["ModeloMarca"]);
            comando2.Parameters.AddWithValue("@Id_modelo", inputs["Id_modelo"]);
            comando2.Parameters.AddWithValue("@Anofabricacao", inputs["AnoFabricacao"]);
            comando2.Parameters.AddWithValue("@AnoModelo", inputs["AnoModelo"]);
            //Executa a Query SQL
            comando2.ExecuteNonQuery();
            // Fecha a conexão
            conexao.Close();
            ViewBag.ModeloMarca = new SelectList
               (
               new ModeloMarca().ListaMarca(),
               "Id",
               "Descricao"
               );
            ViewBag.ModeloModelo = new SelectList
               (
               new ModeloModelo().ListaModelo(),
               "Id",
               "Descricao",
               "Id_marca"
               );
            return View();
        }
        public ActionResult ListaModelo(int id)
        {
            var serializador = new JavaScriptSerializer();
            var resultado = serializador.Serialize(new ModeloModelo().ListaModelo(id));
            return Content(resultado);
        }
    }
}