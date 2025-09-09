using Hiscary.Shared.Domain.ClientModels;
using System.Collections.Immutable;

namespace Hiscary.Stories.Domain;

public sealed record StoryClientQueryableModelWithSortableRules : ClientQueryableModelWithSortableRules
{
    public static ImmutableHashSet<string> SortablePropertyList = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
    {
        "Title",
        "CreatedAt",
        "DateWritten",
    }.ToImmutableHashSet();

    protected override ImmutableHashSet<string> SortableProperties => SortablePropertyList;
}
