using BankAccounts.Application.UseCases.CreateBankAccount;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

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
    }
}
