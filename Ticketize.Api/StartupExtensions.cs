using Microsoft.EntityFrameworkCore;
using Ticketize.Application;
using Ticketize.Infrastructure;
using Ticketize.Persistence;

namespace Ticketize.Api
{
    public static class StartupExtensions
    {
        public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
        {
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
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseCors();
            app.MapControllers();

            return app;
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
