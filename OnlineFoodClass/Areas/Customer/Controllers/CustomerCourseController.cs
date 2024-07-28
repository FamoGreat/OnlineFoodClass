using Microsoft.AspNetCore.Mvc;
using OnlineFoodClass.Repositories;

namespace OnlineFoodClass.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CustomerCourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public CustomerCourseController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Course.GetAllAsync();
            return View(courses);
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var course = await _unitOfWork.Course.GetAsync(c => c.Id == id);
                if (course == null)
                {
                    return NotFound();
                }

                return View(course);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving course details");
                TempData["error"] = "An error occurred while retrieving the course details. Please try again.";
                return RedirectToAction(nameof(Index));
            }

        }
    }
}
