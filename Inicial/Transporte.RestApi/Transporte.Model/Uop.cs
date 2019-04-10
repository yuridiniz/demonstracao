using Transporte.Model.AbstractEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model
{
    public class Uop : EntityIntKey
    {

        [Key]
        [Column("CdUop")]
        public override int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataAtivacao { get; set; }

        [ForeignKey("Projeto")]
        public Guid IdProjeto { get; set; }

        public Projeto Projeto { get; set; }
    }
}
