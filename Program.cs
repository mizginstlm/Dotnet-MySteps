global using DotnetSteps.Models;
global using DotnetSteps.Services.CharacterService;
global using Microsoft.EntityFrameworkCore;
using System.Reflection;
using DotnetSteps.Data;
using DotnetSteps.Extensions;
using DotnetSteps.Repository;
using DotnetSteps.Services.MatchService;
using DotnetSteps.Services.PowerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<DataContext>(options => options.UseSqlServer
    (builder.Configuration.GetConnectionString("sqlConnection")));

builder.Services.AddControllers();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = """Standard Authorization header using the Bearer scheme. Example: "bearer {token}" """,
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddScoped<ICharacterService, CharacterService>();
builder.Services.AddScoped<IAuthRepository, AuthRepository>();
builder.Services.AddScoped<IPowerService, PowerService>();
builder.Services.AddScoped<IMatchService, MatchService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//auth scheme
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8
                .GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value!)),//null forgiving operator(!)
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//this is automatically will be addscoped by ef pacckage , and it will construct options of this datacontex
builder.Services.AddHttpContextAccessor();
var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseOffensiveWordsFilter();
app.UseHttpsRedirection();
app.UseAuthentication();//middleware for auth
app.UseAuthorization();

app.MapControllers();

app.Run();
