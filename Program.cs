global using DotnetSteps.Models;
global using DotnetSteps.Services.CharacterService;
global using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotnetSteps.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(connString));
builder.Services.AddControllers();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddSqlServer<DataContext>(connString);//this is automatically will be addscoped by ef pacckage , and it will construct options of this datacontex

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
