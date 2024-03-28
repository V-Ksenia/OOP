using ATMApplication.Domain.Enteties;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Persistense.UserUseCase
{
    public sealed record ListAllUsersRequest(string Id) : IRequest<IEnumerable<UserAccount>>
    { }
}
