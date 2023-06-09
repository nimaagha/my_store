using System.Collections.Generic;

namespace Nima_Store.Application.Services.Usres.Queries.GetUsres
{
    public class ResultGetUserDto
    {
        public List<GetUsersDto> Users { get; set; }
        public int Rows { get; set; }
    }
}
