using GreenhouseBackend.Models;
using GreenhouseBackend.Services;
using Microsoft.AspNetCore.Mvc;


namespace GreenhouseBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReadingsController : ControllerBase
    {
        private readonly ReadingsService _readingsService;
        public ReadingsController(ReadingsService readingsService) =>
            _readingsService = readingsService;
        [HttpGet]
        public async Task<List<Reading>> Get() =>
            await _readingsService.GetAsync();

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Reading>> Get(string id)
        {
            var reading = await _readingsService.GetAsync(id);
            if (reading is null)
            {
                return new NotFoundResult();
            }
            return reading;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Reading newReading)
        {
            await _readingsService.CreateAsync(newReading);
            return new CreatedAtActionResult(
                nameof(Get),
                "Readings",
                new { id = newReading.Id },
                newReading);
        }

        [HttpPost("webhook/ttn")]
        public async Task<IActionResult> TtnWebhook([FromBody] TtnWebhookPayload payload)
        {
            if (payload?.UplinkMessage?.DecodedPayload == null)
            {
                return BadRequest("Invalid TTN payload");
            }

            var decodedPayload = payload.UplinkMessage.DecodedPayload;
            
            var newReading = new Reading
            {
                AirTemperature = decodedPayload.AirTemperature,
                GroundHumidity = decodedPayload.GroundHumidity,
                WaterTank = decodedPayload.WaterTank,
                Battery = decodedPayload.Battery
            };

            await _readingsService.CreateAsync(newReading);
            
            return Ok(new { message = "Reading created successfully", id = newReading.Id });
        }
        [HttpPut("{id:length(24)}")]

        public async Task<IActionResult> Update(string id, Reading updatedReading)
        {
            var reading = await _readingsService.GetAsync(id);
            if (reading is null)
            {
                return new NotFoundResult();
            }
            updatedReading.Id = reading.Id;
            await _readingsService.UpdateAsync(id, updatedReading);
            return new NoContentResult();
        }
        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var reading = await _readingsService.GetAsync(id);
            if (reading is null)
            {
                return new NotFoundResult();
            }
            await _readingsService.RemoveAsync(id);
            return new NoContentResult();
        }
    }
}
