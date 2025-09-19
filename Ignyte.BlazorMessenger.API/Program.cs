using Ignyte.BlazorMessenger.API.ChatHub;
using Ignyte.BlazorMessenger.DataLayer.DatabaseContext;
using Ignyte.BlazorMessenger.DataLayer.Interface;
using Ignyte.BlazorMessenger.DataLayer.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ChatDatabaseContext>(options => options.UseSqlite("Data Source=messanger.db"));
builder.Services.AddTransient<IAuthService, AuthServices>();
builder.Services.AddTransient<IChatServices, ChatServices>();

builder.Services.AddSignalR();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
      builder =>
      {
          builder
        .AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
      });
});



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("AllowAllOrigins");
app.MapControllers();
app.MapHub<ChatHub>("/chatHub");

app.Run();
