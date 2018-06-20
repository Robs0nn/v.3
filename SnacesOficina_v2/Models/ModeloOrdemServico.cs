using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloOrdemServico
    {
        public int Id { get; set; }
        public string Numero { get; set; }
        public ModeloSituacao Situacao { get; set; }
        public ModeloTipoOs TipoOs { get; set; }
        public ModeloVeiculo Veiculo { get; set; }
        public int Quilometragem { get; set; }
        public ModeloCliente Cliente { get; set; }
        public ModeloConsultor Consultor { get; set; }
        public DateTime Abertura { get; set; }
        public DateTime PrevisaoEntrega { get; set; }
        public DateTime Entrega { get; set; }
        public Reclamacao Reclamacao { get; set; }
        public ModeloFormaPagamento FormaPagamento { get; set; }
       

        public object NumeroOS()
        {
            string query = "select max(numero) from ordemservico;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                leitor.Read();
                ModeloOrdemServico ordemServico = new ModeloOrdemServico();
                string num = leitor.GetString("max(Numero)");
                int NumeroOS = int.Parse(num);
                NumeroOS++;
                return NumeroOS;
            }
        }
        public List<ModeloOrdemServico> ListaOrd()
        {
            List<ModeloOrdemServico> OrdemServico = new List<ModeloOrdemServico>();
            string query = "select os.Id as idos, Numero, pessoa1.Nome as cliente, pessoa2.Nome as Consultor, st.Descricao as Situacao, " +
                " tos.Descricao as TipoOS, vei.Placa as Placa, " +
                " quilometragem, dtabertura, dtprevisaoentrega, dtentrega, rec.Descricao as Reclamacao, " +
                " fpgt.Descricao as FormaPagamento from ordemservico as os " +
                " inner join pessoas as pessoa1 on pessoa1.id = os.Id_cliente " +
                " inner join pessoas as pessoa2 on pessoa2.id = os.Id_consultor " +
                " inner join situacao as st on st.Id = os.Id_situacao " +
                " inner join tipoos as tos on tos.Id = os.Id_tipoos  " +
                " inner join veiculos as vei on vei.Id = os.Id_veiculo " +
                " left join reclamacao as rec on rec.Id = os.Id_Reclamacao " +
                " inner join formapagamento as fpgt on fpgt.Id = os.Id_Formapagamento; ";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloOrdemServico ordemServico = new ModeloOrdemServico();
                    ordemServico.Id = leitor.GetInt32("idos");
                    ordemServico.Numero = leitor.GetString("Numero");
                    ordemServico.Situacao = new ModeloSituacao();
                    ordemServico.Situacao.Descricao = leitor.GetString("Situacao");
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

                    OrdemServico.Add(ordemServico);
                }
                leitor.Close();


                return (OrdemServico);
        }   }

       
        

        internal IEnumerable ListaOrdemServico()
        {
            List<ModeloOrdemServico> OrdemServico = new List<ModeloOrdemServico>();
            string query = "select * from ordemservico;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloOrdemServico ordemServico = new ModeloOrdemServico();
                    ordemServico.Id = leitor.GetInt32("Id");
                    ordemServico.Numero = leitor.GetString("Numero");
                    ordemServico.Situacao = new ModeloSituacao();
                    ordemServico.Situacao.Id = leitor.GetInt32("Id");
                    ordemServico.Situacao.Descricao = leitor.GetString("Descricao");
                    ordemServico.TipoOs = new ModeloTipoOs();
                    ordemServico.TipoOs.Id = leitor.GetInt32("Id");
                    ordemServico.TipoOs.Descricao = leitor.GetString("Descricao");
                    ordemServico.Veiculo = new ModeloVeiculo();
                    ordemServico.Veiculo.Id = leitor.GetInt32("Id");
                    ordemServico.Veiculo.Placa = leitor.GetString("Placa");
                    ordemServico.Veiculo.Chassi = leitor.GetString("Chassi");
                    ordemServico.Veiculo.Marca = new ModeloMarca();
                    ordemServico.Veiculo.Marca.Id = leitor.GetInt32("Id");
                    ordemServico.Veiculo.Marca.Descricao = leitor.GetString("Descricao");
                    ordemServico.Veiculo.Modelo = new ModeloModelo();
                    ordemServico.Veiculo.Modelo.ID = leitor.GetInt32("Id");
                    ordemServico.Veiculo.Modelo.Descricao = leitor.GetString("Descricao");
                    ordemServico.Veiculo.Modelo.ID_Marca = leitor.GetInt32("Id_marca");
                    ordemServico.Quilometragem = leitor.GetInt32("quilometragem");
                    ordemServico.Cliente = new ModeloCliente();
                    ordemServico.Cliente.Id = leitor.GetInt32("Id");
                    ordemServico.Cliente.Nome = leitor.GetString("Id_pessoa");
                    ordemServico.Consultor = new ModeloConsultor();
                    ordemServico.Consultor.Id = leitor.GetInt32("Id");
                    ordemServico.Consultor.Login = leitor.GetString("Login");
                    ordemServico.Consultor.Senha = leitor.GetString("Senha");
                    ordemServico.Abertura = leitor.GetDateTime("dtabertura");
                    ordemServico.PrevisaoEntrega = leitor.GetDateTime("dtprevisaoentrega");
                    ordemServico.Entrega = leitor.GetDateTime("dtentrega");
                    ordemServico.Reclamacao = new Reclamacao();
                    ordemServico.Reclamacao.Id = leitor.GetInt32("Id");
                    ordemServico.Reclamacao.Descricao = leitor.GetString("Descricao");
                    ordemServico.FormaPagamento = new ModeloFormaPagamento();
                    ordemServico.FormaPagamento.Id = leitor.GetInt32("Id");
                    ordemServico.FormaPagamento.Descricao = leitor.GetString("Id_Formapagamento");

                    OrdemServico.Add(ordemServico);
                }
                leitor.Close();

            }
            return OrdemServico;
        }
        internal IEnumerable ListaOS()
        {
            List<ModeloOrdemServico> OrdemServico = new List<ModeloOrdemServico>();
            string query = "select * from ordemservico;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloOrdemServico ordemServico = new ModeloOrdemServico();
                    ordemServico.Id = leitor.GetInt32("Id");
                    ordemServico.Numero = leitor.GetString("Numero");
            

                    OrdemServico.Add(ordemServico);
                }
                leitor.Close();

            }
            return OrdemServico;
        }
    }
}