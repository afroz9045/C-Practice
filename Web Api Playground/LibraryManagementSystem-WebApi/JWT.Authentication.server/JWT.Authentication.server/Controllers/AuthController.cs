using JWT.Authentication.Core.Entities;
using JWT.Authentication.server.Infrastructure.Vm;
using JWT.Authentication.Server.Core.Contract.Repositories;
using JWT.Authentication.Server.Infrastructure.Extensions;
using JWT.Authentication.Server.Infrastructure.VM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace JWT.Authentication.Server.Controllers
{
    [Route("/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IDesignationRepository _designationRepository;
        private readonly IStaffRepository _staffRepository;
        private readonly IConfiguration _config;

        public AuthController(IUserRepository userRepository, IDesignationRepository designationRepository, IStaffRepository staffRepository, IConfiguration config)
        {
            _userRepository = userRepository;
            _designationRepository = designationRepository;
            _staffRepository = staffRepository;
            _config = config;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] RegistrationVm staffWithCredentials)
        {
            var staffDetails = await _staffRepository.GetStaffByStaffId(staffWithCredentials.StaffId);
            if (staffDetails != null && string.Equals(staffDetails.StaffName, staffWithCredentials.FullName, StringComparison.OrdinalIgnoreCase))
            {
                var passwordSalt = GenerateSalt();
                staffWithCredentials.Password += passwordSalt;
                var passwordHash = GenerateHashPassword(staffWithCredentials.Password);
                UserDetail user = new()
                {
                    FullName = staffWithCredentials.FullName,
                    Email = staffWithCredentials.Email,
                    Password = passwordHash,
                    StaffId = staffWithCredentials.StaffId,
                    SaltedPassword = passwordSalt
                };
                await _userRepository.RegisterUser(user);
                return Ok("registration successful");
            }
            return BadRequest("Not an authorized user to register");
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginVm userVm)
        {
            var user = await _userRepository.GetUserDetails(userVm.Email);
            var userRole = await _designationRepository.GetUserDesignation(user.StaffId);
            if (user is null)
                return BadRequest("Invalid Email address or Password");
            userVm.Password += user.SaltedPassword;
            var passwordHash = GenerateHashPassword(userVm.Password);
            if (passwordHash == user.Password)
            {
                var token = GenerateToken(user, userRole);
                return Ok(token);
            }
            return BadRequest("Invalid Email address or Password");
        }

        private string GenerateHashPassword(string password)
        {
            string machineKey = _config["MachineKey"].ToString();
            var hmac = new HMACSHA1()
            {
                Key = machineKey.HexToByte()
            };
            return Convert.ToBase64String(hmac.ComputeHash(password.GetByteArray()));
        }

        private static string GenerateSalt()
        {
            int saltLength = 8;
            byte[] salt = new byte[saltLength];
            using (var random = new RNGCryptoServiceProvider())
            {
                random.GetNonZeroBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        private string GenerateToken(UserDetail user, string role)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("FullName", user.FullName),
                new Claim("Email", user.Email),
                new Claim("Role",role),
                new Claim(ClaimTypes.Role,role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var token = new JwtSecurityToken(
                _config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(2),
                signingCredentials: signIn);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}