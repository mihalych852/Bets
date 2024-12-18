﻿using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using UserServer.Core.DTO;
using UserServer.Core.Interfaces;
using UserServer.Core.Exceptions;
using UserServer.WebHost.Classes;

namespace UserServer.WebHost.Controllers.V1
{
    [ApiVersion("1")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : BaseContoller
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthController(IAuthenticationService authenticationService, ILogger<BaseContoller> logger) : base(logger)
        {
            _authenticationService = authenticationService;
        }

        /// <summary>
        /// Метод для входа
        /// </summary>
        /// <param name="loginDto"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResult>> Login(LoginDto loginDto)
        {
            try
            {
                var result = await _authenticationService.LoginAsync(loginDto);

                LogInformationByUser("Авторизовался", loginDto.Email);

                return Ok(result);
            }
            catch (InvalidCredentialsException ice)
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }
            catch(Exception ex)
            {
                return IternalServerError(ex, new { Message = "Unforeseen error" });
            }
        }

        /// <summary>
        /// Метод для выхода
        /// </summary>
        /// <returns></returns>
        [HttpPost("logout")]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Получаем Id текущего пользователя
            LogInformationByUser("Разлогинился");
            await _authenticationService.LogoutAsync(userId);

            return NoContent();
        }

        /// <summary>
        /// Метод для обновления токена
        /// </summary>
        /// <param name="refreshToken"></param>
        /// <returns></returns>
        [HttpPost("refreshToken")]
        public async Task<ActionResult<AuthenticationResult>> RefreshToken(string refreshToken)
        {
            try
            {
                var result = await _authenticationService.RefreshTokenAsync(refreshToken);
                LogInformationByUser("Обновил токен");
                return Ok(result);
            }
            catch (InvalidOperationException)
            {
                return Unauthorized(new { Message = "Invalid refresh token." });
            }
        }

        [HttpGet("validateToken")]
        [Authorize]
        public IActionResult ValidateToken()
        {
            var roles = User.Claims
                    .Where(c => c.Type == ClaimTypes.Role)
                    .Select(c => c.Value)
                    .ToList();

            //var claims = User.Claims.ToList();
            if (!roles.Any()) 
            {
                return Unauthorized();
            }

            return Ok(roles);
        }

        [HttpGet("GetUserInfo")]
        [Authorize]
        public IActionResult GetUserInfo()
        {
            return Ok(new UserClaimsDto
            {
                Email = User.FindFirstValue(ClaimTypes.Email),
                Id = User.FindFirstValue(ClaimTypes.NameIdentifier),
                UserName = User.FindFirstValue(ClaimTypes.Name),
                Rolles = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).AsEnumerable()
            });
        }
    }
}
