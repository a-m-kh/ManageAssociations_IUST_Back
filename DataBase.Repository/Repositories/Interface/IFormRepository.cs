using DataBase.Configuration.Domain;
using DataBase.Configuration.Dtos;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository.Repositories.Interface
{
	public interface IFormRepository : IGeneralRepository<Form>
	{
		public int Create(string Url, string Title);
		public bool Update(FormUpdateDto EDto);
		public bool Delete(int Id);
		public List<Form> GetAll();
	}
}
