﻿namespace RentAMovie.Infrastructure
{
    using System.Security.Claims;

    using static Areas.Admin.AdminConstants;

    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user )
        {
            return user != null ? user.IsInRole(AdministratorRoleName) : false;
        }
    }
}
