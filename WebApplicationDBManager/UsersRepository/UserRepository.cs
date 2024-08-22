using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationDBManager.DBContext;
using WebApplicationDBManager.Interface;
using WebApplicationDBManager.Models;
using WebApplicationDBManager.ViewModels;

namespace WebApplicationDBManager.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext dbContext)
        {
            _context = dbContext;
        }

        public object AddUser(UserViewModel user)
        {
            var response = new object();
            try
            {
                if (user != null)
                {
                    User addUser = new User()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        Password = user.Password,
                    };
                    _context.Users.Add(addUser);
                    _context.SaveChanges();
                    response = addUser;
                }
                else
                {
                    response = new ErrorResponse
                    {
                        Code = 404,
                        Message = "User is null",
                    };
                }
            }
            catch (Exception ex)
            {
                response = new ErrorResponse
                {
                    Code = 404,
                    Message = ex.Message,
                };
            }
            return response;
        }

        public List<object> GetUsers()
        {
            var response = new List<object>();
            try
            {
                List<User> Users = _context.Users.ToList();
                if (Users.Any())
                {
                    foreach (var user in Users)
                    {
                        UserViewModel userViewModel = new UserViewModel()
                        {
                            CreatedOn = DateTime.Parse(user.CreatedOn).ToString("h:mm tt, dd-MM-yyyy"),
                            Email = user.Email,
                            Name = user.Name,
                            Password = "N/A",
                        };
                        response.Add(userViewModel);
                    }
                }
                else
                {
                    ErrorResponse error = new ErrorResponse
                    {
                        Code = 403,
                        Message = "No User available"
                    };
                    response.Clear();
                    response.Add(error);
                }
            }
            catch (Exception ex)
            {
                ErrorResponse error = new ErrorResponse
                {
                    Code = 403,
                    Message = ex.Message,
                };
                response.Clear();
                response.Add(error);
            }
            return response;
        }
    }
}
