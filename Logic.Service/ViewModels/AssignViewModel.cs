using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels;

public class AssignViewModel
{
	[Required(ErrorMessage ="لطفا کاربر مورد نظر را انتخاب نمایید")]
	public string UserId { get; set; }
	[Required(ErrorMessage ="لطفا انجمن مورد نظر را انتخاب نمایید")]
	public int AssociationId { get; set; }
}
