using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Bakery.Data;
using Bakery.DTO;
using Bakery.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace Bakery.Controller;

    [Authorize]
    [Route("[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly MyDbContext _context;
        //private readonly ILogger<DomainsController> _logger;
        private readonly IConfiguration _configuration;
        private readonly UserManager<BakeryUser> _userManager;
        private readonly SignInManager<BakeryUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(
            MyDbContext context,
            //ILogger<DomainsController> logger,
            IConfiguration configuration,
            UserManager<BakeryUser> userManager,
            SignInManager<BakeryUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            //_logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        
        [HttpPost("createAdmin")]
        public async Task<ActionResult> createAdmin([FromBody] RegisterDTO model)
        {
            var user = new BakeryUser
            {
                UserName = model.FullName,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Failed to create admin role");
                    }
                }

                await _userManager.AddToRoleAsync(user, "Admin");
                
                // var timestamp = new DateTimeOffset(DateTime.UtcNow);
                // var loginfo = new Loginfo
                // {
                //     specificUser = User.Identity?.Name,
                //     Operation = "Post CreateAdmin",
                //     Timestamp = timestamp.DateTime
                // };
                // _logger.LogInformation("Get called {@LogInfo} ", loginfo);
                
                return Ok("Admin user created successfully.");
            }
            return BadRequest("Failed to create admin user.");
        }
        
        [HttpPost("createManager")]
        public async Task<ActionResult> createManager([FromBody] RegisterDTO model)
        {
            var user = new BakeryUser
            {
                UserName = model.FullName,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Manager"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Manager"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Failed to create Manager role");
                    }
                }

                await _userManager.AddToRoleAsync(user, "Manager");
                
                // var timestamp = new DateTimeOffset(DateTime.UtcNow);
                // var loginfo = new Loginfo
                // {
                //     specificUser = User.Identity?.Name,
                //     Operation = "Post CreateAdmin",
                //     Timestamp = timestamp.DateTime
                // };
                // _logger.LogInformation("Get called {@LogInfo} ", loginfo);
                
                return Ok("Manager user created successfully.");
            }
            return BadRequest("Failed to create Manager user.");
        }
        
        [HttpPost("createBaker")]
        public async Task<ActionResult> createBaker([FromBody] RegisterDTO model)
        {
            var user = new BakeryUser
            {
                UserName = model.FullName,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Baker"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Baker"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Failed to create Baker role");
                    }
                }

                await _userManager.AddToRoleAsync(user, "Baker");
                
                // var timestamp = new DateTimeOffset(DateTime.UtcNow);
                // var loginfo = new Loginfo
                // {
                //     specificUser = User.Identity?.Name,
                //     Operation = "Post CreateAdmin",
                //     Timestamp = timestamp.DateTime
                // };
                // _logger.LogInformation("Get called {@LogInfo} ", loginfo);
                
                return Ok("Baker user created successfully.");
            }
            return BadRequest("Failed to create Baker user.");
        }
        
        [HttpPost("createDriver")]
        public async Task<ActionResult> createDriver([FromBody] RegisterDTO model)
        {
            var user = new BakeryUser
            {
                UserName = model.FullName,
                Email = model.Email
            };

            var result = _userManager.CreateAsync(user, model.Password).Result;

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Driver"))
                {
                    var roleResult = await _roleManager.CreateAsync(new IdentityRole("Driver"));
                    if (!roleResult.Succeeded)
                    {
                        return BadRequest("Failed to create Driver role");
                    }
                }

                await _userManager.AddToRoleAsync(user, "Driver");
                
                // var timestamp = new DateTimeOffset(DateTime.UtcNow);
                // var loginfo = new Loginfo
                // {
                //     specificUser = User.Identity?.Name,
                //     Operation = "Post CreateAdmin",
                //     Timestamp = timestamp.DateTime
                // };
                // _logger.LogInformation("Get called {@LogInfo} ", loginfo);
                
                return Ok("Driver user created successfully.");
            }
            return BadRequest("Failed to create Driver user.");
        }

        [Authorize]
        [HttpPost("SeedUsers")]
        public async Task<ActionResult> SeedUsers()
        {
            var admin = new RegisterDTO()
            {
                FullName = "Admin",
                Email = "Admin@hotmail.com",
                Password = "AdminPassword123!"
            };
            var manager = new RegisterDTO()
            {
                FullName = "Manager",
                Email = "Manager@hotmail.com",
                Password = "ManagerPassword123!"
            };
            var driver = new RegisterDTO()
            {
                FullName = "Driver",
                Email = "Driver@hotmail.com",
                Password = "DriverPassword123!"
            };
            var baker = new RegisterDTO()
            {
                FullName = "Baker",
                Email = "Baker@hotmail.com",
                Password = "BakerPassword123!"
            };
            if (await createAdmin(admin) != BadRequest("Failed to create admin user") &&
                await createManager(manager) != BadRequest("Failed to create manager user") &&
                await createBaker(baker) != BadRequest("Failed to create baker user") &&
                await createDriver(driver) != BadRequest("Failed to create driver user"))
            {
                return Ok(("Users created succesfully"));
            }

            return BadRequest("Failed to create user");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login(LoginDTO input)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(input.UserName);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, input.Password))
                        throw new Exception("Invalid login attempt.");
                    else
                    {
                        var signingCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(
                                System.Text.Encoding.UTF8.GetBytes(_configuration["JWT:SigningKey"])),
                                SecurityAlgorithms.HmacSha256);
                        var claims = new List<Claim>();
                        claims.Add(new Claim(ClaimTypes.Name, user.UserName));

                        var userRoles = await _userManager.GetRolesAsync(user);
                        
                        claims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));
                        
                        var jwtObject = new JwtSecurityToken(
                                issuer: _configuration["JWT:Issuer"],
                                audience: _configuration["JWT:Audience"],
                                claims: claims,
                                expires: DateTime.Now.AddSeconds(300),
                                signingCredentials: signingCredentials);
                        var jwtString = new JwtSecurityTokenHandler()
                        .WriteToken(jwtObject);
                        return StatusCode(StatusCodes.Status200OK, jwtString);
                    }
                }
                else
                {
                    var details = new ValidationProblemDetails(ModelState);
                    details.Type =
                    "https:/ /tools.ietf.org/html/rfc7231#section-6.5.1";
                    details.Status = StatusCodes.Status400BadRequest;
                    return new BadRequestObjectResult(details);
                }
            }
            catch (Exception e)
            {
                var exceptionDetails = new ProblemDetails();
                exceptionDetails.Detail = e.Message;
                exceptionDetails.Status =
                StatusCodes.Status401Unauthorized;
                exceptionDetails.Type =
                "https:/ /tools.ietf.org/html/rfc7231#section-6.6.1";
                return StatusCode(
                    StatusCodes.Status401Unauthorized, exceptionDetails);
            }
        }
    }