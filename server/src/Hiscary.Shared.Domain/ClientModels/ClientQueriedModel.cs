namespace Hiscary.Shared.Domain.ClientModels;

// TODO: can be extracted to StackNucleus

public sealed class ClientQueriedModel<T> where T : class
{
    public static ClientQueriedModel<T> Empty = ClientQueriedModel<T>.Create([], 0);

    public required IEnumerable<T> Items { get; set; }

    public required int TotalItemsCount { get; set; }

    public int? ItemsPerCurrentPageCount { get; set; } = null;

    public static ClientQueriedModel<T> Create(IEnumerable<T> items, int totalCount)
    {
        return new ClientQueriedModel<T>()
        {
            Items = items,
            TotalItemsCount = totalCount
        };
    }

    public static ClientQueriedModel<T> Create(IEnumerable<T> items, int totalCount, int itemsPerPage)
    {
        return new ClientQueriedModel<T>()
        {
            Items = items,
            TotalItemsCount = totalCount,
            ItemsPerCurrentPageCount = itemsPerPage
        };
    }
}
