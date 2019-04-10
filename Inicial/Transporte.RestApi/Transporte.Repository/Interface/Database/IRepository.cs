using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transporte.Model.AbstractEntity;
using Transporte.Repository.Interface.KeyGeneric;
using Microsoft.EntityFrameworkCore.Storage;

namespace Transporte.Repository.Interface.KeyGeneric
{
    public interface IRepository<Entity, TKey>
    {
        Task<List<Entity>> Listar();
        Task<Entity> Obter(TKey id);
        Task Salvar(Entity entidade);
        Task Cadastrar(Entity entidade);
        Task Atualizar(Entity entidade);
        Task Excluir(Entity entidade);
        Task Excluir(TKey id);
        Task<IDbContextTransaction> BeginTransaction();
    }
}

namespace Transporte.Repository.Interface.KeyInt
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public interface IRepository<Entity> : IRepository<Entity, int>
            where Entity : EntityModel<int>, new() { }
}

namespace Transporte.Repository.Interface.KeyShort
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public interface IRepository<Entity> : IRepository<Entity, short>
            where Entity : EntityModel<short>, new() { }
}

namespace Transporte.Repository.Interface.KeyGuid
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public interface IRepository<Entity> : IRepository<Entity, Guid>
            where Entity : EntityModel<Guid>, new() { }
}
