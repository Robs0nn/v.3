using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloSituacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        internal IEnumerable ListaSituacao()
        {
            List<ModeloSituacao> Situacao = new List<ModeloSituacao>();
            string query = "select * from situacao; ";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloSituacao situacao  = new ModeloSituacao();
                    situacao.Id = leitor.GetInt32("Id");
                    situacao.Descricao = leitor.GetString("Descricao");

                    Situacao.Add(situacao);
                }
                leitor.Close();

            }
            return Situacao;
        }
    }
}