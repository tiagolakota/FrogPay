using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LojaController : ControllerBase
    {
        private readonly ILojaService _lojaService;
        private readonly IMapper _mapper;

        public LojaController(ILojaService lojaService, IMapper mapper)
        {
            _lojaService = lojaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<LojaDTO>>> ObterTodosAsync()
        {
            var lojas = await _lojaService.ObterTodosAsync();
            var lojasDTO = _mapper.Map<IEnumerable<Loja>, IEnumerable<LojaDTO>>(lojas);
            return Ok(lojasDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LojaDTO>> ObterPorIdAsync(Guid id)
        {
            var loja = await _lojaService.ObterPorIdAsync(id);

            if (loja == null)
                return NotFound();

            var lojaDTO = _mapper.Map<Loja, LojaDTO>(loja);
            return Ok(lojaDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAsync([FromBody] LojaDTO lojaDTO)
        {
            var loja = _mapper.Map<LojaDTO, Loja>(lojaDTO);
            await _lojaService.AdicionarAsync(loja);
            return CreatedAtAction(nameof(ObterPorIdAsync), new { id = lojaDTO.Id }, lojaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarAsync(Guid id, [FromBody] LojaDTO lojaDTO)
        {
            var loja = _mapper.Map<LojaDTO, Loja>(lojaDTO);
            await _lojaService.AtualizarAsync(id, loja);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoverAsync(Guid id)
        {
            await _lojaService.RemoverAsync(id);
            return NoContent();
        }
    }
}