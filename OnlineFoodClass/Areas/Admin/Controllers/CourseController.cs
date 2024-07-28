using Microsoft.AspNetCore.Mvc;
using OnlineFoodClass.Models;
using OnlineFoodClass.Repositories;

namespace OnlineFoodClass.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CourseController( IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Course.GetAllAsync();
            return View(courses);
        }

      

        public IActionResult Create()
        {

            return View(new Course());
        }

        [HttpPost]
        public async Task<IActionResult> Create(Course course, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string coursePath = Path.Combine(wwwRootPath, "images/course");

                    using( var fileStream = new FileStream(Path.Combine(coursePath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    course.ImageUrl = $"/images/course/{fileName}";
                }
                _unitOfWork.Course.AddAsync(course);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var course = await _unitOfWork.Course.GetAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Course course, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnvironment.WebRootPath;

                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string coursePath = Path.Combine(wwwRootPath, "images/course");

                    if (!Directory.Exists(coursePath))
                    {
                        Directory.CreateDirectory(coursePath);
                    }

                    using (var fileStream = new FileStream(Path.Combine(coursePath, fileName), FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }

                    if (!string.IsNullOrEmpty(course.ImageUrl))
                    {
                        var oldImagePath = Path.Combine(wwwRootPath, course.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    course.ImageUrl = $"/images/course/{fileName}";
                }
                _unitOfWork.Course.UpdateAsync(course);
                await _unitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(course);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var course = await _unitOfWork.Course.GetAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await _unitOfWork.Course.GetAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }

            if (!string.IsNullOrEmpty(course.ImageUrl))
            {
                var oldImagePath = Path.Combine(_webHostEnvironment.WebRootPath, course.ImageUrl.TrimStart('/'));
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Course.RemoveAsync(course);
            await _unitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
