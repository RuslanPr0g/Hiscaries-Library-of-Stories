public sealed class JobConfiguration
{
    public string Key { get; set; }
    public Type Type { get; set; }
    public int RepeatInterval { get; set; } = -1;
    public bool RepeatForever { get; set; } = false;
}