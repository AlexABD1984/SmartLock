using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SmartLock.Services.Locking.API.Infrastructure
{
    public class AccessRightHttpClient
    {
        public AccessRightHttpClient(HttpClient client)
        {
            Client = client;
        }

        public HttpClient Client { get; }
    }
}
