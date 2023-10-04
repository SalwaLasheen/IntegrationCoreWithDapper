using BenchmarkDotNet.Running;
using EFCore2VSDapper;
using EFCoreVsDapper.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
var app = builder.Build();

var summary = BenchmarkRunner.Run(typeof(EFCore2VsDapper));

Console.WriteLine("done");
Console.ReadLine();
app.Run();
