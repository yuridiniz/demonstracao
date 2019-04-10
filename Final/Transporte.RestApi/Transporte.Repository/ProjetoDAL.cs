using Transporte.Repository.Base.Database.KeyGuid;
using Transporte.Repository.Context;
using Transporte.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transporte.Repository
{
    public class ProjetoDAL : EntityDbAccess<Projeto>
    {
        public ProjetoDAL(TransporteContext dbContext) : base(dbContext)
        {
        }
    }
}
