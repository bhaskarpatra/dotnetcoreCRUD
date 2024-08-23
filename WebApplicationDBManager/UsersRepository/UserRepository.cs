using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApplicationDBManager.DBContext;
using WebApplicationDBManager.Interface;
using WebApplicationDBManager.Models;
using WebApplicationDBManager.ViewModels;

namespace WebApplicationDBManager.UsersRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _configuration;
        public UserRepository(AppDbContext dbContext, IConfiguration configuration)
        {
            _context = dbContext;
            _configuration = configuration;
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

        public object Login(UserViewModel userVm)
        {
            string errorMessage = string.Empty;
            var response = new object();
            try
            {
                if (userVm != null)
                {
                    User user = _context.Users.FirstOrDefault(x => x.Email == userVm.Email);
                    if (user != null)
                    {
                        if (user.Password == userVm.Password)
                        {
                            var claims = new[]
                            {
                                new Claim(JwtRegisteredClaimNames.Sub, _configuration["Jwt:Subject"] ),
                                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() ),
                                new Claim("UserId",user.Id),
                                new Claim("UserEmail",user.Email)
                            };
                            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                            var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                            var token = new JwtSecurityToken(
                                _configuration["Jwt:Issuer"],
                                _configuration["Jwt:Audience"],
                                claims,
                                expires: DateTime.UtcNow.AddSeconds(30),
                                signingCredentials: signIn
                                );
                            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
                            response = tokenValue;
                        }
                        else { errorMessage += $"| Wrong Email or password, "; }                        
                    }
                    else
                    { errorMessage += $"| Wrong Email or password, "; }
                }
                else
                {
                    errorMessage += $"| user is null, ";
                }
            }
            catch (Exception ex)
            {
                errorMessage += $"| {ex.Message}";
            }
            finally
            {
                if (errorMessage != string.Empty)
                {
                    ErrorResponse error = new ErrorResponse
                    {
                        Message = errorMessage,
                        Code = 111
                    };
                    response = error;
                }
            }
            return response;
        }
    }
}
