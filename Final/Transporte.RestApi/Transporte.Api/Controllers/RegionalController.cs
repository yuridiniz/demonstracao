using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transporte.Api.Controllers.Base;
using Microsoft.AspNetCore.Mvc;

namespace Transporte.Api.Controllers
{
    [ApiVersion("1.0")]
    [ApiVersion("2.0")]
    [Route("api/[controller]")]
    [Route("api/v{version:apiVersion}/values")]
    [ApiController]
    public class RegionalController : CustomController
    {
      
    }
}
