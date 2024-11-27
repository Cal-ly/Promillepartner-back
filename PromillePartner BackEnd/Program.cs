using PromillePartner_BackEnd.Data.MockData;
using PromillePartner_BackEnd.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<PersonRepository>();
builder.Services.AddSingleton<PiReadingRepository>();

builder.Services.AddControllers();

const string allCanGet = "AllGetOnly";
builder.Services.AddCors(options => options.AddPolicy(name: allCanGet, policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));

builder.Services.AddSwaggerGen();
builder.Services.AddEndpointsApiExplorer();
var app = builder.Build();

app.UseCors(allCanGet);
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//var personRepository = app.Services.GetRequiredService<PersonRepository>();
//if (!personRepository.GetPersons().Any())
//    MockPerson.AddMockPersonsToRepository(personRepository);

app.Run();
