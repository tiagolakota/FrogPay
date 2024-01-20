using AutoMapper;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

namespace FrogPay.Application.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Pessoa, PessoaDTO>().ReverseMap();
            CreateMap<Loja, LojaDTO>().ReverseMap();
            CreateMap<DadosBancarios, DadosBancariosDTO>().ReverseMap();
            CreateMap<Endereco, EnderecoDTO>().ReverseMap();
        }
    }
}
