using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Common;
using Nima_Store.Common.Dto;
using Nima_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Application.Services.Usres.Commands.UserRegister
{
    public interface IUserRegisterService
    {
        ResultDto<ResultUserRegisterDto> Execute(RequestUserRegisterDto request);
    }
    public class UserRegisterService : IUserRegisterService
    {
        private readonly IDataBaseContext _context;
        public UserRegisterService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto<ResultUserRegisterDto> Execute(RequestUserRegisterDto request)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(request.Email))
                {
                    return new ResultDto<ResultUserRegisterDto>()
                    {
                        Data = new ResultUserRegisterDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "Please insert the email address"
                    };
                }

                if (string.IsNullOrWhiteSpace(request.FullName))
                {
                    return new ResultDto<ResultUserRegisterDto>()
                    {
                        Data = new ResultUserRegisterDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "Please insert full name"
                    };
                }
                if (string.IsNullOrWhiteSpace(request.Password))
                {
                    return new ResultDto<ResultUserRegisterDto>()
                    {
                        Data = new ResultUserRegisterDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "Please insert the password"
                    };
                }
                if (request.Password != request.RePasword)
                {
                    return new ResultDto<ResultUserRegisterDto>()
                    {
                        Data = new ResultUserRegisterDto()
                        {
                            UserId = 0,
                        },
                        IsSuccess = false,
                        Message = "Passwords are not the same!"
                    };
                }

                User user = new User()
                {
                    Email = request.Email,
                    FullName = request.FullName,
                    Password = HashPassword.Execute(request.Password),
                };

                List<UserInRole> userInRoles = new List<UserInRole>();

                foreach (var item in request.roles)
                {
                    var roles = _context.Roles.Find(item.Id);
                    userInRoles.Add(new UserInRole
                    {
                        Role = roles,
                        RoleId = roles.Id,
                        User = user,
                        UserId = user.Id,
                    });
                }
                user.UserInRoles = userInRoles;

                _context.Users.Add(user);

                _context.SaveChanges();

                return new ResultDto<ResultUserRegisterDto>()
                {
                    Data = new ResultUserRegisterDto()
                    {
                        UserId = user.Id,

                    },
                    IsSuccess = true,
                    Message = "User added successfully",
                };
            }
            catch (Exception)
            {
                return new ResultDto<ResultUserRegisterDto>()
                {
                    Data = new ResultUserRegisterDto()
                    {
                        UserId = 0,
                    },
                    IsSuccess = false,
                    Message = "An error occured!"
                };
            }

        }
    }
    public class RequestUserRegisterDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePasword { get; set; }

        public List<RolesInUserRegisterDto> roles { get; set; }
    }
    public class RolesInUserRegisterDto
    {
        public long Id { get; set; }
    }
    public class ResultUserRegisterDto
    {
        public long UserId { get; set; }
    }
}
