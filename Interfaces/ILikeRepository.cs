using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Interfaces
{
    public interface ILikeRepository
    {
        Task<User> GetLikeAsync(int id);
        Task<Like> CreateAsync(Like likeModel);
        Task<Like?> DeleteAsync(int id, int userId, LikeType likeType);
        Task<bool> LikeExists(int id, int userId, LikeType likeType);
    }
}