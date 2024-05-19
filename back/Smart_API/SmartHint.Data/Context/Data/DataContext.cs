using Microsoft.EntityFrameworkCore;
using SmartHint.Data.Mapping;
using SmartHint.Domain.Entities;

namespace SmartHint.Data.Context.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Cliente> Cliente { get; set; }
        public DbSet<Endereco> Endereco { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //criar a tabela baseado no Map
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new EnderecoMap());

            modelBuilder.Entity<Cliente>()
                        .HasOne(c => c.Endereco)
                        .WithMany()
                        .HasForeignKey(c => c.IdEndereco);
        }
    }
}
