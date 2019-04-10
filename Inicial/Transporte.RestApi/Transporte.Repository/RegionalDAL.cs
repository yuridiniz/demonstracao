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
    public class RegionalDAL : EntityDbAccess<Regional>, IRegionalDAL
    {
        public RegionalDAL(TransporteContext dbContext) : base(dbContext)
        {
        }

    }
}
