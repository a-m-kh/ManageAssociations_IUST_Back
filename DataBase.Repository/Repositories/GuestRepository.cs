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
	public class GuestRepository: GeneralRepository<Guest> , IGuestRepository
	{
		private readonly IMapper _mapper;

		public GuestRepository(IUnitOfWork uow, IMapper mapper):base(uow) 
		{
			_mapper = mapper;
		}
		public int Create(CreateGuestDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<Guest>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateGuestDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.ImageUrl = Model.ImageUrl == null ? (entity.ImageUrl) : (Model.ImageUrl);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title);
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

		public GetGuestDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Event.IsDelete)
				.Select(a => new GetGuestDto()
				{
					Name = a.Name,
					Id = a.ID,
					Title = a.Title,
					ImageUrl = a.ImageUrl,
					AssociationId = a.Event.AssociationID
				})
				.FirstOrDefault();
			return (entity);
		}

		public List<GetGuestDto> GetAll_WithPublic(int EventId)
		{
			var entity = TEntity.Where(a => a.EventId == EventId && !a.IsDelete && !a.Event.IsDelete && a.Event.IsPublic)
				.Select(a => new GetGuestDto()
				{
					Name = a.Name,
					Id = a.ID,
					Title = a.Title,
					ImageUrl = a.ImageUrl,
				})
				.ToList();
			return (entity);
		}

		public List<GetGuestDto> GetAll(int EventId)
		{
			var entity = TEntity.Where(a => a.EventId == EventId && !a.IsDelete && !a.Event.IsDelete)
				.Select(a => new GetGuestDto()
				{
					Name = a.Name,
					Id = a.ID,
					Title = a.Title,
					ImageUrl = a.ImageUrl,
				})
				.ToList();
			return (entity);
		}
	}
}
