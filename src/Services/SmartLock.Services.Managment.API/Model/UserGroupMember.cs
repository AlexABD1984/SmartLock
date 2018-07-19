
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Model
{
    [Table("UserGroupMember")]
    public class UserGroupMember
    {
        [Key]
        public Guid UserGroupMemberId { get; set; }
        [Required(ErrorMessage = "UserGroupId is required")]
        public Guid UserGroupId { get; set; }
        [Required(ErrorMessage = "UserId is required")]
        public Guid UserId { get; set; }

        public UserGroup UserGroup { get; set; }
        public User User { get; set; }
    }
}
