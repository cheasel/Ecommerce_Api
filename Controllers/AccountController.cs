using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Account;
using eCommerceApi.Extensions;
using eCommerceApi.Interfaces;
using eCommerceApi.Mappers;
using eCommerceApi.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eCommerceApi.Controllers
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IAccountRepository _accountRepo;
        private readonly IProductRepository _productRepo;
        private readonly ApplicationDbContext _context;

        public AccountController(UserManager<User> userManager, RoleManager<UserRole> roleManager, SignInManager<User> signInManager, IJwtTokenService tokenService, IAccountRepository accountRepo, IProductRepository productRepo, ApplicationDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepo = accountRepo;
            _productRepo = productRepo;
            _context = context;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var users = await _accountRepo.GetAllAsync();

            //var userDto = users.Select( u => u.ToUserDto(_userManager)).ToList();
            var userDtos = new List<UserDto>();
            foreach (var user in users)
            {
                userDtos.Add(await user.ToUserDto(_userManager));
            }

            return Ok(userDtos);
        }

        [HttpGet("like")]
        [Authorize(Roles = "Customer")]
        public async Task<IActionResult> GetLike(){
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            var userLike = await _accountRepo.GetLikeAsync(user.Id);

            var userLikeDto = new UserLikeDto {
                ProductLikes = userLike.Likes.Where(l => l.ProductId != null).Select(l => l.Product.Id).ToList(),
                ReviewLikes = userLike.Likes.Where(l => l.ReviewId != null).Select(l => l.Review.Id).ToList(),
            };

            return Ok(userLikeDto);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if (user == null)
            {
                return Unauthorized("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (!result.Succeeded)
            {
                return Unauthorized("Username not found and/or password incorrect");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(
                new NewUserDto
                {
                    Username = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register/user")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var User = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(User, registerDto.Password);

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(User, "Customer");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Username = User.UserName,
                                Email = User.Email,
                                Token = await _tokenService.CreateToken(User)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("register/vendor")]
        public async Task<IActionResult> VendorRegister([FromBody] VendorRegisterDto registerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var User = new User
                {
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(User, registerDto.Password);

                var vendor = new Vendor
                {
                    CompanyName = registerDto.CompanyName,
                    Description = registerDto.Description,
                    Email = registerDto.CompanyEmail,
                    PhoneNumber = registerDto.PhoneNumber,
                    Address = registerDto.Address,
                    WebsiteUrl = registerDto.WebsiteUrl,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now,
                    UserId = User.Id,
                };

                if (createdUser.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(User, "Vendor");

                    if (roleResult.Succeeded)
                    {
                        await _context.Vendors.AddAsync(vendor);
                        await _context.SaveChangesAsync();

                        return Ok(
                            new NewUserDto
                            {
                                Username = User.UserName,
                                Email = User.Email,
                                Token = await _tokenService.CreateToken(User)
                            }
                        );
                    }
                    else
                    {
                        return StatusCode(500, roleResult.Errors);
                    }
                }
                else
                {
                    return StatusCode(500, createdUser.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update(UpdateUserDto userDto){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);

            if(user == null){
                return NotFound();
            }

            user.UserName = userDto.Username;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.DateOfBirth = userDto.DateOfBirth;
            user.ProfilePictureUrl = userDto.ProfilePictureUrl;

            var result = await _userManager.UpdateAsync(user);
            
            if(!result.Succeeded){
                return BadRequest(result.Errors);
            }

            return Ok(new {
                Message = "User updated successfully"
            });
        }
    }
}