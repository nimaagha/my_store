using Nima_Store.Domain.Entities.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Application.Services.Usres.Queries.GetUsres
{
    public interface IGetUsersService
    {
        List<GetUsersDto> Execute(RequestGetUserDto request);
    }
}
