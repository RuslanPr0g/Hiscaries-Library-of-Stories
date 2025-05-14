using Enterprise.Domain.DataAccess;
using Enterprise.Domain.ReadModels;

public abstract class BaseReadRepository<TReadModel> : IBaseReadRepository<TReadModel>
    where TReadModel : IReadModel
{
}