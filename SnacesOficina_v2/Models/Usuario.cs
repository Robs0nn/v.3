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
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Senha { get; set; }

        internal IEnumerable ListaUsuario()
        {
            List<Usuario> usuarios = new List<Usuario>();
            string query = "select * from usuario;";

            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    Usuario usuario  = new Usuario();
                    usuario.Id = leitor.GetInt32("Id");
                    usuario.Login = leitor.GetString("Login");
                    usuario.Senha = leitor.GetString("Senha");
                    usuarios.Add(usuario);
                }
                leitor.Close();

            }
            return usuarios;
        }

    }
}