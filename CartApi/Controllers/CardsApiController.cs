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
    public class CardsApiController : ApiController
    {
        private Context db = new Context();

        // GET: api/CardsApi
        public IQueryable<Card> GetCards()
        {
            return db.Cards;
        }

        // GET: api/CardsApi/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult GetCard(string id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            return Ok(card);
        }

        // PUT: api/CardsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCard(string id, Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != card.ID)
            {
                return BadRequest();
            }

            db.Entry(card).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CardExists(id))
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

        // POST: api/CardsApi
        [ResponseType(typeof(Card))]
        public IHttpActionResult PostCard(Card card)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Cards.Add(card);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (CardExists(card.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = card.ID }, card);
        }

        // DELETE: api/CardsApi/5
        [ResponseType(typeof(Card))]
        public IHttpActionResult DeleteCard(string id)
        {
            Card card = db.Cards.Find(id);
            if (card == null)
            {
                return NotFound();
            }

            db.Cards.Remove(card);
            db.SaveChanges();

            return Ok(card);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CardExists(string id)
        {
            return db.Cards.Count(e => e.ID == id) > 0;
        }
    }
}