using Aliquota.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aliquota.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<FundoInvestimento> Fundos { get; set; }
    public DbSet<Aplicacao> Aplicacoes { get; set; }
    public DbSet<Resgate> Resgates { get; set; }
    public DbSet<Cliente> Clientes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aplicacao>()
            .Property(a => a.Valor)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Aplicacao>()
            .HasOne<FundoInvestimento>(a => a.FundoInvestimento)
            .WithMany(fi => fi.Aplicacoes)
            .HasForeignKey(ap => ap.FundoId);
        
        modelBuilder.Entity<Resgate>()
            .Property(r => r.ImpostoDeRenda)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Resgate>()
            .Property(r => r.ValorResgate)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Resgate>()
            .Property(r => r.ValorLiquido)
            .HasColumnType("decimal(18,2)");
        
        modelBuilder.Entity<Aplicacao>()
            .HasOne<Cliente>(a => a.Cliente)
            .WithMany(cl => cl.Aplicacoes)
            .HasForeignKey(ap => ap.ClienteId);
        
        modelBuilder.Entity<Resgate>()
            .HasOne<FundoInvestimento>(r => r.FundoInvestimento)
            .WithMany(fi => fi.Resgates)
            .HasForeignKey(rs => rs.FundoId);
        
        modelBuilder.Entity<Resgate>()
            .HasOne<Cliente>(r => r.Cliente)
            .WithMany(c => c.Resgates)
            .HasForeignKey(rs => rs.ClienteId);

        modelBuilder.Entity<FundoInvestimento>().HasData(
            new FundoInvestimento("Fundo de Investimento A") { Id = 1 },
            new FundoInvestimento("Fundo de Investimento B") { Id = 2 },
            new FundoInvestimento("Fundo de Investimento C") { Id = 3 }
        );

        modelBuilder.Entity<Cliente>().HasData(
            new Cliente("Ronaldo") { Id = 1 },
            new Cliente("Jose") { Id = 2 },
            new Cliente("Maria") { Id = 3 }
        );
    }
}