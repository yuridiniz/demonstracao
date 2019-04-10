using System;
using Transporte.Model.AbstractEntity;

namespace Transporte.Model
{
    public class Viagem : EntityGuidKey
    {
        public string Matricula { get; set; }
        public DateTime DataRegistro { get; set; }
        public Guid StatusViagemId { get; set; }

        // Como não iremos efetura a carga de status, manteremos nossa referência comentada
        // Para o EntityFramework não criar FK
        //
        // public StatusViagem StatusViagem { get; set; }
    }
}