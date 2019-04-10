using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Model;

namespace Transporte.Api.Model.Auth
{
    public class RequestAuth : ModelRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public class AuthInputValidation : CustomValidator<RequestAuth>
        {
            public AuthInputValidation()
            {
                RuleFor(reg => reg.Username).NotEmpty().WithErrorCode("username_empty").WithMessage("Informe o usuário");
                RuleFor(reg => reg.Password).NotEmpty().WithErrorCode("password_empty").WithMessage("Informe a senha");
            }
        }
    }
}
