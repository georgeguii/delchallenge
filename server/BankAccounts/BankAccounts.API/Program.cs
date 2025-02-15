using BankAccounts.API.Configurations;
using BankAccounts.API.Endpoints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddContext(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfraServices();
builder.Services.AddApplicationServices();


var app = builder.Build();

var mapGroupV1 = app.MapGroup("v1");
mapGroupV1.AddBankAccountsEndpoints();

app.UseSwaggerConfiguration(app.Environment);
app.UseHttpsRedirection();
app.Run();
