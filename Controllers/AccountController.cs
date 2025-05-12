using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Sakan_project.DTO;
using Sakan_project.models;
using Sakan_project.Services;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Sakan_project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration config;
        private readonly IEmailService emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            IConfiguration config,
            IEmailService emailService)
        {
            this.userManager = userManager;
            this.config = config;
            this.emailService = emailService;
        }

        // POST: api/Account/Register
        [HttpPost("Register")]
        public async Task<IActionResult> Registeration(RegisterUserDto userDto)
        {
            if (userDto.Role != "Owners" && userDto.Role != "Students")
            {
                return BadRequest("Invalid role. Allowed roles: Owners, Students");
            }

            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = userDto.Name,
                    Email = userDto.Email
                };

                IdentityResult result = await userManager.CreateAsync(user, userDto.Password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, userDto.Role);
                    return Ok("Account added successfully.");
                }
                return BadRequest(result.Errors.FirstOrDefault());
            }
            return BadRequest(ModelState);
        }

        // POST: api/Account/Login
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = await userManager.FindByEmailAsync(userDto.Email);
                if (user != null)
                {
                    bool found = await userManager.CheckPasswordAsync(user, userDto.Password);
                    if (found)
                    {
                        // Build token claims
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.NameIdentifier, user.Id),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                        };

                        // Add user roles as claims
                        var roles = await userManager.GetRolesAsync(user);
                        foreach (var role in roles)
                        {
                            claims.Add(new Claim(ClaimTypes.Role, role));
                        }

                        SecurityKey securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:Secret"]));
                        SigningCredentials signinCred = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: config["JWT:ValidIssuer"],
                            audience: config["JWT:ValidAudiance"],
                            claims: claims,
                            expires: DateTime.Now.AddHours(1),
                            signingCredentials: signinCred
                        );

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = myToken.ValidTo
                        });
                    }
                }
                return Unauthorized();
            }
            return Unauthorized();
        }

        // POST: api/Account/ForgotPassword
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto forgotPasswordDto)
        {
            if (string.IsNullOrEmpty(forgotPasswordDto.Email))
            {
                return BadRequest("Email is required.");
            }

            var user = await userManager.FindByEmailAsync(forgotPasswordDto.Email);
            if (user == null)
            {
                // Return OK to prevent user enumeration
                return Ok("If an account with this email exists, a password reset link has been sent.");
            }

            // Generate the password reset token
            var token = await userManager.GeneratePasswordResetTokenAsync(user);
            // URL-encode the token when embedding in a URL
            var resetLink = $"http://localhost:3000/resetpassword?email={forgotPasswordDto.Email}&token={Uri.EscapeDataString(token)}";

            // Send the reset link via email
            await emailService.SendEmailAsync(
                forgotPasswordDto.Email,
                "Reset Your Password",
                $"Please reset your password by clicking <a href='{resetLink}'>here</a>");

            return Ok("If an account with this email exists, a password reset link has been sent.");
        }

        // POST: api/Account/ResetPassword
        [HttpPost("ResetPassword")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto resetPasswordDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (resetPasswordDto.NewPassword != resetPasswordDto.ConfirmPassword)
            {
                return BadRequest("New password and confirmation do not match.");
            }

            var user = await userManager.FindByEmailAsync(resetPasswordDto.Email);
            if (user == null)
            {
                return BadRequest("Invalid Request.");
            }

            var result = await userManager.ResetPasswordAsync(user, resetPasswordDto.Token, resetPasswordDto.NewPassword);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok("Password reset successful.");
        }
    }
}
