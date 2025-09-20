using CandidatosModel;
using Microsoft.EntityFrameworkCore;

namespace CandidatosData
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        // Novo DbSet para a API de listagem de sites de apostas
        public DbSet<ApostaSiteModel> ApostaSites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Mapeamento da entidade ApostaSiteModel
            modelBuilder.Entity<ApostaSiteModel>(entity =>
            {
                entity.ToTable("APOSTA_SITES"); // nome da tabela no Oracle

                entity.HasKey(e => e.Id);

                // String -> NVARCHAR2 no Oracle; defina tamanhos explícitos
                entity.Property(e => e.Id)
                      .HasMaxLength(36)
                      .IsRequired();

                entity.Property(e => e.Nome)
                      .HasMaxLength(120)
                      .IsRequired();

                entity.Property(e => e.Url)
                      .HasMaxLength(300)
                      .IsRequired();

                entity.Property(e => e.Categoria)
                      .HasMaxLength(50)
                      .HasDefaultValue("Geral");

                entity.Property(e => e.NivelRisco)
                      .HasMaxLength(20)
                      .HasDefaultValue("Médio");

                // Default no banco (Oracle)
                entity.Property(e => e.DataCadastro)
                      .HasColumnType("DATE")
                      .HasDefaultValueSql("SYSDATE");

                // Índice único por URL para evitar duplicidade de sites
                entity.HasIndex(e => e.Url)
                      .IsUnique()
                      .HasDatabaseName("UX_APOSTA_SITES_URL");
            });
        }
    }
}
