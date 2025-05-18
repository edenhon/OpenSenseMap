using OpenSenseMap.API.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
var apiBaseUrl = builder.Configuration.GetValue<string>("OpenSenseMapAPIBaseURL");
if (string.IsNullOrEmpty(apiBaseUrl))
{
    throw new InvalidOperationException("The configuration value for 'OpenSenseMapAPIBaseURL' in appsettings.json is missing or null.");
}
builder.Services.AddHttpClient<HttpService>(cl => cl.BaseAddress = new Uri(apiBaseUrl));
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ISenseBoxService, SenseBoxService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new() { Title = "OpenSenseMap API", Version = "v1", Description = "Technical Assessment for developer: Hon Jiann Shyang" });
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    options.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();