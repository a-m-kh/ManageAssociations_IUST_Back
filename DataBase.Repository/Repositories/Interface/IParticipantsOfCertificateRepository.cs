using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
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
		List<ParticipantsOfCertificate> Get(int certificationId);
		List<CreateParticipantsOfCertificateDto> GetById(int Id);
	}
}
