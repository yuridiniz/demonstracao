using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Model;

namespace Transporte.Api.Model.Uop
{
    public class RequestUop : ModelRequest
    {
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public class RequestCadastroUopValidator : CustomValidator<RequestUop>
        {
            public RequestCadastroUopValidator()
            {
                RuleFor(reg => reg.Nome).NotEmpty().WithErrorCode("name_empty").WithMessage("Nome é obrigatório");
                RuleFor(reg => reg.Nome).MinimumLength(3).WithErrorCode("name_short").WithMessage("Nome muito curto");
                RuleFor(reg => reg.Nome).MaximumLength(255).WithErrorCode("name_long").WithMessage("Nome muito longo");
            }
        }
    }
}
