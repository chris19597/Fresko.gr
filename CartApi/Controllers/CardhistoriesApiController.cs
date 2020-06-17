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
using CartApi;
using CartApi.Models;

namespace CartApi.Controllers
{
    public class CardhistoriesApiController : ApiController
    {
        private Context db = new Context();

        // GET: api/CardhistoriesApi
        public IQueryable<Cardhistory> GetCardsH()
        {
            return db.CardsH;
        }

        // GET: api/CardhistoriesApi/5
        [ResponseType(typeof(Cardhistory))]
        public IHttpActionResult GetCardhistory(string id)
        {
            Cardhistory cardhistory = db.CardsH.Find(id);
            if (cardhistory == null)
            {
                return NotFound();
            }

            return Ok(cardhistory);
        }

        // PUT: api/CardhistoriesApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCardhistory(string id, Cardhistory cardhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != cardhistory.ID)
            {
                return BadRequest();
            }

            db.Entry(cardhistory).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardhistoryExists(id))
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

        // POST: api/CardhistoriesApi
        [ResponseType(typeof(Cardhistory))]
        public IHttpActionResult PostCardhistory(Cardhistory cardhistory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.CardsH.Add(cardhistory);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardhistoryExists(cardhistory.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = cardhistory.ID }, cardhistory);
        }

        // DELETE: api/CardhistoriesApi/5
        [ResponseType(typeof(Cardhistory))]
        public IHttpActionResult DeleteCardhistory(string id)
        {
            Cardhistory cardhistory = db.CardsH.Find(id);
            if (cardhistory == null)
            {
                return NotFound();
            }

            db.CardsH.Remove(cardhistory);
            db.SaveChanges();

            return Ok(cardhistory);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardhistoryExists(string id)
        {
            return db.CardsH.Count(e => e.ID == id) > 0;
        }
    }
}