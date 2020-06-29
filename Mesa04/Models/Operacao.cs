using System;
using System.ComponentModel.DataAnnotations;  //para usar as anotações de Formatos
using System.ComponentModel.DataAnnotations.Schema;//para usar a tag NotMapped, que serve para não criar a coluna na tabela, vou testar ainda
using System.Data;


namespace Mesa04.Models
{
    public class Operacao
    {

        public int Id { get; set; }            //atributo basico

        //[NotMapped]
        public TipoOperacao TipoOperacao { get; set; }       //atributo basico //associação de 1 fechamento para 1 Tipo

        
        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro  //Obs: tive que tirar o Required para funcionar o Edit Post da Operação
        [Display(Name = "Tipo")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int TipoOperacaoId { get; set; } //foreign Key 
        

        //[NotMapped]
        public Operador Operador { get; set; } //associação de 1 Fechamento para 1 Operador

        
        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [Display(Name = "Operador")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int OperadorId { get; set; }  //foreign Key
        
       
        [DataType(DataType.Date)]                             // anotation usada para personalizar como aparecerá os dados na tela
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  //anotation usada para configurar a data como dia, mes e ano.
        public DateTime Data { get; set; }     //atributo basico

        //[NotMapped]
        public Cliente Cliente { get; set; } //associação de 1 Fechamento para 1 Cliente

        
        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [Display(Name = "Cliente")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int ClienteId { get; set; }    //foreign Key
        

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [DisplayFormat(DataFormatString = "{0:F2}")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        public double Valor { get; set; }      //atributo basico

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        //[DisplayFormat(DataFormatString = "{0:F2}")]
        //[DisplayFormat(DataFormatString = "{0:C2}")]
        public double Taxa { get; set; }       //atributo basico

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [DisplayFormat(DataFormatString = "{0:F2}")]
        public double Despesa { get; set; }    //atributo basico

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [DisplayFormat(DataFormatString = "{0:F0}")]
        public int FluxoMn { get; set; }         //atributo basico

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [DisplayFormat(DataFormatString = "{0:F0}")]
        public int FluxoMe { get; set; }         //atributo basico

        [Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        public double TaxaReferencia { get; set; }         //atributo basico

        //[Required(ErrorMessage = "{0} required")]   //como opção podemos automatizar alguns strings da mensagem de erro
        public BancoMe BancoMe { get; set; }      //atributo basico// associaçao de 1 operacao com 1 BancoMe

        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [Display(Name = "BancoMe")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int BancoMeId { get; set; } //foreign Key 

        [Display(Name = "Status")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public OperacaoStatus OperacaoStatus { get; set; } //associação de 1 Fechamento com 1 SaleStatus

        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [Display(Name = "Status")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int OperacaoStatusId { get; set; } //foreign Key 

        //[Required(ErrorMessage = "{0} required")]   //como opção podemos automatizar alguns strings da mensagem de erro
        public Me Me { get; set; }      //atributo basico// associaçao de 1 operacao com 1 Me

        //[Required(ErrorMessage = "{0} required")]  //como opção podemos automatizar alguns strings da mensagem de erro
        [Display(Name = "Me")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        public int MeId { get; set; } //foreign Key 


        //Construtor sem argumento, precisamos criar pois o framework precisa dele
        public Operacao()
        {
        }

        //Construtor com argumento
        public Operacao(int id, /*Tipo tipo,*/ /*Operador operador,*/ DateTime data, /*Cliente cliente,*/ double valor, double taxa, double despesa, int fluxoMn, int fluxoMe, double taxaReferencia /*, BancoMe banco, SaleStatus status*/)
        {
            Id = id;
            //Tipo = tipo;
            //Operador = operador;
            Data = data;
            //Cliente = cliente;
            Valor = valor;
            Taxa = taxa;
            Despesa = despesa;
            FluxoMn = fluxoMn;
            FluxoMe = fluxoMe;
            TaxaReferencia = taxaReferencia;
            //BancoMe = bancoMe;
            //Status = status;
        }

    }
}
