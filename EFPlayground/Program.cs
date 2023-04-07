using EFPlayground;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using AutoMapper;

var builder = WebApplication.CreateBuilder(); 

// Load configuration from appsettings.json
builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

// Add Services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<VideoGameDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("VideoGameDbConnection")));
builder.Services.AddAutoMapper(typeof(VideoGameProfile));


// Add DbInitializer to ensure the database is created and contains sample data
var serviceScopeFactory = builder.Services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
using var scope = serviceScopeFactory.CreateScope();
var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<VideoGameDbContext>();
    await DbInitializer.InitializeAsync(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred while seeding the database.");
}

// Add swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo {Title = "Video Game API", Version = "v1"});
});

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // use developer exception page for better error messages
    app.UseDeveloperExceptionPage();
}

// Redirect HTTP requests to HTTPS, set up routing middleware, set up authorization middleware and Map controllers to endpoints
app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});


// Set up Swagger and Swagger UI
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Video Game API V1");
});

// Start the application
app.Run();

