using System;
using System.Linq;
using System.Globalization;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Transporte.Business;
using Transporte.Api.Controllers.Base;
using Transporte.Api.Model.Comum;
using Transporte.Api.Model.Viagem;
using Transporte.Model;

namespace Transporte.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ViagensController : CustomController
    {
        private readonly ViagemBusiness viagemBusiness;

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] RequestListar param)
        {
            var request = Preparar<RequestListar.DefaultValidator>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Listar(param.Dia);
            return Result(resposta);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Obter([FromRoute] RequestGuidKey param)
        {
            var request = Preparar<RequestGuidKey.DefaultValidator>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Obter(param.Id);
            return Result(resposta);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir([FromRoute] RequestGuidKey param)
        {
            var request = Preparar<RequestGuidKey.DefaultValidator>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Excluir(param.Id);
            return Result(resposta);
        }

        [HttpPost("{id}/finalizar")]
        public async Task<IActionResult> Finalizar([FromRoute] RequestGuidKey param)
        {
            var request = Preparar<RequestGuidKey.DefaultValidator>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Finalizar(param.Id);
            return Result(resposta);
        }

        [HttpPost("{id}/estornar")]
        public async Task<IActionResult> Estornar([FromRoute] RequestGuidKey param)
        {
            var request = Preparar<RequestGuidKey.DefaultValidator>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Estornar(param.Id);
            return Result(resposta);
        }

        [HttpPost]
        public async Task<IActionResult> Cadastrar(RequestCadastrar param)
        {
            var request = Preparar<RequestCadastrar.DefaultValidator, Viagem>(param);
            if (!request.IsSuccess)
                return Result(request);

            var resposta = await viagemBusiness.Registrar(request.Result);
            return Result(resposta);
        }

        // ViagemBusiness e suas dependências serão injetadas automaticamente
        public ViagensController(ViagemBusiness viagemBusiness)
        {
            this.viagemBusiness = viagemBusiness;
        }
    }
}