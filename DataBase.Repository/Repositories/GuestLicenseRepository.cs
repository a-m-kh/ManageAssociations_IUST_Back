using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;
using AutoMapper;
using DataBase.Configuration.Domain;

namespace DataBase.Repository.Repositories
{
	public class GuestLicenseRepository: GeneralRepository<GuestLicense>, IGuestLicenseRepository
	{
		private readonly IMapper _mapper;
		
		public GuestLicenseRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public int Create(CreateGuestLicenseDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<GuestLicense>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateGuestLicenseDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.Association.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.JobTitle = Model.JobTitle ?? entity.JobTitle;
			entity.Name = Model.Name ?? entity.Name;
			entity.IsGuest = Model.IsGuest ?? entity.IsGuest;
			entity.IsFromIust = Model.IsFromIust ?? entity.IsFromIust;
			entity.NationalCode = Model.NationalCode ?? entity.NationalCode;
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.IsDelete = true;
			var status = _uow.SaveChanges();
			return status > 0;
		}

		public GetGuestLicenseDto GetGuestLicense(int Id)
		{
			var res = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetGuestLicenseDto()
			{
				IsFromIust = a.IsFromIust,
				IsGuest = a.IsGuest,
				JobTitle = a.JobTitle,
				Name = a.Name,
				NationalCode = a.NationalCode,
				RegistrationDate = a.RegistrationDate,
				Status = a.Status != null ?(a.Status.Title):(""),
				StatusId = a.StatusId,
				AssociationId = a.AssociationId
			}).FirstOrDefault();
			return res;
		}

		public bool ChangeStatus(int Id, int StatusId)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			entity.StatusId = StatusId;
			var status = _uow.SaveChanges();
			return status > 0;
		}


		public GeneralPaginationModel<GetGuestLicenseDto> GetAll(int Page, int AssociationId)
		{
			var total = TEntity.Where(a => a.AssociationId == AssociationId && !a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.AssociationId == AssociationId)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetGuestLicenseDto()
				{
					AssociationId = a.AssociationId,
					Name = a.Name,
					RegistrationDate = a.RegistrationDate,
					Status = a.Status != null ?(a.Status.Title):(""),
					Id = a.ID
				}).ToList();
			var res = new GeneralPaginationModel<GetGuestLicenseDto>(total, entities);
			return (res);
		}



		public GeneralPaginationModel<GetGuestLicenseDto> GetAllForSuperAdmin(int Page)
		{
			var total = TEntity.Where(a =>   !a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete )
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetGuestLicenseDto()
				{
					AssociationId = a.AssociationId,
					Name = a.Name,
					RegistrationDate = a.RegistrationDate,
					Status = a.Status != null ? (a.Status.Title) : (""),
					Id = a.ID,
					AssociationName = a.Association != null ?(a.Association.Name):("")
				}).ToList();
			var res = new GeneralPaginationModel<GetGuestLicenseDto>(total, entities);
			return (res);
		}


	}
}
