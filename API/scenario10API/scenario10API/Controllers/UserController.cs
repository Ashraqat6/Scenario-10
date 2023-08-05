using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using scenario10API.models;
using scenario10API.models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace scenario10API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        private readonly MyDBContext _dbContext;

        public UserController(IConfiguration configuration, UserManager<User> userManager, MyDBContext dBContext)
        {
            _configuration = configuration;
            _userManager = userManager;
            _dbContext = dBContext;
        }

        #region Authentication With ASP Identity
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<TokenDTO>> Login(LoginDTO credentials)
        {
            var User = await _userManager.FindByEmailAsync(credentials.Email);

            if (User == null)
            {
                return BadRequest("User not found");
            }
            if (await _userManager.IsLockedOutAsync(User))
            {
                return BadRequest("Try again");
            }

            bool isAuthenticated = await _userManager.CheckPasswordAsync(User, credentials.Password);
            if (!isAuthenticated)
            {
                await _userManager.AccessFailedAsync(User);
                return Unauthorized("Wrong Credentials");
            }


            var userClaims = await _userManager.GetClaimsAsync(User);

            //Generate Key
            var secretKey = _configuration.GetValue<string>("SecretKey");

            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

            //Determine how to generate hashing result
            SigningCredentials methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var exp = DateTime.Now.AddDays(1);

            //Genete Token 
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                notBefore: DateTime.Now,
                expires: exp,
                signingCredentials: methodUsedInGeneratingToken);

            var tokenHandler = new JwtSecurityTokenHandler();
            string tokenString = tokenHandler.WriteToken(jwt);
            var claims = await _userManager.GetClaimsAsync(User);



            return new TokenDTO
            {
                Token = tokenString,
                ExpiryDate = exp,
                Id = User.Id,
            };
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult<string>> UserRegister(RegisterDTO registerDTO)
        {
            var newUser = new User
            {
                PhoneNumber = registerDTO.Mobile,
                Email = registerDTO.Email,
                PasswordHash = registerDTO.Password,
                UserName = registerDTO.Name,
            };

            var creationResult = await _userManager.CreateAsync(newUser, registerDTO.Password);

            if (!creationResult.Succeeded)
            {
                return BadRequest(creationResult.Errors);
            }

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, newUser.UserName),
                new Claim(ClaimTypes.Role, "NormalUser"),
            };


            await _userManager.AddClaimsAsync(newUser, userClaims);

            var message = new { success = true, message = "This User was created successfully." };
            return Ok(message);
        }
        #endregion

        [HttpGet("{id}")]
        public ActionResult<UserDetailsDTO> UserDetails(string id)
        {
            var user = _dbContext.Users
                .Include(u => u.Reports)
                .FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            var userDetailsDTO = new UserDetailsDTO
            {
                UserName = user.UserName,
                Email = user.Email,
                Phone = user.PhoneNumber,
                Reports = user.Reports.Select(report => new ReportDTO
                {
                    Id = report.Id,
                    Location = report.Location,
                    Img = report.Img,
                    Date = report.Date,
                    UserId = report.UserId,
                    SpeciesId = report.SpeciesId
                }).ToList()
            };

            return userDetailsDTO;
        }


    }
}
