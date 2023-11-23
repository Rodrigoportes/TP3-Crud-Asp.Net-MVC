using System;
using System.Collections.Generic;

namespace TP3Crud.Models
{
    public partial class Encarregado
    {
        public Encarregado()
        {
            Especializacao = new HashSet<Especializacao>();
        }

        public int EncarregadoId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime? DataContratacao { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Especializacao> Especializacao { get; set; }
    }
}
