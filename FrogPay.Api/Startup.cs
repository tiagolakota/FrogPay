using FrogPay.Application.Interfaces.Repositories;
using FrogPay.Infrastructure.Data.Contexts;
using FrogPay.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace SlnFrogPay
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

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
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "FrogPay", Version = "v1" });
            });

            // Configuração para adicionar autenticação JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = "your_issuer",
                    ValidAudience = "your_audience",
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("your_secret_key"))
                };
            });

            // AutoMapper
            services.AddAutoMapper(typeof(Startup));

            // Outras configurações de serviços...

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "FrogPay v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // Ativar CORS (exemplo para qualquer origem)
            app.UseCors("AllowAnyOrigin");

            // Ativar autenticação JWT
            app.UseAuthentication();
            app.UseAuthorization();

            // Configure as rotas da API
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "api/{controller}/{action=Index}/{id?}");
            });
        }
    }
}
