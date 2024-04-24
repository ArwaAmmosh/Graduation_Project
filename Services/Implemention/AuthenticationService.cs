using Graduation_Project.Entities.Identity;
using Graduation_Project.Results;
using Graduation_Project.Services.Abstracts;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Graduation_Project.Services.Implemention
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        #endregion 

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                     UserManager<User> userManager)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
        }


        #endregion

        #region Handle Functions

        public async Task<string> GetJWTToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim (nameof(UserClaimModel.UserName),user.UserName),
                new Claim (nameof(UserClaimModel.Email),user.Email),
                new Claim (nameof(UserClaimModel.University),user.Univserity)


            };
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return await Task.FromResult(accessToken);
        }

      





        #endregion
    }
}