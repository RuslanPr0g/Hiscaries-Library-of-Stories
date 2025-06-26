namespace Hiscary.Shared.Api.Rest.Requests;

public sealed class SortableRequest
{
    public string? Property { get; set; }
    public bool Ascending { get; set; } = true;
}