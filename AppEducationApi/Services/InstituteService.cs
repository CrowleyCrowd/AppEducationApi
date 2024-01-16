using AppEducationApi.Data;
using AppEducationApi.Models;
using System.Text.Json;

namespace AppEducationApi.Services
{
	public class InstituteService
	{
		private readonly HttpClient _httpClient;
		private readonly AppDbContext _dbContext;

		public InstituteService(HttpClient httpClient, AppDbContext dbContext)
		{
			_httpClient = httpClient;
			_dbContext = dbContext;
		}

		public async Task<List<InstitutionResults>> GetInstitutionResultsAsync()
		{
			var response = await _httpClient.GetAsync($"https://www.gob.ec/api/v1/instituciones");

			response.EnsureSuccessStatusCode();

			var content = await response.Content.ReadAsStringAsync();
			var institutionJson = JsonSerializer.Deserialize<InstitutionResults>(content);
			var results = institutionJson.Results;

			return results;
		}

		public async Task SaveInstitutionAsync(Institution institution)
		{
			_dbContext.Institutions.Add(institution);
			await _dbContext.SaveChangesAsync();
		}
	}
}
