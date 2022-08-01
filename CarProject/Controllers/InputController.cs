using MongoDB.Bson;
using MongoDB.Driver.Builders;
using MongoDemo.App_Start;
using MongoDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MongoDemo.Controllers
{
    public class MongoInputController : Controller
    {
        MongoDemo.App_Start.MongoContext _dbContext;


        public MongoInputController()
        {
            _dbContext = new MongoDemo.App_Start.MongoContext();
        }
        public ActionResult Input()
        {
            return View();
        }

        public ActionResult Create()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Create(InputModel inputmodel)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("InputModel");
            MongoDB.Driver.IMongoQuery query = Query.And(Query.EQ("Username", inputmodel.Username), Query.EQ("Surname", inputmodel.Surname));

            var count = document.FindAs<InputModel>(query).Count();

            if (count == 0)
            {
                var result = document.Insert(inputmodel);
                return RedirectToAction("Create","MongoInput");

            }
            else
            {
                TempData["AlertMessage"] = "  İslem Basarılı";
                return RedirectToAction("Index", "Secure");
            }
        }  

    }
}