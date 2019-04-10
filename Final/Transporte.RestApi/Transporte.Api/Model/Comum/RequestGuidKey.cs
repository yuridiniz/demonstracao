using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Model;

namespace Transporte.Api.Model.Comum
{
    public class RequestGuidKey : ModelRequest
    {
        public string id { get; set; }

        // Essa propriedade deve ter seu método de set privado
        // Dessa forma, garante que nenhum valor seja inputado pela api
        // Também garante nenhum conflito de nomenclatura entre "id" e "Id" pelo HTTP
        public Guid Id { get; private set; }

        public class DefaultValidator : CustomValidator<RequestGuidKey>
        {
            public DefaultValidator()
            {
                 RuleFor(model => model.id).TryParseGuid()
                                           .WithErrorCode("id_formato_invalido")
                                           .WithMessage("O valor informado para o Id não é válido, informe uma Guid válida no formato {format}");
            }

            public override object Converter(RequestGuidKey entidade) {
                entidade.Id = Guid.ParseExact(entidade.id, "D");

                return entidade.Id;
            }
        }
    }
}
