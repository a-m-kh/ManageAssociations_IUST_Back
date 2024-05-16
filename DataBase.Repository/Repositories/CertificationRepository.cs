using DataBase.Repository.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Repository;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace DataBase.Repository.Repositories
{
	public class CertificationRepository :GeneralRepository<Certification>, ICertificationRepository
	{

		private readonly IMapper _mapper;

		public CertificationRepository(IUnitOfWork uow, IMapper mapper): base(uow)
		{
			_mapper = mapper;
		}

		public int Create(CreateCertificationDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<Certification>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateCertificationDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.Organizer = Model.Organizer == null ? (entity.Organizer) : (Model.Organizer);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title);
			entity.ExcelUrl = Model.ExcelUrl == null ? (entity.ExcelUrl) : (Model.ExcelUrl);
			entity.Number = Model.Number ?? entity.Number;
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.IsDelete = true;

			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public GetCertificationDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete)
				.Select(a => new GetCertificationDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					Status = a.Status.Title,
					AssociationId = a.AssociationId,
					Number = a.Number,
					Organizer = a.Organizer,
					StatusId = a.StatusId
				})
				.FirstOrDefault();
			return (entity);
		}

		public async Task<GeneralPaginationModel<GetCertificationDto>> GetAllAsync(int AssociationId, int Page = 1)
		{
			var total = TEntity.Where(a => a.IsDelete && a.AssociationId == AssociationId).Count();
			var entities = await TEntity.Where(a => !a.IsDelete)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetCertificationDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					Status = a.Status.Title,
					AssociationId = a.AssociationId,
					Number = a.Number,
					Organizer = a.Organizer,
					StatusId = a.StatusId
				}).ToListAsync();
			var res = new GeneralPaginationModel<GetCertificationDto>(total, entities);
			return (res);
		}


		public async Task<GeneralPaginationModel<GetCertificationDto>> GetAllForAdminAsync(int Page = 1)
		{
			var total = TEntity.Where(a => a.IsDelete).Count();
			var entities = await TEntity.Where(a => !a.IsDelete)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetCertificationDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					Status = a.Status.Title,
					AssociationId = a.AssociationId,
					Number = a.Number,
					Organizer = a.Organizer,
					StatusId = a.StatusId,
					RegistrationDate = a.RegistrationDate
					
				}).ToListAsync();
			var res = new GeneralPaginationModel<GetCertificationDto>(total, entities);
			return (res);
		}



		public bool UpdateStatus(int Id, int StatusId)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;
			
			entity.StatusId = StatusId;
			var IsUpdate = _uow.SaveChanges();

			return (IsUpdate > 0);
		}
	}
}
