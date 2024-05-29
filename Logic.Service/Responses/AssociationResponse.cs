using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logic.Service.Responses;

internal class AssociationResponse
{
}

public class AssociationResponseBase
{
	public int? Id { get; set; }
	public string? Name { get; set; }
	public string? LogoUrl { get; set; }
	public string? Phone { get; set; }
	public string? Email { get; set; }
	public string? Address { get; set; }
}

public class GetAssociationResponse: AssociationResponseBase
{
	public string AdminUserName{ get; set; }
}
