using System.Collections.Generic;

namespace Mesa04.Models.ViewModels
{
    public class OperadorFormViewModel
    {
        public Operador Operador { get; set; }
        public ICollection<Departamento> Departamentos { get; set; }
    }
}
