namespace BankAccounts.Domain.Enums;
public enum TransactionType
{
    CREDIT,       // Crédito
    DEBIT,        // Débito
    AMOUNT_HOLD,  // Bloqueio
    AMOUNT_RELEASE // Desbloqueio
}