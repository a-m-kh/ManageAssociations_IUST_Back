using DataBase.Configuration.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IAssociationMemberRepository
	{
		int Create(CreateAssociationMemberDto Model);
		bool Update(UpdateAssociationMemberDto Model);
		bool Delete(int id);
		GetAssociationMemberDto Get(int id);
		List<GetAssociationMemberDto> GetAll(int AssociationId);
	}
}
