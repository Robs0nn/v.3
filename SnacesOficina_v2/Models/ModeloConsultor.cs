using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloConsultor : ModeloPessoa
    {
        public string Login { get; set; }
        public string Senha { get; set; }
        internal IEnumerable ListaConsultor()
        {
            List<ModeloConsultor> Consultor = new List<ModeloConsultor>();
            string query = "select * from consultor;";
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloConsultor consultor = new ModeloConsultor();
                    consultor.Id = leitor.GetInt32("Id");
                    consultor.Login = leitor.GetString("Login");
                    consultor.Senha = leitor.GetString("Senha");
                    
                    Consultor.Add(consultor);
                }
                leitor.Close();

            }
            return Consultor;
        }
    }
}