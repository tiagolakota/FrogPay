using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FrogPay.Api.Controllers;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

public class LojaControllerTests
{
    [Fact]
    public async Task ObterTodasAsync_DeveRetornarOkResult()
    {
        var lojaServiceMock = new Mock<ILojaService>();
        lojaServiceMock.Setup(repo => repo.ObterTodasAsync()).ReturnsAsync(new List<Loja>());

        var pessoaServiceMock = new Mock<IPessoaService>();
        var mapperMock = new Mock<IMapper>();
        var controller = new LojaController(lojaServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.ObterTodasAsync();

        Assert.IsType<OkObjectResult>(resultado);
    }

    [Fact]
    public async Task AdicionarAsync_ComDadosValidos_DeveRetornarCreatedAtActionResult()
    {
        var lojaServiceMock = new Mock<ILojaService>();
        lojaServiceMock.Setup(repo => repo.AdicionarAsync(It.IsAny<Loja>())).Returns(Task.CompletedTask);

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<LojaDTO, Loja>(It.IsAny<LojaDTO>())).Returns(new Loja());

        var controller = new LojaController(lojaServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var lojaDTO = new LojaDTO
        {
            NomeFantasia = "Loja Teste",
            RazaoSocial = "Razao Social Teste",
            CNPJ = "82.823.949/0001-56",
            DataAbertura = DateTime.Now
        };

        var resultado = await controller.AdicionarAsync(lojaDTO, "147.631.820-48");

        Assert.IsType<CreatedAtActionResult>(resultado);
    }

    [Fact]
    public async Task AdicionarAsync_ComCnpjExistente_DeveRetornarBadRequest()
    {
        var lojaServiceMock = new Mock<ILojaService>();
        lojaServiceMock.Setup(repo => repo.ObterPorCnpjAsync(It.IsAny<string>())).ReturnsAsync(new Loja());

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        var controller = new LojaController(lojaServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var lojaDTO = new LojaDTO
        {
            NomeFantasia = "Loja Teste",
            RazaoSocial = "Razao Social Teste",
            CNPJ = "82.823.949/0001-56",
            DataAbertura = DateTime.Now
        };

        var resultado = await controller.AdicionarAsync(lojaDTO, "147.631.820-48");

        Assert.IsType<BadRequestObjectResult>(resultado);
    }
}