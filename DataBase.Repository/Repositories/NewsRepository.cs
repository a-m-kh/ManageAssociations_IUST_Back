using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using DataBase.Repository.Repositories.Interface;
using AutoMapper;

namespace DataBase.Repository.Repositories
{
	public class NewsRepository:GeneralRepository<News>, INewsRepository
	{
		private readonly IMapper _mapper;
		public NewsRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public int Create(CreateNewsDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<News>(Model);
			entity.RegistrationDate = DateTime.UtcNow;
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateNewsDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.Association.IsDelete && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.Description = Model.Description ?? entity.Description;
			entity.Title = Model.Title ?? entity.Title;
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

		public GetNewsDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			if(entity == null)
			{
				return null;
			}
			else
			{
				entity.Views += 1;
				_uow.SaveChanges();
			}

			var res = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).Select(a => new GetNewsDto()
			{
				Id = a.ID,
				Description = a.Description,
				Title = a.Title,
				Images = a.Images.Select(I=> new ImageDto()
				{
					Id = I.ID,
					Url = I.Url
				}).ToList(),
				Status =a.Status !=null ? a.Status.Title : (""),
				IsPublic = a.IsActive,
				RegistrationDate = a.RegistrationDate,
				Views = a.Views,
			}).FirstOrDefault();



			return res;
		}


		public GetNewsDto Get_NotViews(int Id)
		{
			
			var res = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).Select(a => new GetNewsDto()
			{
				Id = a.ID,
				Description = a.Description,
				Title = a.Title,
				Images = a.Images.Select(I => new ImageDto()
				{
					Id = I.ID,
					Url = I.Url
				}).ToList(),
				Status = a.Status != null ? a.Status.Title : (""),
				IsPublic = a.IsActive,
				RegistrationDate = a.RegistrationDate,
				Views = a.Views,
				AssociationId = a.AssociationId
			}).FirstOrDefault();



			return res;
		}



		public GeneralPaginationModel<GetNewsDto> GetForAdmin(int Page, int AssociationId)
		{
			var Finally = new GeneralPaginationModel<GetNewsDto>();

			var total =  TEntity.Where(a => a.Association.ID == AssociationId && !a.IsDelete && !a.Association.IsDelete).Count();
			
			var entities = TEntity.Where(a => a.Association.ID == AssociationId && !a.IsDelete && !a.Association.IsDelete)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetNewsDto()
				{
					Description = a.Description,
					Id = a.ID,
					Images = a.Images.Select(I=> new ImageDto()
					{
						Id = I.ID,
						Url = I.Url
					}).ToList(),
					IsPublic = a.IsActive,
					Status = a.Status.Title,
					RegistrationDate = a.RegistrationDate == null ? (DateTime.Now) : a.RegistrationDate,
					Views = a.Views,
					Title = a.Title,
				}).ToList();
			Finally.Total = total;
			Finally.Values = entities;
			return Finally;
		}

		public bool ChangeStatus(int Id, int StatusId)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			if (entity == null)
			{
				return false;
			}
			else
			{
				entity.StatusId = StatusId;
				var status = _uow.SaveChanges();
				return status > 0;
			}
		}

		public bool ChangePublic(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			if (entity == null)
			{
				return false;
			}
			else
			{
				entity.IsActive = !entity.IsActive;
				var status = _uow.SaveChanges();
				return status > 0;
			}
		}
	}
}
