using MySql.Data.MySqlClient;
using SnacesOficina_v2.metodos;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class ModeloCliente : ModeloPessoa
    {
        internal IEnumerable ListaCliente()
        {
            List<ModeloCliente> Clientes = new List<ModeloCliente>();
            string query = "select cliente.Id, pessoas.Nome as NomeCliente from cliente " +
                           " inner join pessoas on pessoas.id = cliente.Id_pessoa;" ;
            ConexaoSql bd = new ConexaoSql();
            MySqlConnection conexao = bd.conexaobd();
            using (MySqlCommand comando = new MySqlCommand(query, conexao))
            {
                conexao.Open();
                MySqlDataReader leitor = comando.ExecuteReader();
                while (leitor.Read())
                {
                    ModeloCliente cliente = new ModeloCliente();
                    cliente.Id = leitor.GetInt32("Id");
                    cliente.Nome = leitor.GetString("NomeCLiente");

                    Clientes.Add(cliente);
                }
                leitor.Close();

            }
            return Clientes;
        }
    }
}