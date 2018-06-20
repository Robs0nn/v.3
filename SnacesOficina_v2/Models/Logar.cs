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
    public class Logar
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        internal IEnumerable ListaLogar()
        {
            List<Logar> logar = new List<Logar>();
            string query = "select * from usuario;";

            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Logar log = new Logar();
                    log.Id = leitor.GetInt32("Id");
                    log.Login = leitor.GetString("Login");
                    log.Senha = leitor.GetString("Senha");
                    logar.Add(log);
                }
                leitor.Close();

            }
            return logar;
        }

}   }