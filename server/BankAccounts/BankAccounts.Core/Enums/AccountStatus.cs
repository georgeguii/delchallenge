namespace BankAccounts.Enums;
public enum AccountStatus
{
    ACTIVE,
    BLOCKED,
    FINISHED
}

public static class AccountStatusMapper
{
    public static bool TryParse(string status, out AccountStatus result)
    {
        return Enum.TryParse(status, true, out result);
    }
}