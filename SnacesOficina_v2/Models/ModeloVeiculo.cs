using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloVeiculo
    {
        public int Id { get; set; }
        public string Placa { get; set; }
        public string Chassi { get; set; }
        public ModeloMarca Marca { get; set; }
        public ModeloModelo Modelo { get; set; }
        public DateTime AnoFabricacao { get; set; }
        public DateTime AnoModelo { get; set; }

        internal List<ModeloVeiculo> ListaVeiculo()
        {
            List<ModeloVeiculo> veiculos = new List<ModeloVeiculo>();
            string query = "select * from veiculos;";

            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloVeiculo veiculo = new ModeloVeiculo();
                    veiculo.Id = leitor.GetInt32("Id");
                    veiculo.Placa = leitor.GetString("Placa");
                    veiculo.Chassi = leitor.GetString("Chassi");
                    veiculo.Marca = new ModeloMarca();
                    veiculo.Marca.Id = leitor.GetInt32("Id");
                    veiculo.Marca.Descricao = leitor.GetString("Id_marca");
                    veiculo.Modelo = new ModeloModelo();
                    veiculo.Modelo.ID = leitor.GetInt32("Id");
                    veiculo.Modelo.Descricao = leitor.GetString("Id_modelo");
                    veiculo.AnoFabricacao = leitor.GetDateTime("AnoFabricacao");
                    veiculo.AnoModelo = leitor.GetDateTime("AnoModelo");

                    veiculos.Add(veiculo);
                }
                leitor.Close();

            }
            return veiculos;
        }
    }
}