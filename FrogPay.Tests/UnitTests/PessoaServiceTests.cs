using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FrogPay.Api.Controllers;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

public class PessoaControllerTests
{
    [Fact]
    public async Task ObterTodosAsync_RetornaOkResult()
    {
        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterTodosAsync()).ReturnsAsync(new List<Pessoa>());

        var mapperMock = new Mock<IMapper>();
        var controller = new PessoaController(pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.ObterTodosAsync();

        Assert.IsType<OkObjectResult>(resultado);
    }

    [Fact]
    public async Task ObterPorIdAsync_ComIdValido_RetornaOkResult()
    {
        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorIdAsync(It.IsAny<Guid>())).ReturnsAsync(new Pessoa());

        var mapperMock = new Mock<IMapper>();
        var controller = new PessoaController(pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.ObterPorIdAsync(Guid.NewGuid());

        Assert.IsType<OkObjectResult>(resultado);
    }

    [Fact]
    public async Task AdicionarAsync_ComDadosValidos_RetornaCreatedAtActionResult()
    {
        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Pessoa>())).Returns(Task.CompletedTask);

        var mapperMock = new Mock<IMapper>();
        var controller = new PessoaController(pessoaServiceMock.Object, mapperMock.Object);

        var pessoaDTO = new PessoaDTO
        {
            Nome = "Fulano de Tal",
            CPF = "147.631.820-48",
            DataNascimento = new DateTime(1990, 1, 1)
        };

        var resultado = await controller.AdicionarAsync(pessoaDTO);

        Assert.IsType<CreatedAtActionResult>(resultado);
    }

    [Fact]
    public async Task AtualizarAsync_ComDadosValidos_RetornaNoContentResult()
    {
        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.AtualizarAsync(It.IsAny<string>(), It.IsAny<Pessoa>())).Returns(Task.CompletedTask);

        var mapperMock = new Mock<IMapper>();
        var controller = new PessoaController(pessoaServiceMock.Object, mapperMock.Object);

        var pessoaDTO = new PessoaDTO
        {
            Nome = "Novo Nome",
            CPF = "147.631.820-48",
            DataNascimento = new DateTime(1990, 1, 1)
        };

        var resultado = await controller.AtualizarAsync("147.631.820-48", pessoaDTO);

        Assert.IsType<NoContentResult>(resultado);
    }

    [Fact]
    public async Task RemoverAsync_ComCpfValido_RetornaNoContentResult()
    {
        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.RemoverAsync(It.IsAny<string>())).Returns(Task.CompletedTask);

        var mapperMock = new Mock<IMapper>();
        var controller = new PessoaController(pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.RemoverAsync("147.631.820-48");

        Assert.IsType<NoContentResult>(resultado);
    }
}