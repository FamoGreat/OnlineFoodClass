using Microsoft.AspNetCore.Mvc;
using OnlineFoodClass.Models;
using OnlineFoodClass.Repositories;
using System.Diagnostics;

namespace OnlineFoodClass.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _unitOfWork.Course.GetAllAsync();
            var topThreeCourses = courses.Take(3).ToList();

            var testimonials = GetTestimonials();
            var model = new HomeViewModel
            {
                Courses = topThreeCourses,
                Testimonials = testimonials
            };
            return View(model);
        }

        private List<Testimonial> GetTestimonials()
        {
            return new List<Testimonial>
            {
                new Testimonial
                {
                    Name = "John Doe",
                    ImageUrl = "/images/feature/shuhuda1.jpeg",
                    Comment = "This is the best cooking class I have ever attended!"
                },
                new Testimonial
                {
                    Name = "Jane Smith",
                    ImageUrl = "/images/feature/shuhuda2.jpeg",
                    Comment = "The instructors are top-notch and the recipes are amazing."
                },
                new Testimonial
                {
                    Name = "Michael Johnson",
                    ImageUrl = "/images/feature/shuhuda3.jpeg",
                    Comment = "I learned so much and had a lot of fun. Highly recommend!"
                }
            };
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
