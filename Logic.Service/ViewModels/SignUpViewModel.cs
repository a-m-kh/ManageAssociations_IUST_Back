using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.ViewModels;

public class SignUpViewModel
{
	[Required(ErrorMessage ="لطفا برای کاربر، نام کاربری انتخاب کنید.")]
	public string UserName { get; set; }
	[Required(ErrorMessage = "لطفا برای کاربر، رمز انتخاب کنید.")]
	public string Password { get; set; }
	public string Email { get; set; }
	public string? PhoneNumber { get; set; }
}
