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
	public class AssociationMemberRepository:GeneralRepository<AssociationMember> , IAssociationMemberRepository
	{
		private readonly IMapper _mapper;
		public AssociationMemberRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
		{
			_mapper = mapper;	
		}

		public int Create(CreateAssociationMemberDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<AssociationMember>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateAssociationMemberDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.ImageUrl = Model.ImageUrl == null ? (entity.ImageUrl) : (Model.ImageUrl);
			entity.Role = Model.Role == null ? (entity.Role) : (Model.Role);
			entity.Name = Model.Name ?? entity.Name;
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

		public GetAssociationMemberDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetAssociationMemberDto()
				{
					Id = a.ID,
					Name = a.Name,
					Role = a.Role,
					ImageUrl = a.ImageUrl,
					AssoiciationId = a.Association.ID
				})
				.FirstOrDefault();
			return (entity);
		}

		public List<GetAssociationMemberDto> GetAll(int AssociationId)
		{
			var entity = TEntity.Where(a =>a.AssociationId == AssociationId &&  !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetAssociationMemberDto()
				{
					Id = a.ID,
					Name = a.Name,
					Role = a.Role,
					ImageUrl = a.ImageUrl,
				})
				.ToList();
			return (entity);
		}
	}
}
