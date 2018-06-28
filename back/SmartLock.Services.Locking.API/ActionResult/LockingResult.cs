namespace SmartLock.Services.Locking.API.Controllers
{
    public class LockingResult
    {
        public LockingResult()
        {
        }

        public LockingResult(int responseCode,string responseMessage)
        {
            ResponseCode = responseCode;
            ResponseMessage = responseMessage;
        }
        public int ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}