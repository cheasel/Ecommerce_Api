using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace eCommerceApi.Dtos.Account
{
    public class UpdateUserDto
    {
        [Required(ErrorMessage = "Username is required")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? Email { get; set; }
        //[Required(ErrorMessage = "Password is required")]
        //public string? Password { get; set; }
        [Required(ErrorMessage = "Firstname is required")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Lastname is required")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Date of birth is required")]
        public DateTime? DateOfBirth { get; set; }
        [Required(ErrorMessage = "Profile picture is required")]
        public string? ProfilePictureUrl { get; set; }
    }
}