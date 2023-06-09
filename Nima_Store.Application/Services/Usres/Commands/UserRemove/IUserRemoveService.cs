using Nima_Store.Common.Dto;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nima_Store.Application.Services.Users.Commands.UserRemove
{
    public interface IUserRemoveService
    {
        ResultDto Execute(long UserId);
    }
}
