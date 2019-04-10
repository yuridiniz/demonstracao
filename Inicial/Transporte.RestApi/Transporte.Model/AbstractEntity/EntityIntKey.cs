using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model.AbstractEntity
{
    public abstract class EntityIntKey : EntityModel<int>
    {
        public override int GenerateNewId()
        {
            return 0;
        }
    }
}
