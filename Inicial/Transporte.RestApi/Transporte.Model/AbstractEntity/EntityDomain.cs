using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model.AbstractEntity
{
    public abstract class EntityModel<TKey>
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual TKey Id { get; set; }

        /// <summary>
        /// Método para gerar Guids pro Entity Framework
        /// 
        /// .NET Core utilizando a ultima versão do Entity Framework, ele se mostra obsoleto, 
        /// mas ainda é necessário verificar no .NET Framework, caso as Guids também sejam
        /// geradas e obtidias automaticamente, as classes EntityGuid, EntityInteger e EntityShort tornam-se
        /// obsoletas também
        /// 
        /// </summary>
        /// <returns></returns>
        [Obsolete("Entity framework gera a GUID sozinho")]
        public abstract TKey GenerateNewId();
    }
}
