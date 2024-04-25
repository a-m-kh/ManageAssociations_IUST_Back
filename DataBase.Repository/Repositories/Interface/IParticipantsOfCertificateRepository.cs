using DataBase.Configuration.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IParticipantsOfCertificateRepository
	{
		public bool Create(List<ParticipantsOfCertificate> participantsOfCertificates);
	}
}
