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
    public class ModeloPessoa
    {
        public int Id { get; set; }
        [Required]
        public string Cpf_Cnpj { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Endereco { get; set; }
        [Display(Name = "Selecionar")]
        public int SeletorPessoa { get; set; }


        internal IEnumerable ListaClientes()
        {
            List<ModeloPessoa> pessoas = new List<ModeloPessoa>();
            string query = "select * from pessoas;";

            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();


            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();

                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                   ModeloPessoa pessoa = new ModeloPessoa();
                    pessoa.Id = leitor.GetInt32("Id");
                    pessoa.Cpf_Cnpj = leitor.GetString("cpf_cnpj");
                    pessoa.Nome = leitor.GetString("Nome");
                    pessoa.Email = leitor.GetString("Email");
                    pessoa.Endereco = leitor.GetString("Endereco");
                    
                    pessoas.Add(pessoa);
                }
                leitor.Close();

            }
            return pessoas;
        }
    }
}