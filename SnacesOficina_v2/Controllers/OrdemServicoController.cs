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
    public class OrdemServicoController : Controller
    {
        // GET: OrdemServico
        public ActionResult Index()
        {
            return View();
        }
        // public ActionResult Index(int id)
        //{ 
        //UPDATE OS SET STATUS = 2 WHERE ID = id
        //   return View();
        //}
        public ActionResult ListarOrdemServico( int Pagina = 1)
        {

            var ListaOs = new ModeloOrdemServico().ListaOrd();
            var listamarca = ListaOs.OrderBy(p => p.Id).ToPagedList(Pagina, 5);
            return View(listamarca);
               
        }
        
        public ActionResult EditarOs(int Id)
        {
            //var ListaEdit = new ModeloOrdemServico().ListaEditar(int Id);
            // return View(ListaEdit);
            string query = "select os.Id as idos, Numero, pessoa1.Nome as cliente, pessoa2.Nome as Consultor, st.Descricao as Situacao, " +
                " tos.Descricao as TipoOS, vei.Placa as Placa, " +
                " quilometragem, dtabertura, dtprevisaoentrega, dtentrega, rec.Descricao as Reclamacao, " +
                " fpgt.Descricao as FormaPagamento from ordemservico as os " +
                " inner join pessoas as pessoa1 on os.Id_cliente = pessoa1.id " +
                " inner join pessoas as pessoa2 on pessoa2.id = os.Id_consultor " +
                " inner join situacao as st on st.Id = os.Id_situacao " +
                " inner join tipoos as tos on tos.Id = os.Id_tipoos  " +
                " inner join veiculos as vei on vei.Id = os.Id_veiculo " +
                " left join reclamacao as rec on rec.Id = os.Id_Reclamacao " +
                " inner join formapagamento as fpgt on fpgt.Id = os.Id_Formapagamento where os.Id = " + Id;
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                ModeloOrdemServico ordemServico = new ModeloOrdemServico();
                while (leitor.Read())
                {
                    ordemServico.Id = leitor.GetInt32("idos");
                    ordemServico.Numero = leitor.GetString("Numero");
                    ordemServico.TipoOs = new ModeloTipoOs();
                    ordemServico.TipoOs.Descricao = leitor.GetString("TipoOS");
                    ordemServico.Veiculo = new ModeloVeiculo();
                    ordemServico.Veiculo.Placa = leitor.GetString("Placa");
                    ordemServico.Quilometragem = leitor.GetInt32("quilometragem");
                    ordemServico.Cliente = new ModeloCliente();
                    ordemServico.Cliente.Nome = leitor.GetString("cliente");
                    ordemServico.Consultor = new ModeloConsultor();
                    ordemServico.Consultor.Nome = leitor.GetString("Consultor");
                    ordemServico.Abertura = leitor.GetDateTime("dtabertura");
                    ordemServico.PrevisaoEntrega = leitor.GetDateTime("dtprevisaoentrega");
                    ordemServico.Entrega = leitor.GetDateTime("dtentrega");
                    ordemServico.Reclamacao = new Reclamacao();
                    ordemServico.Reclamacao.Descricao = leitor.GetString("Reclamacao");
                    ordemServico.FormaPagamento = new ModeloFormaPagamento();
                    ordemServico.FormaPagamento.Descricao = leitor.GetString("FormaPagamento");
                }
                leitor.Close();
                ViewBag.Numero = ordemServico.Numero;
                ViewBag.ModeloCliente = new SelectList
                   (
                   new ModeloCliente().ListaCliente(),
                   "Id",
                   "Nome"
                   );
                ViewBag.ModeloConsultor = new SelectList
                  (
                  new ModeloConsultor().ListaConsultor(),
                  "Id",
                  "Nome"
                  );

                ViewBag.ModeloVeiculo = new SelectList
                  (
                  new ModeloVeiculo().ListaVeiculo(),
                  "Id",
                  "Placa"
                  );

                ViewBag.ModeloSituacao = new SelectList
                   (
                   new ModeloSituacao().ListaSituacao(),
                   "Id",
                   "Descricao"
                   );
                ViewBag.ModeloFormaPagamento = new SelectList
                (
                 new ModeloFormaPagamento().ListaFormaPagamento(),
                "Id",
                "Descricao"
                );
                ViewBag.ModeloTipoOS = new SelectList
                    (
                    new ModeloTipoOs().ListaTipoOs(),
                    "Id",
                    "Descricao"
                    );
                ViewBag.reclamacao = new SelectList
               (
                new Reclamacao().ListaReclamacao(),
                "Id",
                "Descricao"
               );


                return View(ordemServico);

        }   }
        [HttpPost]
        public ActionResult EditarOs(FormCollection inputs, int Id)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand("update ordemservico set dtentrega = @Entrega, Id_tipoos = @ModeloTipoOs, " +
            " Id_cliente = @ModeloCliente,  Id_Formapagamento = @ModeloFormaPagamento where ordemservico.Id =  " + Id, conexao);
            //comando2.Parameters.AddWithValue("@id", inputs["id"]);
            //comando2.Parameters.AddWithValue("@NumeroOS", inputs["NumeroOS"]);
            comando2.Parameters.AddWithValue("@ModeloCliente", inputs["ModeloCliente"]);
            comando2.Parameters.AddWithValue("@ModeloSituacao", inputs["ModeloSituacao"]);
            comando2.Parameters.AddWithValue("@ModeloTipoOs", inputs["ModeloTipoOs"]);
           // comando2.Parameters.AddWithValue("@ModeloVeiculo", inputs["ModeloVeiculo"]);
           // comando2.Parameters.AddWithValue("@quilometragem", inputs["quilometragem"]);
            //comando2.Parameters.AddWithValue("@dtabertura", inputs["dtabertura"]);
           // comando2.Parameters.AddWithValue("@dtprevisaoentrega", DateTime.Parse(inputs["dtprevisaoEntrega"]).ToString("yyyy-MM-dd"));
            comando2.Parameters.AddWithValue("Entrega", DateTime.Parse(inputs["Entrega"]).ToString("yyyy-MM-dd hh:mm:ss"));
           // comando2.Parameters.AddWithValue("@Reclamacao", inputs["Reclamacao"]);
            comando2.Parameters.AddWithValue("@ModeloFormaPagamento", inputs["ModeloFormaPagamento"]);

            //Executa a Query SQL
            comando2.ExecuteNonQuery();

            
                ViewBag.ModeloCliente = new SelectList
                   (
                   new ModeloCliente().ListaCliente(),
                   "Id",
                   "Nome"
                   );
                ViewBag.ModeloConsultor = new SelectList
                  (
                  new ModeloConsultor().ListaConsultor(),
                  "Id",
                  "Nome"
                  );

                ViewBag.ModeloVeiculo = new SelectList
                  (
                  new ModeloVeiculo().ListaVeiculo(),
                  "Id",
                  "Placa"
                  );

                ViewBag.ModeloSituacao = new SelectList
                   (
                   new ModeloSituacao().ListaSituacao(),
                   "Id",
                   "Descricao"
                   );
                ViewBag.ModeloFormaPagamento = new SelectList
                (
                 new ModeloFormaPagamento().ListaFormaPagamento(),
                "Id",
                "Descricao"
                );
                ViewBag.ModeloTipoOS = new SelectList
                    (
                    new ModeloTipoOs().ListaTipoOs(),
                    "Id",
                    "Descricao"
                    );
                ViewBag.reclamacao = new SelectList
               (
                new Reclamacao().ListaReclamacao(),
                "Id",
                "Descricao"
               );

            conexao.Close();

                return RedirectToAction("ListarOrdemServico");

            
        }
        public ActionResult CadastroOrdemServico()
        {

            ViewData["NumeroOS"] = new ModeloOrdemServico().NumeroOS();
            ViewBag.ModeloCliente = new SelectList
             (
             new ModeloCliente().ListaCliente(),
             "Id",
             "Nome"
             );

            ViewBag.ModeloVeiculo = new SelectList
              (
              new ModeloVeiculo().ListaVeiculo(),
              "Id",
              "Placa"
              );
            
            ViewBag.ModeloSituacao = new SelectList
               (
               new ModeloSituacao().ListaSituacao(),
               "Id",
               "Descricao"
               );
            ViewBag.ModeloFormaPagamento = new SelectList
            (
             new ModeloFormaPagamento().ListaFormaPagamento(),
            "Id",
            "Descricao"
            );
            ViewBag.ModeloTipoOS = new SelectList
                (
                new ModeloTipoOs().ListaTipoOs(),
                "Id",
                "Descricao"
                );
            ViewBag.reclamacao = new SelectList
           (
            new Reclamacao().ListaReclamacao(),
            "Id",
            "Descricao"
           );

            return View();
            
        }
        [HttpPost]
        public ActionResult CadastroOrdemServico(FormCollection inputs)
        {
            MySqlConnection conexao = new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost;database=sancesoficinaversao_2;uid=root");
            // Abre a conexão
            conexao.Open();
            MySqlCommand comando2 = new MySqlCommand("insert into ordemservico(Numero, Id_cliente, Id_situacao, Id_Tipoos, Id_veiculo, quilometragem, dtabertura, dtprevisaoentrega, " +
             " dtentrega, Id_Reclamacao, Id_Formapagamento) values( @NumeroOS, @ModeloCliente, @ModeloSituacao, @ModeloTipoOs, @ModeloVeiculo, @quilometragem, NOW(), @dtprevisaoEntrega, @dtentrega, @Reclamacao, " +
                " @ModeloFormaPagamento) ", conexao);
            //comando2.Parameters.AddWithValue("@id", inputs["id"]);
            comando2.Parameters.AddWithValue("@NumeroOS", inputs["NumeroOS"]);
            comando2.Parameters.AddWithValue("@ModeloCliente", inputs["ModeloCliente"]);
            comando2.Parameters.AddWithValue("@ModeloSituacao", inputs["ModeloSituacao"]);
            comando2.Parameters.AddWithValue("@ModeloTipoOs", inputs["ModeloTipoOs"]);
            comando2.Parameters.AddWithValue("@ModeloVeiculo", inputs["ModeloVeiculo"]);
            comando2.Parameters.AddWithValue("@quilometragem", inputs["quilometragem"]);
            //comando2.Parameters.AddWithValue("@dtabertura", inputs["dtabertura"]);
            comando2.Parameters.AddWithValue("@dtprevisaoentrega", DateTime.Parse(inputs["dtprevisaoEntrega"]).ToString("yyyy-MM-dd"));
            comando2.Parameters.AddWithValue("@dtentrega", DateTime.Parse(inputs["dtentrega"]).ToString("yyy-MM-dd"));
            comando2.Parameters.AddWithValue("@Reclamacao", inputs["Reclamacao"]);
            comando2.Parameters.AddWithValue("@ModeloFormaPagamento", inputs["ModeloFormaPagamento"]);

            //Executa a Query SQL
            comando2.ExecuteNonQuery();
            string email = inputs["email"];
            Email.EmailEnviar(email);

            ViewData["NumeroOS"] = new ModeloOrdemServico().NumeroOS();
            ViewBag.ModeloCliente = new SelectList
             (
             new ModeloCliente().ListaCliente(),
             "Id",
             "Nome"
             );

            ViewBag.ModeloVeiculo = new SelectList
               (
               new ModeloVeiculo().ListaVeiculo(),
               "Id",
               "Placa"
               );
            ViewBag.ModeloSituacao = new SelectList
               (
               new ModeloSituacao().ListaSituacao(),
               "Id",
               "Descricao"
               );
            ViewBag.ModeloFormaPagamento = new SelectList
            (
            new ModeloFormaPagamento().ListaFormaPagamento(),
            "Id",
            "Descricao"
            );
            ViewBag.ModeloTipoOS = new SelectList
                (
                new ModeloTipoOs().ListaTipoOs(),
                "Id",
                "Descricao"
                );
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