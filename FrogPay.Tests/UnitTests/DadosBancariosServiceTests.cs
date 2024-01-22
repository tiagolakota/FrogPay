using Xunit;
using Moq;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using FrogPay.Api.Controllers;
using FrogPay.Application.Interfaces.Services;
using FrogPay.Application.Models;
using FrogPay.Domain.Entities;

public class DadosBancariosControllerTests
{
    [Fact]
    public async Task ObterPorCpfAsync_DeveRetornarOkResult()
    {
        var dadosBancariosServiceMock = new Mock<IDadosBancariosService>();
        dadosBancariosServiceMock.Setup(repo => repo.ObterPorIdPessoaAsync(It.IsAny<Guid>())).ReturnsAsync(new DadosBancarios());

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        var controller = new DadosBancariosController(dadosBancariosServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var resultado = await controller.ObterPorCpfAsync("147.631.820-48");

        Assert.IsType<OkObjectResult>(resultado);
    }

    [Fact]
    public async Task AdicionarAsync_ComDadosValidos_DeveRetornarCreatedAtActionResult()
    {
        var dadosBancariosServiceMock = new Mock<IDadosBancariosService>();
        dadosBancariosServiceMock.Setup(repo => repo.AdicionarAsync(It.IsAny<DadosBancarios>())).Returns(Task.CompletedTask);

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(mapper => mapper.Map<DadosBancariosDTO, DadosBancarios>(It.IsAny<DadosBancariosDTO>())).Returns(new DadosBancarios());

        var controller = new DadosBancariosController(dadosBancariosServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var dadosBancariosDTO = new DadosBancariosDTO
        {
            CodigoBanco = "001",
            Agencia = "1234",
            DigitoConta = "5"
        };

        var resultado = await controller.AdicionarAsync(dadosBancariosDTO, "147.631.820-48");

        Assert.IsType<CreatedAtActionResult>(resultado);
    }

    [Fact]
    public async Task AtualizarAsync_ComDadosValidos_DeveRetornarNoContentResult()
    {
        var dadosBancariosServiceMock = new Mock<IDadosBancariosService>();
        dadosBancariosServiceMock.Setup(repo => repo.AtualizarAsync(It.IsAny<DadosBancarios>())).Returns(Task.CompletedTask);
        dadosBancariosServiceMock.Setup(repo => repo.ObterPorIdPessoaAsync(It.IsAny<Guid>())).ReturnsAsync(new DadosBancarios());

        var pessoaServiceMock = new Mock<IPessoaService>();
        pessoaServiceMock.Setup(repo => repo.ObterPorCpfAsync(It.IsAny<string>())).ReturnsAsync(new Pessoa { Id = Guid.NewGuid() });

        var mapperMock = new Mock<IMapper>();
        var controller = new DadosBancariosController(dadosBancariosServiceMock.Object, pessoaServiceMock.Object, mapperMock.Object);

        var dadosBancariosDTO = new DadosBancariosDTO
        {
            CodigoBanco = "002",
            Agencia = "5678",
            DigitoConta = "9"
        };

        var resultado = await controller.AtualizarAsync("147.631.820-48", dadosBancariosDTO);

        Assert.IsType<NoContentResult>(resultado);
    }
}
