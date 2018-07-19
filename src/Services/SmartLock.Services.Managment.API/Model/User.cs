using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Model
{
    [Table("User")]
    public class User
    {
        [Key]
        public Guid UserId { get; set; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(60, ErrorMessage = "Name can't be longer than 60 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Family is required")]
        [StringLength(60, ErrorMessage = "Family can't be longer than 60 characters")]
        public string Family { get; set; }

        public List<UserGroupMember> UserGroupMembers { get; set; }
        public List<AccessRight> AccessRights { get; set; }

    }
}
