using Challenge.Infrastructure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Challenge.Infrastructure.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Challenge.WebApi;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Challenge.Service.Interfaces;
using Challenge.Service.Implementations;
using System.Configuration;
using Challenge.Domain.Options;
using Challenge.Infrastructure.Kafka;
using Challenge.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthentication, JwtAuthentication>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddScoped<KafkaService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<Func<string, IMessage>>((context) => {
    return message =>
    {
        if (message.StartsWith("/stock"))
            return context.GetService<KafkaService>();

        return context.GetService<ChatService>();
    };
    });

builder.Services.AddScoped<IMessage, KafkaService>();
//builder.Services.AddHostedService<KafkaConsumerHandler>();


builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(nameof(KafkaSettings)));

builder.Services.AddDbContext<ChallengeDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ChallengeIdentityDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddIdentity<ChallengeUser, IdentityRole>(options =>
{
    options.User.RequireUniqueEmail = false;
})
    .AddEntityFrameworkStores<ChallengeIdentityDbContext>()
    .AddDefaultTokenProviders();
var app = (WebApplication)builder.Build().MigrateDbContext<ChallengeIdentityDbContext>().MigrateDbContext<ChallengeDbContext>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(options=> options
    .WithOrigins(new string[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200", "http://localhost:5000" })
    .AllowAnyHeader()
    .AllowAnyMethod());
app.MapControllers();

app.Run();

