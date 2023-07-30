using ApiProducer.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProducer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SendController : ControllerBase
    {
        private readonly IProducerService producerService;
        private const string topic = "MESSAGES";

        public SendController(IProducerService producerService)
        {
            this.producerService = producerService;
        }

        [HttpPost]
        public async Task<ActionResult> PostAsync([FromBody]string message)
        {
            if(message == null)
            {
                return BadRequest("String is empty");
            }
            await producerService.SetTopic(topic);
            await producerService.Send(message);
            return Ok();
        }
    }
}
