using BankAccounts.Application.UseCases.CreateBankAccount;
using BankAccounts.Domain.Interfaces.Repositories;
using BankAccounts.Infra.Data.Context;
using BankAccounts.Infra.Repositories;
using BankAccounts.Shared.Exceptions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.API.Configurations;

public static class DependencyInjection
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database") ?? throw new NotConnectionDefinedException($"Nenhuma conexão com o banco de dados foi definida.");
        services.AddDbContext<BankAccountsContext>(options => options.UseSqlServer(connection));
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateBankAccountCommand).Assembly));
        services.AddValidatorsFromAssemblyContaining<CreateBankAccountCommandValidator>();
    }

    public static void AddInfraServices(this IServiceCollection services)
    {
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IBankAccountsRepository, BankAccountsRepository>();
        services.AddScoped<IBalancesRepository, BalancesRepository>();
    }
}
