using SnacesOficina_v2.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SnacesOficina_v2.Controllers
{
    public class ApiOSController : ApiController
    {
        
        // GET: api/ApiOS
        public IEnumerable Get()
        {
            
            try
            {
                var pessoa = new ModeloPessoa().ListaClientes();
                return pessoa;

            }
            catch (Exception)
            {

                throw;
            }
            
        }

        // GET: api/ApiOS/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/ApiOS
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/ApiOS/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/ApiOS/5
        public void Delete(int id)
        {
        }
    }
}
