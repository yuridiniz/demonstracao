using Transporte.Model.AbstractEntity;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transporte.Model
{
    public class Projeto : EntityGuidKey
    {
        public string Nome { get; set; }

        [ForeignKey("Regional")]
        public int IdRegional { get; set; }

        public Regional Regional { get; set; }
    }
}
