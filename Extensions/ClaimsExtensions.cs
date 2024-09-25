using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eCommerceApi.Extensions
{
    public static class ClaimsExtensions
    {
        public static string GetUsername(this ClaimsPrincipal user){
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname")).Value;
        }

        public static string GetRole(this ClaimsPrincipal user){
            return user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.microsoft.com/ws/2008/06/identity/claims/role")).Value;
        }
    }
}