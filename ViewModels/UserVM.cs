using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace NBD.ViewModels
{
    public class UserVM
    {
        public string Id { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [Display(Name = "Role")]
        public IList<string> userRoles { get; set; }
        
    }

    public class RoleVM
    {
        public string RoleId{ get; set; }

       
        public string RoleName { get; set; }

        
        public bool Assigned { get; set; }

    }


}
