using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApi.Interfaces
{
    public interface IAccountRepository
    {
        Task<List<User>> GetAllAsync();
    }
}