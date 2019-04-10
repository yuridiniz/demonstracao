using Transporte.Business.Result;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Middleware
{
    public class ApiVersionError : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            var errorDetail = new BusinessValidation();
            errorDetail.AddErrorDetail(new ErrorDetail("", context.ErrorCode, context.Message));
            errorDetail.Validate(new ErrorGroup(context.StatusCode, context.ErrorCode, context.MessageDetail));

            var objResult = new ObjectResult(errorDetail.Error);
            objResult.StatusCode = context.StatusCode;

            return objResult;
        }
    }
}
