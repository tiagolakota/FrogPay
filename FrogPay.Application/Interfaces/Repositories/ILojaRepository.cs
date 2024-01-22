using FrogPay.Domain.Entities;

namespace FrogPay.Application.Interfaces.Repositories
{
    public interface ILojaRepository
    {
        Task<IEnumerable<Loja>> ObterTodasAsync();
        Task<Loja> ObterPorIdAsync(Guid id);
        Task<IEnumerable<Loja>> ObterPorNomeFantasiaAsync(string nomeFantasia);
        Task<IEnumerable<Loja>> ObterPorRazaoSocialAsync(string razaoSocial);
        Task<Loja> ObterPorCnpjAsync(string cnpj);
        Task AdicionarAsync(Loja loja);
        Task AtualizarAsync(string cnpj, Loja loja);
        Task RemoverAsync(string cnpj);
    }
}
