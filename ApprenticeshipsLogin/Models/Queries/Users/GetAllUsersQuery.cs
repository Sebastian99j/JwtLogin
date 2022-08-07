using MediatR;

namespace ApprenticeshipsLogin.Models.Queries.Users
{
    public record GetAllUsersQuery : IRequest<List<string>>;
}
