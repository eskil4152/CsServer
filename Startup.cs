using Microsoft.EntityFrameworkCore;

public class Startup
{
    private readonly IConfiguration config;

    public Startup(IConfiguration configuration)
    {
        config = configuration;
    }

    public void ConfigureServices(IServiceCollection services){
        var connectionString = config.GetConnectionString("DefaultConnection");
        services.AddScoped<Person>();
        services.AddScoped<PersonFunctions>();
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
        services.AddControllers();

        services.AddCors(options => {
            options.AddPolicy("AllowAnyOrigin",
                builder => {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
        });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
        if (env.IsDevelopment()){
            app.UseDeveloperExceptionPage();
            
        } else {
            // Configure error handling, logging, and other production-specific settings here...
        }
        
        app.UseRouting();

        app.UseCors("AllowAnyOrigin");

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}