using AppEducationApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppEducationApi.Data
{
	public partial class AppDbContext (DbContextOptions<AppDbContext> options) : DbContext(options)
	{
		public virtual DbSet<Institution> Institutions { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseNpgsql("Name=ConnectionStrings:DatabaseContext");
			}
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Institution>(entity =>
			{
				entity.HasKey(p => p.InstitucionId);
				entity.Property(p => p.Institucion);
				entity.Property(p => p.Siglas);
				entity.Property(p => p.Logo);
				entity.Property(p => p.Url);
				entity.Property(p => p.Website);
				entity.Property(p => p.Tipo);
				entity.Property(p => p.Descripcion);
				entity.Property(p => p.Sector);
				entity.Property(p => p.Modificado);
				entity.Property(p => p.Publicado);
			});

			OnModelCreatingPartial(modelBuilder);
		}

		partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
	}
}
