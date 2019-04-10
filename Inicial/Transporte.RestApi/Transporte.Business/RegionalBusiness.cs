using Transporte.Repository;
using Transporte.Repository.Interface;
using Transporte.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transporte.Business.Base.KeyInt;

namespace Transporte.Business
{
    public class RegionalBusiness : BasicBusiness<Regional>
    {
        private readonly IRegionalDAL regionalDal;

        public RegionalBusiness(IRegionalDAL regionalDAL)
            :base(regionalDAL)
        {
            this.regionalDal = regionalDAL;
        }
    }
}
