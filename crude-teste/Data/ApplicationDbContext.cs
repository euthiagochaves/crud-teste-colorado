using crude_teste.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace crude_teste.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Telefone> Telefones { get; set; }
        public DbSet<TipoTelefone> TiposTelefone { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração da chave composta para Telefone
            modelBuilder.Entity<Telefone>()
                .HasKey(t => new { t.CodigoCliente, t.NumeroTelefone });

            // Configurações de relacionamentos
            modelBuilder.Entity<Cliente>()
                .HasMany(c => c.Telefones)
                .WithOne(t => t.Cliente)
                .HasForeignKey(t => t.CodigoCliente)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<TipoTelefone>()
                .HasMany(tt => tt.Telefones)
                .WithOne(t => t.TipoTelefone)
                .HasForeignKey(t => t.CodigoTipoTelefone)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<TipoTelefone>().HasData(
                new TipoTelefone { 
                    CodigoTipoTelefone = 1, 
                    DescricaoTipoTelefone = "RESIDENCIAL",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new TipoTelefone { 
                    CodigoTipoTelefone = 2, 
                    DescricaoTipoTelefone = "COMERCIAL",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new TipoTelefone { 
                    CodigoTipoTelefone = 3, 
                    DescricaoTipoTelefone = "WHATSAPP",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                },
                new TipoTelefone { 
                    CodigoTipoTelefone = 4, 
                    DescricaoTipoTelefone = "CELULAR",
                    DataInsercao = DateTime.Now,
                    UsuarioInsercao = "SYSTEM"
                }
            );
        }
    }
}
