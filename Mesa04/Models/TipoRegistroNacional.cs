
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mesa04.Models
{
    public class TipoRegistroNacional
    {


        public int Id { get; set; }                            //atributo basico da classe

        public string Nome { get; set; }                       //atributo basico da classe

       
        //associação com a classe Operador, 1 Departamento tem varios Operadores, 
        public ICollection<Cliente> Clientes { get; set; } = new List<Cliente>();
        //já instanciando a coleção, só para garantir que a minha lista seja instanciada
       


        //construtor sem argumento, pois o framework precisa dele
        public TipoRegistroNacional()
        {
        }

        //Construtor com argumento, todos os atributos com exceção das coleções
        public TipoRegistroNacional(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }

        
        //operação (metodo customizado) para adicionar Operador na lista de Operadores do Departamento
        public void AddCliente(Cliente cliente)
        {
            Clientes.Add(cliente);
        }
        

        //operação (metodo customizado) para retornar o total de vendas do departamento
        public decimal TotalSales(DateTime inicial, DateTime final)
        {
            return Clientes.Sum(cliente => cliente.TotalSales(inicial, final));
        }

    }
}
