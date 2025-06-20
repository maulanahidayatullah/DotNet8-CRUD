using Dotnet_AnimeCRUD.Model;
using Dotnet_AnimeCRUD.Service;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<AnimeService>();

builder.Services.AddDbContext<AnimeDBContext>(options =>
    // GetConnectionString yang ada pada appsettings.json
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// middleware untuk redirection kya dia unauthorize
app.UseHttpsRedirection();

// middleware untuk authorization
app.UseAuthorization();

app.MapControllers();

app.Run();
