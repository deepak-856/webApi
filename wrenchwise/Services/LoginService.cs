using Dapper;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using wrenchwise.Interfaces;
using wrenchwise.Models;
using wrenchwise.Utility;

namespace wrenchwise.services
{

    public class LoginService : ILoginService
    {
        private readonly DBGateway _dbGateway;
        private readonly IConfiguration _configuration;

        public LoginService(DBGateway dbGateway, IConfiguration configuration)
        {
            _dbGateway = dbGateway;
            _configuration = configuration;
        }

        public async Task<LoginResponse> Authenticate(LoginRequest request)
        {
            var parameters = new DynamicParameters();
            parameters.Add("p_email", request.email);
            parameters.Add("p_password", request.password);
            parameters.Add("p_role_type", RoleTypeIntToEnum(request.role_type));

            try
            {
                var result = await _dbGateway.ExeSPScaler<dynamic>("sp_authenticate_user", parameters);

                if (result == null)
                {
                    return new LoginResponse
                    {
                        Success = false,
                        Message = "Invalid credentials"
                    };
                }

                var token = GenerateJwtToken(request.email, RoleTypeIntToEnum(request.role_type));

                return new LoginResponse
                {
                    Success = true,
                    Message = "Login successful",
                    Data = new
                    {
                        Token = token,
                        Role = RoleTypeIntToEnum(request.role_type)
                    }
                };
            }
            catch (Exception ex)
            {
                return new LoginResponse
                {
                    Success = false,
                    Message = $"Login error: {ex.Message}"
                };
            }
        }


        private string GenerateJwtToken(string email, string role)
        {
            var jwtSettings = _configuration.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.Name, email),
        new Claim(ClaimTypes.Role, role)
    };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpireMinutes"])),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }


        private string RoleTypeIntToEnum(int roleType)
        {
            return roleType switch
            {
                1 => "Admin",
                2 => "User",
                3 => "Technician",
                _ => throw new ArgumentException("Invalid role type")
            };
        }
    }
}
