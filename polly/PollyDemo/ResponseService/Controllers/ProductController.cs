using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ResponseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet("[action]/{id:int}")]
        public async Task<IActionResult> GetResponse(int id)
        {
            var ranodm = new Random();

            if(ranodm.Next(1, 100) >= id)
                return BadRequest(StatusCodes.Status400BadRequest);

            return Ok();
        }


    }
}
