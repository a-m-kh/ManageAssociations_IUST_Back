using DataBase.Configuration.Domain;
using DataBase.Repository.Repositories.Interface;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories
{
	public class ParticipantsOfCertificateOfAbsenceRepository : GeneralRepository<ParticipantOfCertificateOfAbsence>, IParticipantsOfCertificateOfAbsenceRepository
	{
		public ParticipantsOfCertificateOfAbsenceRepository(IUnitOfWork uow) : base(uow)
		{
		}

		public bool Create(List<ParticipantOfCertificateOfAbsence> participantsOfCertificates)
		{
			TEntity.AddRange(participantsOfCertificates);
			var status = _uow.SaveChanges();
			if (status > 0)
			{
				return true;
			}
			return false;
		}
	}
}
