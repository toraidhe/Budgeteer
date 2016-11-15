using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace Budgeteer.Data.Claims
{
    public class AppClaimsTransformer : IClaimsTransformer
    {
        public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("ProjectReader", "true"));
            return Task.FromResult(principal);
        }

        public Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
        {
            var principal = context.Principal;
            ((ClaimsIdentity)principal.Identity).AddClaim(new Claim("ProjectReader", "true"));
            return Task.FromResult(principal);
        }
    }
}
