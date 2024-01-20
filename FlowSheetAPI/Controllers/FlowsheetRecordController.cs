using FlowSheetAPI.DataTransferObjects;
using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    //[Authorize]
    public class FlowsheetRecordController : ControllerBase
    {
        private readonly IFlowsheetService _flowsheetService;
        private readonly ILogger<FlowsheetRecordController> _logger;
        private IMemoryCache _cache;
        private string flowSheetWrapperCacheKey = "flowSheetWrapper";

        public FlowsheetRecordController(IFlowsheetService endocrinologyService, ILogger<FlowsheetRecordController> logger, IMemoryCache cache)
        {
            _flowsheetService = endocrinologyService;
            _logger = logger;
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
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
            flowSheetWrapperCacheKey = string.Concat("flowSheetWrapper", conditionSpecialityType.Replace(" ",""), ehrPatientId.ToString());
            if (_cache.TryGetValue(flowSheetWrapperCacheKey, out FlowSheetWrapper? flowsheets))
            {
                _logger.Log(LogLevel.Information, "flowSheetWrapper list found in cache.");
            } else
            {
                try
                {
                    flowsheets = _flowsheetService.GetBySpecialityConditionAndPatient(conditionSpecialityType, ehrPatientId).Result;

                    var cacheEntryOptions = new MemoryCacheEntryOptions()
                    .SetSlidingExpiration(TimeSpan.FromSeconds(3600))
                    .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                    .SetPriority(CacheItemPriority.Normal);
                    //.SetSize(1024);
                    _cache.Set(flowSheetWrapperCacheKey, flowsheets, cacheEntryOptions);
                }
                catch (Exception ex)
                {
                    _logger.LogError("Error occurred while fetching flowsheet by patient and doctor" + ex);
                    return StatusCode(StatusCodes.Status500InternalServerError);
                }
            }
            return Ok(flowsheets);
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
        public IActionResult AddFlowSheet([FromBody] FlowSheetIM? flowsheet)
        {
            try
            {
                if (flowsheet == null)
                {
                    _logger.LogError("Flowsheet object sent from client is null.");
                    return BadRequest("Invalid data");
                }

                //Insert a record into the Flowsheet table
                var response = _flowsheetService.InsertFlowSheet(flowsheet);
                //remove the cached entry
                _cache.Remove(response.Cachekey);
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
