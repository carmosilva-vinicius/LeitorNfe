using LeitorNfe.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LeitorNfe.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<NotaFiscal> NotaFiscals { get; set; } = default!;
    public DbSet<Emitente> Emitentes { get; set; } = default!;
    public DbSet<Destinatario> Destinatarios { get; set; } = default!;
    public DbSet<Endereco> Enderecos { get; set; } = default!;
    public DbSet<Item> Items { get; set; } = default!;
    public DbSet<NotaItem> NotaItems { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<NotaFiscal>()
            .HasOne(nf => nf.Emitente)
            .WithMany(e => e.NotasFiscais)
            .HasForeignKey(nf => nf.EmitenteId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<NotaFiscal>()
            .HasOne(nf => nf.Destinatario)
            .WithMany(e => e.NotasFiscais)
            .HasForeignKey(nf => nf.DestinatarioId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Entity<NotaItem>()
            .HasKey(ni => new { ni.NotaFiscalId, ni.ItemId });

        builder.Entity<NotaItem>()
            .HasOne(ni => ni.NotaFiscal)
            .WithMany(nf => nf.NotaItems)
            .HasForeignKey(ni => ni.NotaFiscalId);
        
        builder.Entity<NotaItem>()
            .HasOne(ni => ni.Item)
            .WithMany(i => i.NotaItems)
            .HasForeignKey(ni => ni.ItemId);
        
        builder.Entity<Emitente>()
            .HasOne(e => e.Endereco)
            .WithOne()
            .HasForeignKey<Emitente>(e => e.EnderecoId);
        
        builder.Entity<Destinatario>()
            .HasOne(d => d.Endereco)
            .WithOne()
            .HasForeignKey<Destinatario>(d => d.EnderecoId);
    }
}
