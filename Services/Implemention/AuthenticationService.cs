
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using System.Text;

namespace Graduation_Project.Services.Implemention
{
    public class AuthenticationService: IAuthenticationService
    {
        #region Fields
        private readonly JWTSettings _jwtSettings;
        #endregion
        #region Constructor
         public AuthenticationService(JWTSettings jwtSettings)
          { 
            _jwtSettings= jwtSettings;
           }
        #endregion
        #region Handle function
          public Task<string> GetJWTToken(User user)
            {
                var claims=new List<Claim>()
                { 
                     new Claim(nameof(UserClaimModel.UserName), user.UserName),
                     new Claim(nameof(UserClaimModel.Email), user.Email),
                     new Claim(nameof(UserClaimModel.University), user.Univserity)



                };
                var jwtToken=new JwtSecurityToken(_jwtSettings.Issuer,_jwtSettings.Audience,claims,expires:DateTime.UtcNow.AddMinutes(80),signingCredentials:new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Sercet)),SecurityAlgorithms.HmacSha256Signature));
                var AcessToken=new JwtSecurityTokenHandler().WriteToken(jwtToken);
                return Task.FromResult(AcessToken);

            }
        #endregion
    }
}
