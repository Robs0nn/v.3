using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.metodos
{
    public class ConexaoSql
    {
        public MySqlConnection conexaobd()
        {
            return new MySqlConnection("Persist Security Info=False;SslMode=none; server=localhost; database=sancesoficinaversao_2;uid=root");
        }
    }
}