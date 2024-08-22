using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationDBManager.ViewModels
{
    public class UserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
