using JWT.Authentication.Core.Entities;
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
        private readonly IUsersRepository _usersRepository;
        private readonly IConfiguration _config;
        private readonly ICustomerRepository _customerRepository;

        public AuthController(IUsersRepository usersRepository,IConfiguration config, ICustomerRepository customerRepository)
        {
            _usersRepository = usersRepository;
            _config = config;
            _customerRepository = customerRepository;
        }

        [Route("register")]
        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserVm userVm)
        {
            var customerDetails = await _customerRepository.GetCustomerByIdAsync(userVm.CustomerId);
            if (customerDetails != null && string.Equals(customerDetails.Name, userVm.FullName, StringComparison.OrdinalIgnoreCase))
            {
                var passwordSalt = GenerateSalt();
                userVm.Password += passwordSalt;
                var passwordHash = GenerateHashPassword(userVm.Password);
                Users user = new()
                {
                    EmailId = userVm.Email,
                    Password = passwordHash,
                    CustomerId = userVm.CustomerId,
                    SaltedPassword = passwordSalt,
                    CreatedDate = DateTime.UtcNow,
                    IsActive = true,
                    role = userVm.role
                };
                await _usersRepository.AddUserAsync(user);
                return Ok("registration successful");
            }
            return BadRequest("Not an authorized user to register");
        }

        [Route("login")]
        [HttpPost]
        public async Task<ActionResult> Login([FromBody] LoginVm userVm)
        {
            var user = await _usersRepository.GetUserByEmailAsync(userVm.Email);
            if (user is null)
                return BadRequest("Invalid Email address or Password");
            userVm.Password += user.SaltedPassword;
            var passwordHash = GenerateHashPassword(userVm.Password);
            if (passwordHash == user.Password)
            {
                var customerId = await _usersRepository.GetCustomerIdByUserId(user.UserId);
                var token = GenerateToken(user, user.role, customerId);
                return Ok(token);
            }
            return BadRequest("Invalid Credentials!");
        }

        [HttpDelete("delete/{userId}")]
        public async Task<ActionResult> RemoveUser(int userId)
        {
            var userDetails = await _usersRepository.GetUserByIdAsync(userId);
            if (userDetails != null)
            {
                var deletedUser = await _usersRepository.DeleteUserAsync(userDetails);
                if (deletedUser)
                    return Ok();
            }
            return NotFound();
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

        private string GenerateToken(Users user, string role,int customerId)
        {
            var claims = new[]
            {
                new Claim("UserId", user.UserId.ToString()),
                new Claim("CustomerId", customerId.ToString()),
                new Claim("Email", user.EmailId),
                new Claim("Role",user.role),
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