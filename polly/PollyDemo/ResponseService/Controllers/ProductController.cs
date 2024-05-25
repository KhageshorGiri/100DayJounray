using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        //[HttpGet("[action]/{id:int}")]
        [HttpGet("[action]")]
        public async Task<IActionResult> GetResponse([FromQuery] int id)
        {
            var ranodm = new Random();

            if (ranodm.Next(1, 100) >= id)
            {
                Console.WriteLine("Failure --> Not able to server cilient request.");
                return StatusCode(StatusCodes.Status500InternalServerError);

            }

            Console.WriteLine("Success --> Done.");
            return Ok(new List<string> { "Item1", "Item2", "Item3"});
        }


    }
}
