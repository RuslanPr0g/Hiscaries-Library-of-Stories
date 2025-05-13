namespace HC.UserAccounts.Application.Write.UserAccounts.Command.UpdateUserData;

public sealed class UpdateUserDataCommand : IRequest<OperationResult>
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public DateTime BirthDate { get; set; }

    public string PreviousPassword { get; set; }
    public string NewPassword { get; set; }
}