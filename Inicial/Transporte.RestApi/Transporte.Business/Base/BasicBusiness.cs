using Transporte.Repository.Interface.KeyGeneric;
using Transporte.Model.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Transporte.Business.Base.KeyGeneric;
using Transporte.Business.Result;

namespace Transporte.Business.Base.KeyGeneric
{
    public abstract class BasicBusiness<T, TKey>
         where T : EntityModel<TKey>, new()
    {

        ///<sumary>
        /// Essa referência do repostirório tem a única e excliva função de adicionar recursos mínimos para uma classe de negócio
        /// Não tem o objetivo de fornecar a referência da interface para as classes filhas.
        ///</sumary>
        private IRepository<T, TKey> _repository { get; }

        public async virtual Task<BusinessResult<List<T>>> Listar()
        {
            var businessResult = new BusinessResult<List<T>>();
            businessResult.Result = await _repository.Listar();
            return businessResult;
        }

        public async virtual Task<BusinessResult<T>> Obter(TKey key)
        {
            var businessResult = new BusinessResult<T>();
            businessResult.Result = await _repository.Obter(key);
            return businessResult;
        }

        public async virtual Task<BusinessResult<TKey>> Salvar(T entidade)
        {
            var businessResult = new BusinessResult<TKey>();
            await _repository.Salvar(entidade);
            businessResult.Result = entidade.Id;
            return businessResult;
        }

        public async virtual Task<BusinessResult> Excluir(TKey id)
        {
            var businessResult = new BusinessResult();
            await _repository.Excluir(id);
            return businessResult;
        }

        public BasicBusiness(IRepository<T, TKey> repository)
        {
            this._repository = repository;
        }
    }
}


namespace Transporte.Business.Base.KeyGuid
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class BasicBusiness<T> : BasicBusiness<T, Guid>
    where T : EntityGuidKey, new()
    {
        public BasicBusiness(IRepository<T, Guid> repository) : base(repository)
        {
        }
    }
}

namespace Transporte.Business.Base.KeyInt
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class BasicBusiness<T> : BasicBusiness<T, int>
        where T : EntityIntKey, new()
    {
        public BasicBusiness(IRepository<T, int> repository) : base(repository)
        {
        }
    }
}

namespace Transporte.Business.Base.KeyShort
{
    /// <summary>
    /// A linguagem C# possui uma limitação no qual não distingue classes genéricas pelo suas restrições
    /// Até o momento, a solução é definir namespaces diferentes para cada uma, mas ja existe uma discussão
    /// sobre esse tema no link: https://github.com/dotnet/csharplang/issues/2013
    /// </summary>
    public abstract class BasicBusiness<T> : BasicBusiness<T, short>
        where T : EntityShortKey, new()
    {
        public BasicBusiness(IRepository<T, short> repository) : base(repository)
        {
        }
    }
}