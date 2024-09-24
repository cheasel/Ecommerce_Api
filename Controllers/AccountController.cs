using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using eCommerceApi.Data;
using eCommerceApi.Dtos.Account;
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
        private readonly IJwtTokenService _tokenService;
        private readonly IAccountRepository _accountRepo;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtTokenService tokenService, IAccountRepository accountRepo)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
            _accountRepo = accountRepo;
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll() {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var users = await _accountRepo.GetAllAsync();
            
            //var userDto = users.Select( u => u.ToUserDto(_userManager)).ToList();
            var userDtos = new List<UserDto>();
            foreach(var user in users){
                userDtos.Add(await user.ToUserDto(_userManager));
            }

            return Ok(userDtos);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }

            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == loginDto.Username.ToLower());

            if(user == null){
                return Unauthorized("Invalid username!");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if(!result.Succeeded){
                return Unauthorized("Username not found and/or password incorrect");
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            return Ok(
                new NewUserDto{
                    Username = user.UserName,
                    Email = user.Email,
                    Token = await _tokenService.CreateToken(user)
                }
            );
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto){
            try {
                if(!ModelState.IsValid){
                    return BadRequest(ModelState);
                }

                var User = new User{
                    UserName = registerDto.Username,
                    Email = registerDto.Email
                };

                var createdUser = await _userManager.CreateAsync(User, registerDto.Password);

                if(createdUser.Succeeded){
                    var roleResult = await _userManager.AddToRoleAsync(User, "Customer");

                    if(roleResult.Succeeded){
                        return Ok(
                            new NewUserDto{
                                Username = User.UserName,
                                Email = User.Email,
                                Token = await _tokenService.CreateToken(User)
                            }
                        );
                    }else{
                        return StatusCode(500, roleResult.Errors);
                    }
                }else{
                    return StatusCode(500, createdUser.Errors);
                }
            } catch(Exception e) {
                return StatusCode(500, e);
            }
        }
    }
}