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
	public class JournalRepository: GeneralRepository<Journal>, IJournalRepository
	{

		private readonly IMapper _mapper;
		public JournalRepository(IUnitOfWork uow, IMapper mapper) : base(uow)
		{
			_mapper = mapper;
		}

		public int Create(CreateJournalDto Model)
		{
			if (Model == null)
				return 0;
			var entity = _mapper.Map<Journal>(Model);
			if (entity == null)
				return 0;
			var DbEntity = TEntity.Add(entity);
			_uow.SaveChanges();
			if (DbEntity == null || DbEntity.Entity == null || DbEntity.Entity.ID == 0)
				return 0;

			return DbEntity.Entity.ID;
		}

		public bool Update(UpdateJournalDto Model)
		{
			var entity = TEntity.Where(a => a.ID == Model.Id && !a.IsDelete && !a.Association.IsDelete).FirstOrDefault();
			if (entity == null)
				return false;

			entity.PdfUrl = Model.PdfUrl == null ? (entity.PdfUrl) : (Model.PdfUrl);
			entity.NoAndDate = Model.NoAndDate == null ? (entity.NoAndDate) : (Model.NoAndDate);
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

		public GetJournalDto Get(int Id)
		{
			var entity = TEntity.Where(a => a.ID == Id && !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetJournalDto()
				{
					Id = a.ID,
					Name = a.Name,
					NoAndDate = a.NoAndDate,
					PdfUrl = a.PdfUrl,
					AssoiciationId = a.Association.ID
				})
				.FirstOrDefault();
			return (entity);
		}

		public List<GetJournalDto> GetAll(int AssociationId)
		{
			var entity = TEntity.Where(a => a.AssociationId == AssociationId && !a.IsDelete && !a.Association.IsDelete)
				.Select(a => new GetJournalDto()
				{
					Id = a.ID,
					Name = a.Name,
					NoAndDate = a.NoAndDate,
					PdfUrl = a.PdfUrl,
				})
				.ToList();
			return (entity);
		}
	}
}
