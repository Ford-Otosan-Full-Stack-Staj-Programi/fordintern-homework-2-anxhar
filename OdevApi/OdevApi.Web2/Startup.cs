using OdevApi.Base;
using OdevApi.Web.Extension;
using OdevApi.Web.Middleware;
namespace OdevApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }
    public static JwtConfig JwtConfig { get; private set; }
    public void ConfigureServices(IServiceCollection services)
    {
        JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));


        services.AddDbContextDI(Configuration);
        services.AddServices();
        services.AddMapperService();
        services.AddBussinessServices();
        services.AddJwtAuthentication(); 
        services.AddCustomSwagger();

        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OdevApi"));
        }

        app.UseHttpsRedirection();

        app.UseMiddleware<HeartbeatMiddleware>();
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<RequestLoggingMiddleware>();

        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.AddApplicationServices();
    }
}
