using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using eCommerceApi.Model;

namespace eCommerceApi.Dtos.Like
{
    public class CreateLikeDto
    {
        [Required(ErrorMessage = "Like type is required")]
        public LikeType LikeType { get; set; }
    }
}