using MediatR;

namespace HC.Application.Users.Query.BecomePublisher;

public class BecomePublisherQuery : IRequest<string>
{
    public string Username { get; set; }
}