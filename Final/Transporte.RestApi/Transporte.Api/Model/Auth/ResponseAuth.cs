using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Transporte.Api.Model.Auth
{
    public class ResponseAuth
    {
        public string Token { get; internal set; }
        public DateTime Expires { get; internal set; }
        public string RefreshToken { get; internal set; }
    }
}
