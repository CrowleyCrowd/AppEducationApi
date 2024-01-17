using AppEducationApi.Data;
using AppEducationApi.Models;
using AppEducationApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace AppEducationApi.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class InstitutionController : ControllerBase
	{
		private readonly InstituteService _instituteService;
		private readonly AppDbContext _dbContext;

		public InstitutionController(InstituteService instituteService, AppDbContext dbContext)
		{
			_instituteService = instituteService;
			_dbContext = dbContext;
		}

		[HttpGet("/")]
		public async Task<ActionResult<List<InstitutionResults>>> GetAll()
		{
			var institution = await _instituteService.GetInstitutionResultsAsync();

			if (institution == null)
			{
				return NotFound();
			}

			return institution;
		}

		[HttpPost("/MigrateInstitution")]
		public async Task<ActionResult<List<Institution>>> MigrateInstitution()
		{
			List<InstitutionResults> institution = await _instituteService.GetInstitutionResultsAsync();

			if (institution == null)
			{
				return NotFound();
			}

			List<Institution> institutions = new();

			foreach (var item in institution)
			{
				var institutionCheck = _dbContext.Institutions.Where(x => x.InstitucionId == item.InstitucionId);
				if (!institutionCheck.Any())
				{
					var institutionItem = new Institution
					{
						InstitucionId = item.InstitucionId,
						Institucion = item.Institucion,
						Siglas = item.Siglas,
						Logo = item.Logo,
						Url = item.Url,
						Website = item.Website,
						Tipo = item.Tipo,
						Descripcion = item.Descripcion,
						Sector = item.Sector,
						Modificado = item.Modificado,
						Publicado = item.Publicado
					};
					_dbContext.Institutions.Add(institutionItem);
					institutions.Add(institutionItem);
				}
				//await _instituteService.SaveInstitutionAsync(institutionItem);
			}
			int result = await _dbContext.SaveChangesAsync();
			if (result == 0)
			{
				return BadRequest();
			}

			return institutions;
		}
	}
}
