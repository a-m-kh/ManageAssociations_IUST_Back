using DataBase.Configuration.Domain;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;

namespace DataBase.Repository.Repositories
{
	public class CertificateOfAbsenceRepository:GeneralRepository<CertificateOfAbsence>, ICertificateOfAbsenceRepository
	{
		private readonly IMapper _mapper;
		public CertificateOfAbsenceRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
		{
			_mapper= mapper;
		}

		public int Create(CreateCertificateOfAbsenceDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<CertificateOfAbsence>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateCertificateOfAbsenceDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.AbsenceDate = Model.AbsenceDate == null ? (entity.AbsenceDate) : (Model.AbsenceDate);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title);
			entity.ExcelUrl = Model.ExcelUrl == null ? (entity.ExcelUrl) : (Model.ExcelUrl);
			entity.CourseName = Model.CourseName ?? entity.CourseName;
			entity.ProfessorName = Model.ProfessorName ?? entity.ProfessorName;
			entity.Reason = Model.Reason ?? entity.Reason;
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

		public GetCertificateOfAbsenceDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete)
				.Select(a => new GetCertificateOfAbsenceDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					AbsenceDate = a.AbsenceDate,
					CourseName = a.CourseName,
					ProfessorName = a.ProfessorName,
					Reason = a.Reason,
					RegistrationDate = a.RegistrationDate,
					Status = a.Status != null ?(a.Status.Title):(""),
					StatusId = a.StatusId,
					AssociationId = a.AssociationId
				})
				.FirstOrDefault();
			return (entity);
		}

		public GeneralPaginationModel<GetCertificateOfAbsenceDto> GetAll(int AssociationId, int Page = 1)
		{
			var total = TEntity.Where(a => !a.IsDelete && a.AssociationId == AssociationId).Count();
			var entities =  TEntity.Where(a => !a.IsDelete)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetCertificateOfAbsenceDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					AbsenceDate = a.AbsenceDate,
					CourseName = a.CourseName,
					ProfessorName = a.ProfessorName,
					Reason = a.Reason,
					RegistrationDate = a.RegistrationDate,
					Status = a.Status != null ? (a.Status.Title) : (""),
					StatusId = a.StatusId,
					AssociationId = a.AssociationId
				}).ToList();
			var res = new GeneralPaginationModel<GetCertificateOfAbsenceDto>(total, entities);
			return (res);
		}

		public bool ChangeStatus(int Id, int StatusId)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.StatusId = StatusId;
			var IsUpdate = _uow.SaveChanges();

			return (IsUpdate > 0);
		}

		public GeneralPaginationModel<GetCertificateOfAbsenceDto> GetAllForAdmin(int Page = 1)
		{
			var total = TEntity.Where(a => !a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetCertificateOfAbsenceDto()
				{
					ExcelUrl = a.ExcelUrl,
					Id = a.ID,
					Title = a.Title,
					AbsenceDate = a.AbsenceDate,
					CourseName = a.CourseName,
					ProfessorName = a.ProfessorName,
					Reason = a.Reason,
					RegistrationDate = a.RegistrationDate,
					Status = a.Status != null ? (a.Status.Title) : (""),
					StatusId = a.StatusId,
					AssociationId = a.AssociationId,
					AssociationName = a.Association != null ?(a.Association.Name):("")
				}).ToList();
			var res = new GeneralPaginationModel<GetCertificateOfAbsenceDto>(total, entities);
			return (res);
		}
	}
}
