using System;

namespace Repository.Models;


public abstract class AuditEntityWithTypedId: Entity {

	public int? CreatedBy { set; get; }
	public int? ModifiedBy { set; get; }
	public DateTime? CreateDateTime { set; get; }
	public DateTime? LastModifiedDateTime { set; get; }
}
