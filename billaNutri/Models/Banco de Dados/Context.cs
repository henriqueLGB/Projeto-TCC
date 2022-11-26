using billaNutri.Models.Home;
using Microsoft.EntityFrameworkCore;

namespace billaNutri.Models.Banco_de_Dados
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options)
    :   base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<USUARIO> USUARIO { get; set; }
        public DbSet<CLIENTE> CLIENTE { get; set; }
        public DbSet<ENDERECO> ENDERECO { get; set; }
        public DbSet<AGCLIENTE> AGCLIENTE { get; set; }
        public DbSet<STATUSPAGAMENTO> STATUSPAGAMENTO { get; set; }
    }
}
