using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BankAccounts.API.Endpoints;

public static class BankAccountsEndpoints
{
    public static void AddBankAccountsEndpoints(this RouteGroupBuilder route)
    {
        var bankAccountRoute = route.MapGroup("bank-accounts");

        bankAccountRoute.MapPost("/", async ([FromBody] CreateLocalityRequest request,
                                         [FromServices] ICreateLocalityHandler handler,
                                         CancellationToken cancellationToken) =>
        {
            var response = await handler.Handle(request, cancellationToken);

            if (response.StatusCode == HttpStatusCode.BadRequest)
                return Results.BadRequest(response);

            if (response.StatusCode == HttpStatusCode.Conflict)
                return Results.Conflict(response);

            return Results.Created("localities/{id}", response);
        })
            .Produces(StatusCodes.Status201Created, typeof(CreatedLocalitySuccessfully))
            .WithOpenApi(operation => new(operation)
            {
                Summary = "Registro de uma nova conta.",
                Description = "Permite a inserção de uma nova conta bancária no sistema.",
            })
            .WithTags("Bank Accounts");
    }
}
