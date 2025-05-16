using Enterprise.Domain.ReadModels;

namespace Enterprise.Domain.DataAccess;

public interface IBaseReadRepository<TReadModel>
    where TReadModel : IReadModel
{
}
