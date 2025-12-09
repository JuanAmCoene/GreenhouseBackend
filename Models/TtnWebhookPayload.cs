using System.Text.Json.Serialization;

namespace GreenhouseBackend.Models
{
    public class TtnWebhookPayload
    {
        [JsonPropertyName("uplink_message")]
        public UplinkMessage? UplinkMessage { get; set; }
    }

    public class UplinkMessage
    {
        [JsonPropertyName("decoded_payload")]
        public DecodedPayload? DecodedPayload { get; set; }
    }

    public class DecodedPayload
    {
        [JsonPropertyName("AirTemperature")]
        public decimal AirTemperature { get; set; }

        [JsonPropertyName("GroundHumidity")]
        public decimal GroundHumidity { get; set; }

        [JsonPropertyName("WaterTank")]
        public decimal WaterTank { get; set; }

        [JsonPropertyName("Battery")]
        public decimal Battery { get; set; }
    }
}
