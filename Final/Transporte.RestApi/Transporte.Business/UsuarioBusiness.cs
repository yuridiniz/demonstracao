using Transporte.Business.Result;
using Transporte.Repository.Interface;
using Transporte.Model;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Business
{
    public class UsuarioBusiness
    {
        public static readonly ErrorDetail USUARIO_OU_SENHA_INVALIDO = new ErrorDetail("username", "invalid_username_or_password", "Usuário ou senha inválido");
        public static readonly ErrorDetail TOKEN_INEXISTENTE = new ErrorDetail("refreshToken", "invalid_refresh_token", "Refresh token inexistente");
        public static readonly ErrorDetail TOKEN_EXPIRADO = new ErrorDetail("refreshToken", "invalid_refresh_token", "Refresh token expirado");

        private readonly IUsuarioDAL usuarioDal;
        private readonly IRefreshTokenDAL refreshTokenDal;

        public async Task<BusinessResult<RefreshToken>> Autenticar(string username, string password)
        {
            var resultado = new BusinessResult<RefreshToken>();

            var usuario = await usuarioDal.ObterPorUsername(username);

            if(usuario == null)
                resultado.AddErrorDetail(USUARIO_OU_SENHA_INVALIDO);

            if (!resultado.Validate(ErrorGroup.INVALID_INPUT))
                return resultado;

            if(usuario.Password != password)
                resultado.AddErrorDetail(USUARIO_OU_SENHA_INVALIDO);

            if (!resultado.Validate(ErrorGroup.INVALID_INPUT))
                return resultado;

            var refreshToken = new RefreshToken();
            refreshToken.DataExpiracao = DateTime.UtcNow.AddDays(30);
            refreshToken.IdUsuario = usuario.Id;
            refreshToken.Usuario = usuario;
            refreshToken.Token = Guid.NewGuid().ToString("N");

            await refreshTokenDal.Cadastrar(refreshToken);

            

            resultado.Result = refreshToken;

            return resultado;
        }

        public async Task<BusinessResult<RefreshToken>> Autenticar(string refreshToken)
        {
            var resultado = new BusinessResult<RefreshToken>();

            var token = await refreshTokenDal.ObterPorToken(refreshToken);

            if (token == null)
                resultado.AddErrorDetail(TOKEN_INEXISTENTE);

            if (!resultado.Validate(ErrorGroup.INVALID_INPUT))
                return resultado;

            if(token.DataExpiracao < DateTime.UtcNow)
                resultado.AddErrorDetail(TOKEN_EXPIRADO);

            if (!resultado.Validate(ErrorGroup.INVALID_INPUT))
                return resultado;

            token.DataExpiracao = DateTime.UtcNow.AddDays(30);
            token.Token = Guid.NewGuid().ToString("N");

            await refreshTokenDal.Atualizar(token);

            resultado.Result = token;

            return resultado;
        }

        public UsuarioBusiness(IUsuarioDAL usuarioDal, IRefreshTokenDAL refreshTokenDal)
        {
            this.usuarioDal = usuarioDal;
            this.refreshTokenDal = refreshTokenDal;
        }

        
    }
}
