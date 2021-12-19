using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FizikalTest_Shai.Controllers
{
    [Route("Contacts")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private readonly ILogger<CustomersController> _logger;

        public CustomersController(ILogger<CustomersController> logger)
        {
            _logger = logger;
        }

        [Route("GetList")]
        [HttpPost]
        public IActionResult GetList()
        {
            try
            {
                _logger.LogDebug($"***************** Start GetList ****************");
                var CustomersLstResponse = BL.ContactsBL.GetList();
                _logger.LogDebug($"***************** Done GetList ****************");
                return Ok(CustomersLstResponse);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"GetList ERROR: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

    }
}
