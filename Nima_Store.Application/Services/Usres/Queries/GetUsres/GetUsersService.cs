using Nima_Store.Application.Interfaces.Contexts;
using Nima_Store.Common;
using System.Collections.Generic;
using System.Linq;

namespace Nima_Store.Application.Services.Usres.Queries.GetUsres
{
    public class GetUsersService : IGetUsersService
    {
        private readonly IDataBaseContext _context;
        public GetUsersService(IDataBaseContext context)
        {
            _context = context;
        }
        public ResultGetUserDto Execute(RequestGetUserDto request)
        {
            var users = _context.Users.AsQueryable();
            if(!string.IsNullOrWhiteSpace(request.SearchKey))
            {
                users = users.Where(p => p.FullName.Contains(request.SearchKey) && p.Email.Contains(request.SearchKey));
            }
            int rowsCount = 0;
            var usersList =  users.ToPaged(request.Page, 20, out rowsCount).Select(p=> new GetUsersDto 
            {
                Email = p.Email,
                FullName = p.FullName,
                Id = p.Id,
                IsActive = p.IsActive
            }).ToList();

            return new ResultGetUserDto
            {
                Rows = rowsCount,
                Users = usersList,
            };
        }
    }
}
