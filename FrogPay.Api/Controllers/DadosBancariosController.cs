using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Common;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DadosBancariosController : ControllerBase
    {
        private readonly IDadosBancariosService _dadosBancariosService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public DadosBancariosController(IDadosBancariosService dadosBancariosService, IPessoaService pessoaService, IMapper mapper)
        {
            _dadosBancariosService = dadosBancariosService ?? throw new ArgumentNullException(nameof(dadosBancariosService));
            _pessoaService = pessoaService ?? throw new ArgumentException(nameof(pessoaService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet("pessoa/{cpf}")]
        public async Task<IActionResult> ObterPorCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O parâmetro 'cpf' deve ser fornecido.");
            }

            if (!CpfHelper.ValidarCpf(cpf))
            {
                return BadRequest("O formato do CPF não é válido.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpf}.");
            }

            var dadosBancarios = await _dadosBancariosService.ObterPorIdPessoaAsync(pessoa.Id);

            if (dadosBancarios == null)
            {
                return NotFound();
            }

            var dadosBancariosDTO = _mapper.Map<DadosBancarios, DadosBancariosDTO>(dadosBancarios);
            return Ok(dadosBancariosDTO);
        }

        [HttpGet("pessoa/{idPessoa}")]
        public async Task<IActionResult> ObterPorIdPessoaAsync(Guid idPessoa)
        {
            var dadosBancarios = await _dadosBancariosService.ObterPorIdPessoaAsync(idPessoa);

            if (dadosBancarios == null)
                return NotFound();

            var dadosBancariosDTO = _mapper.Map<DadosBancarios, DadosBancariosDTO>(dadosBancarios);
            return Ok(dadosBancariosDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] DadosBancariosDTO dadosBancariosDTO, [FromQuery] string cpfPessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(cpfPessoa))
            {
                return BadRequest("O parâmetro 'cpfPessoa' deve ser fornecido.");
            }

            if (!CpfHelper.ValidarCpf(cpfPessoa))
            {
                return BadRequest("O formato do CPF da pessoa não é válido.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpfPessoa);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpfPessoa}.");
            }

            dadosBancariosDTO.IdPessoa = pessoa.Id;

            var dadosBancarios = _mapper.Map<DadosBancariosDTO, DadosBancarios>(dadosBancariosDTO);

            await _dadosBancariosService.AdicionarAsync(dadosBancarios);

            return CreatedAtAction(nameof(ObterPorCpfAsync), new { cpf = cpfPessoa }, dadosBancariosDTO);
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> AtualizarAsync(string cpf, [FromBody] DadosBancariosDTO dadosBancariosDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O parâmetro 'cpf' deve ser fornecido.");
            }

            if (!CpfHelper.ValidarCpf(cpf))
            {
                return BadRequest("O formato do CPF não é válido.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpf}.");
            }

            dadosBancariosDTO.IdPessoa = pessoa.Id;

            var dadosBancarios = _mapper.Map<DadosBancariosDTO, DadosBancarios>(dadosBancariosDTO);
            await _dadosBancariosService.AtualizarAsync(dadosBancarios);
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> RemoverAsync(string cpf)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O parâmetro 'cpf' deve ser fornecido.");
            }

            if (!CpfHelper.ValidarCpf(cpf))
            {
                return BadRequest("O formato do CPF não é válido.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpf}.");
            }

            await _dadosBancariosService.RemoverPorIdPessoaAsync(pessoa.Id);
            return NoContent();
        }
    }
}