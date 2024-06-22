using Azure.Core;
using Graduation_Project.Entities.Identity;
using Graduation_Project.Services.Abstracts;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Identity;
using EntityFrameworkCore.EncryptColumn.Interfaces;
using EntityFrameworkCore.EncryptColumn.Util;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using Graduation_Project.Features.Users.Queries.Results;

namespace Graduation_Project.Services.Implemention
{
    public class AuthenticationService : IAuthenticationService
    {
        #region Fields
        private readonly JwtSettings _jwtSettings;
        private readonly UserManager<User> _userManager;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly UNITOOLDbContext _unitoolDbContext;
        private readonly IEmailsService _emailService;
        private readonly IMapper _mapper;
       // private readonly IEncryptionProvider _encryptionProvider;

        #endregion 

        #region Constructors
        public AuthenticationService(JwtSettings jwtSettings,
                                     UserManager<User> userManager,
                                     IRefreshTokenRepository refreshTokenRepository,
                                     UNITOOLDbContext unitoolDbContext,
                                     IEmailsService emailService,
                                     IMapper mapper)
        {
            _jwtSettings = jwtSettings;
            _userManager = userManager;
            _refreshTokenRepository = refreshTokenRepository;
            _unitoolDbContext = unitoolDbContext;
            _emailService = emailService;
            _mapper = mapper;

            //_encryptionProvider = new GenerateEncryptionProvider("8a4dcaaec64d412380fe4b02193cd26f");

        }


        #endregion

        #region Handle Functions

        public async Task<JwtAuthResult> GetJWTToken(User user)
        {
            var (jwtToken,accessToken) =await GenerateJWTToken(user);
            var refreshToken = GetRefreshToken(user.UserName);
            var userRefreshToken = new UserRefreshToken
            {
                AddedTime = DateTime.Now,
                ExpiryDate = DateTime.Now.AddDays(_jwtSettings.RefreshTokenExpireDate),
                IsUsed=true,
                IsRevoked=false,
                JwtId=jwtToken.Id,
                RefreshToken=refreshToken.TokenString,
                Token=accessToken,
                UserId=user.Id

            };
            //var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id==request.Id);
          //  var userinfo = await _userManager.FindByIdAsync(_currentUserService.GetUserId().ToString());
            var result = _mapper.Map<GetUserByIdResponse>(user);
            await _refreshTokenRepository.AddAsync(userRefreshToken);
            return new JwtAuthResult
            {
                AccessToken = accessToken,
                Getuser=result
                //refreshToken = refreshToken
            };
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            var randomNumberGenerate = RandomNumberGenerator.Create();
            randomNumberGenerate.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
        public async Task<List<Claim>> GetClaims(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(nameof(UserClaimModel.Id), user.Id.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.UserName),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(nameof(UserClaimModel.UserName), user.UserName),
                new Claim(nameof(UserClaimModel.Email), user.Email),
                new Claim(nameof(UserClaimModel.Univserity), user.University),
                new Claim(nameof(UserClaimModel.FirstName), user.FirstName),
                new Claim(nameof(UserClaimModel.LastName), user.LastName),
   };
            var roles = await _userManager.GetRolesAsync(user);
            var userClaims = await _userManager.GetClaimsAsync(user);
            if (user.AcademicYear != null)
            {
                claims.Add(new Claim(nameof(UserClaimModel.AcadmicYear), user.AcademicYear));
            }
            if (user.College != null)
            {
                claims.Add(new Claim(nameof(UserClaimModel.College), user.College));
            }
            if (user.Government != null)
            {
                claims.Add(new Claim(nameof(UserClaimModel.Government), user.Government));
            }
            
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            claims.AddRange(userClaims);
            return claims;
        } 
        private RefreshToken GetRefreshToken(string userName)
        {
            var refreshToken = new RefreshToken
            {
                UserName = userName,
                TokenString = GenerateRefreshToken(),
                ExpireAt = DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate)
            };
            return refreshToken;
        }
        private async Task<(JwtSecurityToken, string)> GenerateJWTToken(User user)
        {
            var claims = await GetClaims(user);
            var jwtToken = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.Now.AddDays(_jwtSettings.AccessTokenExpireDate),
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)), SecurityAlgorithms.HmacSha256Signature));
            var accessToken = new JwtSecurityTokenHandler().WriteToken(jwtToken);
            return (jwtToken,accessToken);

        }
        
        public JwtSecurityToken ReadJWTToken(string accessToken)
        {
            if (string.IsNullOrEmpty(accessToken))
            {
                throw new ArgumentNullException(nameof(accessToken));
            }
            var handler = new JwtSecurityTokenHandler();
           return handler.ReadJwtToken(accessToken);
            
        }
        public async Task<string> ValidateToken(string accessToken)
        {
            var handler = new JwtSecurityTokenHandler();
            var parameters = new TokenValidationParameters
            {
                ValidateIssuer = _jwtSettings.ValidateIssuer,
                ValidIssuers = new[] { _jwtSettings.Issuer },
                ValidateIssuerSigningKey = _jwtSettings.ValidateIssuerSigningKey,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtSettings.Secret)),
                ValidAudience = _jwtSettings.Audience,
                ValidateAudience = _jwtSettings.ValidateAudience,
                ValidateLifetime = _jwtSettings.ValidateLifeTime,
                NameClaimType=ClaimTypes.NameIdentifier
            };
            try
            {
                var validator = handler.ValidateToken(accessToken, parameters, out SecurityToken validatedToken);

                if (validator == null)
                {
                    return "InvalidToken";
                }

                return "NotExpired";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public async Task<(string, DateTime?)> ValidateDetails(JwtSecurityToken jwtToken, string accessToken, string refreshToken)
        {
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256Signature))
            {
                return ("AlgorithmIsWrong", null);
            }
            if (jwtToken.ValidTo>DateTime.UtcNow)
            {
                return ("TokenIsNotExpired", null);
            } 

            //Get User

            var userId = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.Id)).Value;
            var userRefreshToken = await _refreshTokenRepository.GetTableNoTracking()
                                                               .FirstOrDefaultAsync(x => x.Token == accessToken &&
                                                                                         x.RefreshToken == refreshToken &&
                                                                                         x.UserId == int.Parse(userId));
            if (userRefreshToken == null)
              {
                    return ("RefreshTokenIsNotFound", null);
                }

            if (userRefreshToken.ExpiryDate < DateTime.UtcNow)
                {
                   userRefreshToken.IsRevoked = true;
                   userRefreshToken.IsUsed = false;
                   await _refreshTokenRepository.UpdateAsync(userRefreshToken);
                   return ("RefreshTokenIsExpired", null);
                }
           var expirydate = userRefreshToken.ExpiryDate;
          return (userId, expirydate);
        }
        public async Task<JwtAuthResult> GetRefreshToken(User user, JwtSecurityToken jwtToken, DateTime? expiryDate, string refreshToken)
        {
            var (jwtSecurityToken, newToken) = await GenerateJWTToken(user);
            var response = new JwtAuthResult();
            response.AccessToken = newToken;
            var refreshTokenResult = new RefreshToken();
            refreshTokenResult.UserName = jwtToken.Claims.FirstOrDefault(x => x.Type == nameof(UserClaimModel.UserName)).Value;
            refreshTokenResult.TokenString = refreshToken;
            refreshTokenResult.ExpireAt = (DateTime)expiryDate;
           // response.refreshToken = refreshTokenResult;
            return response;

        }

        public async Task<string> ConfirmEmail(int? userId, string? code)
        {
            if (userId == null || code == null)
                return "ErrorWhenConfirmEmail";
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return "User Not Found";
            }
             //var confirmEmail = await _userManager.ConfirmEmailAsync(user, code);
            var userCode = user.Code;
            //Equal With Code
            if (userCode == code)
            {
                user.EmailConfirmed = true;
                await _unitoolDbContext.SaveChangesAsync();
                return "Success";
            }
            return "ErrorWhenConfirmEmail";

        }

        public async Task<string> SendResetPasswordCode(string Email)
        {
            var trans = await _unitoolDbContext.Database.BeginTransactionAsync();
            try
            {
                //user
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                //Generate Random Number
                var chars = "0123456789";
                var random = new Random();
                var randomNumber = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

                //update User In Database Code
                user.Code = randomNumber;
                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                    return "ErrorInUpdateUser";
                var message = "Code To Reset Passsword : " + user.Code;
                //Send Code To  Email 
                await _emailService.SendEmail(user.Email, message, "Reset Password");
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }
        public async Task<string> ConfirmResetPassword(string Code, string Email)
        {
            //Get User
            //user
            var user = await _userManager.FindByEmailAsync(Email);
            //user not Exist => not found
            if (user == null)
                return "UserNotFound";
            //Decrept Code From Database User Code
            var userCode = user.Code;
            //Equal With Code
            if (userCode == Code) return "Success";
            return "Failed";
        }

        public async Task<string> ResetPassword(string Email, string Password)
        {
            var trans = await _unitoolDbContext.Database.BeginTransactionAsync();
            try
            {
                //Get User
                var user = await _userManager.FindByEmailAsync(Email);
                //user not Exist => not found
                if (user == null)
                    return "UserNotFound";
                await _userManager.RemovePasswordAsync(user);
                if (!await _userManager.HasPasswordAsync(user))
                {
                    await _userManager.AddPasswordAsync(user, Password);
                }
                await trans.CommitAsync();
                return "Success";
            }
            catch (Exception)
            {
                await trans.RollbackAsync();
                return "Failed";
            }
        }


        #endregion
    }
}