
using Graduation_Project.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace Graduation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UNITOOLContext _context;
        public UserController(UNITOOLContext context)
        {
            context = context;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Regiester (UserRegiesteration request)
        {
            if (_context.User.Any(u => u.Email == request.Email)) { 
                    
                return BadRequest("User Already exists");
            }
            CreatePasswordHash(request.Password,out byte[] PasswordHash ,out byte[] PasswordSalt);
            var user = new User
            {
                Email = request.Email,
                PasswordHash = PasswordHash,
                PasswordSalt = PasswordSalt,
                verficationToken = CreateRandomToken()
            };
            _context.User.Add(user);
            await _context.SaveChangesAsync();
            return Ok("User Successfully Created.");

        }
        //Encoding Password
        private void CreatePasswordHash(string Password,out byte[] PasswordHash,out byte[] PasswordSalt)
        {
            using (var hmac=new HMACSHA512())
            {
                PasswordSalt = hmac.Key;
                PasswordHash=hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(Password));
            }
        }
        //Create Random Token
        private string CreateRandomToken()
        {
            return Convert.ToHexString(RandomNumberGenerator.GetBytes(32));
        }
    }

}
