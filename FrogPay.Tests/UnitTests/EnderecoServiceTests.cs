using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;
using FrogPay.API.Controllers;

public class EnderecoControllerTests
{
    [Fact]
    public async Task ObterPorNomePessoaAsync_DeveRetornarOkResult()
    {
        var enderecoServiceMock = new Mock<IEnderecoService>();
        enderecoServiceMock.Setup(repo => repo.ObterPorIdPessoaAsync(It.IsAny<Guid>())).ReturnsAsync(new Endereco());

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorNomeAsync(It.IsAny<string>())).ReturnsAsync(new List<Pessoa> { new Pessoa { Id = Guid.NewGuid() } });

        var mapperMock = new Mock<IMapper>();
        var controller = new EnderecoController(enderecoServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.ObterPorNomePessoaAsync("NomeTeste");

        Assert.IsType<OkObjectResult>(resultado);
    }

    [Fact]
    public async Task AdicionarAsync_ComDadosValidos_DeveRetornarCreatedAtActionResult()
    {
        var enderecoServiceMock = new Mock<IEnderecoService>();
        enderecoServiceMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Endereco>())).Returns(Task.CompletedTask);

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<EnderecoDTO, Endereco>(It.IsAny<EnderecoDTO>())).Returns(new Endereco());

        var controller = new EnderecoController(enderecoServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var enderecoDTO = new EnderecoDTO
        {
            UFEstado = "SP",
            Cidade = "São Paulo",
            Bairro = "Bairro Teste",
            Logradouro = "Rua Teste",
            Numero = "123"
        };

        var resultado = await controller.AdicionarAsync(enderecoDTO, "147.631.820-48");

        Assert.IsType<CreatedAtActionResult>(resultado);
    }

    [Fact]
    public async Task AtualizarEnderecoAsync_ComEnderecoExistente_DeveRetornarNoContentResult()
    {
        var enderecoServiceMock = new Mock<IEnderecoService>();
        enderecoServiceMock.Setup(repo => repo.AtualizarAsync(It.IsAny<Guid>(), It.IsAny<Endereco>())).Returns(Task.CompletedTask);
        enderecoServiceMock.Setup(repo => repo.ObterPorIdPessoaAsync(It.IsAny<Guid>())).ReturnsAsync(new Endereco());

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        var controller = new EnderecoController(enderecoServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var enderecoDTO = new EnderecoDTO
        {
            UFEstado = "SP",
            Cidade = "São Paulo",
            Bairro = "Bairro Teste",
            Logradouro = "Rua Teste",
            Numero = "123"
        };

        var resultado = await controller.AtualizarEnderecoAsync("147.631.820-48", enderecoDTO);

        Assert.IsType<NoContentResult>(resultado);
    }
}