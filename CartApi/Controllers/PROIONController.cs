using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;
using CartApi.Classes;
using CartApi.Models;

namespace CartApi.Controllers
{
    public class PROIONController : Controller
    {
        // GET: PROION
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CreateProion(Proion model)
        {
            using (var db = new Context())
            {
                model.ID = Guid.NewGuid().ToString();
                db.Proioda.Add(model);
                db.SaveChanges();
            }

            return new EmptyResult();
        }

        public ActionResult View()
        {
            using (var db = new Context())
            {
                return View(db.Proioda.ToList());
            }
        }

        public ActionResult PriceCart(Temp model)
        {
            try
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

                    var cardh = new Cardhistory
                        {ID = Guid.NewGuid().ToString(), CardID = card.ID, username = User.Identity.Name};

                    db.Cards.Add(card);
                    db.CardsH.Add(cardh);

                    db.SaveChanges();
                }

                CalculateValues(model);
                return Json(new {price = counter});
            }
            catch (Exception e)
            {
                return Json(new {price = -1});
            }

            ;
        }

        private void CalculateValues(Temp model)
        {
            using (var db = new Context())
            {
                foreach (var item in db.Proioda)
                    for (var i = 0; i < model.itemlist.Count; i++)
                    {
                        if (item.Name == model.itemlist[i])
                        {
                            var oldnum = int.Parse(item.Qantity);
                            var newnum = int.Parse(model.quantitylist[i]);
                            var final = oldnum - newnum;
                            item.Qantity = final.ToString();
                        }

                        db.Proioda.AddOrUpdate(item);
                    }

                db.SaveChanges();
            }
        }

        public ActionResult History()
        {
            var cards = new List<Cardhistory>();
            using (var db = new Context())
            {
                return View(db.CardsH.Where(sh => sh.username == User.Identity.Name).ToList());
            }
        }

        public ActionResult GeneralHistory()
        {
            using (var db = new Context())
            {
                return View(db.CardsH.ToList());
            }
        }
    }
}