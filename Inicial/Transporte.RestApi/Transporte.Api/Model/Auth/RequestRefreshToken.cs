using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Model;

namespace Transporte.Api.Model.Auth
{
    public class RequestRefreshToken : ModelRequest
    {
        public string Token { get; set; }

        public class RefreshTokenInputValidation : CustomValidator<RequestRefreshToken>
        {
            public RefreshTokenInputValidation()
            {
                RuleFor(reg => reg.Token).NotEmpty().WithErrorCode("token_empty").WithMessage("Informe um token");
            }
        }
    }
}
