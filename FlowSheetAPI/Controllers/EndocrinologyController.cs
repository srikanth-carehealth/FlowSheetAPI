//using FlowSheetAPI.Services.Interfaces;
//using Microsoft.AspNetCore.Mvc;

//namespace FlowSheetAPI.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class EndocronologyController : ControllerBase
//    {
//        private readonly IEndocrinologyService _endocrinologyService;
//        public EndocronologyController(IEndocrinologyService endocrinologyService)
//        {
//            _endocrinologyService = endocrinologyService;
//        }

//        [HttpGet]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public IActionResult GetAll()
//        {
//            return Ok("Endocronology");
//        }

//        [HttpGet]
//        [Route("{patientId}/GetByPatient")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public IActionResult GetByPatient(string patientId)
//        {
//            try
//            {
//                var endocrinology = _endocrinologyService.GetByPatient(patientId);
//                return Ok(endocrinology);
//            }
//            catch (Exception)
//            {
//                return StatusCode(StatusCodes.Status500InternalServerError);
//            }
//        }

//        [HttpPost]
//        [ProducesResponseType(StatusCodes.Status201Created)]
//        public IActionResult Post()
//        {
//            return Created("", "Endocronology");
//        }

//        [HttpGet]
//        [Route("{id}/Lock")]
//        [ProducesResponseType(StatusCodes.Status200OK)]
//        public IActionResult Lock(Guid id)
//        {
//            try
//            {
//                var endocrinology = _endocrinologyService.GetByIdAsync(id);
//                return Ok(endocrinology);
//            }
//            catch (Exception) { return StatusCode(StatusCodes.Status500InternalServerError); }
//        }
//    }
//}
