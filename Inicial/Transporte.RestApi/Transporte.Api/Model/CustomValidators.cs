using FluentValidation;
using FluentValidation.Results;
using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Model
{
   public abstract class CustomValidator<T> : AbstractValidator<T>, ICustomValidator<T>
    {
        public virtual object Converter(T entidade) { return null; }

        public object Converter(object entidade)
        {
            return Converter((T)entidade);
        }
    }

    public interface ICustomValidator<T> : IValidator<T>, ICustomValidator {
        object Converter(T entidade);
    }

    public interface ICustomValidator : IValidator  {
        object Converter(object entidade);
    }


    public static class ExtendedValidators
    {
        public static IRuleBuilderOptions<T, string> TryParseGuid<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, valor, context) =>
            {
                Guid _guidResult;
                var success = Guid.TryParseExact(valor, "D", out _guidResult);

                if (!success)
                {
                    context.MessageFormatter.AppendArgument("format", "00000000-0000-0000-0000-000000000000");
                }

                return success;
            });
        }

        public static IRuleBuilderOptions<T, string> TryParseDateTime<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must((rootObject, valor, context) =>
            {
                DateTime _diaParse;
                var success = DateTime.TryParseExact(valor, "yyyy-MM-dd", CultureInfo.CurrentCulture, DateTimeStyles.None, out _diaParse);

                if (!success)
                {
                    context.MessageFormatter.AppendArgument("format", "yyyy-MM-dd");
                }

                return success;
            });
        }
    }

}