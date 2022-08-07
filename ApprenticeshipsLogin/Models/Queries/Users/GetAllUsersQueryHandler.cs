using ApprenticeshipsLogin.Services.Interfaces;
using MediatR;

namespace ApprenticeshipsLogin.Models.Queries.Users
{
    public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, List<string>>
    {
        private readonly IAccountService _service;

        public GetAllUsersQueryHandler(IAccountService service)
        {
            _service = service;
        }

        public Task<List<string>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
        {
            return Task.FromResult(_service.GetAllUsers());
        }
    }
}
