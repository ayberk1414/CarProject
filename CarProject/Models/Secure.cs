using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDemo.Models
{
    public class Secure
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public ObjectId Id { get; set; }
        [BsonElement("Username")]
        public string UserName { get; set; }
        [BsonElement("Password")]
        public string Password { get; set; }

    };
}
