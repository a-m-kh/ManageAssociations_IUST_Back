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
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
			newEvent.RegistrationDate = DateTime.Now;
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
			entity.Place = Model.Place == null ? (entity.Place) : (Model.Place);
			entity.Capacity = Model.Capacity == null ? (entity.Capacity) : (Model.Capacity);
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
				Issue = a.IssueID == null ?(""):(a.Issue.Title),
				Period = a.PeriodID == null ?(""):(a.Period.Title),
				TypeOfEvent = a.TypeOfEventID == null ?(""):(a.TypeOfEvent.Title),
				AssociationId = a.AssociationID,
				Price = a.Price,
				Title = a.Title,
				Place = a.Place,
				Capacity = a.Capacity,
				Providers = a.Providers,
				RegistrationDate = a.RegistrationDate,
			}).FirstOrDefaultAsync();
			if (entity == null)
				return null;
			return (entity);

		}

		public List<GetEventDto> GetByName(string name)
		{
			var entity = TEntity.Where(a => !a.IsDelete && a.IsPublic == true && a.IsConfirm == true && a.Title.Contains(name)).Select(a => new GetEventDto()
			{
				Description = a.Description,
				EndTime = a.EndTime,
				ID = a.ID,
				StartTime = a.StartTime,
				ImageUrl = a.ImageUrl,
				Issue = a.Issue == null ? ("") : (a.Issue.Title),
				Period = a.Period == null ? ("") : (a.Period.Title),
				TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
				AssociationId = a.AssociationID,
				Price = a.Price,
				Title = a.Title,
				Place = a.Place,
				Capacity = a.Capacity,
				Providers = a.Providers,
				IsConfirm = a.IsConfirm,
				RegistrationDate = a.RegistrationDate,
				AssociationName = a.association != null ? (a.association.Name) : (""),
			}).ToList();
			if (entity == null)
				return null;
			return (entity);
		}
		public EventViewDto GetById(int Id)
		{
			try
			{
				var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete).Select(a => new EventViewDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					AssociationId = a.AssociationID,
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					IsConfirm = a.IsConfirm,
					RegistrationDate = a.RegistrationDate,
					AssociationName = a.association != null ? (a.association.Name) : (""),
					ReportDto = new GetReportDto()
					{
						Id= a.Report == null ? (0) : (a.Report.ID)
					},
				}).FirstOrDefault();
				if (entity == null)
					return null;
				return (entity);
			}
			catch(Exception ex)
			{
				return null;
			}
			

		}
		public async Task<GeneralPaginationModel<EventViewDto>> GetByAssociationIdAsync(int Id, int Page = 1)
		{
			
			var entityTotal =  TEntity.Where(a => a.association.ID == Id && a.IsDelete).Count();
			var total = entityTotal / 4 + (entityTotal % 4 == 0 ? (0) : (1));

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
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					RegistrationDate = a.RegistrationDate,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			var res = new GeneralPaginationModel<EventViewDto>(total,entities);
			return (res);
		}

		public async Task<GeneralPaginationModel<EventViewDto>> GetLastEvent(int Page = 1)
		{
			var entityTotal = TEntity.Where(a => a.IsPublic && a.IsDelete).Count();
			var total = entityTotal / 4 + (entityTotal % 4 == 0 ? (0) : (1));

			var entities = TEntity.Where(a => !a.IsDelete && a.IsPublic)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new EventViewDto()
			{
				Description = a.Description,
				EndTime = a.EndTime,
				ID = a.ID,
				StartTime = a.StartTime,
				ImageUrl = a.ImageUrl,
				Issue = a.Issue == null ? ("") : (a.Issue.Title),
				Period = a.Period == null ? ("") : (a.Period.Title),
				TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
				Price = a.Price,
				Title = a.Title,
				Place = a.Place,
				Capacity = a.Capacity,
				Providers = a.Providers,
				RegistrationDate =a.RegistrationDate,
				AssociationName = a.association != null ? (a.association.Name) : ("")
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


		public List<GetForUserEventDto> GetAllEventsForUser(int AssociationId) 
		{
			//var total = TEntity.Where(a => a.IsConfirm && a.IsDelete && a.IsPublic && a.AssociationID == AssociationId).Count();
			var entities = TEntity.Where(a => a.IsConfirm == true && !a.IsDelete && a.IsPublic && a.AssociationID == AssociationId)
				.OrderByDescending(a => a.ID)
				.Select(a => new GetForUserEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					Id = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			//var res = new GeneralPaginationModel<GetForUserEventDto>(total, entities);
			return (entities);
		}


		public List<GetForUserEventDto> GetAllEventsForUser()
		{
			//var total = TEntity.Where(a => a.IsConfirm && a.IsDelete && a.IsPublic && a.AssociationID == AssociationId).Count();
			var entities = TEntity.Where(a => a.IsConfirm == true && !a.IsDelete && a.IsPublic)
				.OrderByDescending(a => a.ID)
				.Select(a => new GetForUserEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					Id = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			//var res = new GeneralPaginationModel<GetForUserEventDto>(total, entities);
			return (entities);
		}




		public GeneralPaginationModel<GetEventDto> GetAllEventsForAdmin(int AssociationId, int Page = 1)
		{
			var entityTotal = TEntity.Where(a =>  !a.IsDelete && a.AssociationID == AssociationId).Count();
			var total = entityTotal / 4 + (entityTotal % 4 == 0 ? (0) : (1));
			var entities = TEntity.Where(a => !a.IsDelete && a.AssociationID == AssociationId)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					IsPublic = a.IsPublic,
					IsConfirm = a.IsConfirm,
					RegistrationDate = a.RegistrationDate,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			var res = new GeneralPaginationModel<GetEventDto>(total, entities);
			return (res);
		}

		public bool ChangePublic(int Id)
		{
			var entity  = TEntity.Where(a => !a.IsDelete).FirstOrDefault();
			if(entity == null)
			{
				return false;
			}
			entity.IsPublic = !entity.IsPublic;
			var isSave = _uow.SaveChanges();
			return isSave > 0;
		}

		public bool ChangeConfirm(int Id, bool? ConfirmStatus)
		{
			var entity = TEntity.Where(a => !a.IsDelete).FirstOrDefault();
			if (entity == null)
			{
				return false;
			}
			entity.IsConfirm = ConfirmStatus;
			var isSave = _uow.SaveChanges();
			return isSave > 0;
		}

		public GeneralPaginationModel<GetEventDto> GetAllEventsForSuperAdmin(int Page = 1)
		{
			var entityTotal = TEntity.Where(a => !a.IsDelete).Count();
			var total = entityTotal / 4 + (entityTotal % 4 == 0 ? (0) : (1));

			var entities = TEntity.Where(a => !a.IsDelete )
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					IsPublic = a.IsPublic,
					RegistrationDate =a.RegistrationDate,
					AssociationName =a.association != null ?( a.association.Name):("")
				}).ToList();
			var res = new GeneralPaginationModel<GetEventDto>(total, entities);
			return (res);
		}


		public GeneralPaginationModel<GetEventDto> PastEvent(int AssociationId,int Page = 1)
		{
			var date = DateTime.Now;
			var entityTotal = TEntity.Where(a => !a.IsDelete && a.AssociationID == AssociationId && a.EndTime < date).Count();
			var total = entityTotal / 4 + (entityTotal % 4 == 0 ? (0) : (1));

			var entities = TEntity.Where(a => !a.IsDelete && a.AssociationID == AssociationId && a.EndTime < date)
				.OrderByDescending(a => a.ID)
				.Skip((Page - 1) * 4)
				.Take(4).Select(a => new GetEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					IsPublic = a.IsPublic,
					RegistrationDate = a.RegistrationDate,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			var res = new GeneralPaginationModel<GetEventDto>(total, entities);
			return (res);
		}




		public List<GetEventDto> PastEventForUser()
		{
			var date = DateTime.Now;
			var entityTotal = TEntity.Where(a => !a.IsDelete && a.EndTime < date && a.IsPublic && a.IsConfirm == true).Count();
			var entities = TEntity.Where(a => !a.IsDelete && a.EndTime < date)
				.OrderByDescending(a => a.ID).Select(a => new GetEventDto()
				{
					Description = a.Description,
					EndTime = a.EndTime,
					ID = a.ID,
					StartTime = a.StartTime,
					ImageUrl = a.ImageUrl,
					Issue = a.Issue == null ? ("") : (a.Issue.Title),
					Period = a.Period == null ? ("") : (a.Period.Title),
					TypeOfEvent = a.TypeOfEvent == null ? ("") : (a.TypeOfEvent.Title),
					Price = a.Price,
					Title = a.Title,
					Place = a.Place,
					Capacity = a.Capacity,
					Providers = a.Providers,
					AssociationId = a.AssociationID,
					IsPublic = a.IsPublic,
					RegistrationDate = a.RegistrationDate,
					AssociationName = a.association != null ? (a.association.Name) : ("")
				}).ToList();
			return (entities);
		}


	}
}
