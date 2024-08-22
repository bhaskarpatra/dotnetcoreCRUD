using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplicationDBManager.Models
{
    public class User
    {
        public string Id { get; set; } = new Guid().ToString();
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now;
    }
}
