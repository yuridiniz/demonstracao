using System;
using Transporte.Model.AbstractEntity;

namespace Transporte.Model
{
    public class StatusViagem : EntityGuidKey
    {
        public static readonly Guid EmAberto = Guid.Parse("10e8fb2f-fb84-4f3f-ae26-373059a01224");
        public static readonly Guid Finalizado = Guid.Parse("24713b49-467a-4e3e-a101-d24f3ddbd525");
        public static readonly Guid Estornado = Guid.Parse("0168120d-259b-4bd8-8919-923d0471f235");

        public string Descricao { get; set; }
    }
}
