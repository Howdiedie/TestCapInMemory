using DotNetCore.CAP;
using DotNetCore.CAP.Messages;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CapTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CAPTestController : ControllerBase, ICapSubscribe
    {
        private readonly ICapPublisher _capPublisher;

        public CAPTestController(ICapPublisher capPublisher)
        {
            _capPublisher = capPublisher;
        }

        [HttpPost("PublishMessage")]
        public async Task<IActionResult> PublishMessage()
        {
            var Msg = new { Message = "測試, CAP!" };
            await _capPublisher.PublishAsync("your_topic", Msg);
            return Ok();
        }

        [CapSubscribe("your_topic")]
        public void ConsumeMessage(Message message)
        {
            Console.WriteLine("得到了" + message);
        }

    }
}