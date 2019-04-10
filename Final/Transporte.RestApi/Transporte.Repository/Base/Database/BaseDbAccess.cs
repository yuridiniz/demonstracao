using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository.Base.Database
{
    /// <summary>
    /// Classe genérica, possui métodos default para cruds básicos
    /// devido a ser genérico, somente as classes filhas poderão utilizar dos métodos
    /// </summary>
    public abstract class BaseDbAccess
    {
        protected DbContext DbContext { get; private set; }

        public BaseDbAccess(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        protected IQueryable<T> Table<T>()
            where T : class, new()
        {
            return DbContext.Set<T>().AsNoTracking();
        }

        protected async Task<List<T>> Listar<T>()
            where T : class, new()
        {
            return await DbContext.Set<T>().AsNoTracking().ToListAsync();
        }

        protected async Task Cadastrar<T>(T entidade)
            where T : class, new()
        {
            DbContext.Entry<T>(entidade).State = EntityState.Added;
            await DbContext.SaveChangesAsync();
        }

        protected async Task Atualizar<T>(T entidade)
            where T : class, new()
        {
            DbContext.Entry<T>(entidade).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        protected async Task Excluir<T>(T entidade)
            where T : class, new()
        {
            DbContext.Remove(entidade);
            await DbContext.SaveChangesAsync();
        }
    }
}
