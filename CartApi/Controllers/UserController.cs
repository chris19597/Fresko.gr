using System;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CartApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace CartApi.Controllers
{
    public class UserController : Controller
    {
        public JsonResult RegisterUs(User model)
        {
            var userStore = new UserStore<IdentityUser>();
            var manager = new UserManager<IdentityUser>(userStore);

            var user = new IdentityUser {UserName = model.username};
            var result = manager.Create(user, model.password);
            using (var db = new Context())
            {
                var rol = new Roles {ID = Guid.NewGuid().ToString(), username = model.username, role = model.role};
                db.Roles.Add(rol);
                db.SaveChanges();
            }

            if (result.Succeeded)
                return Json(new {success = true});
            return Json(new {success = result.Errors.FirstOrDefault()});
        }

        public JsonResult LoginUs(User model)
        {
            var userStore = new UserStore<IdentityUser>();
            var userManager = new UserManager<IdentityUser>(userStore);
            var user = userManager.Find(model.username, model.password);

            if (user != null)
            {
                var authenticationManager = System.Web.HttpContext.Current.GetOwinContext().Authentication;
                var userIdentity = userManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                authenticationManager.SignIn(new AuthenticationProperties {IsPersistent = false}, userIdentity);
                return Json(new {success = true});
            }

            return Json(new {success = false});
        }
    }
}