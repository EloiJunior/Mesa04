using System.Collections.Generic;

namespace Mesa04.Models
{
    public class OperacaoStatus
    {
        /*
        Pendente = 0,
        Fechado = 1,
        Cancelado = 2,
        */

        public int Id { get; set; }                            //atributo basico da classe

        public string Nome { get; set; }                       //atributo basico da classe


        //associação com a classe Operador, 1 TipoOperação tem varias Operações, 
        public ICollection<Operacao> Operacaos { get; set; } = new List<Operacao>();
        //já instanciando a coleção, só para garantir que a minha lista seja instanciada



        //construtor sem argumento, pois o framework precisa dele
        public OperacaoStatus()
        {
        }

        //Construtor com argumento, todos os atributos com exceção das coleções
        public OperacaoStatus(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        //operação (metodo customizado) para adicionar Operacao na lista de Operacaos do TipoOperacao
        public void AddOperacao(Operacao operacao)
        {
            Operacaos.Add(operacao);
        }

    }
}
