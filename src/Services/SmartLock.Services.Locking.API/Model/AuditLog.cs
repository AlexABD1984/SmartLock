using SmartLock.Services.Locking.API.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.AuditLogs.API.Model
{
    [Table("AuditLog")]
    public class AuditLog
    {
        public AuditLog(Guid auditLogId, string commandResult)
        {
            this.AuditLogId = auditLogId;
            this.CommandResult = commandResult;
        }

        public AuditLog(Guid userId, Guid lockId, string command)
        {
            RequestDate = DateTime.Now; 
            UserId = userId;
            LockId = lockId;
            Command = command;
        }
        [Key]
        public Guid AuditLogId { get; set; }

        [Required(ErrorMessage = "RequestDate is required")]
        public DateTime RequestDate { get; set; }

        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        [Required(ErrorMessage = "UserNameFamily is required")]
        public string UserNameFamily { get; set; }

        [Required(ErrorMessage = "LockId is required")]
        public Guid LockId { get; set; }

        [Required(ErrorMessage = "LockName is required")]
        public string LockName { get; set; }

        [Required(ErrorMessage = "Command is required")]
        [StringLength(10, ErrorMessage = "Command can't be longer than 10 characters")]
        public string Command { get; set; }

        [StringLength(10, ErrorMessage = "CommandResult can't be longer than 10 characters")]
        [Required(ErrorMessage = "CommandResult is required")]
        public string CommandResult { get; set; }        
    }
}
