using Challenge.Domain.Options;
using Challenge.Infrastructure;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Repository;
using Challenge.Service.Implementations;
using Challenge.Service.Interfaces;
using Challenge.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
                    };
                });
builder.Services.AddMvc();
builder.Services.AddControllers();
builder.Services.AddRazorPages();



builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAuthentication, JwtAuthentication>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(SqlRepository<>));
builder.Services.AddScoped<KafkaService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<Func<string, IMessage>>((context) =>
{
    return message =>
    {
        if (message.StartsWith("/stock"))
            return context.GetService<KafkaService>();

        return context.GetService<ChatService>();
    };
});

builder.Services.AddScoped<IMessage, KafkaService>();
builder.Services.Configure<KafkaSettings>(builder.Configuration.GetSection(nameof(KafkaSettings)));

builder.Services.AddDbContext<ChallengeDbContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddDbContext<ChallengeIdentityDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services
    .AddIdentityCore<ChallengeUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ChallengeIdentityDbContext>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

var app = (WebApplication)builder.Build().MigrateDbContext<ChallengeIdentityDbContext>().MigrateDbContext<ChallengeDbContext>();

app.UseDeveloperExceptionPage();
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();
app.UseCors(options => options
    .WithOrigins(new string[] { "http://localhost:3000", "http://localhost:8080", "http://localhost:4200", "http://localhost:5000" })
    .AllowAnyHeader()
    .AllowAnyMethod());
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

app.Run();
