using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBase.Configuration.Domain;

namespace DataBase.Repository.Repositories.Interface
{
    public interface IParticipantsOfCertificateOfAbsenceRepository
    {
	    public bool Create(List<ParticipantOfCertificateOfAbsence> participantsOfCertificates);
	}
}
