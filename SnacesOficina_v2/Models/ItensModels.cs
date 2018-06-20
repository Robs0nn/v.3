using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ItensModels
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Preco { get; set; }
        internal IEnumerable ListaItens()
        {
            List<ItensModels> Itens = new List<ItensModels>();
            string query = "select * from itens; ";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ItensModels itens = new ItensModels();
                    itens.Id = leitor.GetInt32("Id");
                    itens.Descricao = leitor.GetString("Descricao");
                    itens.Preco = leitor.GetInt32("Preco");
                    Itens.Add(itens);
                }
                leitor.Close();

            }
            return Itens;
        }
    }
}