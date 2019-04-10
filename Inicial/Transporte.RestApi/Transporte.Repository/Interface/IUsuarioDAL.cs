using Transporte.Repository.Interface.KeyGuid;
using Transporte.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository.Interface
{
    public interface IUsuarioDAL : IRepository<Usuario>
    {
        Task<Usuario> ObterPorUsername(string username);
    }
}
