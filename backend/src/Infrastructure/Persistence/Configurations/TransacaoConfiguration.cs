using ControleGastos.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControleGastos.Infrastructure.Persistence.Configurations;

/// <summary>
/// Aqui acrescentamos algo bem importante: Em casos que se delete uma pessoa, todas a transa��es dessa pessoa dever�o ser apagadas.
/// Temos cuidado para n�o deletar a Categoria
/// </summary>
public class TransacaoConfiguration : IEntityTypeConfiguration<Transacao>
{
    public void Configure(EntityTypeBuilder<Transacao> builder)
    {
        builder.ToTable("transacoes");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Descricao)
            .IsRequired()
            .HasMaxLength(Transacao.DescricaoMaxLength);

        builder.Property(x => x.Valor)
            .HasPrecision(18, 2)
            .IsRequired();

        builder.Property(x => x.Tipo)
            .IsRequired();

        builder.HasOne<Pessoa>()
            .WithMany()
            .HasForeignKey(x => x.PessoaId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Categoria>()
            .WithMany()
            .HasForeignKey(x => x.CategoriaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
