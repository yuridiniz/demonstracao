using Transporte.Model.AbstractEntity;
using Transporte.Repository.Base.Database.KeyGeneric;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Transporte.Repository.Base.Database.KeyGeneric
{
    /// <summary>
    /// Classe de entidade específica, essa classe fornece uma interface pública no qual terá os cruds básicos
    /// para uma entidade, isso garante o princípio da responsábilidade única, as classes que a herdarem permitirão
    /// acesso somente aos métodos básicos, não permitindo que seja executado métodos genéricos através dela
    /// </summary>
    /// <typeparam name="Entity"></typeparam>
    public abstract class EntityDbAccess<Entity, TKey> : BaseDbAccess
        where Entity : EntityModel<TKey>, new()
    {
        public EntityDbAccess(DbContext dbContext) : base(dbContext)
        {
        }

        public virtual async Task<Entity> Obter(TKey id)
        {
            return await DbContext.Set<Entity>().FindAsync(id);
        }

        public virtual async Task<List<Entity>> Listar()
        {
            return await base.Listar<Entity>();
        }

        public virtual async Task Cadastrar(Entity entidade)
        {
            await base.Cadastrar(entidade);
        }

        public virtual async Task Atualizar(Entity entidade)
        {
            await base.Atualizar(entidade);
        }

        public virtual async Task Excluir(Entity entidade)
        {
            await base.Excluir(entidade);
        }

        public virtual async Task Excluir(TKey id)
        {
            var entidade = new Entity() { Id = id };
            await Excluir(entidade);
        }

        public virtual async Task Salvar(Entity entidade)
        {
            if (entidade.Id.Equals(default(TKey)))
            {
                entidade.Id = entidade.GenerateNewId();
                await base.Cadastrar(entidade);
            }
            else
            {
                await base.Atualizar(entidade);
            }
        }

        public async Task<IDbContextTransaction> BeginTransaction()
        {
            return await DbContext.Database.BeginTransactionAsync();
        }
    }
}

namespace Transporte.Repository.Base.Database.KeyInt
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class EntityDbAccess<Entity> : EntityDbAccess<Entity, int>
            where Entity : EntityModel<int>, new()
    {
        public EntityDbAccess(DbContext dbContext) : base(dbContext)
        {
        }
    }
}

namespace Transporte.Repository.Base.Database.KeyShort
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class EntityDbAccess<Entity> : EntityDbAccess<Entity, short>
            where Entity : EntityModel<short>, new()
    {
        public EntityDbAccess(DbContext dbContext) : base(dbContext)
        {
        }
    }
}
namespace Transporte.Repository.Base.Database.KeyGuid
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class EntityDbAccess<Entity> : EntityDbAccess<Entity, Guid>
            where Entity : EntityModel<Guid>, new()
    {
        public EntityDbAccess(DbContext dbContext) : base(dbContext)
        {
        }
    }
}