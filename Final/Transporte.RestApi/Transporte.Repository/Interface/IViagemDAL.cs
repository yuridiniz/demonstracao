using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transporte.Model;
using Transporte.Repository.Interface.KeyGuid;

namespace Transporte.Repository.Interface
{
    public interface IViagemDAL : IRepository<Viagem>
    {
        Task<List<Viagem>> ListarPorData(DateTime dia);
    }
}