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

namespace DataBase.Repository.Repositories
{
	public class EventRepository: GeneralRepository<Event>,IEventRepository
	{
		private readonly IMapper _mapper;
		public EventRepository(IUnitOfWork uow , IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public async Task<int> CreateAsync(EventCreateDto Model)
		{
			if (Model == null)
				return 0; 

			var newEvent = _mapper.Map<Event>(Model);
			if (newEvent == null)
				return 0;

			var DbEntity = await TEntity.AddAsync(newEvent);
			await _uow.SaveChangesAsync();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;
			

			return (DbEntity.Entity.ID);
		}

		public async Task<bool> UpdateAsync(EventUpdateDto Model)
		{
			if (Model == null)
				return false;
			var entity = await TEntity.Where(a => a.ID == Model.ID && !a.IsDelete).FirstOrDefaultAsync();
			if(entity == null)
				return false;

			entity.PeriodID = Model.PeriodID == null ? (entity.PeriodID) : (Model.PeriodID ?? 0);
			entity.TypeOfEventID = Model.TypeOfEventID == null ? (entity.TypeOfEventID) : (Model.TypeOfEventID ?? 0);
			entity.IssueID = Model.IssueID == null ? (entity.IssueID) : (Model.IssueID ?? 0);
			entity.Title = Model.Title == null ? (entity.Title) : (Model.Title);
			entity.Price = Model.Price == null ? (entity.Price) : (Model.Price ?? 0);
			entity.StartTime = Model.StartTime == null ? (entity.StartTime) : (Model.StartTime ?? DateTime.Now);
			entity.EndTime = Model.EndTime == null ? (entity.EndTime) : (Model.EndTime ?? DateTime.Now);
			entity.ImageUrl = Model.ImageUrl == null ? (entity.ImageUrl) : (Model.ImageUrl);
			entity.Description = Model.Description == null ? (entity.Description) : (Model.Description);

			var IsUpdate = await _uow.SaveChangesAsync();

			return(IsUpdate > 0);

		}

		public async Task<bool> DeleteAsync(int Id)
		{
			var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefaultAsync();
			if(entity == null) return false;
			entity.IsDelete = true;
			var IsUpdate = await _uow.SaveChangesAsync();
			return(IsUpdate > 0);
		}

		public async Task<EventViewDto> GetByIdAsync(int Id)
		{
			var entity = await TEntity.Where(a => a.ID == Id && !a.IsDelete).Select(a=>new EventViewDto()
			{
				Description = a.Description,
				EndTime = a.EndTime,
				ID= a.ID,
				StartTime = a.StartTime,
				ImageUrl = a.ImageUrl,
				Issue = a.Issue == null ?(""):(a.Issue.Title),
				Period = a.Period == null ?(""):(a.Period.Title),
				TypeOfEvent = a.TypeOfEvent == null ?(""):(a.TypeOfEvent.Title),
				AssociationId = a.AssociationID,
				Price = a.Price,
				Title = a.Title
			}).FirstOrDefaultAsync();
			if (entity == null)
				return null;
			return (entity);

		}

		public async Task<GeneralPaginationModel<EventViewDto>> GetByAssociationIdAsync(int Id, int Page = 1)
		{
			
			var total =  TEntity.Where(a => a.association.ID == Id && a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.association.ID == Id)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new EventViewDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					//Issue = a.Issue == null ? ("") : (a.Issue.Title),
					//Period = a.Period == null ? ("") : (a.Period.Title),
					//TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title
				}).ToList();
			var res = new GeneralPaginationModel<EventViewDto>(total,entities);
			return (res);
		}

		public async Task<GeneralPaginationModel<EventViewDto>> GetLastEvent(int Page = 1)
		{
			var total = TEntity.Where(a => a.IsConfirm && a.IsDelete).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.IsConfirm)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new EventViewDto()
			{
				Description = a.Description,
				EndTime = a.EndTime,
				ID = a.ID,
				StartTime = a.StartTime,
				ImageUrl = a.ImageUrl,
				//Issue = a.Issue == null ? ("") : (a.Issue.Title),
				//Period = a.Period == null ? ("") : (a.Period.Title),
				//TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
				Price = a.Price,
				Title = a.Title
			}).ToList();
			var res = new GeneralPaginationModel<EventViewDto>(total, entities);
			return (res);
		}
		public bool ConfirmEvent(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;
			entity.IsConfirm = true;
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}

		public bool ChangePublicState(int Id, bool IsPublic)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;
			entity.IsPublic = IsPublic;
			var IsUpdate = _uow.SaveChanges();
			return (IsUpdate > 0);
		}
	}
}
