using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Ticketize.Api.Utility;
using Ticketize.Application;
using Ticketize.Infrastructure;
using Ticketize.Persistence;

namespace Ticketize.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
            AddSwagger(builder.Services);

            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
            builder.Services.AddPersistenceServices(builder.Configuration);

            builder.Services.AddControllers();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("Open", builder => builder.AllowAnyOrigin()
                                                            .AllowAnyHeader().
                                                            AllowAnyMethod());
            });

            return builder.Build();
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(s =>
                {
                    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Ticketize Api");
                });
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();
            app.MapControllers();

            return app;
        }

        private static void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Ticketize Api"
                });

                swagger.OperationFilter<FileResultContentTypeOperationFilter>();
            });
        }

        /// <summary>
        /// Helps reset the database each time.
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static async Task ResetDatabaseAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetService<TicketizeDbContext>();
                if (context is not null)
                {
                    await context.Database.EnsureDeletedAsync();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception)
            {
                // log exception here.
            }
        }
    }
}
