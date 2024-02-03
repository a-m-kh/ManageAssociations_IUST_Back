namespace Repository.Models;
 
public class EntityWithTypedId<TId> : IEntityWithTypedId<TId> {
	public TId ID { get; set; }
}
