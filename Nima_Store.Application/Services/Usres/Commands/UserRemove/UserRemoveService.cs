using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Application.Services.Users.Commands.UserRemove;
using Nima_Store.Common.Dto;
using System;
using System.Linq;

namespace Nima_Store.Application.Services.Users.Commands.UserRemove
{
    public class UserRemoveService : IUserRemoveService
    {
        private readonly IDataBaseContext _context;

        public UserRemoveService(IDataBaseContext context)
        {
            _context = context;
        }


        public ResultDto Execute(long UserId)
        {
 
            var user = _context.Users.Find(UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "User not found!"
                };
            }
            user.RemoveTime = DateTime.Now;
            user.IsRemoved = true;
            _context.SaveChanges();
            return new ResultDto()
            {
                IsSuccess = true,
                Message = "User removed successfully"
            };
        }
    }
}
