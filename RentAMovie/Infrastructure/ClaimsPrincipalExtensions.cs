namespace RentAMovie.Infrastructure
{
    using System.Security.Claims;

    using static WebConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user )
        {
            return user.IsInRole(AdministratorRoleName);
        }
    }
}
