using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloModelo
    {
        public int ID { get; set; }
        public string Descricao { get; set; }
        public int ID_Marca { get; set; }
        public IEnumerable ListaModelo()
        {
            return ListaModelo(0);
        }
        public List<ModeloModelo> ListaModelo(int id)
        {
            List<ModeloModelo> Modelos = new List<ModeloModelo>();
            string query = "select * from modelos ";
            if(id > 0)
            {
                query += " where Id_marca = " + id.ToString();
            }
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloModelo modelo = new ModeloModelo();
                    modelo.ID = leitor.GetInt32("Id");
                    modelo.Descricao = leitor.GetString("Descricao");
                    modelo.ID_Marca = leitor.GetInt32("Id_marca");
                    Modelos.Add(modelo);
                }
                leitor.Close();

            }
            return Modelos;
        }
    }

}