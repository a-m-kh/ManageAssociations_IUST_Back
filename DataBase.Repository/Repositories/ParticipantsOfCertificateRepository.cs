using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
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


		public List<ParticipantsOfCertificate> Get(int certificationId)
		{
			return (TEntity.Where(a => a.CertificationId == certificationId).ToList());
		}



		public List<CreateParticipantsOfCertificateDto> GetById(int Id)
		{
			return TEntity.Where(a => a.ID == Id).Select(a => new CreateParticipantsOfCertificateDto()
			{
				CertificationId = a.CertificationId,
				Sex = a.Sex,
				Email = a.Email,
				FirstName = a.FirstName,
				LastName = a.LastName,
				NationalCode = a.NationalCode,
				Phone = a.Phone,
				TitleOfCertificate = a.Certification.Title,
				TypeOfCooperation = a.TypeOfCooperation
			}).ToList();
		}
	}
}
