using BlogApp.Areas.Admin.Models;
using BlogApp.Business.Abstract;
using BlogApp.Entity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BlogApp.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var categories = _categoryService.GetListAll();
            var categoryViewList = new List<CategoryViewModel>();
            foreach (var category in categories)
            {
                categoryViewList.Add(new CategoryViewModel
                {
                    Id = category.Id,
                    Name = category.Name,
                    CreatedDate = category.CreatedDate,
                    UpdatedDate = category.UpdatedDate,

                });
            }

            return View(categoryViewList);
        }

        [HttpPost]
        [Route("Admin/Category/Create")]
        public IActionResult Create(string name)
        {
            if (ModelState.IsValid)
            {
                var category = new Category
                {
                    Name = name,
                    CreatedDate = DateTime.Now,

                };
                _categoryService.Insert(category);

                return RedirectToAction("Category", "Admin");
            }

            return View();
        }

        [HttpPost]
        [Route("Admin/Category/Update")]
        public IActionResult Update(int id, CategoryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var category = _categoryService.GetById(id);

                category.Name = model.Name;
                category.UpdatedDate = DateTime.Now;

                _categoryService.Update(category);

                return RedirectToAction("Category", "Admin"); // Düzenlemeden sonra blog detay sayfasına yönlendirme.
            }

            return View(model); // Model geçerli değilse tekrar düzenleme sayfasını göster.
        }

        [HttpPost]
        [Route("Admin/Category/Delete")]

        public IActionResult Delete(int id)
        {
            var category = _categoryService.GetById(id);


            _categoryService.Delete(category);

            return Ok(); // Silme işleminden sonra blog listesini gösterecek şekilde yönlendirin.
        }
    }
}
