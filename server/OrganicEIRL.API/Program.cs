using OrganicEIRL.Application;
using OrganicEIRL.Infrastructure;
using OrganicEIRL.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddApplication();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();

  await InitializeDatabaseAsync(app);
}

app.UseCors(policy =>
    policy.AllowAnyOrigin()
          .AllowAnyHeader()
          .AllowAnyMethod()
);

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

static async Task InitializeDatabaseAsync(WebApplication app)
{
  using var scope = app.Services.CreateScope();
  var services = scope.ServiceProvider;
  var logger = services.GetRequiredService<ILogger<Program>>();
  var context = services.GetRequiredService<ApplicationDbContext>();

  for (int i = 0; i < 10; i++)
  {
    try
    {
      await context.Database.EnsureCreatedAsync();
      await SeedData.SeedAsync(context);
      logger.LogInformation("Base de datos inicializada correctamente");
      return;
    }
    catch (Exception ex)
    {
      logger.LogWarning("Intento {Attempt}/10 falló: {Message}", i + 1, ex.Message);
      await Task.Delay(5000);
    }
  }

  logger.LogError("No se pudo inicializar la base de datos");
}