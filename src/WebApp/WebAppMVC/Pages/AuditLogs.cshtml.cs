using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SmartLock.ClientApp.MVC;
using SmartLock.ClientApp.MVC.Model;
using WebAppMVC.Infrastructure;
using WebAppMVC.Model;

namespace WebAppMVC.Pages
{
    public class AuditLogsModel : PageModel
    {
        private AuditLogClient _client;

        public AuditLogsModel(AuditLogClient Client)
        {
            _client = Client;
        }

        public PaginatedList<SmartLock.ClientApp.MVC.Model.AuditLog> AuditLog { get;set; }

        public async Task OnGetAsync(int? pageIndex)
        {
            //AuditLog = await _context.AuditLogs.ToListAsync();
            AuditLog = await _client.GetAuditLogsAsync(pageIndex ?? 1, 10);
        }
    }
}
