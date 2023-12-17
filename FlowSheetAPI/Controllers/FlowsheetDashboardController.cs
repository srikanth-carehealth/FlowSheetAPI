using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class FlowsheetDashboardController : ControllerBase
    {
        private readonly IFlowsheetService _flowsheetService;
        private readonly ILogger<FlowsheetDashboardController> _logger;

        public FlowsheetDashboardController(IFlowsheetService flowsheetService,
            ILogger<FlowsheetDashboardController> logger)
        {
            _flowsheetService = flowsheetService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll()
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetById(Guid id)
        {
            try
            {
                var flowSheet = _flowsheetService.GetByIdAsync(id);
                return Ok(flowSheet);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by id" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{ehrUserName}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByDoctor(string ehrUserName)
        {
            try
            {
                var doctor = _flowsheetService.GetByDoctor(ehrUserName);
                return Ok(doctor);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by doctor" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{ehrPatientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByPatient(int ehrPatientId)
        {
            try
            {
                var patient = _flowsheetService.GetByPatient(ehrPatientId);
                return Ok(patient);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by patient" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
