using Microsoft.EntityFrameworkCore;
using PromillePartner_BackEnd.Data;
using PromillePartner_BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<VoresDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add services to the container.
builder.Services.AddScoped<PersonRepository>();
builder.Services.AddScoped<PiReadingRepository>();
builder.Services.AddScoped<DrinkPlanRepository>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseCors();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
