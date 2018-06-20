using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace SnacesOficina_v2.Controllers
{
    public class ModeloController : Controller
    {
        // GET: Modelo
        public ActionResult Index()
        {
          
            return View();
        }
        public ActionResult ListarModelos(int Pagina = 1)
        {
            List<ModeloModelo> Modelos = new List<ModeloModelo>();
            string query = "select * from modelos ";
          
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloModelo modelo = new ModeloModelo();
                    modelo.ID = leitor.GetInt32("Id");
                    modelo.Descricao = leitor.GetString("Descricao");
                    modelo.ID_Marca = leitor.GetInt32("Id_marca");
                    Modelos.Add(modelo);
                }
                leitor.Close();

            }
           
            var listaModelo = Modelos.OrderBy(p => p.ID)
                             .ToPagedList(Pagina, 10);
            return View(listaModelo);


        }

        public ActionResult CadastroModelo()
        {
            ViewBag.ModeloModelo = new SelectList
            (
            new ModeloModelo().ListaModelo(),
            "Id",
            "Desceicao",
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
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO modelos(id, Descricao, Id_marca) values (@id, @Descricao, @Id_marca) ", conexao);
            comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@descricao", inputs["Descricao"]);
            comando2.Parameters.AddWithValue("@Id_marca", inputs["Id_marca"]);
            //Executa a Query SQL
            comando2.ExecuteNonQuery();
            // Fecha a conexão
            conexao.Close();
            ViewBag.ModeloModelo = new SelectList
                (
                new ModeloModelo().ListaModelo(),
                "Id",
                "Desceicao",
                "Id_marca"
                );
            return View();
        }


    }
}