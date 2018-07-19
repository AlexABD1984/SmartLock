using System;
using System.Threading.Tasks;

namespace SmartLock.Services.AccessRight.API.ApplicationServices
{
    public interface IAccessRightService
    {
        Task<bool> HasAccess(Guid userId,Guid lockId);
    }
}