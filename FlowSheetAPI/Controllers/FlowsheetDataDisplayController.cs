using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlowsheetDataDisplayController : ControllerBase
    {
        private readonly IFlowsheetService _flowsheetService;
        private readonly ILogger<FlowsheetDataDisplayController> _logger;

        public FlowsheetDataDisplayController(IFlowsheetService flowsheetService,
            ILogger<FlowsheetDataDisplayController> logger)
        {
            _flowsheetService = flowsheetService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllFlowsheets()
        {
            try
            {
                var flowSheets = await _flowsheetService.GetAllAsync();
                return Ok(flowSheets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheets" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{flowsheetId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetFlowsheetById(Guid flowsheetId)
        {
            try
            {
                var flowSheet = _flowsheetService.GetByIdAsync(flowsheetId);
                return Ok(flowSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by id" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
