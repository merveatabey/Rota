using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Rota.Core.Interfaces;
using Rota.DataAccess.Repositories;
using Rota.Entities.DTOs;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rota.Api.Areas.User.Controllers
{
    [Area("User")]
    [Route("api/user/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
             
                var token = await _authService.RegisterAsync(dto);
                return Ok(new { token });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            try
            {
                var token = await _authService.LoginAsync(dto);
                return Ok(new { token });
            }catch(Exception ex)
            {
                return Unauthorized(new { message = ex.Message });
            }
        }


        //Kullanıcıya şifre sıfırlama linki veya geçici token içeren mail gönderir.
        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto dto)
        {
            try
            {
                await _authService.ForgotPasswordAsync(dto);
                return Ok(new { message = "If the email is correct, a reset link has been sent." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        //kullanıcı yeni şifre oluşturur
        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto dto)
        {
            try
            {
                await _authService.ResetPasswordAsync(dto);
                return Ok(new { message = "Password has been succesfully reset." });

            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

    }
}

