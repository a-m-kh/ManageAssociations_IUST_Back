namespace Repository.Models;

public interface IEntityWithTypedId<TId> {
	TId ID { get; }
}
