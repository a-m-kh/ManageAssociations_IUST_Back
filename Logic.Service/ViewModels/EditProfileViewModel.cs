using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels;

public class EditProfileViewModel
{
	public string? Email { get; set; }
	public string? PhoneNumber { get;set; }
}


public class ChangePassword
{
	[Required]
	public string NewPassword { get; set; }
	[Required]
	public int AssociationId { get; set; }
	[Required]
	public string CurrentPassword { get; set; }
}