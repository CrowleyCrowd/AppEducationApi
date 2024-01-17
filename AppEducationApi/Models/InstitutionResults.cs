using System.Text.Json.Serialization;

namespace AppEducationApi.Models
{
	public class InstitutionResults
	{
		[JsonPropertyName("institucion_id")]
		public string InstitucionId { get; set; }

		[JsonPropertyName("institucion")]
		public string Institucion { get; set; }

		[JsonPropertyName("siglas")]
		public string Siglas { get; set; }

		[JsonPropertyName("logo")]
		public string Logo { get; set; }

		[JsonPropertyName("url")]
		public string Url { get; set; }

		[JsonPropertyName("website")]
		public string Website { get; set; }

		[JsonPropertyName("tipo")]
		public string Tipo { get; set; }

		[JsonPropertyName("descripcion")]
		public string Descripcion { get; set; }

		[JsonPropertyName("sector")]
		public string Sector { get; set; }

		[JsonPropertyName("modificado")]
		public string Modificado { get; set; }

		[JsonPropertyName("publicado")]
		public string Publicado { get; set; }
	}
}
