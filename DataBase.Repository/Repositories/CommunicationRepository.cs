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
	public class CommunicationRepository : GeneralRepository<Communication>, ICommunicationRepository 
	{

		private readonly IMapper _mapper;

		public CommunicationRepository(IUnitOfWork uow, IMapper mapper) : base(uow) 
		{
			_mapper = mapper;
		}
		public int Create(CreateCommunicationDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<Communication>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateCommunicationDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.Link = Model.Link == null ? (entity.Link) : (Model.Link);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title);
			entity.TypeOfLinkId = Model.TypeOfLinkId ??  entity.TypeOfLinkId;
			var IsUpdate =  _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool Delete(int Id)
		{
			var entity =  TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.IsDelete = true;

			var IsUpdate =  _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public GetCommunicationDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete)
				.Select(a => new GetCommunicationDto()
				{
					Id = a.ID,
					Link = a.Link,
					Title = a.Title,
					TypeOfLink = a.TypeOfLink.Title,
					association = new AssociationViewDto()
					{
						ID = a.Association.ID,
						AdminId = a.Association.AdminID
					}
					
				})
				.FirstOrDefault();
			return (entity);
		}

		public List<GetCommunicationDto> GetAll(int AssociationId)
		{
			var entity = TEntity.Where(a => a.AssociationId == AssociationId && !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetCommunicationDto()
				{
					Id = a.ID,
					Link = a.Link,
					Title = a.Title,
					TypeOfLink = a.TypeOfLink.Title,
					association = new AssociationViewDto()
					{
						ID = a.Association.ID,
						AdminId = a.Association.AdminID
					}

				})
				.ToList();
			return (entity);
		}
	}
}
