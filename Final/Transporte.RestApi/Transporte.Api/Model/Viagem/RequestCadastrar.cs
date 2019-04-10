using FluentValidation;

namespace Transporte.Api.Model.Viagem
{
    public class RequestCadastrar : ModelRequest
    {
        public string Matricula { get; set; }

        public class DefaultValidator : CustomValidator<RequestCadastrar>
        {
            public DefaultValidator()
            {
                RuleFor(reg => reg.Matricula).NotEmpty()
                                       .WithErrorCode("matricula_obrigatoria")
                                       .WithMessage("Deve ser informado a matrícula do usuário");
            }

            public override object Converter(RequestCadastrar entidade)
            {
                // Converter o Modelo para nossa classe de negócio
                // Pode ser tanto usando uma ferramenta como AutoMapper
                // ou manualmente
                var viagem = new Transporte.Model.Viagem();
                viagem.Matricula = entidade.Matricula;

                return viagem;
            }
        }
    }
}