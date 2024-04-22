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
        private readonly IConfiguration _configuration;
        private readonly UserManager<BakeryUser> _userManager;

        private readonly ILogger<AccountController> _logger;
        //private readonly SignInManager<BakeryUser> _signInManager;
        public AccountController(
            MyDbContext context,
            IConfiguration configuration,
            UserManager<BakeryUser> userManager,
            ILogger<AccountController> logger//,
            //SignInManager<BakeryUser> signInManager
            )
        {
            _context = context;
            _logger = logger;
            _configuration = configuration;
            _userManager = userManager;
            //_signInManager = signInManager;
        }

        [Authorize]
        [HttpPost("SeedUsers")]
        public async Task<ActionResult> SeedUsers()
        {
            var timestamp = new DateTimeOffset(DateTime.Now);
            var loginfo = new { Operation = "Post users", Timestamp = timestamp };
        
            _logger.LogInformation("Post called {@loginfo} ", loginfo);
            
            var admin = new BakeryUser()
            {
                UserName = "Admin",
                FullName = "Admin",
                Email = "admin@hotmail.com"
            };
                
            var userAdmin = await _userManager.FindByEmailAsync("admin@hotmail.com");
            
            if (userAdmin == null)
            {
                var result = _userManager.CreateAsync(admin, "AdminPassword123!").Result;
                if (result.Succeeded)
                {
                    userAdmin = _userManager.FindByEmailAsync("admin@hotmail.com").Result;

                    if (userAdmin != null)
                    {
                        _userManager.AddToRoleAsync(userAdmin, UserRoles.Administrator).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Failed to create admin user.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to create admin user.");
                }
            }
            
         
            var manager = new BakeryUser()
            {
                UserName = "Manager",
                FullName = "Manager",
                Email = "manager@hotmail.com"
            };
                
            var userManager = _userManager.FindByEmailAsync("manager@hotmail.com").Result;
            
            if (userManager == null)
            {
                var result = _userManager.CreateAsync(manager, "ManagerPassword123!").Result;
                if (result.Succeeded)
                {
                    userManager = _userManager.FindByEmailAsync("manager@hotmail.com").Result;

                    if (userManager != null)
                    {
                        _userManager.AddToRoleAsync(userManager, UserRoles.Manager).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Failed to create manager user.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to create manager user.");
                }
            }
            
            var driver = new BakeryUser()
            {
                UserName = "Driver",
                FullName = "Driver",
                Email = "driver@hotmail.com"
            };
                
            var userDriver = _userManager.FindByEmailAsync("driver@hotmail.com").Result;
            
            if (userDriver == null)
            {
                var result = _userManager.CreateAsync(driver, "DriverPassword123!").Result;
                if (result.Succeeded)
                {
                    userDriver = _userManager.FindByEmailAsync("driver@hotmail.com").Result;

                    if (userDriver != null)
                    {
                        _userManager.AddToRoleAsync(userDriver, UserRoles.Driver).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Failed to create driver user.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to create driver user.");
                }
            }
            
            var baker = new BakeryUser()
            {
                UserName = "Baker",
                FullName = "Baker",
                Email = "baker@hotmail.com"
            };
                
            var userBaker = _userManager.FindByEmailAsync("baker@hotmail.com").Result;
            
            if (userBaker == null)
            {
                var result = _userManager.CreateAsync(baker, "BakerPassword123!").Result;
                if (result.Succeeded)
                {
                    userBaker = _userManager.FindByEmailAsync("Baker@hotmail.com").Result;

                    if (userBaker != null)
                    {
                        _userManager.AddToRoleAsync(userBaker, UserRoles.Baker).Wait();
                    }
                    else
                    {
                        Console.WriteLine("Failed to create baker user.");
                    }
                }
                else
                {
                    Console.WriteLine("Failed to create baker user.");
                }
            }
            
            return Ok("Users created successfully.");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult> Login([FromQuery] string username, [FromQuery] string password)
        {
            var timestamp = new DateTimeOffset(DateTime.Now);
            var loginfo = new { Operation = "Post Login", Timestamp = timestamp };
        
            _logger.LogInformation("Post called {@loginfo} ", loginfo);
            
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByNameAsync(username);
                    if (user == null || !await _userManager.CheckPasswordAsync(user, password))
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