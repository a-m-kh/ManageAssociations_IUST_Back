using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Dtos
{
	internal class CertificateOfAbsenceDto
	{
	}

	public class CertificateOfAbsenceDtoBase
	{
		public string Title { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string CourseName { get; set; }
		public string ProfessorName { get; set; }
		public string Reason { get; set; }
		public string ExcelUrl { get; set; }
		public DateTime AbsenceDate { get; set; }
	}

	public class GetCertificateOfAbsenceDto : CertificateOfAbsenceDtoBase
	{
		public int Id { get; set; }
		public string Status { get; set; }
		public int StatusId { get; set; }
		public int AssociationId { get; set; }
	}

	public class CreateCertificateOfAbsenceDto : CertificateOfAbsenceDtoBase
	{
		public int StatusId { get; set; }
	}

	public class UpdateCertificateOfAbsenceDto : CertificateOfAbsenceDtoBase
	{
		public int Id { get; set; }
	}

}
