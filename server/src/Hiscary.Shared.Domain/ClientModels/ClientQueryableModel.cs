using System.Collections.Immutable;

namespace Hiscary.Shared.Domain.ClientModels;

// TODO: can be extracted to StackNucleus
public record ClientQueryableModel
{
    public required int StartIndex { get; set; }
    public required int ItemsCount { get; set; }
    public required string SortProperty { get; set; }
    public required bool SortAsc { get; set; }
}

public abstract record ClientQueryableModelWithSortableRules : ClientQueryableModel
{
    protected abstract ImmutableHashSet<string> SortableProperties { get; }
}