namespace FrogPay.Common
{
    public static class PaginacaoHelper
    {
        public static IEnumerable<T> PaginarDados<T>(IEnumerable<T> dados, int page, int pageSize)
        {
            return dados.Skip((page - 1) * pageSize).Take(pageSize);
        }
    }
}