using System;
using System.Collections.Generic;

namespace TP3Crud.Models
{
    public partial class Tarefa
    {
        public int TarefaId { get; set; }
        public string Nome { get; set; } = null!;
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public int? FuncionarioId { get; set; }
        public int? EspecializacaoId { get; set; }

        public virtual Especializacao? Especializacao { get; set; }
        public virtual Funcionario? Funcionario { get; set; }
    }
}
