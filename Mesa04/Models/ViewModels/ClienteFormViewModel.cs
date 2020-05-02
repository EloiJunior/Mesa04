using System.Collections.Generic;

namespace Mesa04.Models.ViewModels
{
    public class ClienteFormViewModel
    {
        public Cliente Cliente { get; set; }
        public ICollection<TipoRegistroNacional> TipoRegistroNacionals { get; set; } //= new List<TipoRegistroNacional>();
    }
}
