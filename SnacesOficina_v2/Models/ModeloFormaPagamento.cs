using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloFormaPagamento
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        internal IEnumerable ListaFormaPagamento()
        {
            List<ModeloFormaPagamento> formaPagamentos = new List<ModeloFormaPagamento>();
            string query = "select * from formapagamento; ";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloFormaPagamento formaPagamento = new ModeloFormaPagamento();
                    formaPagamento.Id = leitor.GetInt32("Id");
                    formaPagamento.Descricao = leitor.GetString("Descricao");

                    formaPagamentos.Add(formaPagamento);
                }
                leitor.Close();

            }
            return formaPagamentos;
        }
    }
}