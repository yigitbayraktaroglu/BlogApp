﻿using BlogApp.Business.Abstract;
using BlogApp.Models;
using BlogApp.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApp.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAppUserService _appUserService;
        private readonly IBlogService _blogService;
        private readonly ICategoryService _categoryService;
        private readonly ILoggerService _loggerService;

        public ProfileController(IAppUserService appUserService, IBlogService blogService, ICategoryService categoryService, ILoggerService loggerService)
        {
            _appUserService = appUserService;
            _blogService = blogService;
            _categoryService = categoryService;
            _loggerService = loggerService;
        }

        [Route("Profile/{username}")]
        public IActionResult Index(string username, string sortOrder, string searchTerm)
        {

            var profile = _appUserService.GetByUsername(username);
            if (profile == null)
            {
                return NotFound();
            }

            var profileViewModel = new ProfileViewModel
            {
                Id = profile.Id.ToString(),
                Name = profile.Name + " " + profile.Surname,
                Username = profile.UserName
            };
            var blogViewList = new List<BlogViewModel>();

            var blogList = _blogService.GetListByAppUserId(profileViewModel.Id).AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                string lowerSearchTerm = searchTerm.ToLower();
                blogList = blogList.Where(b => b.Title.ToLower().Contains(lowerSearchTerm) ||
                                               b.Content.ToLower().Contains(lowerSearchTerm));
            }

            switch (sortOrder)
            {
                case "popular":
                    blogList = blogList.OrderByDescending(b => b.Highlight);
                    break;
                case "title_desc":
                    blogList = blogList.OrderByDescending(b => b.Title);
                    break;
                case "date":
                    blogList = blogList.OrderBy(b => b.CreatedDate);
                    break;
                case "date_desc":
                    blogList = blogList.OrderByDescending(b => b.CreatedDate);
                    break;
                default:
                    blogList = blogList.OrderBy(b => b.Title);
                    break;
            }




            var finalBlogList = blogList.ToList();
            foreach (var blog in finalBlogList)
            {
                blogViewList.Add(new BlogViewModel
                {
                    Id = blog.Id,
                    CategoryName = _categoryService.GetById(blog.CategoryId).Name,
                    AppUserId = blog.AppUserId.ToString(),
                    AuthorUsername = _appUserService.GetById(blog.AppUserId).UserName,
                    Title = blog.Title,
                    Content = blog.Content,
                    CreatedDate = blog.CreatedDate,
                    IsDraft = blog.IsDraft,
                });
            }
            profileViewModel.Blogs = blogViewList;

            return View(profileViewModel);
        }

        [Route("Profile/Edit/{username}")]
        [HttpGet]
        public IActionResult Edit()
        {

            var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var user = _appUserService.GetById(userId);

            if (user == null)
            {
                return NotFound();
            }

            var model = new ProfileViewModel
            {
                Id = user.Id.ToString(),
                Name = user.Name,
                Surname = user.Surname,
                Username = user.UserName
            };

            return View(model);
        }

        [Route("Profile/Edit/{username}")]
        [HttpPost]
        public IActionResult Edit(ProfileViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = Int32.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var user = _appUserService.GetById(userId);

                if (user == null)
                {
                    return NotFound();
                }


                user.Name = model.Name;
                user.Surname = model.Surname;
                user.UserName = model.Username;

                _appUserService.Update(user);
                _loggerService.Log("Edit Profile", $"{User.Identity.Name} edit profile.");
                return RedirectToAction("Index", new { username = user.UserName });
            }

            return View(model);
        }
    }
}

