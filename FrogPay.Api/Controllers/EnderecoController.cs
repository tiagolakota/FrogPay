using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoService enderecoService, IMapper mapper) // Adicione IMapper ao construtor
        {
            _enderecoService = enderecoService;
            _mapper = mapper;
        }

        [HttpGet("pessoa/{idPessoa}")]
        public async Task<ActionResult<EnderecoDTO>> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            var endereco = await _enderecoService.ObterPorIdPessoaAsync(idPessoa);

            if (endereco == null)
                return NotFound();

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAsync([FromBody] EnderecoDTO enderecoDTO)
        {
            var endereco = _mapper.Map<EnderecoDTO, Endereco>(enderecoDTO);
            await _enderecoService.AdicionarAsync(endereco);
            return CreatedAtAction(nameof(ObterPorIdPessoaAsync), new { idPessoa = enderecoDTO.IdPessoa }, enderecoDTO);
        }

        [HttpPut("{idPessoa}")]
        public async Task<ActionResult> AtualizarAsync(Guid idPessoa, [FromBody] EnderecoDTO enderecoDTO)
        {
            var endereco = _mapper.Map<EnderecoDTO, Endereco>(enderecoDTO);
            await _enderecoService.AtualizarAsync(idPessoa, endereco);
            return NoContent();
        }

        [HttpDelete("{idPessoa}")]
        public async Task<ActionResult> RemoverAsync(Guid idPessoa)
        {
            await _enderecoService.RemoverAsync(idPessoa);
            return NoContent();
        }
    }
}
