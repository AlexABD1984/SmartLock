using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Model
{
    [Table("Locks")]
    public class Lock
    {
        [Key]
        public Guid LockId { get; set; }

        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Status of Lock (Open or Close)")]
        public LockStatus Status { get; set; }
    }
    public enum LockStatus
    {
        Open,
        Close,
        Unknown
    }
}

