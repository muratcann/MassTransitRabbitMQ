using CommonWork;
using MassTransit;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SenderApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SenderController : ControllerBase
    {
        private readonly IBus _bus;
        private readonly IRequestClient<BalanceUpdate> _client;
        public SenderController(IBus bus, IRequestClient<BalanceUpdate> client)
        {
            _bus = bus;
            _client = client;
        }

        [HttpPost("send-tutorial")]
        public async Task<IActionResult> Test1()
        {
            var product = new Product
            {
                Name = "computer",
                Price = 500
            };

            var url = new Uri("rabbitmq://localhost/send-tutorial");

            var endpoint = await _bus.GetSendEndpoint(url);
            await endpoint.Send(product);

            return Ok("hello command send");
        }

        [HttpPost("publish-tutorial")]
        public async Task<IActionResult> Test2()
        {
            await _bus.Publish(new Person
            {
                Name = "murat",
                Email = "murat@gmail.com"
            });

            return Ok("publish message");
        }

        [HttpPost("reqres-tutorial")]
        public async Task<IActionResult> Test3()
        {
            var requestData = new BalanceUpdate
            {
                TypeOfInstruction = "minusAmount",
                Amount = 50
            };

            var request = _client.Create(requestData);
            var response = await request.GetResponse<NowBalance>();

            return Ok(response);
        }
    }
}
