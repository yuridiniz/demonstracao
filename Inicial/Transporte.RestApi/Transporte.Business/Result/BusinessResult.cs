using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Transporte.Business.Result
{
    public class BusinessResult<T> : BusinessValidation
    {
        public T Result { get; set; }
    }

    public class BusinessResult : BusinessValidation
    {
    }
}
