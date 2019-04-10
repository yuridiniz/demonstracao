using Transporte.Repository.Base.Http;
using Transporte.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository
{
    public class FeedDAL : BaseHttpAccess, IFeedDAL
    {
        public async Task Listar(string token)
        {
            var header = new Dictionary<string, string>();
            header.Add("Authorize", "Bearer " + token);

            var result = await Get<object>("/v3.1/me/feed", header);
        }

        public async Task Salvar(string token)
        {
            var header = new Dictionary<string, string>();
            header.Add("Authorize", "Bearer " + token);

            var result = await Post<object>("/v3.1/me/feed", "teste", header);
        }

        public FeedDAL(HttpClient http)
            : base (http)
        {
        }
    }
}
