using Challenge.Domain.Options;
using Challenge.Infrastructure;
using Challenge.Infrastructure.Entities;
using Challenge.Infrastructure.Repository;
using Challenge.Infrastructure.WebSocket;
using Challenge.Service.Implementations;
using Challenge.Service.Interfaces;
using Challenge.WebApi;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.WebSockets;
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
builder.Services.AddScoped<IChatRoomService, ChatRoomService>();
builder.Services.AddScoped<IChatMessageRepository, ChatMessageRepository>();
builder.Services.AddScoped<KafkaService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddSingleton<IWebSocketManager, WebSocketConnectionManager>();
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
    .AllowAnyHeader()
    .AllowAnyMethod()
    .AllowAnyOrigin());

var webSocketOptions = new WebSocketOptions()
{
    KeepAliveInterval = TimeSpan.FromSeconds(120),
    ReceiveBufferSize = 4 * 1024
};
app.UseWebSockets(webSocketOptions);
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapRazorPages();
});

var websocketsManager = app.Services.GetService<IWebSocketManager>();

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.StartsWith("/ws"))
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            var roomid = context.Request.Path.Value.Last().ToString();
            WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var socketFinishedTcs = new TaskCompletionSource<object>();
            websocketsManager.AddSocket(webSocket,int.Parse(roomid));
            await socketFinishedTcs.Task;
        }
        else
        {
            context.Response.StatusCode = 400;
        }
    }
    else
    {
        await next();
    }
});


app.Run();

