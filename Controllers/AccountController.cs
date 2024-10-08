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
        private readonly ILogger<AccountController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<UserRole> _roleManager;
        private readonly IJwtTokenService _tokenService;
        private readonly IAccountRepository _accountRepo;
        private readonly IProductRepository _productRepo;
        private readonly ApplicationDbContext _context;

        public AccountController(ILogger<AccountController> logger, UserManager<User> userManager, RoleManager<UserRole> roleManager, SignInManager<User> signInManager, IJwtTokenService tokenService, IAccountRepository accountRepo, IProductRepository productRepo, ApplicationDbContext context)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepo = accountRepo;
            _productRepo = productRepo;
            _context = context;
        }

        // Get all users [Admin only]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            try{
                var users = await _accountRepo.GetAllAsync();

                if(users == null || !users.Any()){
                    return NotFound("No users found.");
                }

                var userDtos = new List<UserDto>();

                foreach (var user in users)
                {
                    userDtos.Add(await user.ToUserDto(_userManager));
                }

                return Ok(userDtos);
            } catch (Exception ex){
                _logger.LogError(ex, "An error occurred while fetching users");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal server error");
            }
        }

        // Get user by Id [Admin Only]
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetById([FromRoute] int id){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _accountRepo.GetByIdAsync(id);

            if(user == null){
                return NotFound("User not found");
            }

            return Ok(user.ToUserDto(_userManager).Result);
        }

        // Get user profile [Authorize]
        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> GetProfile(){
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var username = User.GetUsername();
            var user = await _userManager.FindByNameAsync(username);
            var role = await _userManager.GetRolesAsync(user);

            return Ok(user.ToProfileDto(role.ToList()));
        }

        // Login 
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

        // Register for Customer
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

        // Register for Vendor
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

        // Update user profile [Authorize]
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