using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace SmartLock.Services.Locking.API.Controllers
{
    public class LockingResult
    {
        public LockingResult()
        {
        }

        public LockingResult(int responseCode,string responseMessage)
        {
            ResponseCode = responseCode = 0;
            ResponseMessage = responseMessage = "Unknown";
        }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }

    }
    public class LockOpenSuccessful : LockingResult
    {
        public LockOpenSuccessful()
        {
            this.ResponseCode = 1;
            this.ResponseMessage = "the lock has been opened successfuly";
        }
    }
    public class LockOpenFailed : LockingResult
    {
        public LockOpenFailed()
        {
            this.ResponseCode = 2;
            this.ResponseMessage = "the lock has not been opened successfuly";
        }
    }
    public class LockIsOutOfService : LockingResult
    {
        public LockIsOutOfService()
        {
            this.ResponseCode = 3;
            this.ResponseMessage = "the lock is out of service";
        }
    }
    public class UserAccessDenied : LockingResult
    {
        public UserAccessDenied()
        {
            this.ResponseCode = 3;
            this.ResponseMessage = "User Access Denied";
        }
    }
}