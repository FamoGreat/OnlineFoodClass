using Microsoft.AspNetCore.Mvc;
using OnlineFoodClass.Models;
using OnlineFoodClass.Repositories;
using System.Diagnostics;

namespace OnlineFoodClass.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
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
            var course = await _unitOfWork.Course.GetAsync(c => c.Id == id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
