using Repository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain
{
	public class Event: EntityWithTypedId<int>
	{
		//public string Name { get; set; }
		public DateTime StartTime { get; set; }
		public DateTime EndTime { get; set; }	
		public bool IsConfirm { get; set; }
		public string Description { get; set; }
		public int AssociationID { get; set; }
		public int Price { get;set; }
		public string Title { get; set; }
		public int TypeOfEventID { get; set; }
		public int IssueID { get; set; }
		public string ImageUrl { get; set; }
		public int PeriodID { get; set; }
		public bool IsDelete { get; set; }=false;
		public Association association { get; set; }
		public BaseInfo period { get; set; }
		public BaseInfo typeOfEvent { get; set; }
		public BaseInfo issue { get; set; }
	}
}
