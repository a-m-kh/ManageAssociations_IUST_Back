using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface ICommunicationRepository
	{
		int Create(CreateCommunicationDto Model);
		bool Update(UpdateCommunicationDto Model);
		bool Delete(int id);
		GetCommunicationDto Get(int id);
		List<GetCommunicationDto> GetAll(int AssociationId);
	}
}
