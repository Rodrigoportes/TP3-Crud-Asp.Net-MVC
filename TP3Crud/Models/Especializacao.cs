using System;
using System.Collections.Generic;

namespace TP3Crud.Models
{
    public partial class Especializacao
    {
        public Especializacao()
        {
            Tarefa = new HashSet<Tarefa>();
        }

        public int EspecializacaoId { get; set; }
        public string Nome { get; set; } = null!;
        public int? Horas { get; set; }
        public int? EncarregadoId { get; set; }

        public virtual Encarregado? Encarregado { get; set; }
        public virtual ICollection<Tarefa> Tarefa { get; set; }
    }
}
