global using DotnetSteps.Models;
global using DotnetSteps.Services.CharacterService;
global using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotnetSteps.Data;
using DotnetSteps.Extensions;
using DotnetSteps.Repository;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddControllers();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();

builder.Services.AddAutoMapper(typeof(Program).Assembly);
//this is automatically will be addscoped by ef pacckage , and it will construct options of this datacontex

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseOffensiveWordsFilter();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
