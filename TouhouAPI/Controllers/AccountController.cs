using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Identity;
using TouhouArticleMaker.Domain;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using TouhouData.Context;
using Microsoft.EntityFrameworkCore;

namespace TouhouAPI.Controllers
{
    [Produces("application/json")]
    [Route("Account")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        readonly TouhouContext _context;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, TouhouContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
        }

        [HttpGet("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Index()
        {
            return Ok();
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            return Ok();
        }

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            var newUser = new IdentityUser { UserName = user.UserName.Text, Email = user.Email.Address };

            var result = await _userManager.CreateAsync(newUser, user.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            await _signInManager.SignInAsync(newUser, isPersistent: false);

            return Ok(CreateToken(newUser));
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] User user)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName.Text, user.Password, false, lockoutOnFailure: false);

                if (!result.Succeeded)
                    return BadRequest();                

                var userFound = await _userManager.FindByEmailAsync(user.Email.Address);

                return Ok(CreateToken(userFound));
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        private string CreateToken(IdentityUser user)
        {
            // Creates an author if it not exists yet
            var articleAuthor =  _context.Authors.FirstOrDefaultAsync(a => a.UserName.Text == user.UserName).Result;

            if (articleAuthor == null)
            {
                //_context.Authors.Add(author);
                //_context.SaveChanges();
            }

            // Get author in order do make a claim


            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes("43e4dbf0-52ed-4203-895d-42b586496bd4");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    //new Claim("Store", "user"),//user.Role,
                    new Claim(ClaimTypes.Role, "Author"),
                    new Claim("AuthorId",articleAuthor.Id),
                    new Claim(JwtRegisteredClaimNames.Sub, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}