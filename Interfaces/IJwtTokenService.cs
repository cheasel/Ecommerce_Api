using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface IJwtTokenService
    {
        Task<string> CreateToken(User user);
    }
}