using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Controllers.Base;
using Transporte.Api.Model.Uop;
using Transporte.Business;
using Transporte.Business.Result;
using Transporte.Repository;
using Transporte.Repository.Context;
using Transporte.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static Transporte.Api.Model.Uop.RequestUop;

namespace Transporte.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UopController : CustomController
    {
        private readonly UopBusiness uopBusiness;

        [Authorize]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("teste");
        }

        [HttpPost]
        public IActionResult Salvar([FromBody]RequestUop uop)
        {
            var request = Preparar<RequestCadastroUopValidator>(uop);
            if (!request.IsSuccess)
                return Result(request);

            var result = uopBusiness.Adicionar(null);

            return Ok(result);
        }

        public UopController(TransporteContext dbContext)
        {
            var regionalDal = new RegionalDAL(dbContext);
            var uopDal = new UopDAL(dbContext);

            uopBusiness = new UopBusiness(regionalDal, uopDal);
        }
    }
}
