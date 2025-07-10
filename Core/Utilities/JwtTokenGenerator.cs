using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Rota.Entities.DTOs;

namespace Rota.Core.Utilities
{
	public class JwtTokenGenerator
	{
		private readonly IConfiguration _config;
		public JwtTokenGenerator(IConfiguration config)
		{
			_config = config;
		}


		public string GeneratorToken(JwtDto jwtDto)
		{

			//claim : kullanıcı bilgileri
			//nameidentifier : kullanıcının benzerisiz id si
			var claims = new[]
			{
				new Claim(ClaimTypes.NameIdentifier, jwtDto.Id.ToString()),
				new Claim(ClaimTypes. Email, jwtDto.Email),
				new Claim(ClaimTypes.Role, jwtDto.Role)
			};

			//token için bir anahtar hazırlıyoruz
			//HmacSha256 : tokenı imzalamak için seçtiğimiz güvenli bir algoritma. Sunucunun token ın orjinal olup olmadığını kontrol etmesini sağlar
			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


			var token = new JwtSecurityToken(
				issuer: _config["Jwt:Issuer"],		//token ı oluşturan sistemin adı(rotaapi)
				audience: _config["Jwt:Audience"],		//token ı kullanabilecek sistem (genelde frontend)
				claims: claims,			//token içine gömülen kullanıcı bilgileri
				expires: DateTime.UtcNow.AddHours(2), //token geçerlilik süresi
				signingCredentials: creds);		//token imzalanma şekli

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

