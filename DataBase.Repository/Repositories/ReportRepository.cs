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

namespace DataBase.Repository.Repositories
{
	public class ReportRepository: GeneralRepository<Report>, IReportRepository
	{
		private readonly IMapper _mapper;


		

		public ReportRepository(IUnitOfWork uow,IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public int Create(CreateReportDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<Report>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateReportDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id  && !a.Event.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.ReflectionLink = Model.ReflectionLink ?? entity.ReflectionLink;
			entity.ApplicationLevel = Model.ApplicationLevel ?? entity.ApplicationLevel;
			entity.DetailsAndPanels = Model.DetailsAndPanels ?? entity.DetailsAndPanels;
			entity.AcademicLevelAndBackGround = Model.AcademicLevelAndBackGround ?? entity.AcademicLevelAndBackGround;
			entity.AssociateCollections = Model.AssociateCollections ?? entity.AssociateCollections;
			entity.ExecutiveColleagues = Model.ExecutiveColleagues ?? entity.ExecutiveColleagues;
			entity.HoldingPeriod = Model.HoldingPeriod ?? entity.HoldingPeriod;
			entity.RoleOfTeachers = Model.RoleOfTeachers ?? entity.RoleOfTeachers;
			entity.WelcomeRate = Model.WelcomeRate ?? entity.WelcomeRate;
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool Delete(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id).FirstOrDefault();
			if (entity == null)
				return false;

			TEntity.Remove(entity);
			var status = _uow.SaveChanges();
			return status > 0;
		}

		public GetReportDto Get(int Id)
		{
			var res = TEntity.Where(a => a.ID == Id).Select(a => new GetReportDto()
			{
				Id = a.ID,
				AcademicLevelAndBackGround = a.AcademicLevelAndBackGround,
				ApplicationLevel = a.ApplicationLevel,
				AssociateCollections = a.AssociateCollections,
				DetailsAndPanels = a.DetailsAndPanels,
				ExecutiveColleagues = a.ExecutiveColleagues,
				HoldingPeriod = a.HoldingPeriod,
				ReflectionLink = a.ReflectionLink,
				RoleOfTeachers = a.RoleOfTeachers,
				WelcomeRate = a.WelcomeRate,
				AssociationId = a.Event.AssociationID,
				ImagesUrl = a.Images.Select(a => a.Url).ToList(),
			}).FirstOrDefault();
			return res;
		}
	}
}
