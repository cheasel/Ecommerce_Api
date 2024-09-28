using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Identity;

namespace eCommerceApi.Mappers
{
    public static class LikeMapper
    {
        public static async Task<LikeDto> ToLikeDto(this Like likeModel, UserManager<User> _userManager){
            var user = await _userManager.FindByIdAsync(likeModel.UserId.ToString());

            return new LikeDto {
                LikeDate = likeModel.LikeDate,
                LikeType = likeModel.LikeType,
                LikedBy = user == null ? "" : user.UserName,
            };
        }

        public static Like ToLikeFromCreateDto(this CreateLikeDto likeDto){
            return new Like {
                LikeDate = DateTime.Now,
                LikeType = likeDto.LikeType,
            };
        }
    }
}