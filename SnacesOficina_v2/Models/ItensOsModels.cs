using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ItensOsModels
    {
        public int Id { get; set; }
        [Display(Name ="Ordem Serviço")]
        public int IdOs { get; set; }
        public int IdItem { get; set; }
        public int QtdItens { get; set; }
        public double ValorItensOs { get; set; }
        internal IEnumerable ListaItensOs()
        {
            List<ItensOsModels> Itens = new List<ItensOsModels>();
            string query = "select * itensos ";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ItensOsModels itens = new ItensOsModels();
                    itens.Id = leitor.GetInt32("Id");
                    itens.IdOs = leitor.GetInt32("IdOS");
                    itens.IdItem = leitor.GetInt32("IdItem");
                    itens.QtdItens = leitor.GetInt32("QtdItens");
                    itens.ValorItensOs = leitor.GetDouble("ValorItensOs");
                    Itens.Add(itens);
                }
                leitor.Close();

            }
            return Itens;
        }
    }
}
