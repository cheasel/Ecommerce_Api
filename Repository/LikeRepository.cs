using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class LikeRepository : ILikeRepository
    {
        private readonly ApplicationDbContext _context;

        public LikeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Like> CreateAsync(Like likeModel)
        {
            await _context.Likes.AddAsync(likeModel);
            await _context.SaveChangesAsync();

            return likeModel;
        }

        public async Task<Like?> DeleteAsync(int id, int userId, LikeType likeType)
        {
            var likeModel = new Like{};
            if(likeType == LikeType.Product){
                likeModel = await _context.Likes.FirstOrDefaultAsync(l => l.ProductId == id && l.UserId == userId);
            }else if(likeType == LikeType.Review){
                likeModel = await _context.Likes.FirstOrDefaultAsync(l => l.ReviewId == id && l.UserId == userId);
            }

            _context.Likes.Remove(likeModel);
            await _context.SaveChangesAsync();

            return likeModel;
        }

        public async Task<bool> LikeExists(int id, int userId, LikeType likeType)
        {
            if(likeType == LikeType.Product)
                return await _context.Likes.AnyAsync(l => l.ProductId == id && l.UserId == userId);
            else
                return await _context.Likes.AnyAsync(l => l.ReviewId == id && l.UserId == userId);
        }
    }
}