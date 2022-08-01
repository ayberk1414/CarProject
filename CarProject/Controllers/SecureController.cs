using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoDemo.Controllers
{
    public class SecureController : Controller
    {
        MongoDemo.App_Start.MongoContext _dbContext;

        public SecureController()
        {
            _dbContext = new MongoDemo.App_Start.MongoContext();
        }

        // GET: Secure
        public ActionResult Index()
        {
            var model = new Secure();
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(Secure model)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("InputModel");
            var query = Query.And(Query.EQ("Username", model.UserName), Query.EQ("Password", model.Password));

            var count = document.FindAs<InputModel>(query).Count();

            
            if(count == 0)
            {
                TempData["AlertMessage"] ="Kullanıcı Bulunamadı!";
            ;
            }
            else
            {
                TempData["AlertMessage"] = " İslem Basarılı";
                return RedirectToAction("Index", "Carinformation");
            }

            return View(model);
        }


    }
}