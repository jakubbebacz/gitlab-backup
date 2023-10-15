using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure;
using Infrastructure.Middleware;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IBackupRepository, BackupRepository>();
builder.Services.AddScoped<IBackupService, BackupService>();
builder.Services.AddScoped<IRestClientService, RestClientService>();

builder.Services.AddDbContext<GitLabDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api");
app.UsePathBase(new PathString("/api"));
app.UseRouting();

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