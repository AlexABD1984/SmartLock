using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SmartLock.Services.Managment.API.Model
{
    public class AccessRight
    {
        [Key]
        public Guid AccessRightId { get; set; }

        /// <summary>
        /// UserGroupId or UserId
        /// </summary>
        public Guid? AccessorId { get; set; }

        public bool HasAccess { get; set; }

        public User User { get; set; }

        public UserGroup UserGroup { get; set; }
    }
}
