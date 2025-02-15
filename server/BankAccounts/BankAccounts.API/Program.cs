using BankAccounts.API.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddContext(builder.Configuration);
builder.Services.AddSwaggerConfiguration();


var app = builder.Build();

var mapGroupV1 = app.MapGroup("v1");

app.UseSwaggerConfiguration(app.Environment);
app.UseHttpsRedirection();
app.Run();
