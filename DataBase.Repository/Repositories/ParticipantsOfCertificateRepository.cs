using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories
{
	public class ParticipantsOfCertificateRepository :GeneralRepository<ParticipantsOfCertificate>, IParticipantsOfCertificateRepository
	{
		public ParticipantsOfCertificateRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public bool Create(List<ParticipantsOfCertificate> participantsOfCertificates)
		{
			 TEntity.AddRange(participantsOfCertificates);
			var status = _uow.SaveChanges();
			if(status > 0)
			{
				return true;
			}
			return false;
		}
	}
}
