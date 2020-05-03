using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Mesa04.Models;

namespace Mesa04.Models
{
    public class Mesa04Context : DbContext
    {
        public Mesa04Context (DbContextOptions<Mesa04Context> options)
            : base(options)
        {
        }

        public DbSet<Mesa04.Models.Departamento> Departamento { get; set; }

        public DbSet<Mesa04.Models.Operacao> Operacao { get; set; }

        public DbSet<Mesa04.Models.Cliente> Cliente { get; set; }

        public DbSet<Mesa04.Models.Operador> Operador { get; set; }

        public DbSet<Mesa04.Models.TipoRegistroNacional> TipoRegistroNacional { get; set; }

        public DbSet<Mesa04.Models.TipoOperacao> TipoOperacao { get; set; }

        public DbSet<Mesa04.Models.OperacaoStatus> OperacaoStatus { get; set; }


        internal Task FindAllAsync()
        {
            throw new NotImplementedException();
        }

    }
}
