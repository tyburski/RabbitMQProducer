using Microsoft.AspNetCore.Mvc;

namespace RabbitMQProducer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderStatusController : ControllerBase
    {
        private readonly IProducer _producer;

        public OrderStatusController(IProducer producer)
        {
            _producer = producer;
        }

        [HttpPost("completing/{id}")]
        public ActionResult CompletingOrder([FromRoute]int id)
        {
            if (_producer.CompletingOrderMessage(id) is true) return Ok();
            else return BadRequest();
            
        }
        [HttpPost("sending/{id}")]
        public ActionResult SendingOrder([FromRoute] int id)
        {
            if (_producer.SendingOrderMessage(id) is true) return Ok();
            else return BadRequest();

        }
    }
}
