using FlowSheetAPI.DomainModel;
using FlowSheetAPI.Model;
using FlowSheetAPI.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlowSheetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
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
        [Route("GetLabItemBySpeciality/{specialityTypeId}")]
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

        [HttpPost]
        [Route("AddSpecialityType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddSpecialityType(SpecialityTypeViewModel specialityTypeViewModel)
        {
            try
            {
                var specialityType = new SpecialityType
                {
                    SpecialityTypeId = specialityTypeViewModel.SpecialityTypeId,
                    ClientId = specialityTypeViewModel.ClientId,
                    ClientName = specialityTypeViewModel.ClientName,
                    SpecialityName = specialityTypeViewModel.SpecialityName,
                    SpecialityCode = specialityTypeViewModel.SpecialityCode,
                };

                var response = _adminService.Upsert(specialityType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while adding speciality type" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddConditionType")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddSpecialityConditionType(SpecialityConditionType specialityConditionType)
        {
            try
            {
                var response = _adminService.Upsert(specialityConditionType);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while adding speciality condition type" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddLabItem")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddLabItem(LabItem labItem)
        {
            try
            {
                var response = _adminService.Upsert(labItem);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while adding lab item" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        [Route("AddLabItemSpeciality")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AddLabItemSpeciality(LabItemSpeciality labItemSpeciality)
        {
            try
            {
                var response = _adminService.Upsert(labItemSpeciality);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError("Error occurred while adding lab item speciality" + ex);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
