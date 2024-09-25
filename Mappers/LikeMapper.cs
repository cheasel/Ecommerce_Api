using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Dtos.Like;
using eCommerceApi.Interfaces;
using eCommerceApi.Model;

namespace eCommerceApi.Mappers
{
    public static class LikeMapper
    {
        public static LikeDto ToLikeDto(this Like likeModel, IAccountRepository _userRepo){
            var username = _userRepo.GetUsername(likeModel.UserId).Result;

            return new LikeDto {
                //Id = likeModel.Id,
                LikeDate = likeModel.LikeDate,
                LikeType = likeModel.LikeType,
                LikedBy = username,
            };
        }
    }
}