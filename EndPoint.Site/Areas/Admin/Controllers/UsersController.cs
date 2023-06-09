using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Nima_Store.Application.Services.Users.Commands.UserEdit;
using Nima_Store.Application.Services.Users.Commands.UserRemove;
using Nima_Store.Application.Services.Users.Commands.UserStatusChange;
using Nima_Store.Application.Services.Users.Queries.GetRoles;
using Nima_Store.Application.Services.Usres.Commands.UserRegister;
using Nima_Store.Application.Services.Usres.Queries.GetUsres;
using System.Collections.Generic;

namespace EndPoint.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IGetUsersService _getUsersService;
        private readonly IGetRolesService _getRolesService;
        private readonly IUserRegisterService _userRegisterService;
        private readonly IUserRemoveService _userRemoveService;
        private readonly IUserStatusChangeService _userStatusChangeService;
        private readonly IUserEditService _userEditService;

        public UsersController(IGetUsersService getUsersService, 
            IGetRolesService getRolesService, 
            IUserRegisterService userRegisterService,
            IUserRemoveService userRemoveService,
            IUserStatusChangeService userStatusChangeService,
            IUserEditService userEditService)
        {
            _getUsersService = getUsersService;
            _getRolesService = getRolesService;
            _userRegisterService = userRegisterService;
            _userRemoveService = userRemoveService;
            _userStatusChangeService = userStatusChangeService;
            _userEditService = userEditService;
        }

        public IActionResult Index(string searchKey, int page = 1)
        {
            return View(_getUsersService.Execute(new RequestGetUserDto
            {
                Page = page,
                SearchKey = searchKey,
            }));
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Roles = new SelectList(_getRolesService.Execute().Data, "Id", "Name");
            return View();
        }
        public IActionResult Create(string Email, string FullName, long RoleId, string Password, string RePassword)
        {
            var result = _userRegisterService.Execute(new RequestUserRegisterDto
            {
                Email = Email,
                FullName = FullName,
                roles = new List<RolesInUserRegisterDto>()
                   {
                        new RolesInUserRegisterDto
                        {
                             Id= RoleId
                        }
                   },
                Password = Password,
                RePasword = RePassword,
            });
            return Json(result);
        }
        [HttpPost]
        public IActionResult Delete(long UserId)
        {
            return Json(_userRemoveService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult UserStatusChange(long UserId)
        {
            return Json(_userStatusChangeService.Execute(UserId));
        }

        [HttpPost]
        public IActionResult Edit(long UserId, string Fullname)
        {
            return Json(_userEditService.Execute(new RequestUserEditDto
            {
                Fullname = Fullname,
                UserId = UserId,
            }));
        }
    }
}
