using BackgroundServiceAPI.Models.Config;
using BackgroundServiceAPI.Services;

namespace BackgroundServiceAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            /*builder.Services.Configure<BackgroundServiceSettings>(
                builder.Configuration.GetSection(nameof(BackgroundServiceSettings)));*/

            builder.Services.AddOptions<BackgroundServiceSettings>()
                .Bind(builder.Configuration.GetSection(nameof(BackgroundServiceSettings)))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            builder.Services.AddTransient<IConfigService, ConfigService>();

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
