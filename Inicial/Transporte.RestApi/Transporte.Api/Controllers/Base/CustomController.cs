using Transporte.Api.Model;
using Transporte.Business.Result;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Controllers.Base
{
    public abstract class CustomController : ControllerBase
    {
        /// <summary>
        /// Valida as informações básicas enviadas pelo usuário
        /// </summary>
        /// <typeparam name="ValidatorType">Classe de validação</typeparam>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        protected BusinessResult Preparar<ValidatorType>(ModelRequest apiRequest)
            where ValidatorType : ICustomValidator, new()
        {
            var result = new BusinessResult();

            if (apiRequest != null)
            {
                var prepareResult = apiRequest.Preparar<ValidatorType>();

                if (prepareResult.Error != null)
                {
                    result.AddErrorDetail(prepareResult.Error.Details);
                    result.Validate(ErrorGroup.INVALID_INPUT);
                }

            }
            else
            {
                result.AddErrorDetail(new ErrorDetail("body", "empty_body", "Nenhuma informação foi enviada"));
                result.Validate(ErrorGroup.BAD_REQUEST);
            }

            return result;
        }

        /// <summary>
        /// Valida as informações básicas enviadas pelo usuário e realiza as devidas conversões
        /// </summary>
        /// <typeparam name="ValidatorType">Classe de validação</typeparam>
        /// <param name="apiRequest"></param>
        /// <returns></returns>
        protected BusinessResult<ResultT> Preparar<ValidatorType, ResultT>(ModelRequest apiRequest)
            where ValidatorType : ICustomValidator, new()
            where ResultT : class, new()
        {
            var result = new BusinessResult<ResultT>();

            if (apiRequest != null)
            {
                object processResult;
                var prepareResult = apiRequest.Preparar<ValidatorType>(out processResult);

                if (prepareResult.Error != null)
                {
                    result.AddErrorDetail(prepareResult.Error.Details);
                    result.Validate(ErrorGroup.INVALID_INPUT);
                }

                result.Result = processResult as ResultT;
            }
            else
            {
                result.AddErrorDetail(new ErrorDetail("body", "empty_body", "Nenhuma informação foi enviada"));
                result.Validate(ErrorGroup.BAD_REQUEST);
            }

            return result;
        }


        /// <summary>
        /// Gera resultado sem resposta
        /// </summary>
        /// <param name="businessResult"></param>
        /// <returns>ObjectResult</returns>
        protected IActionResult Result(BusinessResult businessResult)
        {
            if (businessResult.Error != null)
            {
                var objectResult = new ObjectResult(businessResult.Error);
                objectResult.StatusCode = businessResult.Error.CodeId;
                return objectResult;
            }

            return Ok();
        }

        /// <summary>
        /// Gera resultado com resposta baseado em um BusinessResult
        /// </summary>
        /// <param name="businessResult"></param>
        /// <returns>ObjectResult</returns>
        protected IActionResult Result<T>(BusinessResult<T> businessResult)
        {
            ObjectResult objectResult;

            if (businessResult.Error != null)
                objectResult = new ObjectResult(businessResult.Error);
            else
                objectResult = new ObjectResult(businessResult.Result);


            objectResult.StatusCode = businessResult.Error == null ? 200 : businessResult.Error.CodeId;
            return objectResult;
        }

    }
}
