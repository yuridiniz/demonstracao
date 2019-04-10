using FluentValidation;
using System;
using System.Globalization;

namespace Transporte.Api.Model.Viagem
{
    public class RequestListar : ModelRequest
    {
        public string dia { get; set; }

        // Essa propriedade deve ter seu método de set privado
        // Dessa forma, garante que nenhum valor seja inputado pela api
        // Também garante nenhum conflito de nomenclatura entre "Dia" e "dia" pelo HTTP
        public DateTime? Dia { get; private set; }

        public class DefaultValidator : CustomValidator<RequestListar>
        {
            public DefaultValidator()
            {
                RuleFor(reg => reg.dia).TryParseDateTime()
                                       .When(p => !String.IsNullOrEmpty(p.dia))
                                       .WithErrorCode("dia_formato_invalido")
                                       .WithMessage("O valor informado para o dia não é válido, informe um valor válido no formato {format}");
            }

            ///<summary>
            /// Método chamado quando a validação retorna sucesso
            ///</summary>
            public override object Converter(RequestListar entidade)
            {
                if (!String.IsNullOrEmpty(entidade.dia))
                    entidade.Dia = DateTime.ParseExact(entidade.dia, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None);

                return entidade;
            }
        }
    }
}