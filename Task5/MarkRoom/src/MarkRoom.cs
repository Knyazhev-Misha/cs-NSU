using Microsoft.AspNetCore.Mvc;
using Task2;
using CardPickStrategy; 

namespace MarkRoom
{
    [ApiController]
    [Route("[controller]")]
    public class MarkRoomController : ControllerBase
    {
        [HttpPost]
        public IActionResult Play([FromBody] Card[] cards)
        {
            Console.WriteLine("caths");
            IPartner mark = new Mark();

            ICardPickStrategy strategy = new RandomCardStrategy();

            mark.strategy = strategy;
            int num = mark.Play(cards);
            return Ok(num);
        }
    }
}