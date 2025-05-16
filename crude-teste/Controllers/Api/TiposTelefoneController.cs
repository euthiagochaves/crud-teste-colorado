using crude_teste.Domain.Interfaces;
using crude_teste.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace crude_teste.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiposTelefoneController : ControllerBase
    {
        private readonly ITipoTelefoneRepository _tipoTelefoneRepository;

        public TiposTelefoneController(ITipoTelefoneRepository tipoTelefoneRepository)
        {
            _tipoTelefoneRepository = tipoTelefoneRepository;
        }

        // GET: api/TiposTelefone
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TipoTelefone>>> GetTiposTelefone()
        {
            var tiposTelefone = await _tipoTelefoneRepository.GetAllAsync();
            return Ok(tiposTelefone);
        }

        // GET: api/TiposTelefone/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TipoTelefone>> GetTipoTelefone(int id)
        {
            var tipoTelefone = await _tipoTelefoneRepository.GetByIdAsync(id);

            if (tipoTelefone == null)
            {
                return NotFound();
            }

            return Ok(tipoTelefone);
        }
    }
}
