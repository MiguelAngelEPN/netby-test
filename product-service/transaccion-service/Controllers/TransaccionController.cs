using Microsoft.AspNetCore.Mvc;
using transaccion_service.Dto;
using transaccion_service.Models;
using transaccion_service.Service;

namespace transaccion_service.Controllers
{
    [ApiController]
    [Route("api/transaccion-service/[controller]")]
    public class TransaccionController : ControllerBase
    {
        private readonly ITransaccionService _transaccionService;
        public TransaccionController(ITransaccionService transaccionService)
        {
            _transaccionService = transaccionService;
        }

        [HttpGet("gettransaccionlist")]
        public async Task<IActionResult> GetListTransaccion([FromQuery] int page = 1, [FromQuery] int amount = 10)
        {
            var result = await _transaccionService.GetListTransaccion(page, amount);
            return StatusCode(result.Code, result);
        }

        [HttpPost("registertransaccion")]
        public async Task<IActionResult> RegisterTransaccion([FromBody] TransaccionDto dto)
        {
            var result = await _transaccionService.RegisterTransaccion(dto);
            return StatusCode(result.Code, result);
        }

        [HttpPut("updatetransaccion")]
        public async Task<IActionResult> UpdateTransaccion([FromBody] Transaccion transaccion)
        {
            var result = await _transaccionService.UpdateTransaccion(transaccion);
            return StatusCode(result.Code, result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaccion(int id)
        {
            var result = await _transaccionService.DeleteTransaccion(id);
            return StatusCode(result.Code, result);
        }
    }
}
