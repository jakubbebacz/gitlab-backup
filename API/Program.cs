using Application.IRepositories;
using Application.IServices;
using Application.Services;
using Infrastructure;
using Infrastructure.Middleware;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

builder.Services.AddScoped<IGroupService, GroupService>();
builder.Services.AddScoped<IBackupRepository, BackupRepository>();
builder.Services.AddScoped<IBackupService, BackupService>();
builder.Services.AddScoped<IRestClientService, RestClientService>();
builder.Services.AddScoped<ILabelService, LabelService>();
builder.Services.AddScoped<IMilestoneService, MilestoneService>();
builder.Services.AddScoped<ILabelRepository, LabelRepository>();
builder.Services.AddScoped<IMilestoneRepository, MilestoneRepository>();


builder.Services.AddDbContext<GitLabDbContext>(options => options.UseSqlite(
    builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

app.UseMiddleware<GlobalRoutePrefixMiddleware>("/api");
app.UsePathBase(new PathString("/api"));
app.UseRouting();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();