using ATMApplication.Domain.Enteties;
using ATMApplication.Domain.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Persistense.UserUseCase
{
    public class ListAllUsersHandler(IRepository<UserAccount> userRepo) : IRequestHandler<ListAllUsersRequest, IEnumerable<UserAccount>>
    {
        public async Task<IEnumerable<UserAccount>> Handle(ListAllUsersRequest request, CancellationToken cancellationToken)
        {
            return userRepo.ListAllAsync(cancellationToken);
        }
    }
}
