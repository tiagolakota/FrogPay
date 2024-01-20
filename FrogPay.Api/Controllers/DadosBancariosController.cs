using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DadosBancariosController : ControllerBase
    {
        private readonly IDadosBancariosService _dadosBancariosService;
        private readonly IMapper _mapper;

        public DadosBancariosController(IDadosBancariosService dadosBancariosService, IMapper mapper)
        {
            _dadosBancariosService = dadosBancariosService;
            _mapper = mapper;
        }

        [HttpGet("pessoa/{idPessoa}")]
        public async Task<ActionResult<DadosBancariosDTO>> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            var dadosBancarios = await _dadosBancariosService.ObterPorIdPessoaAsync(idPessoa);

            if (dadosBancarios == null)
                return NotFound();

            var dadosBancariosDTO = _mapper.Map<DadosBancarios, DadosBancariosDTO>(dadosBancarios);
            return Ok(dadosBancariosDTO);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAsync([FromBody] DadosBancariosDTO dadosBancariosDTO)
        {
            var dadosBancarios = _mapper.Map<DadosBancariosDTO, DadosBancarios>(dadosBancariosDTO);
            await _dadosBancariosService.AdicionarAsync(dadosBancarios);
            return CreatedAtAction(nameof(ObterPorIdPessoaAsync), new { idPessoa = dadosBancariosDTO.IdPessoa }, dadosBancariosDTO);
        }

        [HttpPut("{idPessoa}")]
        public async Task<ActionResult> AtualizarAsync(Guid idPessoa, [FromBody] DadosBancariosDTO dadosBancariosDTO)
        {
            var dadosBancarios = _mapper.Map<DadosBancariosDTO, DadosBancarios>(dadosBancariosDTO);
            await _dadosBancariosService.AtualizarAsync(idPessoa, dadosBancarios);
            return NoContent();
        }

        [HttpDelete("{idPessoa}")]
        public async Task<ActionResult> RemoverAsync(Guid idPessoa)
        {
            await _dadosBancariosService.RemoverAsync(idPessoa);
            return NoContent();
        }
    }
}
