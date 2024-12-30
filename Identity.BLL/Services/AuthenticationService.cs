using Identity.Common.DTOs.Request;
using Identity.Common.DTOs.Response;
using Identity.Common.Exceptions;
using Identity.Common.Helpers;
using Identity.DAL.Entities;
using Identity.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.BLL.Services
{
    public class AuthenticationService
    {
        private readonly AppSettings _appSettings;
        private readonly UserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly InvalidatedTokenRepository _invalidatedTokenRepository;

        public AuthenticationService(IOptions<AppSettings> appSettings, UserRepository userRepository,
            IPasswordHasher<User> passwordHasher, InvalidatedTokenRepository invalidatedTokenRepository)
        {
            _appSettings = appSettings.Value;
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _invalidatedTokenRepository = invalidatedTokenRepository;
        }

        public async Task<AuthenticationResponse> Authenticate(AuthenticationRequest request)
        {
            var user = await _userRepository.GetUserAsync(user => user.Username == request.Username);

            if (_passwordHasher.VerifyHashedPassword(user, user.Password, request.Password) == 
                PasswordVerificationResult.Failed)
            {
                throw new AppException("username or password incorrect");
            }
            // authentication successful so generate jwt token
            var token = GenerateJwtToken(user);

            return new AuthenticationResponse 
            { 
                Token = token,
                isAuthenticated = true,
            };
        }

        public async Task Logout(ClaimsPrincipal user)
        {
            var jwtId = user.FindFirst(JwtRegisteredClaimNames.Jti)!.Value;
            var expiryTimeSql = user.FindFirst(JwtRegisteredClaimNames.Exp)!.Value;

            var dateExpiry = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryTimeSql))
                .UtcDateTime);
            var jwt = await _invalidatedTokenRepository.GetAsync(jwtId);

            if(DateTime.UtcNow.CompareTo(dateExpiry.AddMinutes(_appSettings.RefreshTime)) < 0 
                && jwt is not null)
            {
                throw new Exception("Token invalid");
            }

            await _invalidatedTokenRepository.CreateAsync(new InvalidatedToken
            {
                ExpiryTime = dateExpiry,
                Id = jwtId
            });
        }

        public async Task<AuthenticationResponse> RefreshToken(HttpContext httpContext)
        {
            // lấy ra jwtId và expiry time (timestamp)
            var jwtId = httpContext.User.FindFirst(JwtRegisteredClaimNames.Jti)!.Value;
            var expiryTimeSql = httpContext.User.FindFirst(JwtRegisteredClaimNames.Exp)!.Value;
            // kiểm tra expiry time và csdl
            var jwt = await _invalidatedTokenRepository.GetAsync(jwtId);
            var dateExpiry = (DateTimeOffset.FromUnixTimeSeconds(long.Parse(expiryTimeSql))
                .UtcDateTime);

            if (DateTime.UtcNow.CompareTo(dateExpiry.AddMinutes(_appSettings.RefreshTime)) < 0
                && jwt is not null)
            {
                throw new Exception("Token invalid to be refresh");
            }

            // tạo token mới
            var userId = httpContext.User.FindFirstValue("userId");
            var user = await _userRepository.GetUserAsync(u => u.UserId.ToString() == userId);
            var newToken = GenerateJwtToken(user);
            // lưu token cũ
            await _invalidatedTokenRepository.CreateAsync(new InvalidatedToken
            {
                ExpiryTime = dateExpiry,
                Id = jwtId
            });

            return new AuthenticationResponse
            {
                Token = newToken,
                isAuthenticated = true,
            };
        }

        private string GenerateJwtToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("userId", user.UserId.ToString()),
                new Claim("username", user.Username),
            };

            claims.AddRange(user.Roles.Select(r => new Claim(ClaimTypes.Role, r.RoleId)));
            // generate token that is valid for x time
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_appSettings.ExpiryTime),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                                                                SecurityAlgorithms.HmacSha512Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
