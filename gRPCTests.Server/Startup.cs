using gRPCTests.Core.Infrastructure.Contexts;
using gRPCTests.Core.Infrastructure.Repositories;
using gRPCTests.Server.Services;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Reflection;

namespace gRPCTests.Server
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(prov => AutoMapperConfigurator.MapperConfiguration().CreateMapper());

            var assembly = AppDomain.CurrentDomain.Load("gRPCTests.Core");
            services.AddMediatR(assembly);

            services.AddDbContext<CadastroContext>(ServiceLifetime.Singleton);

            services.AddScoped<ProdutoRepository>();
            services.AddScoped<Core.Domain.Services.ProdutoService>();

            services.AddGrpc(options =>
            {
                options.EnableDetailedErrors = true;
                options.ReceiveMaxMessageSize = 1 * 1024 * 1024;
                options.SendMaxMessageSize = 5 * 1024 * 1024;
                options.ResponseCompressionLevel = System.IO.Compression.CompressionLevel.Optimal;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGrpcService<ProdutoService>();
            });
        }
    }
}
