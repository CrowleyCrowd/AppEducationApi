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

			var newInstitution = (from i in institution
								  join idb in _dbContext.Institutions.ToList() on i.InstitucionId equals idb.InstitucionId into lidb
								  from idb in lidb.DefaultIfEmpty()
								  where idb == null || i.InstitucionId != idb.InstitucionId
								  select new Institution
								  {
									  InstitucionId = i.InstitucionId,
									  Institucion = i.Institucion,
									  Siglas = i.Siglas,
									  Logo = i.Logo,
									  Url = i.Url,
									  Website = i.Website,
									  Tipo = i.Tipo,
									  Descripcion = i.Descripcion,
									  Sector = i.Sector,
									  Modificado = i.Modificado,
									  Publicado = i.Publicado
								  });
			await _dbContext.Institutions.AddRangeAsync(newInstitution);

			int result = await _dbContext.SaveChangesAsync();
			if (result == 0)
			{
				return NotFound();
			}

			return newInstitution.ToList();
		}
	}
}
