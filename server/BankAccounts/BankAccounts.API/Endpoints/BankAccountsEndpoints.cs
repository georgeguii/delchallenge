using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankAccounts.API.Endpoints;

public static class BankAccountsEndpoints
{
    public static void AddBankAccountsEndpoints(this RouteGroupBuilder route)
    {
        var bankAccountRoute = route.MapGroup("bank-accounts");

        bankAccountRoute.MapPost("/", async ([FromBody] dynamic request,
                                         [FromServices] dynamic handler,
                                         CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.Conflict(response);

            return Results.Created("bank-accounts/{id}", response);
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
