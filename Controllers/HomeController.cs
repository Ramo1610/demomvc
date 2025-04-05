using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVCPrac.CustomeFilter;
using MVCPrac.Models;
using System.Diagnostics;

namespace MVCPrac.Controllers
{
    [ServiceFilter(typeof(CustomActionFilter))]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [TypeFilter(typeof(CustomActionFilter))]
        public IActionResult Index()
        {
            ViewBag.Title = "Student Details Page";
            //Using ViewData
            ViewData["Header"] = "Student Details";
            //Creating Student Object to Hold Student data
            Student student = new Student()
            {
                StudentId = 101,
                Name = "James",
                Branch = "CSE",
                Section = "A",
                Gender = "Male",
                ImageUrl="~/image/2.png"
            };

            List<SelectListItem> items = new List<SelectListItem>()
            {
                // First dropdown item: Displayed as "IT", with a value of "1" when selected
                new SelectListItem { Text = "Male", Value = "1", Selected = true },
                // Second dropdown item: Displayed as "HR", with a value of "2" when selected
                new SelectListItem { Text = "Female", Value = "2" } 
            };
            ViewBag.Gender = items;

            return View(student);
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Submitdata(Student s)
        {
            ViewBag.Title = "Student Details Page";
            //Using ViewData
            ViewData["Header"] = "Student Details";
            //Creating Student Object to Hold Student data
            Student student = new Student()
            {
                StudentId = s.StudentId,
                Name = s.Name,
                Branch = s.Branch,
                Section = s.Section,
                //Gender = "Male"
            };

            List<SelectListItem> items = new List<SelectListItem>()
            {
                // First dropdown item: Displayed as "IT", with a value of "1" when selected
                new SelectListItem { Text = "Male", Value = "1"},
                // Second dropdown item: Displayed as "HR", with a value of "2" when selected
                new SelectListItem { Text = "Female", Value = "2" }
            };
            foreach (var item in items)
            {
                item.Selected = item.Value == s.Gender;
            }
            ViewBag.Gender = items;
            //return View("Index", student);
            return View("SingleFileUpload");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [RequestSizeLimit(10000)]
        public async Task<IActionResult> SingleFileUpload(IFormFile SingleFile)
        {

            if (ModelState.IsValid)
            {
                if (SingleFile != null && SingleFile.Length > 0)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\uploads", SingleFile.FileName);
                    //Using Buffering
                    using (var stream = System.IO.File.Create(filePath))
                    {
                        // The file is saved in a buffer before being processed
                        await SingleFile.CopyToAsync(stream);
                    }
                    //Using Streaming
                    //using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
                    //{
                    //    await SingleFile.CopyToAsync(stream);
                    //}
                    // Process the file here (e.g., save to the database, storage, etc.)
                    return View("Index");
                }
            }
            return View("SingleFileUpload");
        }
    }
}