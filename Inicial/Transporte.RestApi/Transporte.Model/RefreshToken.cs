using Transporte.Model.AbstractEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Transporte.Model
{
    public class RefreshToken : EntityGuidKey
    {
        public string Token { get; set; }
        public DateTime DataExpiracao { get; set; }

        [ForeignKey("Usuario")]
        public Guid IdUsuario { get; set; }
        public Usuario Usuario { get; set; }
    }
}
