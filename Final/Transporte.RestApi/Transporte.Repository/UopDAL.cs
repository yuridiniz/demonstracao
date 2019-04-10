using Transporte.Repository.Base.Database.KeyInt;
using Transporte.Repository.Context;
using Transporte.Repository.Interface;
using Transporte.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository
{
    public class UopDAL : EntityDbAccess<Uop>, IUopDAL
    {
        public UopDAL(TransporteContext dbContext) : base(dbContext)
        {
        }

        public async Task<Uop> ObterPorNome(string nomeUop)
        {
            throw new NotImplementedException();
        }
    }
}
