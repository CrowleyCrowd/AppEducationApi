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

		public InstitutionController(InstituteService instituteService)
		{
			_instituteService = instituteService;
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
			var institution = await _instituteService.GetInstitutionResultsAsync();

			if (institution == null)
			{
				return NotFound();
			}

			List<Institution> institutions = new();

			foreach (var item in institution)
			{
				var institutionItem = new Institution { InstitucionId = item.InstitucionId, Institucion = item.Institucion, Siglas = item.Siglas, Logo = item.Logo, Url = item.Url, Website = item.Website, Tipo = item.Tipo, Descripcion = item.Descripcion, Sector = item.Sector, Modificado = item.Modificado, Publicado = item.Publicado };
				await _instituteService.SaveInstitutionAsync(institutionItem);
				institutions.Add(institutionItem);
			}

			return institutions;
		}
	}
}
