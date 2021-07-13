using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;

        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }

        //public ActionResult Index()
        //{   
        //    var upcomingCourses = _dbContext.Courses
        //        .Include(c => c.Lecturer)
        //        .Include(c => c.Category)
        //        .Where(c => c.DateTime > DateTime.Now);

        //    var viewModel = new CoursesViewModel
        //    {
        //        UpcomingCourses = upcomingCourses,
        //        ShowAction = User.Identity.IsAuthenticated
        //    };
        //    return View(viewModel);
        //}

        public ActionResult Index()
        {

            var test = _dbContext.Courses.ToList();
            var idUser = User.Identity.GetUserId();
            if (User.Identity.IsAuthenticated)
            {
                if (!idUser.Equals(""))
                {
                    ViewBag.ListCoursesRegistered = _dbContext.Attendances.Where(a => a.AttendeeId == idUser).Select(a => a.CourseId).ToList();
                    ViewBag.ListFollowed = _dbContext.Followings.Where(a => a.FollowerId == idUser).Select(a => a.FolloweeId).ToList();
                }
            }
            var upcomingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now);
            var viewModel = new CoursesViewModel
            {
                UpcomingCourses = upcomingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        //public ActionResult Index()
        //{
        //    return View();
        //}

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}