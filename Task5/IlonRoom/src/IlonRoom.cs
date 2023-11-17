using Microsoft.AspNetCore.Mvc;
using Task2;
using CardPickStrategy; 

namespace IlonRoom
{
    [ApiController]
    [Route("[controller]")]
    public class IlonRoomController : ControllerBase
    {
        [HttpPost]
        public IActionResult Play([FromBody] Card[] cards)
        {
            Console.WriteLine("caths");
            IPartner ilon = new Ilon();

            ICardPickStrategy strategy = new RandomCardStrategy();

            ilon.strategy = strategy;
            int num = ilon.Play(cards);
            return Ok(num);
        }
    }
}