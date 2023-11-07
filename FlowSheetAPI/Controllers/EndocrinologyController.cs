using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EndocronologyController : ControllerBase
    {
        public EndocronologyController() { }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            return Ok("Endocronology");
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get(int id)
        {
            return Ok("Endocronology");
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult Post()
        {
            return Created("", "Endocronology");
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Put(int id)
        {
            return Ok("Endocronology");
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Lock(int id)
        {
            return Ok("Endocronology");
        }
    }
}
