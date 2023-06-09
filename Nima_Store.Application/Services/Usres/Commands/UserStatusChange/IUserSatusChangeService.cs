using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Application.Services.Users.Commands.UserStatusChange
{
    public interface IUserStatusChangeService
    {
        ResultDto Execute(long UserId);
    }

    public class UserStatusChangeService : IUserStatusChangeService
    {
        private readonly IDataBaseContext _context;


        public UserStatusChangeService(IDataBaseContext context)
        {
            _context = context;
        }

        public ResultDto Execute(long UserId)
        {
            var user = _context.Users.Find( UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "User not found"
                };
            }

            user.IsActive = !user.IsActive;
            _context.SaveChanges();
            string userstate = user.IsActive == true ? "Active" : "Deactive";
            return new ResultDto()
            {
                IsSuccess = true,
                Message = $" {userstate} logged in successfully!",
            };
        }
    }
}
