using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses
{
	internal class CertificateOfAbsenceResponse
	{
	}

	public class GetCertificateOfAbsenceResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public string CourseName { get; set; }
		public string ProfessorName { get; set; }
		public string Reason { get; set; }
		public string ExcelUrl { get; set; }
		public DateTime AbsenceDate { get; set; }
	}

	public class GetListCertificateOfAbsenceResponse
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime RegistrationDate { get; set; }
		public string CourseName { get; set; }
		public string Status { get; set; }
	}
}
