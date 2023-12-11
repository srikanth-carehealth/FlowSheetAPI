using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LookupController : ControllerBase
    {
        private readonly ILookupService _lookupService;
        private readonly ILogger<LookupController> _logger;

        public LookupController(ILookupService lookupService, ILogger<LookupController> logger)
        {
            _lookupService = lookupService;
            _logger = logger;
        }

        [HttpGet]
        [Route("GetAllSpecialityTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSpecialityTypes()
        {
            try
            {
                var specialityTypes = await _lookupService.GetAllAsyncSpecialityTypes();
                return Ok(specialityTypes);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Error occurred while fetching speciality types");
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
