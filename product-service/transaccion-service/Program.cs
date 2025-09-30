using Microsoft.EntityFrameworkCore;
using transaccion_service.Data;
using transaccion_service.Repository;
using transaccion_service.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//----------Conecion a BD
builder.Services.AddDbContext<AppDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("CadenaSQL"));
});

//----------registro de servicios
builder.Services.AddScoped<ITransaccionService, TransaccionRepository>();

//----------validacion de los tipos num -> acepten strings
builder.Services.AddControllers()
    .AddJsonOptions(opts =>
    {
        opts.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter());
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
