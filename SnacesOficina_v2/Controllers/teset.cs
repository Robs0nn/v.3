using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using SnacesOficina_v2.Models;

namespace SnacesOficina_v2.Controllers
{
    public class teset : ApiController
    {
        private SnacesOficina_v2Context db = new SnacesOficina_v2Context();

        // GET: api/teset
        public IQueryable<ModeloPessoa> GetModeloPessoas()
        {
            return db.ModeloPessoas;
        }

        // GET: api/teset/5
        [ResponseType(typeof(ModeloPessoa))]
        public IHttpActionResult GetModeloPessoa(int id)
        {
            ModeloPessoa modeloPessoa = db.ModeloPessoas.Find(id);
            if (modeloPessoa == null)
            {
                return NotFound();
            }

            return Ok(modeloPessoa);
        }

        // PUT: api/teset/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutModeloPessoa(int id, ModeloPessoa modeloPessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != modeloPessoa.Id)
            {
                return BadRequest();
            }

            db.Entry(modeloPessoa).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ModeloPessoaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/teset
        [ResponseType(typeof(ModeloPessoa))]
        public IHttpActionResult PostModeloPessoa(ModeloPessoa modeloPessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.ModeloPessoas.Add(modeloPessoa);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = modeloPessoa.Id }, modeloPessoa);
        }

        // DELETE: api/teset/5
        [ResponseType(typeof(ModeloPessoa))]
        public IHttpActionResult DeleteModeloPessoa(int id)
        {
            ModeloPessoa modeloPessoa = db.ModeloPessoas.Find(id);
            if (modeloPessoa == null)
            {
                return NotFound();
            }

            db.ModeloPessoas.Remove(modeloPessoa);
            db.SaveChanges();

            return Ok(modeloPessoa);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ModeloPessoaExists(int id)
        {
            return db.ModeloPessoas.Count(e => e.Id == id) > 0;
        }
    }
}