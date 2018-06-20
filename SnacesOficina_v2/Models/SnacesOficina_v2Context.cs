using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SnacesOficina_v2.Models
{
    public class SnacesOficina_v2Context : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public SnacesOficina_v2Context() : base("name=SnacesOficina_v2Context")
        {
        }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.Usuario> Usuarios { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ItensOsModels> ItensOsModels { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ItensModels> ItensModels { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ModeloPessoa> ModeloPessoas { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.Reclamacao> Reclamacaos { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ModeloOrdemServico> ModeloOrdemServicoes { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ModeloVeiculo> ModeloVeiculoes { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.Logar> Logars { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ModeloMarca> ModeloMarcas { get; set; }

        public System.Data.Entity.DbSet<SnacesOficina_v2.Models.ModeloModelo> ModeloModeloes { get; set; }
    }
}
