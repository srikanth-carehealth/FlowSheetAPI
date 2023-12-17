using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    { 
        private ILogger<AdminController> _logger { get; }
        private readonly IAdminService _adminService;

        public AdminController(ILogger<AdminController> logger,
            IAdminService adminService)
        {
            _logger = logger;
            _adminService = adminService;
        }

        [HttpGet]
        [Route("GetSpecialityConditionTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecialityConditionTypes()
        {
            try
            {
                var specialityConditionTypes = await _adminService.GetSpecialityConditionTypes();
                return Ok(specialityConditionTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching speciality condition types" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{specialityTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetConditionTypeBySpeciality(Guid specialityTypeId)
        {
            try
            {
                var conditionTypes = await _adminService.GetConditionTypeBySpeciality(specialityTypeId);
                return Ok(conditionTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching condition types by speciality" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetSpecialityTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSpecialityTypes()
        {
            try
            {
                var specialityTypes = await _adminService.GetSpecialityTypes();
                return Ok(specialityTypes);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching speciality types" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetLabItems")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLabItems()
        {
            try
            {
                var labItems = await _adminService.GetLabItems();
                return Ok(labItems);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching lab items" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("GetLabItemSpeciality")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLabItemSpeciality()
        {
            try
            {
                var labItemSpeciality = await _adminService.GetLabItemSpeciality();
                return Ok(labItemSpeciality);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching lab item speciality list" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet]
        [Route("{specialityTypeId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetLabItemBySpeciality(Guid specialityTypeId)
        {
            try
            {
                var labItemSpeciality = await _adminService.GetLabItemBySpeciality(specialityTypeId);
                return Ok(labItemSpeciality);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while fetching lab item by speciality" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //public async Task<IActionResult> AddSpecialityType(SpecialityType specialityType)
        //{
        //    try
        //    {
        //        var specialityTypes = await .AddSpecialityType(specialityType);
        //        return Ok(specialityTypes);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.LogError("Error occurred while adding speciality type" + ex);
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
    }
}
