using SmartLock.Services.Managment.API.Contracts;
using SmartLock.Services.Managment.API.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Data.Repository
{
    public class LockRepository //: RepositoryBase<Lock>, ILockRepository
    {
        public LockRepository(ManagmentDbContext repositoryContext)
            //: base(repositoryContext)
        {
        }

    }
}
