using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mesa04.Models
{
    public class BancoMe
    {
        public int Id { get; set; }                            //atributo basico da classe

        public string Nome { get; set; }                       //atributo basico da classe

        //associação com a classe Operacao, 1 BancoMe tem varios Operacaos, 
        public ICollection<Operacao> Operacaos { get; set; } = new List<Operacao>();
        //já instanciando a coleção, só para garantir que a minha lista seja instanciada



        //construtor sem argumento, pois o framework precisa dele
        public BancoMe()
        {
        }

        //Construtor com argumento, todos os atributos com exceção das coleções
        public BancoMe(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        //operação (metodo customizado) para adicionar Operador na lista de Operadores do Departamento
        public void AddOperacao(Operacao operacao)
        {
            Operacaos.Add(operacao);
        }

        /*
        //operação (metodo customizado) para retornar o total de vendas do departamento
        public double TotalSales(DateTime inicial, DateTime final)
        {
            return Operacaos.Sum(operacao => operacao.TotalSales(inicial, final));
        }
        */

    }
}
