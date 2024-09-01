using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDBManager.Models;
using WebApplicationDBManager.ViewModels;

namespace WebApplicationDBManager.Interface
{
    public interface IUserRepository
    {
        public List<object> GetUsers();
        
        public object AddUser(UserViewModel user);
        public object UpdateUser(UserViewModel user);
        public object Login(UserViewModel user);
    }
}
