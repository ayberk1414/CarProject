using MongoDB.Bson;
using MongoDB.Driver;
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
    public class CarinformationController : Controller
    {
        MongoContext _dbContext;
        public CarinformationController()
        {
            _dbContext = new MongoContext();
        }

        public ActionResult Index()
        {
            var carDetails = _dbContext._database.GetCollection<CarModel>("CarModel").FindAll().ToList();
            return View(carDetails);
        }



        public ActionResult Create()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        [HttpPost]
        public ActionResult Create(CarModel carmodel)
        {
            var document = _dbContext._database.GetCollection<BsonDocument>("CarModel");
            var query = Query.And(Query.EQ("Carname", carmodel.Carname), Query.EQ("Color", carmodel.Color));

            var count = document.FindAs<CarModel>(query).Count();

            if (count == 0)
            {
                var result = document.Insert(carmodel);
                return RedirectToAction("Index");

            }
            else
            {
                TempData["AlertMessage"] = "  İslem Basarılı";
                return RedirectToAction("Index");
            }
            
        }

        public ActionResult Edit(string id)
        {
            var document = _dbContext._database.GetCollection<CarModel>("CarModel");

            var carDetailscount = document.FindAs<CarModel>(Query.EQ("_id", new ObjectId(id))).Count();

            if (carDetailscount > 0)
            {
                var carObjectid = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));

                var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel").FindOne(carObjectid);

                return View(carDetail);
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult Edit(string id, CarModel carmodel)
        {
            try
            {
                carmodel.Id = new ObjectId(id);
                //Mongo Query  
                var CarObjectId = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
                // Document Collections  
                var collection = _dbContext._database.GetCollection<CarModel>("CarModel");
                // Document Update which need Id and Data to Update  
                var result = collection.Update(CarObjectId, Update.Replace(carmodel), UpdateFlags.None);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(string id)
        {
            var carId = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
            var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel").FindOne(carId);
            return View(carDetail);
        }
        public ActionResult Delete(string id)
        {
            var carObjectId = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
            var carDetail = _dbContext._database.GetCollection<CarModel>("CarModel")
                .FindOne(carObjectId);
            return View(carDetail);
        }
        // POST: Carinformation/Delete/5  
        [HttpPost]
        public ActionResult Delete(string id, CarModel CarModel)
        {
            try
            {
                //Mongo Query  
                var carObjectid = Query<CarModel>.EQ(p => p.Id, new ObjectId(id));
                // Document Collections  
                var collection = _dbContext._database.GetCollection<CarModel>("CarModel");
                // Document Delete which need ObjectId to Delete Data   
                var result = collection.Remove(carObjectid, RemoveFlags.Single);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

    }
}