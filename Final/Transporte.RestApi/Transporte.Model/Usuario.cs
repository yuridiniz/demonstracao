using Transporte.Model.AbstractEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Transporte.Model
{
    public class Usuario : EntityGuidKey
    {
        public string Nome { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
