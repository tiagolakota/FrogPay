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
    public class LojaController : ControllerBase
    {
        private readonly ILojaService _lojaService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public LojaController(ILojaService lojaService, IPessoaService pessoaService, IMapper mapper)
        {
            _lojaService = lojaService ?? throw new ArgumentNullException(nameof(lojaService));
            _pessoaService = pessoaService ?? throw new ArgumentNullException(nameof(pessoaService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<IActionResult> ObterTodasAsync([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var lojas = await _lojaService.ObterTodasAsync();
            var paginaData = PaginacaoHelper.PaginarDados(lojas, page, pageSize);
            var lojasDTO = _mapper.Map<IEnumerable<Loja>, IEnumerable<LojaDTO>>(paginaData);
            return Ok(lojasDTO);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest("O parâmetro 'id' deve ser fornecido.");
            }

            var loja = await _lojaService.ObterPorIdAsync(id);

            if (loja == null)
            {
                return NotFound();
            }

            var lojaDTO = _mapper.Map<Loja, LojaDTO>(loja);
            return Ok(lojaDTO);
        }

        [HttpGet("porNomeFantasia/{nomeFantasia}")]
        public async Task<IActionResult> ObterPorNomeFantasiaAsync(string nomeFantasia, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia))
            {
                return BadRequest("O parâmetro 'nomeFantasia' deve ser fornecido.");
            }

            var lojas = await _lojaService.ObterPorNomeFantasiaAsync(nomeFantasia);
            var paginaData = PaginacaoHelper.PaginarDados(lojas, page, pageSize);
            var lojasDTO = _mapper.Map<IEnumerable<Loja>, IEnumerable<LojaDTO>>(paginaData);
            return Ok(lojasDTO);
        }

        [HttpGet("porRazaoSocial/{razaoSocial}")]
        public async Task<IActionResult> ObterPorRazaoSocialAsync(string razaoSocial, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
            {
                return BadRequest("O parâmetro 'razaoSocial' deve ser fornecido.");
            }

            var lojas = await _lojaService.ObterPorRazaoSocialAsync(razaoSocial);
            var paginaData = PaginacaoHelper.PaginarDados(lojas, page, pageSize);
            var lojasDTO = _mapper.Map<IEnumerable<Loja>, IEnumerable<LojaDTO>>(paginaData);
            return Ok(lojasDTO);
        }

        [HttpGet("porCnpj/{cnpj}")]
        public async Task<IActionResult> ObterPorCnpjAsync(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return BadRequest("O parâmetro 'cnpj' deve ser fornecido.");
            }

            if (!CnpjHelper.ValidarCnpj(cnpj))
            {
                return BadRequest("O formato do CNPJ não é válido.");
            }

            var loja = await _lojaService.ObterPorCnpjAsync(cnpj);

            if (loja == null)
            {
                return NotFound();
            }

            var lojaDTO = _mapper.Map<Loja, LojaDTO>(loja);
            return Ok(lojaDTO);
        }

        [HttpPost]
        public async Task<IActionResult> AdicionarAsync([FromBody] LojaDTO lojaDTO, [FromQuery] string cpfPessoa)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpfPessoa);

            if (pessoa == null)
            {
                return BadRequest("Não foi possível encontrar a pessoa com o CPF fornecido.");
            }

            lojaDTO.IdPessoa = pessoa.Id;

            if (string.IsNullOrWhiteSpace(lojaDTO.NomeFantasia) ||
                string.IsNullOrWhiteSpace(lojaDTO.RazaoSocial) ||
                string.IsNullOrWhiteSpace(lojaDTO.CNPJ))
            {
                return BadRequest("Todos os campos obrigatórios devem ser preenchidos.");
            }

            if (!CnpjHelper.ValidarCnpj(lojaDTO.CNPJ))
            {
                return BadRequest("O formato do CNPJ não é válido.");
            }

            if (!DateTime.TryParse(lojaDTO.DataAbertura.ToString(), out _))
            {
                return BadRequest("A data de abertura não é válida.");
            }

            var lojaExistente = await _lojaService.ObterPorCnpjAsync(lojaDTO.CNPJ);

            if (lojaExistente != null)
            {
                return BadRequest("Já existe uma loja com o mesmo CNPJ.");
            }

            var novaLoja = _mapper.Map<LojaDTO, Loja>(lojaDTO);

            await _lojaService.AdicionarAsync(novaLoja);

            return CreatedAtAction(nameof(ObterPorCnpjAsync), new { cnpj = novaLoja.CNPJ }, lojaDTO);
        }

        [HttpPut("{cnpj}")]
        public async Task<IActionResult> AtualizarAsync(string cnpj, [FromBody] LojaDTO lojaDTO, [FromQuery] string cpfPessoa = null)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var lojaExistente = await _lojaService.ObterPorCnpjAsync(cnpj);

            if (lojaExistente == null)
            {
                return NotFound("Loja não encontrada.");
            }

            if (!string.IsNullOrWhiteSpace(cpfPessoa))
            {
                var pessoa = await _pessoaService.ObterPorCpfAsync(cpfPessoa);

                if (pessoa == null)
                {
                    return BadRequest("Não foi possível encontrar a pessoa com o CPF fornecido.");
                }

                lojaDTO.IdPessoa = pessoa.Id;
            }

            _mapper.Map(lojaDTO, lojaExistente);

            await _lojaService.AtualizarAsync(cnpj, lojaExistente);
            return NoContent();
        }

        [HttpDelete("{cnpj}")]
        public async Task<IActionResult> RemoverAsync(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                return BadRequest("O parâmetro 'cnpj' deve ser fornecido.");
            }

            await _lojaService.RemoverAsync(cnpj);
            return NoContent();
        }
    }
}