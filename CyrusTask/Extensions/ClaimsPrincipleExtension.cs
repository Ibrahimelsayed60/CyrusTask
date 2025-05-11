﻿using System.Security.Authentication;
using System.Security.Claims;

namespace CyrusTask.Extensions
{
    public static class ClaimsPrincipleExtension
    {
        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email)
                ?? throw new AuthenticationException("Email claim not found");

            return email;
        }
    }
}
