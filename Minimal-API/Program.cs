using DataAccessLayer.DependencyInjection;
using DataAccessLayer.Persistence.Seeds;
using Minimal_API.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
//seed data
GenerateData.UseItToSeedSqlServer(app);
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.SerializeAsV2 = true;
    });
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
//BenchmarkRunner.Run(typeof(TestBench));
app.MapEmployeeEndPoints();
app.MapControllers();
app.Run();


