using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model.AbstractEntity
{
    public abstract class EntityShortKey : EntityModel<short>
    {
        public override short GenerateNewId()
        {
            return 0;
        }
    }
}
