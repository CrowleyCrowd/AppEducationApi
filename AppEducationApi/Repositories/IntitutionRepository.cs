using AppEducationApi.Data;
using AppEducationApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppEducationApi.Repositories
{
	[Route("api/[controller]")]
	[ApiController]
	public class IntitutionRepository
	{
		private readonly AppDbContext _dbContext;

		public IntitutionRepository(AppDbContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		public async Task<List<Institution>> GetAll()
		{
			return await _dbContext.Institutions.ToListAsync();
		}
		[HttpGet("{id}")]
		public async Task<Institution> GetById(string id)
		{	
			return await _dbContext.Institutions.Where(x => x.InstitucionId == id).FirstOrDefaultAsync();
		}
		[HttpPost]
		public async Task<Institution> Create(Institution institution)
		{
			_dbContext.Institutions.Add(institution);
			await _dbContext.SaveChangesAsync();

			return institution;
		}
		[HttpPut("{id}")]
		public async Task<Institution> Update(string id, Institution institution)
		{
			if (id != institution.InstitucionId)
			{
				return null;
			}

			_dbContext.Entry(institution).State = EntityState.Modified;
			await _dbContext.SaveChangesAsync();

			return institution;
		}	
		//[HttpPut]
		//public async Task<Institution> Update(Institution institution)
		//{
		//	_dbContext.Entry(institution).State = EntityState.Modified;
		//	await _dbContext.SaveChangesAsync();

		//	return institution;
		//}
		[HttpDelete]
		public async Task<Institution> Delete(string id)
		{
			var institution = await _dbContext.Institutions.Where(x => x.InstitucionId == id).FirstOrDefaultAsync();
			_dbContext.Institutions.Remove(institution);
			await _dbContext.SaveChangesAsync();

			return institution;
		}
	}
}
