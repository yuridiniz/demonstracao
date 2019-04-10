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
    public class RefreshTokenDAL : EntityDbAccess<RefreshToken>, IRefreshTokenDAL
    {
        public RefreshTokenDAL(TransporteContext dbContext) : base(dbContext)
        {
        }

        public async Task<RefreshToken> ObterPorToken(string token)
        {
            return await Table<RefreshToken>().Include(p => p.Usuario).FirstOrDefaultAsync(p => p.Token == token);
        }
    }
}
