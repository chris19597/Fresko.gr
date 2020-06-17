using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using CartApi.Classes;
using CartApi.Models;

namespace CartApi.Controllers
{
    public class ProionsApiController : ApiController
    {
        private readonly Context db = new Context();

        // GET: api/ProionsApi
        public IQueryable<Proion> GetProioda()
        {
            return db.Proioda;
        }

        // GET: api/ProionsApi/5
        [ResponseType(typeof(Proion))]
        public IHttpActionResult GetProion(string id)
        {
            var proion = db.Proioda.Find(id);
            if (proion == null) return NotFound();

            return Ok(proion);
        }

        // PUT: api/ProionsApi/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutProion(string id, Proion proion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            if (id != proion.ID) return BadRequest();

            db.Entry(proion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProionExists(id))
                    return NotFound();
                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/ProionsApi
        [ResponseType(typeof(Proion))]
        public IHttpActionResult PostProion(Proion proion)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            db.Proioda.Add(proion);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (ProionExists(proion.ID))
                    return Conflict();
                throw;
            }

            return CreatedAtRoute("DefaultApi", new {id = proion.ID}, proion);
        }

        // DELETE: api/ProionsApi/5
        [ResponseType(typeof(Proion))]
        public IHttpActionResult DeleteProion(string id)
        {
            var proion = db.Proioda.Find(id);
            if (proion == null) return NotFound();

            db.Proioda.Remove(proion);
            db.SaveChanges();

            return Ok(proion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing) db.Dispose();
            base.Dispose(disposing);
        }

        private bool ProionExists(string id)
        {
            return db.Proioda.Count(e => e.ID == id) > 0;
        }


        public void PriceCart(Temp model)
        {
            var card = new Card();
            card.ID = Guid.NewGuid().ToString();
            var counter = 0;
            for (var i = 0; i < model.itemlist.Count; i++)
                switch (i)
                {
                    case 0:
                        card.name1 = model.itemlist[i];
                        card.quantity1 = model.quantitylist[i];
                        break;
                    case 1:
                        card.name2 = model.itemlist[i];
                        card.quantity2 = model.quantitylist[i];
                        break;
                    case 2:
                        card.name3 = model.itemlist[i];
                        card.quantity3 = model.quantitylist[i];
                        break;
                }

            using (var db = new Context())
            {
                for (var i = 0; i < model.itemlist.Count; i++)
                {
                    var _value = model.itemlist[i];
                    var tocount = db.Proioda.First(sh => sh.Name.Contains(_value)).Price;
                    var price = int.Parse(tocount);
                    counter = counter + price * int.Parse(model.quantitylist[i]);
                }

                db.Cards.Add(card);
                db.SaveChanges();
            }
        }
    }
}