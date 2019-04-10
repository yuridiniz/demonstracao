using Transporte.Repository.Interface.KeyInt;
using Transporte.Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository.Interface
{
    public interface IUopDAL : IRepository<Uop>
    {
        Task<Uop> ObterPorNome(string nomeUop);
    }
}
