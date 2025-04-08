using Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Interface;

namespace GaiaProject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GaiaProject : ControllerBase
    {
        private readonly IOperationService _service;

        public GaiaProject(IOperationService service)
        {
            _service = service;
        }

        [HttpPost("calculate")]
        public async Task<IActionResult> Calculate([FromBody] OperationRequest request)
        {
            try
            {
                var result = await _service.CalculateAsync(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("supported-operations")]
        public async Task<IActionResult> GetSupportedOperations()
        {
            var operations = await _service.GetSupportedOperationsAsync();
            return Ok(operations);
        }
    }
}
