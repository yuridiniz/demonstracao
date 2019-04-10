using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Transporte.Api.Controllers.Base;
using Transporte.Api.Model.Auth;
using Transporte.Business;
using Transporte.Business.Result;
using Transporte.Repository;
using Transporte.Repository.Context;
using Transporte.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using static Transporte.Api.Model.Auth.RequestAuth;
using static Transporte.Api.Model.Auth.RequestRefreshToken;

namespace Transporte.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class AuthController : CustomController
    {
        private readonly UsuarioBusiness usuarioBusiness;
        private readonly IOptions<AuthSettings> authSettings;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RequestAuth model)
        {
            var request = Preparar<AuthInputValidation>(model);
            if (!request.IsSuccess)
                return Result(request);

            var usuarioResult = await usuarioBusiness.Autenticar(model.Username, model.Password);

            if(!usuarioResult.IsSuccess)
                return Result(usuarioResult);

            var authResult = GerarToken(usuarioResult.Result);

            return Ok(authResult.Result);
        }

        [HttpGet("refreshToken")]
        public async Task<IActionResult> RefreshToken([FromQuery] RequestRefreshToken model)
        {
            var request = Preparar<RefreshTokenInputValidation>(model);
            if (!request.IsSuccess)
                return Result(request);

            var usuarioResult = await usuarioBusiness.Autenticar(model.Token);

            if (!usuarioResult.IsSuccess)
                return Result(usuarioResult);

            var authResult = GerarToken(usuarioResult.Result);

            return Ok(authResult.Result);
        }

        private BusinessResult<ResponseAuth> GerarToken(RefreshToken refreshToken)
        {
            var responseAuth = new ResponseAuth();

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(authSettings.Value.Key);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, refreshToken.Usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(authSettings.Value.Expires),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var jwtToken = tokenHandler.CreateToken(tokenDescriptor);
            responseAuth.Token = tokenHandler.WriteToken(jwtToken);
            responseAuth.Expires = tokenDescriptor.Expires.Value;
            responseAuth.RefreshToken = refreshToken.Token;

            var result = new BusinessResult<ResponseAuth>();
            result.Result = responseAuth;

            return result;
        }

        public AuthController(UsuarioBusiness usuarioBusiness, IOptions<AuthSettings> authSettings)
        {
            this.usuarioBusiness = usuarioBusiness;
            this.authSettings = authSettings;
        }

    }
}
