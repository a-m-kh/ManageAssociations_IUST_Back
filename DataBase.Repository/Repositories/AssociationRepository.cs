using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repository.Repositories;

public class AssociationRepository: GeneralRepository<Association> , IAssociationRepository
{

	private readonly IMapper _mapper;
	public AssociationRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
	{
		_mapper = mapper;
	}

	public async Task<int> CreateAsync(AssociationCreateDto Model)
	{
		if (Model == null)
			return 0;
		var entity = _mapper.Map<Association>(Model);
		if(entity == null)
			return 0;
		var DbEntity =  await TEntity.AddAsync(entity);
		await _uow.SaveChangesAsync();
		if(DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
			return 0;

		return DbEntity.Entity.ID;
	}

	public async Task<bool> UpdateAsync(AssociationUpdateDto Model)
	{
		var entity = await TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefaultAsync();
		if(entity == null)
			return false;

		entity.LogoUrl = Model.LogoUrl == null ? (entity.LogoUrl) : (Model.LogoUrl);
		entity.Name = Model.Name == null ? (entity.Name) : (Model.Name);

		var IsUpdate = await _uow.SaveChangesAsync();
		return (IsUpdate > 0);

	}

	public async Task<bool> DeleteAsync(int Id)
	{
		var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefaultAsync();
		if (entity == null)
			return false;

		entity.IsDelete = true;

		var IsUpdate = await _uow.SaveChangesAsync();
		return (IsUpdate > 0);
	}

	public async Task<AssociationViewDto> GetAsync(int Id)
	{
		var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete)
			.Select(a=> new AssociationViewDto()
			{
				ID= a.ID,
				LogoUrl = a.LogoUrl,
				Name = a.Name,
				AdminId = a.AdminID
			})
			.FirstOrDefaultAsync();
		return (entity);
	}


	public async Task<AssociationViewDto> GetByNameAsync(string Name)
	{
		var entity = await TEntity.Where(a => a.Name == Name && !a.IsDelete)
			.Select(a => new AssociationViewDto()
			{
				ID = a.ID,
				LogoUrl = a.LogoUrl,
				Name = a.Name
			})
			.FirstOrDefaultAsync();
		return (entity);
	}
	public async Task<GeneralPaginationModel<AssociationViewDto>> GetAllAsync(int Page = 1)
	{
		var total = TEntity.Where( a=> a.IsDelete).Count();
		var entities = await TEntity.Where(a => !a.IsDelete)
			.OrderByDescending(a => a.ID)
			.Skip((Page - 1) * 4)
			.Take(4).Select(a => new AssociationViewDto()
			{
				ID = a.ID,
				LogoUrl = a.LogoUrl,
				Name = a.Name
			}).ToListAsync();
		var res = new GeneralPaginationModel<AssociationViewDto>(total, entities);
		return(res);
	}

	public async Task<List<AssociationViewDto>> GetAll()
	{
		var total = TEntity.Where(a => a.IsDelete).Count();
		var entities =  TEntity.Where(a => !a.IsDelete)
			.OrderByDescending(a => a.ID)
			.Select(a => new AssociationViewDto()
			{
				ID = a.ID,
				LogoUrl = a.LogoUrl,
				Name = a.Name
			}).ToList();
		return (entities);
	}

	public async Task<bool> AssignAdminAsync(User user, int AssociationID)
	{
		if (user == null || user.Id == null)
			return false;

		if (user.Association != null)
			return false;

		var entity = await TEntity.Where(a=>a.ID == AssociationID).FirstOrDefaultAsync();

		if(entity == null)
			return false;

		entity.AdminID = user.Id;
		var IsUpdate = _uow.SaveChanges();
		return (IsUpdate > 0);

	}
}
