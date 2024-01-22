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
    public class PessoaController : ControllerBase
    {
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public PessoaController(IPessoaService pessoaService, IMapper mapper)
        {
            _pessoaService = pessoaService ?? throw new ArgumentNullException(nameof(pessoaService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodosAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pessoas = await _pessoaService.ObterTodosAsync();
            var paginaData = PaginacaoHelper.PaginarDados(pessoas, page, pageSize);
            var pessoasDTO = _mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaDTO>>(paginaData);
            return Ok(pessoasDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("O parâmetro 'id' deve ser fornecido.");
            }

            var pessoa = await _pessoaService.ObterPorIdAsync(id);

            if (pessoa == null)
            {
                return NotFound();
            }

            var pessoaDTO = _mapper.Map<Pessoa, PessoaDTO>(pessoa);
            return Ok(pessoaDTO);
        }

        [HttpGet("porNome/{nome}")]
        public async Task<IActionResult> ObterPorNomeAsync(string nome, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return BadRequest("O parâmetro 'nome' deve ser fornecido.");
            }

            var pessoas = await _pessoaService.ObterPorNomeAsync(nome);
            var paginaData = PaginacaoHelper.PaginarDados(pessoas, page, pageSize);
            var pessoasDTO = _mapper.Map<IEnumerable<Pessoa>, IEnumerable<PessoaDTO>>(paginaData);
            return Ok(pessoasDTO);
        }

        [HttpGet("porCpf/{cpf}")]
        public async Task<IActionResult> ObterPorCpfAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O parâmetro 'cpf' deve ser fornecido.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound();
            }

            var pessoaDTO = _mapper.Map<Pessoa, PessoaDTO>(pessoa);
            return Ok(pessoaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] PessoaDTO pessoaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!CpfHelper.ValidarCpf(pessoaDTO.CPF))
            {
                return BadRequest("O formato do CPF não é válido.");
            }

            var pessoaExistente = await _pessoaService.ObterPorCpfAsync(pessoaDTO.CPF);

            if (pessoaExistente != null)
            {
                return BadRequest("Já existe uma pessoa com o mesmo CPF.");
            }

            var pessoa = _mapper.Map<PessoaDTO, Pessoa>(pessoaDTO);
            await _pessoaService.AdicionarAsync(pessoa);

            return CreatedAtAction(nameof(ObterPorCpfAsync), new { cpf = pessoa.CPF }, pessoaDTO);
        }

        [HttpPut("{cpf}")]
        public async Task<IActionResult> AtualizarAsync(string cpf, [FromBody] PessoaDTO pessoaDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (cpf != pessoaDTO.CPF)
            {
                return BadRequest("O CPF na URL e no corpo da requisição não correspondem.");
            }

            var pessoaExistente = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoaExistente == null)
            {
                return NotFound("Pessoa não encontrada.");
            }

            await _pessoaService.AtualizarAsync(cpf, _mapper.Map<PessoaDTO, Pessoa>(pessoaDTO));
            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public async Task<IActionResult> RemoverAsync(string cpf)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return BadRequest("O parâmetro 'cpf' deve ser fornecido.");
            }

            await _pessoaService.RemoverAsync(cpf);
            return NoContent();
        }
    }
}
