using Microsoft.AspNetCore.ResponseCompression;
using ProjectSolarEdge.Shared.Services.Games;
using ProjectSolarEdge.Shared.Services.Questions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
string connectionString = builder.Configuration.GetConnectionString("SolarEdgeDB");
builder.Services.AddSingleton<IQuestionRepository>(new QuestionRepository(connectionString));
builder.Services.AddSingleton<IGameRepository>(new GameRepository(connectionString));

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

app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseWebAssemblyDebugging();

app.Run();
