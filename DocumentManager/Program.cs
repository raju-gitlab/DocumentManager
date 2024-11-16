using CommonApplicationFramework.ConfigurationHandling;
using DocumentManager.Services;
using System.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

string CorspolicyName = "test";
// Add services to the container.
builder.Services.AddControllers().AddJsonOptions(opts => opts.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddHttpContextAccessor();
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: CorspolicyName, builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

DependencyInjection.Resolve(builder.Services);
var app = builder.Build();
APIConfigurationManager ConfigManager = new APIConfigurationManager();
// Configure the HTTP request pipeline.
app.UseCors(CorspolicyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
