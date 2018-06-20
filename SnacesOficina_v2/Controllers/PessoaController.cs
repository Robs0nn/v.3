using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using SnacesOficina_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SnacesOficina_v2.Controllers
{
    public class PessoaController : Controller
    {
        // GET: Pessoa
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult ListarPessoas()
        {
            var pessoa = new ModeloPessoa().ListaClientes();
            return View(pessoa);
        }

        public ActionResult CadastroPessoa()
        {
           
              
            return View();
         
        }
        [HttpPost]
        public ActionResult CadastroPessoa(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");

            // Abre a conexão
            conexao.Open();

            

            //ou
            MySqlCommand comando2 = new MySqlCommand(" INSERT INTO pessoas(id, cpf_cnpj, nome, email, Endereco) values (@id, @cpf_cnpj, @nome, @email, @Endereco) ", conexao);
            comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@cpf_cnpj", inputs["cpf_cnpj"]);
            comando2.Parameters.AddWithValue("@nome", inputs["nome"]);
            comando2.Parameters.AddWithValue("@email", inputs["email"]);
            comando2.Parameters.AddWithValue("@Endereco", inputs["Endereco"]);
           

            //Executa a Query SQL
            comando2.ExecuteNonQuery();
            string email = inputs["email"];
            Email.EmailEnviar( email);

            // Fecha a conexão
            conexao.Close();


            return View();
        }
    }
}