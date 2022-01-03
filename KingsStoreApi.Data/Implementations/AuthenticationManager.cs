using KingsStoreApi.Data.Interfaces;
using KingsStoreApi.Model.DataTransferObjects.UserServiceDTO;
using KingsStoreApi.Model.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KingsStoreApi.Data.Implementations
{
    public class AuthenticationManager : IAuthenticationManager
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        private User _user;
        public AuthenticationManager(UserManager<User> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<string> CreateToken()
        {
            var claims = await GetClaimsAsync();
            var signinCredential = GetSigningCredentials();

            var token = GenerateToken(signinCredential, claims);
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public async Task<bool> ValidateUser(ValidateUserDTO credential)
        {
            _user = credential.User;

            return _user != null && await _userManager.CheckPasswordAsync(_user, credential.Password);
        }

        private SigningCredentials GetSigningCredentials()
        {
            var secretKeyBytes = Encoding.UTF8.GetBytes(_configuration["SecretKey"]);

            var symmetricSecurityKey = new SymmetricSecurityKey(secretKeyBytes);
            return new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
        }

        private async Task<List<Claim>> GetClaimsAsync()
        {
            var roles = await _userManager.GetRolesAsync(_user);
            var claim = new List<Claim>
           {
               new Claim (ClaimTypes.Email, _user.Email ),
               new Claim (ClaimTypes.Name, _user.FullName ),
           };

            foreach (var role in roles)
            {
                claim.Add(new Claim(ClaimTypes.Role, role));
            }

            return claim;
        }

        private JwtSecurityToken GenerateToken(SigningCredentials credentials, List<Claim> claims)
        {
            var JwtSection = _configuration.GetSection("JwtSettings");

            return new JwtSecurityToken
                (
                    issuer: JwtSection.GetSection("ValidIssuer").Value,
                    audience: JwtSection.GetSection("ValidAudience").Value,
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(Convert.ToDouble(JwtSection.GetSection("Expires").Value)),
                    signingCredentials: credentials
                );
        }
    }
}
