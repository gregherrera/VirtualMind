using Microsoft.EntityFrameworkCore;
using Cotization.Library.Domain;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace Cotization.Library.Infrastructure
{
    public partial class CotizacionContext : DbContext
    {
        public CotizacionContext()
        {
        }

        public CotizacionContext(DbContextOptions<CotizacionContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Compra> Compras { get; set; }
        public virtual DbSet<Limite> Limites { get; set; }
        public virtual DbSet<Moneda> Monedas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
				var builder = new ConfigurationBuilder().AddJsonFile($"appsettings.json", true, true);
				var config = builder.Build();
				var connectionString = config["ConnectionStrings:Exchange"];

				optionsBuilder.UseSqlServer(connectionString);
			}
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Latin1_General_CI_AS");

            modelBuilder.Entity<Compra>(entity =>
            {
                entity.HasIndex(e => e.Fecha, "IDX_Compras_Fecha");

                entity.HasIndex(e => e.IdMoneda, "IDX_Compras_Moneda");

                entity.HasIndex(e => e.IdUsuario, "IDX_Compras_Usuario");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.IdMoneda).HasColumnName("Id_Moneda");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Monto).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Tasa).HasColumnType("decimal(12, 2)");

                entity.Property(e => e.Valor).HasColumnType("decimal(12, 2)");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Compras)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Compras_Monedas");
            });

            modelBuilder.Entity<Limite>(entity =>
            {
                entity.HasIndex(e => e.Anio, "IDX_Limites_Anio");

                entity.HasIndex(e => e.Mes, "IDX_Limites_Mes");

                entity.HasIndex(e => e.IdMoneda, "IDX_Limites_Moneda");

                entity.HasIndex(e => e.IdUsuario, "IDX_Limites_Usuario");

                entity.HasIndex(e => new { e.IdMoneda, e.IdUsuario, e.Anio, e.Mes }, "UQ_Limites")
                    .IsUnique();

                entity.Property(e => e.Anio).HasColumnName("anio");

                entity.Property(e => e.IdMoneda).HasColumnName("Id_Moneda");

                entity.Property(e => e.IdUsuario).HasColumnName("Id_Usuario");

                entity.Property(e => e.Monto).HasColumnType("decimal(16, 0)");

                entity.HasOne(d => d.Moneda)
                    .WithMany(p => p.Limites)
                    .HasForeignKey(d => d.IdMoneda)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Limites_Monedas");
            });

            modelBuilder.Entity<Moneda>(entity =>
            {
                entity.HasIndex(e => e.Descripcion, "UQ_Monedas")
                    .IsUnique();

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Factor).HasColumnType("decimal(5, 2)");

                entity.Property(e => e.Url)
                    .IsRequired()
                    .HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
