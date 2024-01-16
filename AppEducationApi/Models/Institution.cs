using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace AppEducationApi.Models
{
	[Table("Institutions")]
	public class Institution
	{
		[Key]
		[Column("institucion_id")]
		public string InstitucionId { get; set; }

		[Required]
		[Column("institucion")]
		public string Institucion { get; set; }

		[Required]
		[Column("siglas")]
		public string Siglas { get; set; }

		[Column("logo")]
		public string Logo { get; set; }

		[Column("url")]
		public string Url { get; set; }

		[Column("website")]
		public string Website { get; set; }

		[Required]
		[Column("tipo")]
		public string Tipo { get; set; }

		[Required]
		[Column("descripcion")]
		public string Descripcion { get; set; }

		[Required]
		[Column("sector")]
		public string Sector { get; set; }

		[Column("modificado")]
		public string Modificado { get; set; }

		[Column("publicado")]
		public string Publicado { get; set; }	
	}
}
