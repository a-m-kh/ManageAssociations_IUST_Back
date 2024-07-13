using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class CertificationDto
	{
	}

	public class CertificationDtoBase
	{
		public string? Title { get; set; }
		public virtual string? Organizer { get; set; }
		public virtual string? ExcelUrl { get; set; }
		public virtual int? Number { get; set; }
	}

	public class GetCertificationDto : CertificationDtoBase
	{
		public int Id { get; set; }
		public int AssociationId { get; set; }
		public string Status { get; set; }
		public int StatusId { get; set; }
		public DateTime RegistrationDate { get; set; }
		public int DayCount { get; set; }
		public string Tarikh { get; set; }
	}

	public class CreateCertificationDto 
	{
		public string Title { get; set; }
		public int AssociationId { get; set; }
		public int StatusId { get; set; }
		public  string Organizer { get; set; }
		public  DateTime RegistrationDate { get; set; }
		public  string ExcelUrl { get; set; }
		public  int Number { get; set; }
		public int DayCount { get; set; }
		public string Tarikh { get; set; }
	}

	public class UpdateCertificationDto : CertificationDtoBase
	{
		public int Id { get; set; }
		public int? DayCount { get; set; }
		public string? Tarikh { get; set; }
		//public int? StatusId { get; set; }
	}

}
