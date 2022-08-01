using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;

namespace MongoDemo.Models
{
    public class InputModel
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Username")]
        public string Username { get; set; }
        [BsonElement("Surname")]
        public string Surname { get; set; }
        [BsonElement("Mail")]
        public string Mail { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }
        public bool Surnam { get; internal set; }

       
    }



}