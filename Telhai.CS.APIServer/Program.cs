//using Microsoft.EntityFrameworkCore.Migrations;
using Telhai.CS.APIServer.Models;
using Microsoft.EntityFrameworkCore;
//using Telhai.CS.FinalProject;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddSingleton<IExamRepository, ExamRepository>();
builder.Services.AddDbContext<ExamDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("ExamDbContext")));

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
