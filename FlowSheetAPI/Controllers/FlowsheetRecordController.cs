using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class FlowsheetRecordController : ControllerBase
    {
        private readonly IFlowsheetService _flowsheetService;
        private readonly ILogger<FlowsheetRecordController> _logger;

        public FlowsheetRecordController(IFlowsheetService endocrinologyService, ILogger<FlowsheetRecordController> logger)
        {
            _flowsheetService = endocrinologyService;
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
                _logger.LogError("Error occurred while fetching flowsheet by patient" + ex);
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

        [HttpGet]
        [Route("{ehrDoctorUserName}/{ehrPatientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetByDoctorAndPatient(string ehrDoctorUserName, int ehrPatientId)
        {
            try
            {
                var flowsheets = _flowsheetService.GetByDoctorAndPatient(ehrDoctorUserName, ehrPatientId);
                return Ok(flowsheets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by patient and doctor" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{conditionSpecialityType}/{ehrPatientId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetBySpecialityAndPatient(string conditionSpecialityType, int ehrPatientId)
        {
            try
            {
                var flowsheets = _flowsheetService.GetBySpecialityConditionAndPatient(conditionSpecialityType, ehrPatientId);
                return Ok(flowsheets);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching flowsheet by patient and doctor" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult UpsertFlowSheet([FromBody] Flowsheet? flowsheet)
        {
            try
            {
                if (flowsheet == null)
                {
                    _logger.LogError("Flowsheet object sent from client is null.");
                    return BadRequest("Invalid data");
                }

                //Insert a record into the Flowsheet history table
                var response = _flowsheetService.Upsert(flowsheet);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while saving flowsheet data. " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public IActionResult InsertFlowSheet([FromBody] FlowSheetIM? flowsheet)
        {
            try
            {
                if (flowsheet == null)
                {
                    _logger.LogError("Flowsheet object sent from client is null.");
                    return BadRequest("Invalid data");
                }

                //Insert a record into the Flowsheet history table
                var response = _flowsheetService.InsertFlowSheet(flowsheet);

                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while saving flowsheet data. " + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
