using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloTipoOs
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        internal IEnumerable ListaTipoOs()
        {
            List<ModeloTipoOs> TipoOs = new List<ModeloTipoOs>();
            string query = "select * from tipoos;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloTipoOs tipoOs = new ModeloTipoOs();
                    tipoOs.Id = leitor.GetInt32("Id");
                    tipoOs.Descricao = leitor.GetString("Descricao");
                    

                    TipoOs.Add(tipoOs);
                }
                leitor.Close();

            }
            return TipoOs;
        }
    }
}