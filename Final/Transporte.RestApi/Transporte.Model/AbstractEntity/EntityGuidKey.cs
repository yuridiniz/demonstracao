using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model.AbstractEntity
{
    public abstract class EntityGuidKey : EntityModel<Guid>
    {
        public override Guid GenerateNewId()
        {
            return Guid.NewGuid();
        }
    }
}
