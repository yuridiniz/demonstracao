using System;
using Transporte.Model;
using Transporte.Repository.Context;
using Transporte.Repository.Interface;
using Transporte.Repository.Base.Database.KeyGuid;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Transporte.Repository
{
    public class ViagemDAL : EntityDbAccess<Viagem>, IViagemDAL
    {

        public async Task<List<Viagem>> ListarPorData(DateTime dia)
        {
            var table = Table<Viagem>();
            return await table.Where(p => p.DataRegistro.Date == dia).ToListAsync();
        }

        public ViagemDAL(TransporteContext dbContext) : base(dbContext)
        {
        }
    }
}