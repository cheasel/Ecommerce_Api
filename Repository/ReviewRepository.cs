using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApi.Repository
{
    public class ReviewRepository : IReviewRepository
    {
        private readonly ApplicationDbContext _context;

        public ReviewRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Review> CreateAsync(Review reviewModel)
        {
            await _context.Reviews.AddAsync(reviewModel);
            await _context.SaveChangesAsync();

            return reviewModel;
        }

        public async Task<Review?> DeleteAsync(int id)
        {
            var reviewModel = await _context.Reviews.FirstOrDefaultAsync(r => r.Id == id);

            if(reviewModel == null){
                return null;
            }

            _context.Reviews.Remove(reviewModel);
            await _context.SaveChangesAsync();

            return reviewModel;
        }

        public async Task<List<Review>> GetAllAsync()
        {
            var reviews = _context.Reviews.Include(r => r.Likes).AsQueryable();

            return await reviews.ToListAsync();
        }

        public async Task<Review?> GetByIdAsync(int id)
        {
            return await _context.Reviews.Include(r => r.Likes).FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<bool> ReviewExists(int id)
        {
            return await _context.Reviews.AnyAsync(r => r.Id == id);
        }

        public async Task<Review?> UpdateAsync(int id, Review reviewModel)
        {
            var existingReview = await _context.Reviews.FindAsync(id);

            if(existingReview == null){
                return null;
            }

            existingReview.Rating = reviewModel.Rating;
            existingReview.Comment = reviewModel.Comment;
            existingReview.UpdatedAt = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingReview;
        }
    }
}