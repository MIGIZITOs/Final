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
using Final.Models;

namespace Final.Controllers
{
    public class finalsController : ApiController
    {
        private DataContext db = new DataContext();

        // GET: api/finals
        public IQueryable<final> Getfinals()
        {
            return db.finals;
        }

        // GET: api/finals/5
        [ResponseType(typeof(final))]
        public IHttpActionResult Getfinal(int id)
        {
            final final = db.finals.Find(id);
            if (final == null)
            {
                return NotFound();
            }

            return Ok(final);
        }

        // PUT: api/finals/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putfinal(int id, final final)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != final.numero)
            {
                return BadRequest();
            }

            db.Entry(final).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!finalExists(id))
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

        // POST: api/finals
        [ResponseType(typeof(final))]
        public IHttpActionResult Postfinal(final final)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.finals.Add(final);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = final.numero }, final);
        }

        // DELETE: api/finals/5
        [ResponseType(typeof(final))]
        public IHttpActionResult Deletefinal(int id)
        {
            final final = db.finals.Find(id);
            if (final == null)
            {
                return NotFound();
            }

            db.finals.Remove(final);
            db.SaveChanges();

            return Ok(final);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool finalExists(int id)
        {
            return db.finals.Count(e => e.numero == id) > 0;
        }
    }
}