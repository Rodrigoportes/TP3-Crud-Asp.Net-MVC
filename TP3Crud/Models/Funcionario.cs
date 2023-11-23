using System;
using System.Collections.Generic;

namespace TP3Crud.Models
{
    public partial class Funcionario
    {
        public Funcionario()
        {
            Tarefa = new HashSet<Tarefa>();
        }

        public int FuncionarioId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime? DataNascimento { get; set; }
        public string? Email { get; set; }

        public virtual ICollection<Tarefa> Tarefa { get; set; }
    }
}
