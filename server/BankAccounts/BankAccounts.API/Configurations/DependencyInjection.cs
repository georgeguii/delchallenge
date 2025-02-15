using BankAccounts.Infra.Data.Context;
using BankAccounts.Shared.Exceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System.Globalization;

namespace BankAccounts.API.Configurations;

public static class DependencyInjection
{
    public static void AddContext(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database") ?? throw new NotConnectionDefinedException($"Nenhuma conexão foi definida com o banco de dados.");
        services.AddDbContext<BankAccountsContext>(options => options.UseSqlServer(connection));
    }

    public static void AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());

    }
}
