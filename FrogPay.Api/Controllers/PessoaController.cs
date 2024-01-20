using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public PessoaController(IPessoaService pessoaService, IMapper mapper)
        {
            _pessoaService = pessoaService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PessoaDTO>>> ObterTodosAsync()
        {
            var pessoas = await _pessoaService.ObterTodosAsync();
            var pessoasDTO = _mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaDTO>>(pessoas);
            return Ok(pessoasDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PessoaDTO>> ObterPorIdAsync(Guid id)
        {
            var pessoa = await _pessoaService.ObterPorIdAsync(id);

            if (pessoa == null)
                return NotFound();

            var pessoaDTO = _mapper.Map<Pessoa, PessoaDTO>(pessoa);
            return Ok(pessoaDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAsync([FromBody] PessoaDTO pessoaDTO)
        {
            var pessoa = _mapper.Map<PessoaDTO, Pessoa>(pessoaDTO);
            await _pessoaService.AdicionarAsync(pessoa);
            return CreatedAtAction(nameof(ObterPorIdAsync), new { id = pessoaDTO.Id }, pessoaDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> AtualizarAsync(Guid id, [FromBody] PessoaDTO pessoaDTO)
        {
            var pessoa = _mapper.Map<PessoaDTO, Pessoa>(pessoaDTO);
            await _pessoaService.AtualizarAsync(id, pessoa);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> RemoverAsync(Guid id)
        {
            await _pessoaService.RemoverAsync(id);
            return NoContent();
        }
    }
}
