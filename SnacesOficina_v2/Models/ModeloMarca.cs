using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloMarca
    {
        public int Id { get; set; }
        public string Descricao { get; set; }

        internal List<ModeloMarca> ListaMarca()
        {
            List<ModeloMarca> marcas = new List<ModeloMarca>();
            string query = "select * from marcas;";

            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloMarca marca = new ModeloMarca();
                    marca.Id = leitor.GetInt32("Id");
                    marca.Descricao = leitor.GetString("Descricao");
                 
                    marcas.Add(marca);
                }
                leitor.Close();

            }
            return marcas;
        }
    }
}