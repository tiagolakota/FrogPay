using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Infrastructure.Data.Contexts;
using FrogPay.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace SlnFrogPay
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Configuração do Entity Framework Core com PostgreSQL
            services.AddDbContext<FrogPayContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("FrogPayConnection")));

            // Adiciona os repositórios como serviços
            services.AddScoped<IPessoaRepository, PessoaRepository>();
            services.AddScoped<ILojaRepository, LojaRepository>();
            services.AddScoped<IDadosBancariosRepository, DadosBancariosRepository>();
            services.AddScoped<IEnderecoRepository, EnderecoRepository>();

            // Configuração de CORS (exemplo para qualquer origem)
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin",
                    builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            // Configuração do Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SlnFrogPay", Version = "v1" });
            });

            // Outras configurações de serviços...

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SlnFrogPay v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Ativar CORS (exemplo para qualquer origem)
            app.UseCors("AllowAnyOrigin");

            // Ativar autenticação JWT (se necessário)
            // app.UseAuthentication();
            // app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
