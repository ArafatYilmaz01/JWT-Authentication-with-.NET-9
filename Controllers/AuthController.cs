using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JwtAuthDotNet9.Entities;
using JwtAuthDotNet9.Models;
using JwtAuthDotNet9.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JwtAuthDotNet9.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController(IAuthService authService) : ControllerBase
    {
        public static User user = new User();
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register([FromBody] UserDto request)
        {
            var user = await authService.RegisterAsync(request);
            if (user == null)
            {
                return BadRequest(new { Message = "User already exists" });
            }
            return Ok(new { Message = "User registered successfully", User = user });
        }

        [HttpPost("login")]
        public ActionResult<TokenResponseDto> Login([FromBody] UserDto request)
        {
            var result = authService.LoginAsync(request).Result;
            if (result == null)
            {
                return BadRequest(new { Message = "Invalid username or password" });
            }
            return Ok(new { Message = "Login successful", Token = result });
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<TokenResponseDto>> RefreshToken([FromBody] RefreshTokenRequestDto request)
        {
            var result = await authService.RefreshTokenAsync(request);
            if (result == null)
            {
                return BadRequest(new { Message = "Invalid or expired refresh token" });
            }
            return Ok(new { Message = "Token refreshed successfully", Token = result });
        }
        [Authorize]
        [HttpGet]
        public IActionResult AuthenticatedOnlyEndpoint()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(new { Message = "You must be logged in to access this endpoint" });
            }
            return Ok(new { Message = "You are authenticated", Username = User.Identity.Name });
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("admin-only")]
        public IActionResult AdminOnlyEndpoint()
        {
            if (User.Identity?.IsAuthenticated != true)
            {
                return Unauthorized(new { Message = "You must be logged in to access this endpoint" });
            }
            return Ok(new { Message = "You are admin!", Username = User.Identity.Name });
        }
    
    }
}