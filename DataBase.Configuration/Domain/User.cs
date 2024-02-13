using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Configuration.Domain;

public class User : IdentityUser<string>
{
	[InverseProperty(nameof(DataBase.Configuration.Domain.Association.Admin))]
	public Association? Association { get; set; }
}
