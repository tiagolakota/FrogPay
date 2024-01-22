using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Common;
using FrogPay.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace FrogPay.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EnderecoController : ControllerBase
    {
        private readonly IEnderecoService _enderecoService;
        private readonly IPessoaService _pessoaService;
        private readonly IMapper _mapper;

        public EnderecoController(IEnderecoService enderecoService, IPessoaService pessoaService, IMapper mapper)
        {
            _enderecoService = enderecoService;
            _pessoaService = pessoaService;
            _mapper = mapper;
        }

        [HttpGet("porNomePessoa/{nomePessoa}")]
        public async Task<ActionResult<IEnumerable<EnderecoDTO>>> ObterPorNomePessoaAsync(string nomePessoa, [FromQuery] int page = 1, [FromQuery] int pageSize = 10)
        {
            var pessoas = await _pessoaService.ObterPorNomeAsync(nomePessoa);

            if (pessoas == null || !pessoas.Any())
                return NotFound("Nenhuma pessoa encontrada com o nome especificado.");

            var enderecosDTO = new List<EnderecoDTO>();

            foreach (var pessoa in pessoas)
            {
                var endereco = await _enderecoService.ObterPorIdPessoaAsync(pessoa.Id);

                if (endereco != null)
                {
                    var enderecoDTO = _mapper.Map<Endereco, EnderecoDTO>(endereco);
                    enderecosDTO.Add(enderecoDTO);
                }
            }

            var paginaData = PaginacaoHelper.PaginarDados(enderecosDTO, page, pageSize);

            // Aqui, garantimos que a lista seja do tipo IEnumerable<Endereco>
            var enderecos = _mapper.Map<IEnumerable<EnderecoDTO>, IEnumerable<Endereco>>(paginaData);
            var paginaEnderecosDTO = _mapper.Map<IEnumerable<Endereco>, IEnumerable<EnderecoDTO>>(enderecos);

            return Ok(paginaEnderecosDTO);
        }

        [HttpGet("porCpf/{cpf}")]
        public async Task<ActionResult<EnderecoDTO>> ObterPorIdPessoaAsync(string cpf)
        {
            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpf}.");
            }

            var endereco = await _enderecoService.ObterPorIdPessoaAsync(pessoa.Id);

            if (endereco == null)
            {
                return NotFound($"Endereço não encontrado para a pessoa com o CPF {cpf}.");
            }

            return Ok(endereco);
        }

        [HttpPost]
        public async Task<ActionResult> AdicionarAsync([FromBody] EnderecoDTO enderecoDTO, [FromQuery] string cpfPessoa)
        {
            if (string.IsNullOrWhiteSpace(enderecoDTO.UFEstado) ||
                string.IsNullOrWhiteSpace(enderecoDTO.Cidade) ||
                string.IsNullOrWhiteSpace(enderecoDTO.Bairro) ||
                string.IsNullOrWhiteSpace(enderecoDTO.Logradouro) ||
                string.IsNullOrWhiteSpace(enderecoDTO.Numero))
            {
                return BadRequest("UFEstado, Cidade, Bairro, Logradouro, and Numero são campos obrigatórios.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpfPessoa);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpfPessoa}.");
            }

            enderecoDTO.IdPessoa = pessoa.Id;

            var endereco = _mapper.Map<EnderecoDTO, Endereco>(enderecoDTO);
            await _enderecoService.AdicionarAsync(endereco);

            return CreatedAtAction(nameof(ObterPorIdPessoaAsync), new { cpf = cpfPessoa }, enderecoDTO);
        }

        [HttpPut("porCpf/{cpf}")]
        public async Task<IActionResult> AtualizarEnderecoAsync(string cpf, [FromBody] EnderecoDTO enderecoDTO)
        {
            if (enderecoDTO == null)
            {
                return BadRequest("Os dados do endereço são inválidos.");
            }

            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada com o CPF fornecido.");
            }

            var enderecoExistente = await _enderecoService.ObterPorIdPessoaAsync(pessoa.Id);

            if (enderecoExistente == null)
            {
                return NotFound("Endereço não encontrado para a pessoa com o CPF fornecido.");
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.UFEstado))
            {
                enderecoExistente.UFEstado = enderecoDTO.UFEstado;
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.Cidade))
            {
                enderecoExistente.Cidade = enderecoDTO.Cidade;
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.Bairro))
            {
                enderecoExistente.Bairro = enderecoDTO.Bairro;
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.Logradouro))
            {
                enderecoExistente.Logradouro = enderecoDTO.Logradouro;
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.Numero))
            {
                enderecoExistente.Numero = enderecoDTO.Numero;
            }

            if (!string.IsNullOrWhiteSpace(enderecoDTO.Complemento))
            {
                enderecoExistente.Complemento = enderecoDTO.Complemento;
            }

            await _enderecoService.AtualizarAsync(pessoa.Id, enderecoExistente);

            return NoContent();
        }

        [HttpDelete("porCpf/{cpf}")]
        public async Task<ActionResult> RemoverAsync(string cpf)
        {
            var pessoa = await _pessoaService.ObterPorCpfAsync(cpf);

            if (pessoa == null)
            {
                return NotFound($"Nenhuma pessoa encontrada com o CPF {cpf}.");
            }

            await _enderecoService.RemoverAsync(pessoa.Id);

            return NoContent();
        }
    }
}