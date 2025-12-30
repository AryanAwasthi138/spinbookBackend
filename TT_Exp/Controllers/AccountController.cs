using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TableTennisBooking.Data;
using TableTennisBooking.DTO;
using TableTennisBooking.Models;
using TableTennisBooking.Services;

namespace TableTennisBooking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly JWTServices _jwtServices;
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _appDbContext;
        private readonly IConfiguration _config;

        public AccountController(JWTServices jwtServices,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            AppDbContext appDbContext,
            IConfiguration config)
        {
            _jwtServices = jwtServices;
            _signInManager = signInManager;
            _userManager = userManager;
            _appDbContext = appDbContext;
            _config = config;
        }


        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO model)
        {
            if (await CheckEmailExistsAsync(model.Email))
            {
                return BadRequest($"An existing account is using {model.Email} email address.Try with another email address.");
            }

            var userToAdd = new User
            {
                FirstName = model.FirstName.ToLower(),
                LastName = model.LastName.ToLower(),
                PhoneNumber = model.PhoneNumber,
                UserName = model.Email.ToLower(),
                Email = model.Email.ToLower(),
                EmailConfirmed = true,
                LockoutEnabled = false,
                JWT = string.Empty
            };
            var result = await _userManager.CreateAsync(userToAdd, model.Password);
            await _userManager.AddToRoleAsync(userToAdd, "User");

            if (!result.Succeeded) return BadRequest(result.Errors);
            //await _userManager.AddToRoleAsync(userToAdd, SD.UserRole);
            return Ok(new { Message = "Your account has been created, now you can login." });

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDTO>> Login(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, model.Password, lockoutOnFailure: false);

            if (!result.Succeeded)
            {
                return Unauthorized("Invalid username or password.");
            }

            var userDto = await CreateApplicationUserDto(user); // Await the CreateApplicationUserDto method
            return Ok(userDto);
        }
        private async Task<UserDTO> CreateApplicationUserDto(User user)
        {
            return new UserDTO
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = await _jwtServices.CreateJWT(user), // Await the JWT creation
            };
        }
        private async Task<bool> CheckEmailExistsAsync(string email)
        {
            return await _userManager.Users.AnyAsync(x => x.Email == email.ToLower());
        }
    }
}

