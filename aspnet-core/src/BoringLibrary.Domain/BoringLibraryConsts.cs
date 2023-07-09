namespace BoringLibrary;

public static class BoringLibraryConsts
{
    public const string DbTablePrefix = "App";

    public const string DbSchema = null;

    public const int DefaultBookCredit = 10;

    public const int DefaultQuantity = 10;

    public const double DefaultUserCredit = 100.0;

    public const int DeductionRate = 20;

    /// <summary>
    ///     Change to CronEveryMinuteExpression for every minute
    /// </summary>
    public const string CronDailyExpression = "0 0 * * *";

    public const string CronEveryMinuteExpression = "*/1 * * * *";


    public const int MaximumBorrow = 5;

    public const int DefaultAddingDaysDueDate = 7;

    /// <summary>
    ///     For development, add more date, CronEveryMinuteExpression
    /// </summary>
    public const bool IsTestDev = false;

    public const string ApproachingExpDate = "ApproachingExpDate";

    public const string Overdue = "Overdue";

    public const string WithinDueDate = "WithinDueDate";
}