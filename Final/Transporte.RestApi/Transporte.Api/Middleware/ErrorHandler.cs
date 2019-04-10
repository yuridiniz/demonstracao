using Transporte.Api.Extension;
using Transporte.Business.Result;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Middleware
{
    public class ErrorHandler
    {
        private static readonly ErrorGroup ERROR_401 = new ErrorGroup(401, "unauthorized", "Você não está autenticado.");
        private static readonly ErrorGroup ERROR_404 = new ErrorGroup(404, "url_not_found", "A URL não foi encontrada.");
        private static readonly ErrorGroup ERROR_500 = new ErrorGroup(500, "internal_error", "Ocorreu um erro nao esperado.");
        private static readonly ErrorGroup ERROR_503 = new ErrorGroup(503, "service_unavailable", "O serviço está sobrecarregado, tente novamente mais tarde.");

        private static readonly ErrorDetail UNAUTHORIZED = new ErrorDetail("token", "unauthorized", "Realize login ou renove seu token de acesso");
        private static readonly ErrorDetail URL_NOT_FOUND = new ErrorDetail("url", "url_not_found", "A URL não foi encontrada: '{0}'");
        private static readonly ErrorDetail INTERNAL_ERROR = new ErrorDetail("", "internal_error", "Identificação do erro: '{0}'");
        private static readonly ErrorDetail SERVICE_UNAVAILABLE = new ErrorDetail("", "service_unavailable", "O serviço está sobrecarregado, tente novamente mais tarde.");

        private readonly RequestDelegate _next;

        public ErrorHandler(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);

                if (context.Response.StatusCode >= 400)
                {
                    await WriteErrorException(context);
                }
            } catch (Exception ex)
            {
                await WriteErrorException(context, ex);
            }
        }

        private async Task WriteErrorException(HttpContext context, Exception ex = null)
        {
            BusinessValidation errorDetail = null;

            if(ex != null)
                errorDetail = Write500(context, ex);
            else if (context.Response.StatusCode <= 401)
                errorDetail = Write401(context);
            else if (context.Response.StatusCode == 404)
                errorDetail = Write404(context);
            else if (context.Response.StatusCode == 503)
                errorDetail = Write503(context, ex);
            else if (context.Response.StatusCode >= 500)
                errorDetail = Write500(context, ex);

            if (!context.Response.HasStarted)
            {
                if(errorDetail != null)
                {
                    var objResult = new ObjectResult(errorDetail.Error);
                    objResult.StatusCode = context.Response.StatusCode;
                    await context.WriteResultAsync(objResult);
                }
            }
        }

        private BusinessValidation Write401(HttpContext context)
        {
            var errorDetail = new BusinessValidation();

            errorDetail.AddErrorDetail(UNAUTHORIZED);
            errorDetail.Validate(ERROR_401);

            return errorDetail;
        }

        private BusinessValidation Write404(HttpContext context)
        {
            var errorDetail = new BusinessValidation();

            errorDetail.AddErrorDetail(URL_NOT_FOUND.Format(context.Request.Path + context.Request.QueryString.Value));
            errorDetail.Validate(ERROR_404);

            return errorDetail;
        }

        private BusinessValidation Write500(HttpContext context, Exception ex)
        {
            var errorDetail = new BusinessValidation();

            errorDetail.AddErrorDetail(INTERNAL_ERROR.Format(Guid.NewGuid().ToString("N")));
            errorDetail.Validate(ERROR_500);

            return errorDetail;
        }

        private BusinessValidation Write503(HttpContext context, Exception ex)
        {
            var errorDetail = new BusinessValidation();

            errorDetail.AddErrorDetail(SERVICE_UNAVAILABLE);
            errorDetail.Validate(ERROR_503);

            return errorDetail;
        }
    }
}
