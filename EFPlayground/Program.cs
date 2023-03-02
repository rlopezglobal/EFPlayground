using EFPlayground;
using Microsoft.EntityFrameworkCore;

var hostBuilder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.UseStartup<StartUp>();
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<VideoGameDbContext>(options =>
            options.UseSqlServer("Server=localhost;Database=VideoGameDb;Trusted_Connection=True;MultipleActiveResultSets=true"));

        services.AddScoped<DbInitializer>();
    })
    .Build();

using (var scope = hostBuilder.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetRequiredService<VideoGameDbContext>();
    DbInitializer.Initialize(dbContext);
}


hostBuilder.Run();

