using ATMApplication.Domain.Enteties;
using ATMApplication.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMApplication.Persistense.Repository
{
    public class UserRepository : IRepository<UserAccount>
    {
        List<UserAccount> _users = new List<UserAccount>
            {
                new UserAccount{Id = 1, FullName = "Ksenia", AccountNumber = 123456, CardNumber = 654321, CardPin = 789456, AccountBalance = 50000.00m, IsLocked = false },
                new UserAccount{Id = 2, FullName = "Julia", AccountNumber = 654321, CardNumber = 123456, CardPin = 123456, AccountBalance = 50000.00m, IsLocked = false }
            };
        public List<UserAccount> ListAllAsync(CancellationToken cancellationToken = default)
        {
            return  _users;
        }

        public Task UpdateAsync(UserAccount entity, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}
