using BlogApp.Areas.Admin.Models;
using BlogApp.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly IAppUserService _appUserService;

        public UserController(IAppUserService appUserService)
        {
            _appUserService = appUserService;
        }


        public IActionResult Index()
        {
            var users = _appUserService.GetListAll();
            var userViewList = new List<UserViewModel>();
            foreach (var user in users)
            {
                userViewList.Add(new UserViewModel
                {
                    Id = user.Id,
                    Name = user.Name,
                    Surname = user.Surname,
                    EmailConfirmed = user.EmailConfirmed,
                    IsActive = user.IsActive,
                    UserName = user.UserName,
                    Email = user.Email,
                });
            }
            return View(userViewList);

        }

        [HttpGet]
        [Route("Admin/User/Edit")]
        public IActionResult Edit(string id)
        {

            var user = _appUserService.GetById(int.Parse(id));

            if (user == null)
            {
                return RedirectToAction("Error", "Home");
            }

            var model = new UserViewModel
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                EmailConfirmed = user.EmailConfirmed,
                IsActive = user.IsActive,
                UserName = user.UserName,
                Email = user.Email,
            };

            return View(model);
        }

        [HttpPost]
        [Route("Admin/User/Edit")]
        public IActionResult Edit(int id, UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _appUserService.GetById(id);


                user.Name = model.Name;
                user.Surname = model.Surname;
                user.EmailConfirmed = model.EmailConfirmed;
                user.IsActive = model.IsActive;


                _appUserService.Update(user);

                return RedirectToAction("User", "Admin");
            }

            return View(model);
        }

        [HttpPost]
        [Route("Admin/User/Delete")]

        public IActionResult DeleteUser(int id)
        {
            var user = _appUserService.GetById(id);


            _appUserService.Delete(user);

            return Ok();
        }

    }
}

