using Volo.Abp.Data;
using Volo.Abp.Identity;

namespace BoringLibrary.CustomUsers;

public static class IdentityUserExtensions
{
    private const string CreditPropertyName = "Credit";

    public static void SetCredit(this IdentityUser user, double credit)
    {
        user.SetProperty(CreditPropertyName, credit);
    }

    public static double GetCredit(this IdentityUser user)
    {
        return user.GetProperty<double>(CreditPropertyName);
    }
}