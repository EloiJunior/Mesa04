using System;
using System.Collections.Generic; //para usar um ICollection, que é uma coleção de Lista etc...
using System.ComponentModel.DataAnnotations; //para usar o [Display] ou [DataType] ou [DisplayFormat], que é uma tag (anotação) de anotação personalizada, usada para difinir como aparecerá na tela do usuario
using System.Linq;                //para usar os codigos linq e as expressões lambda

namespace Mesa04.Models
{
    public class Operador
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Name required")]                                                    //anotation que define que o campo é obrigatorio, e mensagem de erro se quiser
        //[Required(ErrorMessage = "{0} required")]                                                   //como opção podemos automatizar alguns strings da mensagem de erro
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Name size shold be between 3 and 60")]   //anotation que define tamanho minimo e maximo dessa string
        //[[StringLength(60, MinimumLength =3, ErrorMessage = "{0} size shold be between {2} and {1}")]  //como opção podemos automatizar alguns strings da mensagem de erro
        public string Nome { get; set; }

        [Required(ErrorMessage = "{0} required")]              //anotation de verificação de campo obrigatorio
        [EmailAddress(ErrorMessage = "Enter a valid email")]  //anotation de verificação de email
        //[DataType(DataType.EmailAddress)]       // anotation usada para transformar o email escrito em link na tela do usuario
        public string Email { get; set; }

        [Required(ErrorMessage = "{0} required")] //anotation de verificação de campo obrigatorio
        [Display(Name = "Birth Date")]            //tag usada para personalizar como o atributo aparecerá no display, que é a tela do site
        [DataType(DataType.Date)]                // anotation usada para personalizar como aparecerá os dados na tela
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]  //anotation usada para configurar a data como dia, mes e ano.
        public DateTime Aniversario { get; set; }

        [Required(ErrorMessage = "{0} required")]                              //anotation de verificação de campo obrigatorio
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be form {1} to {2}")] //anotation de verificação de intervalo permitido no campo
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString = "{0:F2}")]                          // anotation usada para configurar o formato para mostrar os dados, o 0 indica o valor do atributo, F2 indica 2 casas decimais
        public double SalarioBase { get; set; }
        
        
        public Departamento Departamento { get; set; } //associação de 1 Operador com apenas 1 Departamento

        
        public int DepartamentoId { get; set; } //foreign key, para forçar que esse campo não possa ser nulo na tabela de operador, uma vez que o tipo "int" não pode ser nulo
        

        //tirei a associação pois não quero associar o operador ao departamento de origem da operação que é o Tipo
        //public Tipo Tipo { get; set; } //associação de 1 Operador com apenas 1 Departamento
        // public int TipoId { get; set; } //foreign key


        //associação de 1 Operador com varios Fechamentos, já instanciando a coleção, para garantir que a lista seja criada
        public ICollection<Operacao> Operacoes { get; set; } = new List<Operacao>();

        //construtor sem argumento
        public Operador()
        {
        }

        //construtor com argumento
        public Operador(int id, string nome, string email, DateTime aniversario, double salarioBase/*, Departamento departamento*/)//Tipo tipo//
        {
            Id = id;
            Nome = nome;
            Email = email;
            Aniversario = aniversario;
            SalarioBase = salarioBase;
            //Departamento = departamento;
            //Tipo = tipo;
        }

        
        //operação para adicionar um fechamento na minha lista Fechamentos
        public void AddSales(Operacao fchto)
        {
            Operacoes.Add(fchto);
        }

        //operação para remover um fechamento na minha lista Fechamentos
        public void RemoveSales(Operacao fchto)
        {
            Operacoes.Remove(fchto);
        }
        //operação de calculo do total de vendas do operador, com data inicial e final
        public decimal TotalSales(DateTime inicial, DateTime final)
        {
            return Operacoes.Where(fchto => fchto.Data >= inicial && fchto.Data <= final).Sum(fchto => fchto.Valor);
        }
        
    }
}
