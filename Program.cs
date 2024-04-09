using BackgroundServiceAPI.BackgroundServices;
using BackgroundServiceAPI.BackgroundTasks;
using BackgroundServiceAPI.DbContexts;
using BackgroundServiceAPI.DTOs.Config;
using BackgroundServiceAPI.Models.Config;
using BackgroundServiceAPI.Repositories;
using BackgroundServiceAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace BackgroundServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            // Register Background Services
            #region Background Services

            builder.Services.AddHostedService<WorkerService>();

            #endregion

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            #region Binding config settings

            builder.Services.AddOptions<BackgroundServiceSettings>()
                .Bind(builder.Configuration.GetSection(nameof(BackgroundServiceSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddOptions<ConnectionStrings>()
                .Bind(builder.Configuration.GetSection(nameof(ConnectionStrings)))
                .ValidateDataAnnotations()
                .ValidateOnStart(); 

            #endregion

            builder.Services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
            {
                var dbSettings = serviceProvider.GetRequiredService<IOptionsMonitor<ConnectionStrings>>().CurrentValue;
                options.UseSqlServer(dbSettings.DbConnection);
            });

            builder.Services.AddSingleton<IConfigService, ConfigService>();
            builder.Services.AddTransient<IEmployeeRepository, EmployeeRepository>();

            builder.Services.AddTransient<ICountEmployeeDataJob, CountEmployeeDataJob>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
