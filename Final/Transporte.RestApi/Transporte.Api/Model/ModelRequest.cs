using Transporte.Business.Result;
using FluentValidation;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Model
{
    public abstract class ModelRequest
    {
        public BusinessValidation Preparar<ValidatorType>()
            where ValidatorType : ICustomValidator, new()
        {
            var errors = new BusinessValidation();

            var validatorInstance = new ValidatorType();
            var result = validatorInstance.Validate(this);

            if (result.IsValid) {
                validatorInstance.Converter(this);
                return errors;
            }

            result.Errors.ToList().ForEach(item => errors.AddErrorDetail(new ErrorDetail(item.PropertyName, item.ErrorCode, item.ErrorMessage)));
            errors.Validate(ErrorGroup.INVALID_INPUT);

            return errors;
        }

        public BusinessValidation Preparar<ValidatorType>(out object processResult)
            where ValidatorType : ICustomValidator, new()
        {
            processResult = null;
            var errors = new BusinessValidation();

            var validatorInstance = new ValidatorType();
            var result = validatorInstance.Validate(this);

            if (result.IsValid)
            {
                processResult = validatorInstance.Converter(this);
                return errors;
            }

            result.Errors.ToList().ForEach(item => errors.AddErrorDetail(new ErrorDetail(item.PropertyName, item.ErrorCode, item.ErrorMessage)));
            errors.Validate(ErrorGroup.INVALID_INPUT);

            return errors;
        }
    }
}
