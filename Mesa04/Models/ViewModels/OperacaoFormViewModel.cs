using System;
using System.Collections.Generic;  //para usar o ICollection
using System.Data;
using System.Globalization;


namespace Mesa04.Models.ViewModels
{
    public class OperacaoFormViewModel
    {
        public Operacao Operacao { get; set; }
        public ICollection<Operador> Operadores { get; set; }
        public ICollection<TipoOperacao> TipoOperacaos { get; set; }
        public ICollection<Cliente> Clientes { get; set; }
        public ICollection<OperacaoStatus> OperacaoStatuss { get; set; }
        public ICollection<BancoMe> BancoMes { get; set; }
        public ICollection<Me> Mes { get; set; }
    }
    //public static DateTime Now { get; }
}
