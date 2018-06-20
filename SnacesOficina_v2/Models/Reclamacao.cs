using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class Reclamacao
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        internal IEnumerable ListaReclamacao()
        {
            List<Reclamacao> reclamacaos = new List<Reclamacao>();
            string query = "select * from reclamacao;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Reclamacao reclamacao = new Reclamacao();
                    reclamacao.Id = leitor.GetInt32("Id");
                    reclamacao.Descricao = leitor.GetString("Descricao");
                    

                    reclamacaos.Add(reclamacao);
                }
                leitor.Close();

            }
            return reclamacaos;
        }
    }
}