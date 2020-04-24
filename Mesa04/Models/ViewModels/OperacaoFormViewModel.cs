using Mesa04.Models.Enums;
using System.Collections.Generic;  //para usar o ICollection

namespace Mesa04.Models.ViewModels
{
    public class OperacaoFormViewModel
    {
        public Operacao Operacao { get; set; }
        public ICollection<Operador> Operadores { get; set; }
        public ICollection<TipoOperacao> Tipos { get; set; }
    }
}
