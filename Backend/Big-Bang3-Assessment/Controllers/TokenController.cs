using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Big_Bang3_Assessment.Data;
using Big_Bang3_Assessment.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Big_Bang3_Assessment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TourismDbContext _context;
        private const string UserRole = "Users";
        private const string AdminRole = "Admin";
        private const string UsersRole = "Agent";

        public TokenController(IConfiguration configuration, TourismDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        [HttpPost("Admin")]
        public async Task<IActionResult> PostAdmin(AdminRegister _adminData)
        {
            if (_adminData != null && _adminData.Admin_Name != null && _adminData.Admin_Password != null)
            {
                var admin = await GetAdmin(_adminData.Admin_Name, _adminData.Admin_Password);

                if (admin != null)
                {

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Admin_Id", admin.Admin_Id.ToString()),
                        new Claim("Admin_Name", admin.Admin_Name),
                        new Claim(ClaimTypes.Role, AdminRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<AdminRegister> GetAdmin(string Admin_Name, string Admin_Password)
        {
            return await _context.adminRegisters.FirstOrDefaultAsync(u => u.Admin_Name == Admin_Name && u.Admin_Password == Admin_Password);
        }

        [HttpPost("User")]
        public async Task<IActionResult> PostUser(User _userData)
        {
            if (_userData != null && _userData.User_Name != null && _userData.User_Password != null)
            {
                var user = await GetUser(_userData.User_Name, _userData.User_Password);

                if (user != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("User_Id", user. User_Id.ToString()),
                        new Claim("User_Name", user. User_Name),
                        new Claim("User_Password", user. User_Password),
                        new Claim(ClaimTypes.Role, UserRole)
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<User> GetUser(string User_Name, string User_Password)
        {
            return await _context.users.FirstOrDefaultAsync(u => u.User_Name == User_Name && u.User_Password == User_Password);
        }

        [HttpPost("Agent")]
        public async Task<IActionResult> PostAgent(AgentRegister _agentData)
        {
            if (_agentData != null && _agentData.Agent_Name != null && _agentData.Agent_Password != null)
            {
                var agent = await GetAgent(_agentData.Agent_Name, _agentData.Agent_Password);

                if (agent != null)
                {
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Agent_Id", agent.Agent_Id.ToString()),
                        new Claim("Agent_Name", agent.Agent_Name),
                        new Claim("Agent_Password", agent.Agent_Password),
                        new Claim(ClaimTypes.Role, UsersRole) // Assuming Agent role is used for users
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration["Jwt:Issuer"],
                        _configuration["Jwt:Audience"],
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(30),
                        signingCredentials: signIn);

                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                }
                else
                {
                    return BadRequest("Invalid credentials");
                }
            }
            else
            {
                return BadRequest();
            }
        }

        private async Task<AgentRegister> GetAgent(string Agent_Name, string Agent_Password)
        {
            return await _context.agentRegisters.FirstOrDefaultAsync(u => u.Agent_Name == Agent_Name && u.Agent_Password==Agent_Password);
        }
    }
}
