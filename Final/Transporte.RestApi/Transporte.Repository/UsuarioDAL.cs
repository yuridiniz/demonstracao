using Transporte.Repository.Base.Database.KeyGuid;
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
    public class UsuarioDAL : EntityDbAccess<Usuario>, IUsuarioDAL
    {
        public UsuarioDAL(TransporteContext dbContext) : base(dbContext)
        {
        }

        public async Task<Usuario> ObterPorUsername(string username)
        {
            return await Table<Usuario>().FirstAsync(p => p.Username == username);
        }
    }
}
