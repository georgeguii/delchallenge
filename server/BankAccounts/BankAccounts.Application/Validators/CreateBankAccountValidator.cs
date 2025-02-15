using BankAccounts.Enums;
using FluentValidation;
using System.Text.RegularExpressions;

namespace BankAccounts.Application.UseCases.CreateBankAccount;

public class CreateBankAccountCommandValidator : AbstractValidator<CreateBankAccountCommand>
{
    public CreateBankAccountCommandValidator()
    {
        RuleFor(x => x.Branch)
            .NotEmpty().WithMessage("Agência é obrigatória")
            .MaximumLength(5).WithMessage("Agência deve ter no máximo 5 caracteres");

        RuleFor(x => x.AccountType)
            .IsInEnum().WithMessage("Tipo de conta inválido")
            .Must(x => x == AccountType.PAYMENT || x == AccountType.CURRENT)
            .WithMessage("Tipos válidos: PAYMENT, CURRENT");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Nome do titular é obrigatório")
            .MaximumLength(200).WithMessage("Nome deve ter no máximo 200 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("E-mail é obrigatório")
            .MaximumLength(200).WithMessage("E-mail deve ter no máximo 200 caracteres")
            .Matches(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")
            .WithMessage("Formato de e-mail inválido");

        RuleFor(x => x.LegalType)
            .IsInEnum().WithMessage("Tipo de titular inválido")
            .Must(x => x == HolderType.NATURAL || x == HolderType.LEGAL)
            .WithMessage("Tipos válidos: NATURAL, LEGAL");

        RuleFor(x => x.Document)
            .NotEmpty().WithMessage("Documento é obrigatório")
            .Must((command, document) => IsValidDocument(document, command.LegalType))
            .WithMessage("Documento inválido para o tipo de titular");
    }

    private bool IsValidDocument(string document, HolderType legalType)
    {
        var cleanedDocument = Regex.Replace(document, @"[^\d]", "");

        return legalType switch
        {
            HolderType.NATURAL => IsValidCpf(cleanedDocument),
            HolderType.LEGAL => IsValidCnpj(cleanedDocument),
            _ => false
        };
    }

    private bool IsValidCpf(string cpf)
    {
        if (cpf.Length != 11 || !cpf.All(char.IsDigit))
            return false;

        var tempCpf = cpf[..9];
        var digitos = cpf.Substring(9, 2);

        var soma = 0;
        for (var i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (10 - i);

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        soma = 0;
        tempCpf += digito1;
        for (var i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * (11 - i);

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return digitos == $"{digito1}{digito2}";
    }

    private bool IsValidCnpj(string cnpj)
    {
        if (cnpj.Length != 14 || !cnpj.All(char.IsDigit))
            return false;

        var tempCnpj = cnpj[..12];
        var digitos = cnpj.Substring(12, 2);

        var soma = 0;
        var multiplicador = 5;
        for (var i = 0; i < 12; i++)
        {
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador;
            multiplicador = (multiplicador == 2) ? 9 : multiplicador - 1;
        }

        var resto = soma % 11;
        var digito1 = resto < 2 ? 0 : 11 - resto;

        soma = 0;
        tempCnpj += digito1;
        multiplicador = 6;
        for (var i = 0; i < 13; i++)
        {
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador;
            multiplicador = (multiplicador == 2) ? 9 : multiplicador - 1;
        }

        resto = soma % 11;
        var digito2 = resto < 2 ? 0 : 11 - resto;

        return digitos == $"{digito1}{digito2}";
    }
}