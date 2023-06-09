using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Common.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Application.Services.Users.Commands.UserEdit
{
    public interface IUserEditService
    {
        ResultDto Execute(RequestUserEditDto request);
    }
    public class UserEditService : IUserEditService
    {
        private readonly IDataBaseContext _context;

        public UserEditService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultDto Execute(RequestUserEditDto request)
        {
            var user = _context.Users.Find(request.UserId);
            if (user == null)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "User not found!"
                };
            }

            user.FullName = request.Fullname;
            _context.SaveChanges();

            return new ResultDto()
            {
                IsSuccess = true,
                Message = "Edit has been done"
            };

        }
    }


    public class RequestUserEditDto
    {
        public long UserId { get; set; }
        public string Fullname { get; set; }
    }
}
