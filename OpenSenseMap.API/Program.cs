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

//When to Use AddTransient?
//Statelessness: Use AddTransient when your service or repository does not maintain any state between method calls.
//Each call receives a fresh instance, which is ideal for purely stateless operations.
builder.Services.AddTransient<IAuthenticationService, AuthenticationService>();
builder.Services.AddTransient<ISenseBoxService, SenseBoxService>();

//When to Use AddScoped?
//Stateful Operations Within a Request: If your service or repository needs to maintain state throughout a single HTTP request, use AddScoped.
//This ensures that all components in the same request share the same instance.
//a single instance of the service is created for each scope. In a web application, a scope typically corresponds to an HTTP request.
//The service instance is reused throughout the scope's lifetime and then disposed of when the scope ends.
//This is useful for services that maintain some state during a single HTTP request.

//When to Use AddSingleton?
//Global Shared Services: AddSingleton is ideal for stateless services or shared resources that need to be consistently available throughout the application.
//It ensures a single instance is reused application - wide.
//a single instance of the service is created for the lifetime of the application.
//This means the service is shared among all requests and clients throughout the lifetime of the application.
//It's suitable for services that are stateless or designed to be shared globally.
//E.g.logging services or configuration objects that are accessed frequently throughout the application.
//Note: Be cautious with AddSingleton for services that depend on scoped or transient services, as this can lead to issues with service lifetimes.

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