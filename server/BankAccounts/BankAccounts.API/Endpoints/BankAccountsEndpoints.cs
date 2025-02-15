using BankAccounts.Application.UseCases.CreateBankAccount;
using BankAccounts.Domain.Entities;
using BankAccounts.Enums;
using BankAccounts.Infra.Data.Context;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAccounts.API.Endpoints;

public static class BankAccountsEndpoints
{
    public static void AddBankAccountsEndpoints(this RouteGroupBuilder route)
    {
        var bankAccountRoute = route.MapGroup("bank-accounts");

        bankAccountRoute.MapPost("/", async (
            [FromBody] CreateBankAccountCommand request,
            [FromServices] IMediator mediator,
            [FromServices] IValidator<CreateBankAccountCommand> validator,
            CancellationToken cancellationToken) =>
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                return Results.ValidationProblem(validationResult.ToDictionary());
            }

            var response = await mediator.Send(request, cancellationToken);
            return Results.Created($"bank-accounts/{response.Id}", response);
        })
            .Produces(StatusCodes.Status201Created)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Registro de uma nova conta.",
                Description = "Permite a inserção de uma nova conta bancária no sistema.",
            })
            .WithTags("Bank Accounts");


        bankAccountRoute.MapGet("/{accountNumber}", async (
            [FromRoute] string accountNumber,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var account = await dbContext.BankAccounts
                .FirstOrDefaultAsync(a => a.Number == accountNumber, cancellationToken);

            return account is not null ? Results.Ok(account) : Results.NotFound();
        })
        .Produces<BankAccount>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Buscar conta por número.",
            Description = "Obtém uma conta bancária pelo número informado.",
        })
        .WithTags("Bank Accounts");

        bankAccountRoute.MapGet("/branch/{branch}", async (
            [FromRoute] string branch,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var accounts = await dbContext.BankAccounts
                .Where(a => a.Branch == branch)
                .ToListAsync(cancellationToken);

            return accounts.Any() ? Results.Ok(accounts) : Results.Ok(new List<BankAccount>());
        })
        .Produces<List<BankAccount>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Buscar contas por agência.",
            Description = "Obtém todas as contas bancárias de uma agência específica.",
        })
        .WithTags("Bank Accounts");


        bankAccountRoute.MapGet("/holder/{document}", async (
            [FromRoute] string document,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var accounts = await dbContext.BankAccounts
                .Where(a => a.HolderDocument == document)
                .ToListAsync(cancellationToken);

            return accounts.Any() ? Results.Ok(accounts) : Results.Ok(new List<BankAccount>());
        })
        .Produces<List<BankAccount>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Buscar contas por titular.",
            Description = "Obtém todas as contas bancárias de um mesmo titular.",
        })
        .WithTags("Bank Accounts");

        bankAccountRoute.MapPut("/holder/{accountId}/email", async (
        [FromRoute] string accountId,
        [FromBody] string newEmail,
        [FromServices] BankAccountsContext dbContext,
        CancellationToken cancellationToken) =>
        {
            var account = await dbContext.BankAccounts.FindAsync(new { accountId }, cancellationToken);
            if (account is null) return Results.NotFound();

            account.UpdateEmail(newEmail);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .Produces(StatusCodes.Status404NotFound)
        .WithOpenApi(operation => new(operation)
        {
            Summary = "Editar e-mail do titular.",
            Description = "Permite editar o e-mail do titular de uma conta bancária.",
        })
        .WithTags("Bank Accounts");

        bankAccountRoute.MapPut("/{accountId}/status", async (
            [FromRoute] string accountId,
            [FromBody] string newStatus,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var account = await dbContext.BankAccounts.FindAsync(new { accountId }, cancellationToken);
            if (account is null) return Results.NotFound();

            if (!AccountStatusMapper.TryParse(newStatus, out var parsedStatus))
            {
                return Results.BadRequest("Invalid account status.");
            }

            account.UpdateStatus(parsedStatus);
            await dbContext.SaveChangesAsync(cancellationToken);
            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Atualizar status da conta.",
                Description = "Permite atualizar o status de uma conta bancária.",
            })
            .WithTags("Bank Accounts");

        bankAccountRoute.MapDelete("/{accountId}", async (
            [FromRoute] int accountId,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var account = await dbContext.BankAccounts.FirstOrDefaultAsync(x => x.Id == accountId);
            if (account is null) return Results.NotFound();

            account.FinishAccount();
            await dbContext.SaveChangesAsync(cancellationToken);

            return Results.NoContent();
        })
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Encerrar conta bancária.",
                Description = "Permite encerrar uma conta bancária.",
            })
            .WithTags("Bank Accounts");

        bankAccountRoute.MapGet("/{accountId}/balance", async (
            [FromRoute] int accountId,
            [FromServices] BankAccountsContext dbContext,
            CancellationToken cancellationToken) =>
        {
            var balance = await dbContext.Balances.FirstOrDefaultAsync(x => x.BankAccountId == accountId);
            if (balance is null) return Results.NotFound();

            return Results.Ok(new { balance });
        })
            .Produces(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Consultar saldo da conta.",
                Description = "Permite consultar o saldo de uma conta bancária.",
            })
            .WithTags("Bank Accounts");


    }
}
