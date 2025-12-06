using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace GreenhouseBackend.Models
{
    public class Reading
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name")]
        [JsonPropertyName("Name")]
        public decimal AirTemperature { get; set; }

        public decimal GroundHumidity { get; set; }

        public decimal WaterTank { get; set; }

        public decimal Battery { get; set; }
    }
}
