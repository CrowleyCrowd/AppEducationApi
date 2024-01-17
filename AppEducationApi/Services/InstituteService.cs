using AppEducationApi.Data;
using AppEducationApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppEducationApi.Services
{
	public class InstituteService: ControllerBase
	{
		private readonly AppDbContext _dbContext;

		public InstituteService(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		public async Task<List<InstitutionResults>> GetInstitutionResultsAsync()
		{
			using HttpClient client = new HttpClient();
			try {

				string apiUrl = $"https://www.gob.ec/api/v1/instituciones";

				HttpResponseMessage response = await client.GetAsync(apiUrl);

				if (response.IsSuccessStatusCode)
				{
					var responseBody = await response.Content.ReadAsStringAsync();
					List<InstitutionResults> result = JsonSerializer.Deserialize<List<InstitutionResults>>(responseBody, new JsonSerializerOptions
					{
						PropertyNameCaseInsensitive = true,
						WriteIndented = true,
						Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }
					});
					return result;
				}
				else
				{
					throw new Exception("Error en la solicitud. Código de estado: " + response.StatusCode);
				}
			}
			catch (Exception ex)
			{
				throw;
			}
		}

		public async Task SaveInstitutionAsync(Institution institution)
		{
			_dbContext.Institutions.Add(institution);
			await _dbContext.SaveChangesAsync();
		}
	}
}
