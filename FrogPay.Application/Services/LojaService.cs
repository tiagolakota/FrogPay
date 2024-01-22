using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Common;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.Services
{
    public class LojaService : ILojaService
    {
        private readonly ILojaRepository _lojaRepository;

        public LojaService(ILojaRepository lojaRepository)
        {
            _lojaRepository = lojaRepository;
        }

        public async Task<IEnumerable<Loja>> ObterTodasAsync()
        {
            return await _lojaRepository.ObterTodasAsync();
        }

        public async Task<Loja> ObterPorIdAsync(Guid id)
        {
            if (id == Guid.Empty)
            {
                throw new ArgumentException("O ID da loja não é válido.");
            }

            return await _lojaRepository.ObterPorIdAsync(id);
        }

        public async Task<IEnumerable<Loja>> ObterPorNomeFantasiaAsync(string nomeFantasia)
        {
            if (string.IsNullOrWhiteSpace(nomeFantasia))
            {
                throw new ArgumentException("O parâmetro 'nomeFantasia' deve ser fornecido.");
            }

            return await _lojaRepository.ObterPorNomeFantasiaAsync(nomeFantasia);
        }

        public async Task<IEnumerable<Loja>> ObterPorRazaoSocialAsync(string razaoSocial)
        {
            if (string.IsNullOrWhiteSpace(razaoSocial))
            {
                throw new ArgumentException("O parâmetro 'razaoSocial' deve ser fornecido.");
            }

            return await _lojaRepository.ObterPorRazaoSocialAsync(razaoSocial);
        }

        public async Task<Loja> ObterPorCnpjAsync(string cnpj)
        {
            if (string.IsNullOrWhiteSpace(cnpj))
            {
                throw new ArgumentException("O parâmetro 'cnpj' deve ser fornecido.");
            }

            if (!CnpjHelper.ValidarCnpj(cnpj))
            {
                throw new ArgumentException("O formato do CNPJ não é válido.");
            }

            return await _lojaRepository.ObterPorCnpjAsync(cnpj);
        }

        public async Task AdicionarAsync(Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentException("Os dados da loja são inválidos.");
            }

            if (string.IsNullOrWhiteSpace(loja.NomeFantasia) ||
                string.IsNullOrWhiteSpace(loja.RazaoSocial) ||
                string.IsNullOrWhiteSpace(loja.CNPJ) ||
                loja.DataAbertura == null ||
                loja.DataAbertura > DateTime.Now ||
                !DateTime.TryParse(loja.DataAbertura.ToString(), out _))
            {
                throw new ArgumentException("Dados inválidos. Certifique-se de fornecer valores válidos para todos os campos.");
            }

            if (!CnpjHelper.ValidarCnpj(loja.CNPJ))
            {
                throw new ArgumentException("O formato do CNPJ não é válido.");
            }

            await _lojaRepository.AdicionarAsync(loja);
        }

        public async Task AtualizarAsync(string cnpj, Loja loja)
        {
            if (loja == null)
            {
                throw new ArgumentException("Os dados da loja são inválidos.");
            }

            if (string.IsNullOrWhiteSpace(loja.NomeFantasia) ||
                string.IsNullOrWhiteSpace(loja.RazaoSocial) ||
                string.IsNullOrWhiteSpace(loja.CNPJ) ||
                loja.DataAbertura == null ||
                loja.DataAbertura > DateTime.Now ||
                !DateTime.TryParse(loja.DataAbertura.ToString(), out _))
            {
                throw new ArgumentException("Dados inválidos. Certifique-se de fornecer valores válidos para todos os campos.");
            }

            var lojaExistente = await _lojaRepository.ObterPorCnpjAsync(cnpj);

            if (lojaExistente == null)
            {
                throw new ArgumentException("Loja não encontrada.");
            }

            if (!CnpjHelper.ValidarCnpj(loja.CNPJ))
            {
                throw new ArgumentException("O formato do CNPJ não é válido.");
            }

            await _lojaRepository.AtualizarAsync(cnpj, loja);
        }

        public async Task RemoverAsync(string cnpj)
        {
            var lojaExistente = await _lojaRepository.ObterPorCnpjAsync(cnpj);

            if (lojaExistente == null)
            {
                throw new ArgumentException("Loja não encontrada.");
            }

            await _lojaRepository.RemoverAsync(cnpj);
        }
    }
}
